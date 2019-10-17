# Markdown Translator
This project translate a markdown without losing formatting.

Any kind of help will be welcome!

For now only Azure as provider has been implemented.


```
MdTranslate.exe --help

MdTranslate:
  Translate markdown languages

Usage:
  MdTranslate [options]

Options:
  --md-input <md-input>                  Input Path of markdown
  --md-output <md-output>                Output file of translated markdown, if not specified input.language.md is the
                                         new file
  --output-language <output-language>    language of output file translated(en-us, en, pt-br)
  --input-language <input-language>      language of input file(en-us, en, pt-br)
  --provider <Amazon|Azure|Google>       Azure, Amazon, Google
  --key <key>                            Api key on provider or Environment Variable
                                         *PROVIDER*_TRANSLATOR_TEXT_SUBSCRIPTION_KEY i.e.
                                         AZURE_TRANSLATOR_TEXT_SUBSCRIPTION_KEY
  --endpoint <endpoint>                  Url of translation api endpoint or Environment Variable
                                         *PROVIDER*_TRANSLATOR_TEXT_ENDPOINT i.e. AZURE_TRANSLATOR_TEXT_ENDPOINT
  --version                              Display version information
```