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

# Opgave 3 - Enums i Micro-ML
## Delopgave 2
Jeg starter med at udvide `Absyn.fs` med følgende union cases:
```fsharp
type expr = 
  ...
  | Enum of string * string list * expr
  | EnumVal of string * string
```

Herefter bygger jeg den abstrakte syntaks for følgende udtryk:
```sml
enum Weekend = Sat | Sun in let r = 1 + Weekend.Sun in r + 1 end end
```

Hvilket resulterer i følgende ydtryk:
```fsharp
Enum("Weeekend", ["Sat";"Sun"], 
  Let("r", Prim("+", CstI 1, EnumVal("Weekend", "Sun"), Prim("+", Var "r", CstI 1)))
)
```

## Delopgave 3
Jeg starter med at lave følgende ændringer i `Funlex.fsl`:
* Tilføjer `enum` som keyword
* Tilføjer | (PIPE) og . (DOT) som tokens
  
```fsharp
let keyword s =
    match s with
    ...
    | "enum"  -> ENUM
    ...
}

rule Token = parse
  ...
  | '|'             { PIPE }
  | '.'             { DOT }
  ...
```

Derefter laver jeg følgende ændringer i `FunPar.fsy`:
* Tilføj `ENUM`, `DOT` og `PIPE` som tokens
* Tilføj en `EnumList` block til at bygge vores string array i enum deklarationen
* Udvid `AtExpr` til at kunne håndtere enum deklaration og brug af enum værdier

```fsharp
%token ELSE END FALSE IF IN LET NOT THEN TRUE ENUM
%token PLUS MINUS TIMES DIV MOD DOT PIPE

EnumList:
    NAME                                { [$1]                   }
  | NAME PIPE EnumList                  { $1::$3                 }
;

AtExpr:
  ...
  | ENUM NAME EQ EnumList IN Expr END   { Enum($2, $4, $6)       }
  | ENUM NAME EQ IN Expr END            { Enum($2, [], $5)       }
  | NAME DOT NAME                       { EnumVal($1, $3)        }
  ...
;
```

Nu kan vi så parse udtrykket fra sidste opgave:
`enum Weekend = Sat | Sun in let r = 1 + Weekend.Sun in r + 1 end end`

Hvilket resulterer i:
```
> fromString "enum Weekend = Sat | Sun in let r = 1 + Weekend.Sun in r + 1 end end";;
val it : Absyn.expr =
  Enum
    ("Weekend",["Sat"; "Sun"],
     Let
       ("r",Prim ("+",CstI 1,EnumVal ("Weekend","Sun")),
        Prim ("+",Var "r",CstI 1)))
```

Og her vises hvordan de 3 eksempel udtryk bliver parset:
```
> fromString "enum OneTwo = One | Two in OneTwo.One end";;
val it : Absyn.expr = Enum ("OneTwo",["One"; "Two"],EnumVal ("OneTwo","One"))

> fromString "enum One = One in One.One end";;
val it : Absyn.expr = Enum ("One",["One"],EnumVal ("One","One"))

> fromString "enum None = in 42 end";;
val it : Absyn.expr = Enum ("None",[],CstI 42)
```

Jeg vil mene at `enum None` eksemplet ikke burde være et gyldigt program. Jeg blev faktisk nødt til at tilføje en special case for at kunne understøtte det, da jeg ikke tænkte at det gav mening at kunne lave en enum deklaration uden at faktisk deklarere en enum. Så kunne man lige så godt bruge et hvilket som helst andet udtryk, da det ikke tilføjer noget til programmet.

## Delopgave 4
Jeg starter med at tilføje `EVal` til `value` typen:
```fsharp
type value = 
  ...
  | EVal of string * string list
```

Herefter udvider jeg `eval` funktionen til at understøtte enums:
```fsharp
let rec eval (e : expr) (env : value env) : value =
    match e with
    | CstI i -> Int i
    ...
    | Enum(name, enums, body) ->
      let enumEnv = (name, EVal(name, enums)) :: env
      eval body enumEnv
    | EnumVal(name, label) ->
      let value = lookup env name
      match value with
      | EVal(n, enums) ->
        let result = List.tryFindIndex (fun elem -> elem = label) enums
        match result with
        | Some index -> Int index
        | None -> failwith "Enum label not found"
      | _ -> failwith "Enum does not exist"
```
Alt dette resulterer i at vi kan evaluere `Weekend` udtrykket fra opgave 2:
```
> run (fromString "enum Weekend = Sat | Sun in let r = 1 + Weekend.Sun in r + 1 end end");;
val it : HigherFun.value = Int 3
```

Givet at `value env` typen kræver en string key udover vores `value`, så kan jeg dog ikke forstå hvorfor at `EVal` typen overhovedet bør have en `string` i dens type, da denne værdi slet ikke vil blive brugt, da man i forvejen skal give samme streng som key, når man tilføjer en `EVal` til `env`.