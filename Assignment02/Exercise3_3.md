# Exercise 3.3

Write out the rightmost derivation of the string below from the expression grammar at the end of Sect. 3.6.5, corresponding to ExprPar.fsy. Take note
of the sequence of grammar rules (A–I) used.

`let z = (17) in z + 2 * 3 end EOF`

Solution:

```
Main
    rule A
Expr EOF
    rule F
LET NAME(z) EQ Expr IN Expr END
    rule H
    rule G
Expr PLUS Expr
Expr TIMES Expr
```
