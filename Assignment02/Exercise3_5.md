

# 3.5

#### Starting the exercise
Running `build.exe` created all files needed.

Running `run.exe` started an interactive session with f#.

#### The result of running the commands:

fromString "1 + 2 * 3";;
 `Absyn.expr = Prim ("+",CstI 1,Prim ("*",CstI 2,CstI 3))`

fromString "1 - 2 - 3";;
`Absyn.expr = Prim ("-",Prim ("-",CstI 1,CstI 2),CstI 3)`

fromString "1 + -2";;
`Absyn.expr = Prim ("+",CstI 1,CstI -2)`

fromString "x++";;
`System.Exception: parse error near line 1, column 3`

fromString "1 + 1.2";;
`System.Exception: Lexer error: illegal symbol near line 1, column 6`

fromString "1 + ";;
`System.Exception: parse error near line 1, column 4`

fromString "let z = (17) in z + 2 * 3 end";;
`Absyn.expr = Let ("z",CstI 17,Prim ("+",Var "z",Prim ("*",CstI 2,CstI 3)))`

fromString "let z = 17) in z + 2 * 3 end";;
`System.Exception: parse error near line 1, column 11`

fromString "let in = (17) in z + 2 * 3 end";;
`System.Exception: parse error near line 1, column 6`

fromString "1 + let x=5 in let y=7+x in y+y end + x end";;
`Absyn.expr = Prim ("+",CstI 1, Let ("x",CstI 5,Prim ("+",Let ("y" Prim ("+",CstI 7,Var "x"),Prim ("+",Var "y",Var "y")), Var "x")))`


The session was ended and cleaned by running `clean.exe`
