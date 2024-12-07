import sys
from tokenizers import BertWordPieceTokenizer

# Initialize tokenizer with vocab file path
tokenizer = BertWordPieceTokenizer("C:/Users/someo/OneDrive/PREV FILES/Documents/Visual Studio 2022/FrontEnd Projects/TokenizationOnnx/vocab.txt", lowercase=True)

# Get input text from arguments
input_text = sys.argv[1]

# Tokenize the input text
encoding = tokenizer.encode(input_text)

# Print the tokenized output in JSON format
print(f'{{"tokens": {encoding.ids}, "attention_mask": {encoding.attention_mask}}}')
