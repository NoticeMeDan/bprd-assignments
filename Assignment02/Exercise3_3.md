# Exercise 3.3

Write out the rightmost derivation of the string below from the expression grammar at the end of Sect. 3.6.5, corresponding to ExprPar.fsy. Take note
of the sequence of grammar rules (A–I) used.

`let z = (17) in z + 2 * 3 end EOF`

Solution:

This is the format that Niels proposed on LearnIT. 
It is very confusing, and i have no idea how it works, so i decided that i 
would write the full grammar in the bottom, and then "simplify" all the way up. 

```
Main
    rule A
Expr EOF
    rule F
LET NAME(z) EQ Expr IN Expr END
    rule G
LET NAME(z) EQ Expr TIMES Expr END
    rule C
LET NAME(z) EQ Expr IN Expr TIMES CSTINT(3) END
    rule H
LET NAME(z) EQ Expr PLUS Expr TIMES CSTINT(3) END
    rule C
LET NAME(z) EQ Expr IN Expr PLUS CSTINT(2) TIMES CSTINT(3) END
    rule B
LET NAME(z) EQ Expr IN NAME(z) PLUS CSTINT(2) TIMES CSTINT(2) END
    rule E
LET NAME(z) EQ LPAR Expr RPAR IN NAME(z) PLUS CSTINT(2) TIMES CSTINT(3) END
    rule C
LET NAME(z) EQ LPAR CSTINT(17) RPAR IN NAME(z) PLUS CSTINT(2) TIMES CSTINT(3) END
```
