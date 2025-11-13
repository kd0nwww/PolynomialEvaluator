# PolynomialEvaluator

## Methods

### `SplitTerms(string polynomial)`
This method takes polynomial as a string and splits it by spaces. It puts clean terms of polynomial into `termsWithSigns`, but before that checks elements in the splitted string, removes empty ones, and adds signs of coefficients where needed.