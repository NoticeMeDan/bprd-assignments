(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

module Intro2

(* Association lists map object language variables to their values *)

let env = [("a", 3); ("c", 78); ("baf", 666); ("b", 111)];;

let emptyenv = []; (* the empty environment *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

let cvalue = lookup env "c";;


// 1.1

type expr = 
  | CstI of int
  | Var of string
  | Prim of string * expr * expr
  | If of expr * expr * expr


(* Evaluation within an environment *)

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Prim(ope, e1, e2) ->
        let i1 = eval e1 env
        let i2 = eval e2 env
        match ope with
            | "+" -> i1 + i2
            | "-" -> i1 - i2
            | "*" -> i1 * i2
            | "max" -> max i1 i2
            | "min" -> min i1 i2
            | "==" -> if i1 = i2 then 1 else 0
            | _ -> failwith "unknown primitive"
    | If(e1, e2, e3) -> if (eval e1 env) <> 0 then eval e2 env else eval e3 env
let example1 = Prim("==", Prim("max", CstI 1, CstI 2), Prim("min", CstI 2, CstI 3))

// 1.2
// (i)
type aexpr =
    | CstI of int
    | Var of string
    | Add of aexpr * aexpr
    | Mul of aexpr * aexpr
    | Sub of aexpr * aexpr
    
// (ii)
let e1 = Sub(Var "v", Add(Var "w", Var "z"))
let e2 = Mul(CstI 2, Sub(Var "v", Add(Var "w", Var "z")))
let e3 = Add(Add(Var "x", Var "y"), Add(Var "z", Var "v"))

// (iii)
let rec fmt (exp: aexpr) : string =
    match exp with
    | CstI i -> string i
    | Var x -> x
    | Add(e1, e2) -> sprintf "(%s + %s)" (fmt e1) (fmt e2)
    | Mul(e1, e2) -> sprintf "(%s * %s)" (fmt e1) (fmt e2)
    | Sub(e1, e2) -> sprintf "(%s - %s)" (fmt e1) (fmt e2)

//tests
let fmtTest1 = fmt (Sub(Var "x", CstI 34))  = "(x - 34)"
let fmtTest2 = fmt (CstI 1)                 = "1"
let fmtTest3 = fmt (Var "x")                = "x"

// (iv)
let isSimplifiable exp =
    match exp with
        | Add(CstI 0, e)
        | Add(e, CstI 0)
        | Sub(e, CstI 0)
        | Mul(CstI 1, e)
        | Mul(e, CstI 1)
        | Mul(CstI 0, e)
        | Mul(e, CstI 0) -> true
        | _ -> false

let rec simplify (exp : aexpr) : aexpr =
    match exp with
    | CstI i -> CstI i
    | Var x -> Var x
    | Add(CstI 0, e)
    | Add(e, CstI 0)
    | Sub(e, CstI 0)
    | Mul(CstI 1, e)
    | Mul(e, CstI 1) -> e
    | Mul(CstI 0, e)
    | Mul(e, CstI 0) -> CstI 0
    | Add(e1, e2) ->
        let result = Add(simplify e1, simplify e2)
        if (isSimplifiable result) then simplify result else result
    | Mul(e1, e2) ->
        let result = Mul(simplify e1, simplify e2)
        if (isSimplifiable result) then simplify result else result
    | Sub(e1, e2) ->
        if e1 = e2 then CstI 0 else
            let result = Sub(simplify e1, simplify e2)
            if (isSimplifiable result) then simplify result else result


let test = Add(Mul(CstI 0, CstI 5), Sub(CstI 5, CstI 0))

// (v)
let rec symbolicDifferentiation exp : aexpr =
   let derivative' =
       match exp with
       | Var x       -> CstI 1
       | CstI i      -> CstI 0
       | Add(e1, e2) -> Add(symbolicDifferentiation(e1), symbolicDifferentiation(e2))
       | Sub(e1, e2) -> Sub(symbolicDifferentiation(e1), symbolicDifferentiation(e2))
       | Mul(e1, e2) -> Add(Mul(symbolicDifferentiation(e1), e2), Mul(e1, symbolicDifferentiation(e2)))
       | _ -> CstI 0
   simplify derivative'

//tests with e1, e2, e3
let diffTest_1 = symbolicDifferentiation e1
let diffTest_2 = symbolicDifferentiation e2
let diffTest_3 = symbolicDifferentiation e3

//5x + 0 -> 5
let diffTest_4 = symbolicDifferentiation (Add(Mul(CstI 5, Var "x"), CstI 0))

//2x + 7 -> 2
let diffTest_5 = symbolicDifferentiation (Add(Mul(CstI 2, Var "x"), CstI 3))

//x -> 1
let diffTest_6 = symbolicDifferentiation (Var "x")

//x8 -> 8
let diffTest_7 = symbolicDifferentiation (Mul(CstI 8, Var "x"))