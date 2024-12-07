//using Python.Runtime;
//namespace SentimentAnalyzer.Web.Services
//{
//    public class PythonTokenizer
//    {
//        public string TokenizeText(string inputText)
//        {
//            // Initialize the Python runtime
//            using (Py.GIL())  // GIL (Global Interpreter Lock) is necessary to interact with Python
//            {
//                // Import your Python tokenizer script
//                dynamic tokenizerScript = Py.Import("distilbert_tokenizer");

//                // Call the Python function
//                dynamic result = tokenizerScript.tokenize_text(inputText);

//                // Process the result (returning the tokenized output)
//                string tokenizedOutput = result.ToString();
//                return tokenizedOutput;
//            }
//        }
//    }
//}
