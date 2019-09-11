# Exercise 3.7

Extend the expression language abstract syntax and the lexer and
parser specifications with conditional expressions. The abstract syntax should be
If(e1, e2, e3), so modify file Absyn.fs as well as ExprLex.fsl and file
ExprPar.fsy. The concrete syntax may be the keyword-laden F#/ML-style:
```
if e1 then e2 else e3

```

Solution:


Add the following code to Absyn.fs

```
type expr = 
  | CstI of int
  | Var of string
  | Let of string * expr * expr
  | Prim of string * expr * expr
  | If of expr*expr*expr 			// ADDED

```

Add the following code to ExprPar.fsy

```
Expr:
    NAME                                { Var $1            }
  | CSTINT                              { CstI $1           }
  | MINUS CSTINT                        { CstI (- $2)       }
  | LPAR Expr RPAR                      { $2                }
  | LET NAME EQ Expr IN Expr END        { Let($2, $4, $6)   }
  | Expr TIMES Expr                     { Prim("*", $1, $3) }
  | Expr PLUS  Expr                     { Prim("+", $1, $3) }  
  | Expr MINUS Expr                     { Prim("-", $1, $3) } 
  | Expr QMRK Expr COLN Expr            { If($1, $3, $5)	} // ADDED
;
```

Add the following code to ExprLex.fsl

```
rule Token = parse
  | [' ' '\t' '\r'] { Token lexbuf }
  | '\n'            { lexbuf.EndPos <- lexbuf.EndPos.NextLine; Token lexbuf }
  | ['0'-'9']+      { CSTINT (System.Int32.Parse (lexemeAsString lexbuf)) }
  | ['a'-'z''A'-'Z']['a'-'z''A'-'Z''0'-'9']*
                    { keyword (lexemeAsString lexbuf) }
  | '+'             { PLUS  } 
  | '-'             { MINUS } 
  | '*'             { TIMES }
  | '='             { EQ    } 
  | '('             { LPAR  } 
  | ')'             { RPAR  } 
  | '?'             { QMRK  } 		// ADDED
  | ':'             { COLN  }		// ADDED
  | eof             { EOF   }
  | _               { failwith "Lexer error: illegal symbol" }r
  
```