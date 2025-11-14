# PolynomialEvaluator

## Methods

### `SplitTerms(string polynomial)`
This method takes polynomial as a string and splits it by spaces. It puts clean terms of polynomial into `termsWithSigns`, but before that checks elements in the splitted string, removes empty ones, and adds signs of coefficients where needed.

### `ParseTerm(string term)`
Takes `term` and divides it into 2 parts: coefficient and power.

### `PowInt(int x, int power)`
Helper method that works like `Math.Pow` but operates on integers.

### `Calculate(int[][] coefPowerPairs, int x)`
Takes the array of arrays, which contain coefficient and power, applies those to `x` and returns result.