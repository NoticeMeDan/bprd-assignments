# Opgave 2 - Icon
## Delopgave 1
Jeg ændrer `FromTo(1,2)` til `FromTo(3,4)` for at opnå det ønskede resultat:
```fsharp
> let iconEx1 = Every(Write(Or(FromTo(3, 4), FromTo(3,4))));;
val iconEx1 : expr = Every (Write (Or (FromTo (3,4),FromTo (3,4))))

> run iconEx1;;
3 4 3 4 val it : value = Int 0
```

## Delopgave 2
Jeg bygger følgende udtryk i abstrakt syntaks, for at opnå det ønskede resultat:
`every(write("I" | "c" | "o" | "n"))`

```fsharp
> let iconString = Every(Write(Or(Or(CstS "I", CstS "c"), Or(CstS "o", CstS "n"))));;
val iconString : expr =
  Every (Write (Or (Or (CstS "I",CstS "c"),Or (CstS "o",CstS "n"))))

> run iconString;;
I c o n val it : value = Int 0
```

## Delopgave 3
Jeg starter med at tilføje en `Bang` union case til `expr` typen:
```fsharp
type expr = 
  ...
  | Bang of string
  ...
```

Derefter tilføjer jeg følgende kodestump til `eval` funktionen:
```fsharp
let rec eval (e : expr) (cont : cont) (econt : econt) = 
    match e with
    | CstI i -> cont (Int i) econt
    ...
    | Bang str -> 
      let rec loop (startIndex: int) =
          if startIndex < str.Length then 
              let result = str.Substring(startIndex, 1)
              cont (Str result) (fun () -> loop (startIndex + 1))
          else 
              econt ()
      loop 0
```

Hvilket resulterer i:
```
> open Icon;;
> run (Every(Write(Bang "Icon")));;
I c o n val it : value = Int 0
```

## Delopgave 4
Jeg tilføjer derefter `BangN` til `expr` typen:
```fsharp
type expr = 
  | CstI of int
  ...
  | Bang of string
  | BangN of string * int
  ...
```

Og udvider `eval` funktionen:
```fsharp
let rec eval (e : expr) (cont : cont) (econt : econt) = 
    match e with
    | CstI i -> cont (Int i) econt
    ...
    | BangN(str, n) -> 
      eval (FromTo(1, n)) (fun x -> fun econt1 -> eval(Bang str) cont econt1) econt
```
Her udnytter vi `FromTo` til at lave en liste fra 1-N, hvor at vi for hvert tal kører vores continuation der evaluerer `Bang` med den givne streng.
Resultet er at vi kan køre følgende:

```
> open Icon;;
> run (Every(Write(BangN("Icon", 2))));;
I c o n I c o n val it : value = Int 0
```