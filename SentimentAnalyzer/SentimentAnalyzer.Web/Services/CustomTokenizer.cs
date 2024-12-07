namespace SentimentAnalyzer.Web.Services
{
    public class CustomTokenizer
    {
        private readonly Dictionary<string, long> _vocab;

        public CustomTokenizer()
        {
            _vocab = new Dictionary<string, long>
            {
                { "[PAD]", 0 }, { "[UNK]", 1 }, { "[CLS]", 101 }, { "[SEP]", 102 },
                { "this", 2001 }, { "product", 2002 }, { "is", 2003 }, { "amazing", 2004 },
                { "bad", 2005 }, { "terrible", 2006 }, { "good", 2007 }
            };
        }

        public (long[] inputIds, long[] attentionMask) Encode(string text, int maxLength = 128)
        {
            var words = text.ToLower().Split(' ');

            // Encode words into token IDs
            var inputIds = new List<long> { _vocab["[CLS]"] };
            foreach (var word in words)
            {
                inputIds.Add(_vocab.ContainsKey(word) ? _vocab[word] : _vocab["[UNK]"]);
            }
            inputIds.Add(_vocab["[SEP]"]);

            // Padding or truncation
            while (inputIds.Count < maxLength) inputIds.Add(_vocab["[PAD]"]);
            if (inputIds.Count > maxLength) inputIds = inputIds.Take(maxLength).ToList();

            // Generate attention mask
            var attentionMask = inputIds.Select(id => id == _vocab["[PAD]"] ? 0L : 1L).ToArray();

            return (inputIds.ToArray(), attentionMask);
        }
    }
}
