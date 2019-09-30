# BRPD Assignment 4 

### Exercise 6.1 
Download and unpack ``fun1.zip`` and ``fun2.zip`` and build the micro-ML higher-order evaluator as described in ﬁle ``README.TXT`` point E. Then run the evaluator on the following four programs. Is the result of the third one as expected? Explain the result of the last one: 

````
let add x = let f y = x+y 
in f end in add 2 5 end

let add x = let f y = x+y in f end 
in let addtwo = add 2 
    in addtwo 5 end 
end

let add x = let f y = x+y in f end 
    
in let addtwo = add 2 
    in let x = 77 in addtwo 5 end 
    end 
end

let add x = let f y = x+y in f end 
in add 2 end
````

##### Solution:

### Exercise 6.2 
Add anonymous functions, similar to F#’s ``fun x -> ...``, to the micro-ML higher-order functional language abstract syntax:

````
type expr = 
    ... 
    | Fun of string* expr 
    | ...
````

For instance, these two expressions in concrete syntax: 

````
fun x -> 2*x 
let y = 22 in fun z -> z+y end
````
should parse to these two expressions in abstract syntax: 
````
Fun("x", Prim("*", CstI 2, Var "x")) 
Let("y", CstI 22, Fun("z", Prim("+", Var "z", Var "y"))) 
````

Evaluation of a ``Fun(...)`` should produce a non-recursive closure of the form 
````
type value = 
    | ... 
    | Clos of string* expr * value env (* (x,body,declEnv) *) 
````
Intheemptyenvironmentthetwoexpressionsshownaboveshouldevaluatetothese two closure values:
````
 Clos("x", Prim("*", CstI 2, Var "x"), []) 
 Clos("z", Prim("+", Var "z", Var "y"), [(y,22)])
 
 ````
  Extend the evaluator eval in ﬁle ``HigherFun.fs`` to interpret such anonymous functions.


##### Solution:

### Exercise6.3 
Extendthemicro-MLlexerandparserspeciﬁcationsinFunLex.fsl and FunPar.fsy to permit anonymous functions. The concrete syntax may be as in F#: ``fun x -> expr`` or as in Standard ML: ``fn x => expr`` , wherex is a variable. The micro-ML examples from Exercise 6.1 can now be written in these two alternative ways: 

````
let add x = fun y -> x+y in add 2 5 end

let add = fun x -> fun y -> x+y in add 2 5 end
````

##### Solution:

### Exercise6.4 
This exercise concerns type rules for ML-polymorphism, as shown in Fig. 6.1. 
#### Part (i) 
Build a type rule tree for this micro-ML program (in the let-body, the type of f should be polymorphic—why?):

``let f x = 1 in f f end``

##### Solution:

#### Part (ii) 
Build a type rule tree for this micro-ML program (in the let-body, f should not be polymorphic—why?):
````
let f x = if x < 10 then 42 else f(x+1) 
in f 20 end
````

##### Solution:

### Exercise 6.5 
Download ``fun2.zip`` and build the micro-ML higher-order type inference as described in ﬁle README.TXT point F.

##### Part (1)  
Use the type inference on the micro-ML programs shown below, and report what type the program has. Some of the type inferences will fail because the programsarenottypableinmicro-ML;inthosecases,explainwhytheprogram is not typable:

```
let f x = 1 
in f f end

let f g = g g 
in f end

let f x = let g y = y 
in g false end in f 42 end

let f x = 
    let g y = if true then y else x 
    in g false end
in f 42 end

let f x = 
    let g y = if true then y else x 
    in g false end 
in f true end

```
##### Solution:


##### Part (2) 
Write micro-ML programs for which the micro-ML type inference report the following types:

 • ``bool -> bool`` 
 • ``int -> int ``
 • ``int -> int -> int ``
 • ``’a -> ’b -> ’a ``
 • ``’a -> ’b -> ’b ``
 • ``(’a -> ’b) -> (’b -> ’c) -> (’a -> ’c) ``
 • ``’a -> ’b ``
 • ``’a`` 
 
 Remember that the type arrow ``(->)`` is right associative, ``soint -> int -> int`` is the same as ``int -> (int -> int)``, and that the choice of type variables does not matter, so the type scheme ``’h -> ’g -> ’h`` is the same as ``a’ -> ’b -> ’a``.

##### Solution: