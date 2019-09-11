# Exercise 3.6

Use the expression parser from Parse.fs and the compiler scomp
(from expressions to stack machine instructions) and the associated datatypes from
Expr.fs, to define a function compString : string -> sinstr list
that parses a string as an expression and compiles it to stack machine code.

Solution:

Add the following code to Parse.fs

```
compString s = scomp (Parse.fromString s) [];
```
