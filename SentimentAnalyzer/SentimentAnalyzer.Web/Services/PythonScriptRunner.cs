using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace SentimentAnalyzer.Web.Services
{
    public class PythonScriptRunner
    {
        //public string RunPythonScript(string inputText)
        //{
        //    string pythonPath = @"C:\Python311\python.exe";  // Path to Python executable
        //    string scriptPath = @"C:\Users\someo\OneDrive\PREV FILES\Documents\Visual Studio 2022\FrontEnd Projects\TokenizationOnnx\distilbert_tokenizer.py";  // Path to Python script

        //    // Prepare the arguments
        //    string arguments = $"\"{scriptPath}\" \"{inputText}\"";

        //    ProcessStartInfo startInfo = new ProcessStartInfo
        //    {
        //        FileName = pythonPath,
        //        Arguments = arguments,
        //        RedirectStandardOutput = true,
        //        RedirectStandardError = true,
        //        UseShellExecute = false,
        //        CreateNoWindow = true
        //    };

        //    try
        //    {
        //        using (Process process = Process.Start(startInfo))
        //        {
        //            using (StreamReader reader = process.StandardOutput)
        //            using (StreamReader errorReader = process.StandardError)
        //            {
        //                // Read the output
        //                string result = reader.ReadToEnd();

        //                // Read error output if exists
        //                string error = errorReader.ReadToEnd();

        //                if (!string.IsNullOrEmpty(error))
        //                {
        //                    Console.WriteLine("Error: " + error);
        //                }

        //                // Parse the JSON output
        //                var json = JObject.Parse(result);
        //                var tokens = json["tokens"];
        //                var attentionMask = json["attention_mask"];

        //                // Output the tokenized result
        //                Console.WriteLine($"Tokens: {tokens}");
        //                Console.WriteLine($"Attention Mask: {attentionMask}");

        //                // Return result (can be used for further processing)
        //                return result;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error: " + ex.Message;
        //    }
        //}
    }
}
