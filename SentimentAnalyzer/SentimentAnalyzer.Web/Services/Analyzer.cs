using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Linq;

namespace SentimentAnalyzer.Web.Services
{
    public class Analyzer
    {
        private readonly InferenceSession _session;
        private readonly CustomTokenizer _tokenizer;
        public Analyzer(string modelPath = "wwwroot/LanguageModel/distilbert-sentiment.onnx")
        {
            _session = new InferenceSession(modelPath);
            _tokenizer = new CustomTokenizer();
        }

        public string AnalyzeSentiment(string inputText)
        {
            // Run the Python script to tokenize the input text
            string tokenizedResult = RunPythonScript(inputText);

            // Parse the JSON output from the Python script
            var json = JObject.Parse(tokenizedResult);
            var tokens = json["tokens"].ToObject<int[]>();
            var attentionMask = json["attention_mask"].ToObject<int[]>();

            // Convert tokens and attention_mask to Int64 (long) arrays for ONNX inference
            var inputTensor = new DenseTensor<long>(tokens.Select(t => (long)t).ToArray(), new[] { 1, tokens.Length });
            var attentionTensor = new DenseTensor<long>(attentionMask.Select(a => (long)a).ToArray(), new[] { 1, attentionMask.Length });

            // Prepare the input data for ONNX model
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input_ids", inputTensor),
                NamedOnnxValue.CreateFromTensor("attention_mask", attentionTensor)
            };

            // Run inference on the model
            using var results = _session.Run(inputs);

            // Get the output logits (class scores)
            var logits = results.First().AsEnumerable<float>().ToArray();

            // Assuming a binary sentiment classification (Positive/Negative)
            var sentiment = logits[0] > logits[1] ? "Negative" : "Positive";  // Based on your model logic

            return sentiment;
        }
        public string RunPythonScript(string inputText)
        {
            string pythonPath = @"C:\Python311\python.exe";                             // Path to Python executable
            string scriptPath = @"C:\TokenizationOnnx\distilbert_tokenizer.py";         // Path to Python script

            // Prepare the arguments
            string arguments = $"\"{scriptPath}\" \"{inputText}\"";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using (Process process = Process.Start(startInfo))
                {
                    using (StreamReader reader = process.StandardOutput)
                    using (StreamReader errorReader = process.StandardError)
                    {
                        // Read the output
                        string result = reader.ReadToEnd();

                        // Read error output if exists
                        string error = errorReader.ReadToEnd();

                        if (!string.IsNullOrEmpty(error))
                        {
                            Console.WriteLine("Error: " + error);
                        }

                        // Parse the JSON output
                        var json = JObject.Parse(result);
                        var tokens = json["tokens"];
                        var attentionMask = json["attention_mask"];

                        // Output the tokenized result
                        Console.WriteLine($"Tokens: {tokens}");
                        Console.WriteLine($"Attention Mask: {attentionMask}");

                        // Return result (can be used for further processing)
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
