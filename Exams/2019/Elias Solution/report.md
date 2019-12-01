# Opgave 1 - Icon

## Delopgave 1

Da `Write` udskriver første element i en sekvens, bruger vi `Every` til at iterere over alle resultaterne i sekvensen:
```fsharp
let iconEx1 = Every(Write(Prim("<",CstI 7,FromTo(1,10))))
```

## Delopgave 2

I konkret syntaks ville udtrykket se ud som:
```fsharp
every(write((1 to 4) & write("\n") & (1 to 4)))
```

## Delopgave 3
Jeg starter med at udvide `expr` typen med følgende union case:
```fsharp
  | Find of string * string
```

Derefter udvider jeg `eval` funktionen med følgende match case:
```fsharp
    | Find(pat, str) -> 
      let rec find (startIndex: int) =
          let result = str.IndexOf(pat, startIndex)
          if result <> -1 then 
              cont (Int result) (fun () -> find (result+1))
          else 
              econt ()
      find 0
```

Her definerer jeg en rekursiv funktion `find`, som finder næste forekomst af `pat`, på den givne `string` - og begynder at lede fra `startIndex`. Hver gang der bliver fundet en forekomst af `pat`, vil `cont` bliver kaldt med resultatet, og en continuation til at hente næste indekset af næste forekomst af `pat`.

## Delopgave 4
Her finder jeg alle forekomster af mellemrum, i en streng:
```fsharp
> let str = "Hi there - if there are anyone";;
val str : string = "Hi there - if there are anyone"

> run (Every(Write(Find(" ",str))));;
2 8 10 13 19 23 val it : value = Int 0
```

Og alle forekomster af "are":
```fsharp
> let str = "Hi there - if there are anyone";;
val str : string = "Hi there - if there are anyone"

> run (Every(Write(Find("are",str))));;
20 val it : value = Int 0
```

Og alle forekomster af "there", som også vist som eksempel i delopgave 3:
```fsharp
> let str = "Hi there - if there are anyone";;
val str : string = "Hi there - if there are anyone"

> run (Every(Write(Find("there",str))));;
3 14 val it : value = Int 0
```

## Delopgave 5
Her finder jeg alle forekomster af "e":
```fsharp
> let str = "Hi there - if there are anyone";;
val str : string = "Hi there - if there are anyone"

> run (Every(Write(Find("e",str))));;
5 7 16 18 22 29 val it : value = Int 0
```

Og her finder jeg alle forekomster af "e", hvor at indekset er højere end 10:
```fsharp
> let str = "Hi there - if there are anyone";;
val str : string = "Hi there - if there are anyone"

> run (Every(Write(Prim("<", CstI 10, Find("e",str)))));;
16 18 22 29 val it : value = Int 0
```

# Opgave 2 - Parsing Records i micro–ML

## Delopgave 1
Abstrakt syntaks af ex1:
```fsharp
Let ("x",Record [],Var "x")
```

Abstrakt syntaks af ex3:
```fsharp
Let ("x",Record [("field1", CstI 32); ("field2", CstI 33)],Field (Var "x",""))
```

Abstrakt syntaks af ex4:
```fsharp
Let ("x",Record [("field1", CstI 32); ("field2", CstI 33)],Field (Var "x","field1"))
```

Abstrakt syntaks af ex5:
```fsharp
Let ("x",Record [("field1", CstI 32); ("field2", CstI 33)],Prim("+", Field (Var "x","field1"), Field (Var "x","field2")))
```

## Delopgave 2
Jeg starter med at tilføje en punktum, venstre/højre bracket og semikolon token til `FunPar.fsy`:
```fsharp
%token DOT LBRACK RBRACK SEMIC
```

Efterfulgt af at tilføje samme tokens til `FunLex.fsl`, til `Token` reglen:
```fsharp
  | '.'             { DOT }
  | '{'             { LBRACK }
  | '}'             { RBRACK }
  | ';'             { SEMIC }
```

Jeg starter med at implementere `Record` i parseren. Dette gør jeg ved først at tilføje en RecordList sektion til `FunPar.fsy`:
```fsharp
RecordList:
  | NAME EQ Expr                  { [($1, $3)]             }
  | NAME EQ Expr SEMIC RecordList { ($1, $3)::$5           }
;
```

Efterfulgt af at tilføje følgende linjer til `AtExpr`sektionen:
```fsharp
  | LET NAME EQ LBRACK RBRACK IN Expr END            { Let($2, Record([]), $7) }
  | LET NAME EQ LBRACK RecordList RBRACK IN Expr END { Let($2, Record($5), $8) }
```

Til sidst implementere jeg `Field`, ved at tilføje følgende linje til `Expr` sektionen:
```fsharp
  | Expr DOT NAME                       { Field($1, $3)          }
```

Resultat af at parse ex1:
```
> fromString "let x = { } in x end";;
val it : Absyn.expr = Let ("x",Record [],Var "x")
```

Resultat af at parse ex2:
```
> fromString "let x = {field1 = 32} in x.field1 end";;
val it : Absyn.expr =
  Let ("x",Record [("field1", CstI 32)],Field (Var "x","field1"))
```

Resultat af at parse ex3:
```
> fromString "let x = {field1 = 32; field2 = 33} in x end";;
val it : Absyn.expr =
  Let ("x",Record [("field1", CstI 32); ("field2", CstI 33)],Var "x") 
```

Resultat af at parse ex4:
```
> fromString "let x = {field1 = 32; field2 = 33} in x.field1 end";;
val it : Absyn.expr =
  Let
    ("x",Record [("field1", CstI 32); ("field2", CstI 33)],
     Field (Var "x","field1"))
```

Resultat af at parse ex5:
```
> fromString "let x = {field1 = 32; field2 = 33} in x.field1+x.field2 end";;
val it : Absyn.expr =
  Let
    ("x",Record [("field1", CstI 32); ("field2", CstI 33)],
     Prim ("+",Field (Var "x","field1"),Field (Var "x","field2")))
```