module Intcomp1

// 2.1
type expr = 
  | CstI of int
  | Var of string
  | Let of (string * expr) list * expr
  | Prim of string * expr * expr;;

(* Evaluation of expressions with variables and bindings *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Let(lets, body) ->
      let newEnv = List.fold (fun env (name, rhs) -> (name, (eval rhs env)) :: env) env lets
      eval body newEnv
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim _            -> failwith "unknown primitive";;

let run e = eval e [];;

let test = Let([("x1", Prim("+", CstI 5, CstI 7));
                ("x2", Prim("*", Var "x1", CstI 7))],
               Prim("+", Var "x1", Var "x2"));; // Result should be 96

(* ---------------------------------------------------------------------- *)

(* Closedness *)

// let mem x vs = List.exists (fun y -> x=y) vs;;

let rec mem x vs = 
    match vs with
    | []      -> false
    | v :: vr -> x=v || mem x vr;;

(* Checking whether an expression is closed.  The vs is 
   a list of the bound variables.  *)


(* Free variables *)

(* Operations on sets, represented as lists.  Simple but inefficient;
   one could use binary trees, hashtables or splaytrees for
   efficiency.  *)

(* union(xs, ys) is the set of all elements in xs or ys, without duplicates *)

let rec union (xs, ys) = 
    match xs with 
    | []    -> ys
    | x::xr -> if mem x ys then union(xr, ys)
               else x :: union(xr, ys);;

(* minus xs ys  is the set of all elements in xs but not in ys *)

let rec minus (xs, ys) = 
    match xs with 
    | []    -> []
    | x::xr -> if mem x ys then minus(xr, ys)
               else x :: minus (xr, ys);;

(* Find all variables that occur free in expression e *)

let rec freevars e : string list =
    match e with
    | CstI i -> []
    | Var x  -> [x]
    | Let(x, ebody) -> 
      match x with
      |[] -> freevars ebody
      |(x1,x2)::xr -> union (freevars x2, minus (freevars (Let(xr,ebody)), [x1]))
    | Prim(ope, e1, e2) -> union (freevars e1, freevars e2);;

(* Alternative definition of closed *)

let closed2 e = (freevars e = []);;


(* ---------------------------------------------------------------------- *)

(* Compilation to target expressions with numerical indexes instead of
   symbolic variable names.  *)

type texpr =                            (* target expressions *)
  | TCstI of int
  | TVar of int                         (* index into runtime environment *)
  | TLet of texpr * texpr               (* erhs and ebody                 *)
  | TPrim of string * texpr * texpr;;


(* Map variable name to variable index at compile-time *)

let rec getindex vs x = 
    match vs with 
    | []    -> failwith "Variable not found"
    | y::yr -> if x=y then 0 else 1 + getindex yr x;;

(* Compiling from expr to texpr *)

let rec tcomp (e : expr) (cenv : string list) : texpr =
    match e with
    | CstI i -> TCstI i
    | Var x  -> TVar (getindex cenv x)
    | Let(x, ebody) ->
      match x with
      |[] -> tcomp ebody cenv
      |(x1,x2)::xr -> let cenv' = x1 :: cenv
                      TLet(tcomp x2 cenv', tcomp ebody cenv')
    | Prim(ope, e1, e2) -> TPrim(ope, tcomp e1 cenv, tcomp e2 cenv);;
    
