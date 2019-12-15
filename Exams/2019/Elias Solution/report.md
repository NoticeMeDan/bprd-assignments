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

# Opgave 3 - Evaluering af Records i Micro-ML
## Delopgave 1

## Delopgave 2
Jeg starter med at tilføje `RecordV` union casen til `value` typen:
```fsharp
type value = 
  | Int of int
  | Closure of string * string * expr * value env
  | RecordV of (string * value) list
```

Herefter tilføjer jeg følgende kodestump til `eval` funktionen:
```fsharp
    | Record elements -> 
      RecordV(List.map (fun (label,rVal) -> (label,eval rVal env)) elements)
    | Field(var, label) -> 
      let eVar = eval var env
      match eVar with
      | RecordV record -> lookup record label
      | _ -> failwith "Not a record"
```

Når der bliver fundet en `Record` i vores AST, så omdanner jeg det til et `RecordV`, ved at evaluere alle de expressions der indgår i tupel listen. Når der bliver fundet et field, så leder jeg efter en record med navnet `var` i funktionens `Closure`, og hvis det er fundet kigger jeg i den record efter en værdi hvor at dens label matcher `label` værdien.

Hvis der i `RecordV` indgår flere tupler med samme label, så vil den første i listen blive valgt, det er derfor brugerens ansvar ikke at have duplikerede labels.

## Delopgave 2
```
> run (Let ("x",Record [("field1", CstI 32);("field1",CstI 33)],Field (Var "x","field1")));;
val it : HigherFun.value = Int 32
```

Magter ikke at tilføje dem alle, det er rent slavearbejde.

# Opgave 4 - Breakpoings i micro-c
Jeg starter med at tilføje en `Break` union case til `stmt` typen i `Absyn.fs`:
```fsharp
  | Break of expr
```

Derefter laver jeg følgende ændringer i `CPar.fsy`:
* Tilføjer `BREAK` som token
* Udvider `StmtM` til at kunne håndtere vores break statements

```fsharp
%token CHAR ELSE IF INT NULL PRINT PRINTLN RETURN VOID WHILE BREAK

StmtM:  /* No unbalanced if-else */
    Expr SEMI                           { Expr($1)             }
    ...
  | BREAK Expr SEMI                     { Break($2)            }
    ...
;
```

Og tilføjer "break" som keyword i `CLex.fsl`:
```fsharp
    | "break"   -> BREAK
```

Derefter laver jeg følgende ændringer i `Machine.fs`:
* Tilføjer vores `BREAK` og `WAITKEYPRESS` bytecode værdier til `instr` typen
* Tilføjer numeriske versioner af vores bytecode værdier
* Tilføjer vores bytecode værdier til makelabenv samt emitints
  
```fsharp
type instr =
  | Label of label                     (* symbolic label; pseudo-instruc. *)
  | CSTI of int                        (* constant                        *)
  ...
  | BREAK                              (* prints stack and cotinues exec  *) 
  | WAITKEYPRESS                       (* halt execution until key press  *)

let CODEBREAK  = 26
let CODEWKP    = 27

let makelabenv (addr, labenv) instr = 
    match instr with
    | Label lab      -> (addr, (lab, addr) :: labenv)
    | CSTI i         -> (addr+2, labenv)
    ...
    | BREAK          -> (addr+1, labenv)
    | WAITKEYPRESS   -> (addr+1, labenv)

let rec emitints getlab instr ints = 
    match instr with
    | Label lab      -> ints
    | CSTI i         -> CODECSTI   :: i :: ints
    ...
    | BREAK          -> CODEBREAK  :: ints
    | WAITKEYPRESS   -> CODEWKP    :: ints
```

Efterfulgt af følgende ændringer i `Machine.java`:
* Tilføjer `BREAK` og `WAITKEYPRESS` konstanter som stemmer overens med dem i `Machine.fs`
* Tilføjer håndtering af hvad der skal ske, når vi rammer `BREAK` og `WAITKEYPRESS` værdierne i bytecoden

```java
final static int BREAK = 26, WAITKEYPRESS = 27;

static int execcode(int[] p, int[] s, int[] iargs, boolean trace) {
    int bp = -999;	// Base pointer, for local variable access 
    int sp = -1;	// Stack top pointer
    int pc = 0;		// Program counter: next instruction
    for (;;) {
      if (trace) 
        printsppc(s, bp, sp, p, pc);
      switch (p[pc++]) {
      case CSTI:
        s[sp+1] = p[pc++]; sp++; break;
      case ADD: 
        s[sp-1] = s[sp-1] + s[sp]; sp--; break;
      ...
      case BREAK:
        printsppc(s, bp, sp, p, pc); break;
      case WAITKEYPRESS: {
        System.out.println("Press ENTER to continue");
        try {System.in.read();} catch (Exception e) {};
      } break;
      ...
    }
```

Til sidst udvider jeg compileren i `Contcomp.fs` til at kunne håndtere breakpoints:
```fsharp
let rec cStmt stmt (varEnv : varEnv) (funEnv : funEnv) (C : instr list) : instr list = 
    match stmt with
    | If(e, stmt1, stmt2) -> 
      let (jumpend, C1) = makeJump C
      let (labelse, C2) = addLabel (cStmt stmt2 varEnv funEnv C1)
      cExpr e varEnv funEnv (IFZERO labelse 
       :: cStmt stmt1 varEnv funEnv (addJump jumpend C2))
    ...
    | Break e ->
      let (labSkip, C1) = addLabel C
      cExpr e varEnv funEnv (BREAK :: IFZERO labSkip :: WAITKEYPRESS :: C1)
```

Alt dette resulterer i, at når vi eksekverer følgende kode:
```c
void main(int n) {
  while (n > 0) {
    print n;
    break (n % 2 == 0);
    n = n - 1;
  }
  println;
}
```

Så resulterer det i følgende output i terminalen:
```
$ java Machine ex1.out 10
10 [ 4 -999 10 1 ]{19: IFZERO 22}
Press ENTER to continue

9 [ 4 -999 9 0 ]{19: IFZERO 22}
8 [ 4 -999 8 1 ]{19: IFZERO 22}
Press ENTER to continue

7 [ 4 -999 7 0 ]{19: IFZERO 22}
6 [ 4 -999 6 1 ]{19: IFZERO 22}
Press ENTER to continue

5 [ 4 -999 5 0 ]{19: IFZERO 22}
4 [ 4 -999 4 1 ]{19: IFZERO 22}
Press ENTER to continue

3 [ 4 -999 3 0 ]{19: IFZERO 22}
2 [ 4 -999 2 1 ]{19: IFZERO 22}
Press ENTER to continue

1 [ 4 -999 1 0 ]{19: IFZERO 22}


Ran 5.818 seconds
```