# Fly Now Pay Later Pre-Interview

[Assignment details](https://www.flynowpaylater.com/hiring/candidates/php-developer/assignment/)

The presented solution takes input from a presented file only, using the FileReader class.


## OOP class rundown

The FileReader class has the following responsibilities:
- Interpret parameters 'fileName' and 'basePath' into a filePath, and run the following checks for the constructed filepath:
    - Verify the existence of a `.txt` file at that filepath location
    - Check the encoding of the file at that location by inspecting file BOM (only UTF-8 accepted)
    ** [StreamReader.currentEncoding()](https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader.currentencoding?view=net-5.0) was rejected as the output was not always accurate**
    - Ensure that the file has contents
    - Inspect all characters in the file in order to detect illegal chars (whitespace, newline characters etc.)
**If any of the above checks fail, then a corresponding exception is thrown, which the caller can interpret to give appropriate response to the user**
- Return file contents via ToString override method (if valid contents)

The StreamParser class is responsible for interpreting the contents of a presented file.
To construct a StreamParser class, you must provide an instance of FileParser, thus ensuring that the file contents are valid and appropriate for processing for the intended output (char frequencies)
The StreamParser class is responsible for the following things
- Taking the constructor parameter FileReader and saving its' contents
- Separating out different character classes (symbol, punctuation, letter)
    - Examining each character for each character class in order to build a frequency map per character-type
- Publically exposing each character-type frequency map to a consumer (outside entity)
- Providing methods to get the most-frequent, least-frequent and unique characters from a given character-type frequency map

Here is an example of the key data structure within StreamParser after the FileReader contents have been fully interpreted:

```
StreamParser:
	stream: [string which contains all the characters in the map]
	frequencies:
    	letter:
        	"z": 1
			"f": 5
			"k": 7
			"a": 20
			"i": 21
			"e": 44
		symbol:
        	"$": 2
			"=": 4
			"<": 5
			">": 5
		punctuation:
        	"/": 10
			"@": 11
           
```

## Important conditions
 - File doesn't exist: program returns `1`
 - Contents of file has illegal characters (uppercase ascii, numbers, punctuation, symbols): program returns `2`
 - Program execution did not provide 'format' flag to select type of frequency: program returns `3`
 - No character-type flags are provided on program execution (--include-letter, --include-punctuation): program returns `4`
 - Entire system must be unit-tested


Language C#
License: MIT
Author: srmes <94andersonsc@googlemail.com>