// Implementation file for parser generated by fsyacc
module FunPar
#nowarn "64";; // turn off warnings that type variables used in production annotations are instantiated to concrete type
open FSharp.Text.Lexing
open FSharp.Text.Parsing.ParseHelpers
# 1 "FunPar.fsy"

 (* File Fun/FunPar.fsy 
    Parser for micro-ML, a small functional language; one-argument functions.
    sestoft@itu.dk * 2009-10-19
  *)

 open Absyn;

# 15 "FunPar.fs"
// This type is the type of tokens accepted by the parser
type token = 
  | PRINT
  | EOF
  | LPAR
  | RPAR
  | EQ
  | NE
  | GT
  | LT
  | GE
  | LE
  | PIPERIGHT
  | COMPOSERIGHT
  | PLUS
  | MINUS
  | TIMES
  | DIV
  | MOD
  | ELSE
  | END
  | FALSE
  | IF
  | IN
  | LET
  | NOT
  | THEN
  | TRUE
  | CSTBOOL of (bool)
  | NAME of (string)
  | CSTINT of (int)
// This type is used to give symbolic names to token indexes, useful for error messages
type tokenId = 
    | TOKEN_PRINT
    | TOKEN_EOF
    | TOKEN_LPAR
    | TOKEN_RPAR
    | TOKEN_EQ
    | TOKEN_NE
    | TOKEN_GT
    | TOKEN_LT
    | TOKEN_GE
    | TOKEN_LE
    | TOKEN_PIPERIGHT
    | TOKEN_COMPOSERIGHT
    | TOKEN_PLUS
    | TOKEN_MINUS
    | TOKEN_TIMES
    | TOKEN_DIV
    | TOKEN_MOD
    | TOKEN_ELSE
    | TOKEN_END
    | TOKEN_FALSE
    | TOKEN_IF
    | TOKEN_IN
    | TOKEN_LET
    | TOKEN_NOT
    | TOKEN_THEN
    | TOKEN_TRUE
    | TOKEN_CSTBOOL
    | TOKEN_NAME
    | TOKEN_CSTINT
    | TOKEN_end_of_input
    | TOKEN_error
// This type is used to give symbolic names to token indexes, useful for error messages
type nonTerminalId = 
    | NONTERM__startMain
    | NONTERM_Main
    | NONTERM_Expr
    | NONTERM_AtExpr
    | NONTERM_AppExpr
    | NONTERM_Const

// This function maps tokens to integer indexes
let tagOfToken (t:token) = 
  match t with
  | PRINT  -> 0 
  | EOF  -> 1 
  | LPAR  -> 2 
  | RPAR  -> 3 
  | EQ  -> 4 
  | NE  -> 5 
  | GT  -> 6 
  | LT  -> 7 
  | GE  -> 8 
  | LE  -> 9 
  | PIPERIGHT  -> 10 
  | COMPOSERIGHT  -> 11 
  | PLUS  -> 12 
  | MINUS  -> 13 
  | TIMES  -> 14 
  | DIV  -> 15 
  | MOD  -> 16 
  | ELSE  -> 17 
  | END  -> 18 
  | FALSE  -> 19 
  | IF  -> 20 
  | IN  -> 21 
  | LET  -> 22 
  | NOT  -> 23 
  | THEN  -> 24 
  | TRUE  -> 25 
  | CSTBOOL _ -> 26 
  | NAME _ -> 27 
  | CSTINT _ -> 28 

// This function maps integer indexes to symbolic token ids
let tokenTagToTokenId (tokenIdx:int) = 
  match tokenIdx with
  | 0 -> TOKEN_PRINT 
  | 1 -> TOKEN_EOF 
  | 2 -> TOKEN_LPAR 
  | 3 -> TOKEN_RPAR 
  | 4 -> TOKEN_EQ 
  | 5 -> TOKEN_NE 
  | 6 -> TOKEN_GT 
  | 7 -> TOKEN_LT 
  | 8 -> TOKEN_GE 
  | 9 -> TOKEN_LE 
  | 10 -> TOKEN_PIPERIGHT 
  | 11 -> TOKEN_COMPOSERIGHT 
  | 12 -> TOKEN_PLUS 
  | 13 -> TOKEN_MINUS 
  | 14 -> TOKEN_TIMES 
  | 15 -> TOKEN_DIV 
  | 16 -> TOKEN_MOD 
  | 17 -> TOKEN_ELSE 
  | 18 -> TOKEN_END 
  | 19 -> TOKEN_FALSE 
  | 20 -> TOKEN_IF 
  | 21 -> TOKEN_IN 
  | 22 -> TOKEN_LET 
  | 23 -> TOKEN_NOT 
  | 24 -> TOKEN_THEN 
  | 25 -> TOKEN_TRUE 
  | 26 -> TOKEN_CSTBOOL 
  | 27 -> TOKEN_NAME 
  | 28 -> TOKEN_CSTINT 
  | 31 -> TOKEN_end_of_input
  | 29 -> TOKEN_error
  | _ -> failwith "tokenTagToTokenId: bad token"

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
let prodIdxToNonTerminal (prodIdx:int) = 
  match prodIdx with
    | 0 -> NONTERM__startMain 
    | 1 -> NONTERM_Main 
    | 2 -> NONTERM_Expr 
    | 3 -> NONTERM_Expr 
    | 4 -> NONTERM_Expr 
    | 5 -> NONTERM_Expr 
    | 6 -> NONTERM_Expr 
    | 7 -> NONTERM_Expr 
    | 8 -> NONTERM_Expr 
    | 9 -> NONTERM_Expr 
    | 10 -> NONTERM_Expr 
    | 11 -> NONTERM_Expr 
    | 12 -> NONTERM_Expr 
    | 13 -> NONTERM_Expr 
    | 14 -> NONTERM_Expr 
    | 15 -> NONTERM_Expr 
    | 16 -> NONTERM_Expr 
    | 17 -> NONTERM_Expr 
    | 18 -> NONTERM_Expr 
    | 19 -> NONTERM_AtExpr 
    | 20 -> NONTERM_AtExpr 
    | 21 -> NONTERM_AtExpr 
    | 22 -> NONTERM_AtExpr 
    | 23 -> NONTERM_AtExpr 
    | 24 -> NONTERM_AtExpr 
    | 25 -> NONTERM_AppExpr 
    | 26 -> NONTERM_AppExpr 
    | 27 -> NONTERM_Const 
    | 28 -> NONTERM_Const 
    | _ -> failwith "prodIdxToNonTerminal: bad production index"

let _fsyacc_endOfInputTag = 31 
let _fsyacc_tagOfErrorTerminal = 29

// This function gets the name of a token as a string
let token_to_string (t:token) = 
  match t with 
  | PRINT  -> "PRINT" 
  | EOF  -> "EOF" 
  | LPAR  -> "LPAR" 
  | RPAR  -> "RPAR" 
  | EQ  -> "EQ" 
  | NE  -> "NE" 
  | GT  -> "GT" 
  | LT  -> "LT" 
  | GE  -> "GE" 
  | LE  -> "LE" 
  | PIPERIGHT  -> "PIPERIGHT" 
  | COMPOSERIGHT  -> "COMPOSERIGHT" 
  | PLUS  -> "PLUS" 
  | MINUS  -> "MINUS" 
  | TIMES  -> "TIMES" 
  | DIV  -> "DIV" 
  | MOD  -> "MOD" 
  | ELSE  -> "ELSE" 
  | END  -> "END" 
  | FALSE  -> "FALSE" 
  | IF  -> "IF" 
  | IN  -> "IN" 
  | LET  -> "LET" 
  | NOT  -> "NOT" 
  | THEN  -> "THEN" 
  | TRUE  -> "TRUE" 
  | CSTBOOL _ -> "CSTBOOL" 
  | NAME _ -> "NAME" 
  | CSTINT _ -> "CSTINT" 

// This function gets the data carried by a token as an object
let _fsyacc_dataOfToken (t:token) = 
  match t with 
  | PRINT  -> (null : System.Object) 
  | EOF  -> (null : System.Object) 
  | LPAR  -> (null : System.Object) 
  | RPAR  -> (null : System.Object) 
  | EQ  -> (null : System.Object) 
  | NE  -> (null : System.Object) 
  | GT  -> (null : System.Object) 
  | LT  -> (null : System.Object) 
  | GE  -> (null : System.Object) 
  | LE  -> (null : System.Object) 
  | PIPERIGHT  -> (null : System.Object) 
  | COMPOSERIGHT  -> (null : System.Object) 
  | PLUS  -> (null : System.Object) 
  | MINUS  -> (null : System.Object) 
  | TIMES  -> (null : System.Object) 
  | DIV  -> (null : System.Object) 
  | MOD  -> (null : System.Object) 
  | ELSE  -> (null : System.Object) 
  | END  -> (null : System.Object) 
  | FALSE  -> (null : System.Object) 
  | IF  -> (null : System.Object) 
  | IN  -> (null : System.Object) 
  | LET  -> (null : System.Object) 
  | NOT  -> (null : System.Object) 
  | THEN  -> (null : System.Object) 
  | TRUE  -> (null : System.Object) 
  | CSTBOOL _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | NAME _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | CSTINT _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
let _fsyacc_gotos = [| 0us; 65535us; 1us; 65535us; 0us; 1us; 24us; 65535us; 0us; 2us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 33us; 14us; 34us; 15us; 35us; 16us; 36us; 17us; 37us; 18us; 38us; 19us; 39us; 20us; 40us; 21us; 41us; 22us; 42us; 23us; 43us; 24us; 44us; 25us; 45us; 26us; 50us; 27us; 51us; 28us; 54us; 29us; 55us; 30us; 57us; 31us; 59us; 32us; 26us; 65535us; 0us; 4us; 4us; 60us; 5us; 61us; 6us; 4us; 8us; 4us; 10us; 4us; 12us; 4us; 33us; 4us; 34us; 4us; 35us; 4us; 36us; 4us; 37us; 4us; 38us; 4us; 39us; 4us; 40us; 4us; 41us; 4us; 42us; 4us; 43us; 4us; 44us; 4us; 45us; 4us; 50us; 4us; 51us; 4us; 54us; 4us; 55us; 4us; 57us; 4us; 59us; 4us; 24us; 65535us; 0us; 5us; 6us; 5us; 8us; 5us; 10us; 5us; 12us; 5us; 33us; 5us; 34us; 5us; 35us; 5us; 36us; 5us; 37us; 5us; 38us; 5us; 39us; 5us; 40us; 5us; 41us; 5us; 42us; 5us; 43us; 5us; 44us; 5us; 45us; 5us; 50us; 5us; 51us; 5us; 54us; 5us; 55us; 5us; 57us; 5us; 59us; 5us; 26us; 65535us; 0us; 46us; 4us; 46us; 5us; 46us; 6us; 46us; 8us; 46us; 10us; 46us; 12us; 46us; 33us; 46us; 34us; 46us; 35us; 46us; 36us; 46us; 37us; 46us; 38us; 46us; 39us; 46us; 40us; 46us; 41us; 46us; 42us; 46us; 43us; 46us; 44us; 46us; 45us; 46us; 50us; 46us; 51us; 46us; 54us; 46us; 55us; 46us; 57us; 46us; 59us; 46us; |]
let _fsyacc_sparseGotoTableRowOffsets = [|0us; 1us; 3us; 28us; 55us; 80us; |]
let _fsyacc_stateToProdIdxsTableElements = [| 1us; 0us; 1us; 0us; 14us; 1us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 1us; 1us; 2us; 2us; 25us; 2us; 3us; 26us; 1us; 4us; 14us; 4us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 1us; 4us; 14us; 4us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 1us; 4us; 14us; 4us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 1us; 5us; 14us; 5us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 14us; 6us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 14us; 6us; 7us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 14us; 6us; 7us; 8us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 14us; 6us; 7us; 8us; 9us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 14us; 6us; 7us; 8us; 9us; 10us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 13us; 14us; 15us; 16us; 17us; 18us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 14us; 15us; 16us; 17us; 18us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 15us; 16us; 17us; 18us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 16us; 17us; 18us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 17us; 18us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 18us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 21us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 21us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 22us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 22us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 23us; 14us; 6us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 24us; 1us; 6us; 1us; 7us; 1us; 8us; 1us; 9us; 1us; 10us; 1us; 11us; 1us; 12us; 1us; 13us; 1us; 14us; 1us; 15us; 1us; 16us; 1us; 17us; 1us; 18us; 1us; 19us; 1us; 20us; 2us; 21us; 22us; 2us; 21us; 22us; 1us; 21us; 1us; 21us; 1us; 21us; 1us; 22us; 1us; 22us; 1us; 22us; 1us; 22us; 1us; 23us; 1us; 23us; 1us; 24us; 1us; 25us; 1us; 26us; 1us; 27us; 1us; 28us; |]
let _fsyacc_stateToProdIdxsTableRowOffsets = [|0us; 2us; 4us; 19us; 21us; 24us; 27us; 29us; 44us; 46us; 61us; 63us; 78us; 80us; 95us; 110us; 125us; 140us; 155us; 170us; 185us; 200us; 215us; 230us; 245us; 260us; 275us; 290us; 305us; 320us; 335us; 350us; 365us; 380us; 382us; 384us; 386us; 388us; 390us; 392us; 394us; 396us; 398us; 400us; 402us; 404us; 406us; 408us; 410us; 413us; 416us; 418us; 420us; 422us; 424us; 426us; 428us; 430us; 432us; 434us; 436us; 438us; 440us; 442us; |]
let _fsyacc_action_rows = 64
let _fsyacc_actionTableElements = [|8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 0us; 49152us; 14us; 32768us; 1us; 3us; 4us; 38us; 5us; 39us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 10us; 44us; 11us; 45us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 0us; 16385us; 6us; 16386us; 0us; 59us; 2us; 57us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 6us; 16387us; 0us; 59us; 2us; 57us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 14us; 32768us; 4us; 38us; 5us; 39us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 10us; 44us; 11us; 45us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 24us; 8us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 14us; 32768us; 4us; 38us; 5us; 39us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 10us; 44us; 11us; 45us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 17us; 10us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 13us; 16388us; 4us; 38us; 5us; 39us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 10us; 44us; 11us; 45us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 3us; 16389us; 14us; 35us; 15us; 36us; 16us; 37us; 3us; 16390us; 14us; 35us; 15us; 36us; 16us; 37us; 3us; 16391us; 14us; 35us; 15us; 36us; 16us; 37us; 0us; 16392us; 0us; 16393us; 0us; 16394us; 9us; 16395us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 9us; 16396us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 9us; 16397us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 9us; 16398us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 9us; 16399us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 9us; 16400us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 9us; 16401us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 9us; 16402us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 14us; 32768us; 4us; 38us; 5us; 39us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 10us; 44us; 11us; 45us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 21us; 51us; 14us; 32768us; 4us; 38us; 5us; 39us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 10us; 44us; 11us; 45us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 18us; 52us; 14us; 32768us; 4us; 38us; 5us; 39us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 10us; 44us; 11us; 45us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 21us; 55us; 14us; 32768us; 4us; 38us; 5us; 39us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 10us; 44us; 11us; 45us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 18us; 56us; 14us; 32768us; 3us; 58us; 4us; 38us; 5us; 39us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 10us; 44us; 11us; 45us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 13us; 16408us; 4us; 38us; 5us; 39us; 6us; 40us; 7us; 41us; 8us; 42us; 9us; 43us; 10us; 44us; 11us; 45us; 12us; 33us; 13us; 34us; 14us; 35us; 15us; 36us; 16us; 37us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 0us; 16403us; 0us; 16404us; 1us; 32768us; 27us; 49us; 2us; 32768us; 4us; 50us; 27us; 53us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 0us; 16405us; 1us; 32768us; 4us; 54us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 0us; 16406us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 0us; 16407us; 8us; 32768us; 0us; 59us; 2us; 57us; 13us; 12us; 20us; 6us; 22us; 48us; 26us; 63us; 27us; 47us; 28us; 62us; 0us; 16409us; 0us; 16410us; 0us; 16411us; 0us; 16412us; |]
let _fsyacc_actionTableRowOffsets = [|0us; 9us; 10us; 25us; 26us; 33us; 40us; 49us; 64us; 73us; 88us; 97us; 111us; 120us; 124us; 128us; 132us; 133us; 134us; 135us; 145us; 155us; 165us; 175us; 185us; 195us; 205us; 215us; 230us; 245us; 260us; 275us; 290us; 304us; 313us; 322us; 331us; 340us; 349us; 358us; 367us; 376us; 385us; 394us; 403us; 412us; 421us; 422us; 423us; 425us; 428us; 437us; 446us; 447us; 449us; 458us; 467us; 468us; 477us; 478us; 487us; 488us; 489us; 490us; |]
let _fsyacc_reductionSymbolCounts = [|1us; 2us; 1us; 1us; 6us; 2us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 1us; 1us; 7us; 8us; 3us; 2us; 2us; 2us; 1us; 1us; |]
let _fsyacc_productionToNonTerminalTable = [|0us; 1us; 2us; 2us; 2us; 2us; 2us; 2us; 2us; 2us; 2us; 2us; 2us; 2us; 2us; 2us; 2us; 2us; 2us; 3us; 3us; 3us; 3us; 3us; 3us; 4us; 4us; 5us; 5us; |]
let _fsyacc_immediateActions = [|65535us; 49152us; 65535us; 16385us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16403us; 16404us; 65535us; 65535us; 65535us; 65535us; 16405us; 65535us; 65535us; 65535us; 16406us; 65535us; 16407us; 65535us; 16409us; 16410us; 16411us; 16412us; |]
let _fsyacc_reductions ()  =    [| 
# 271 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
                      raise (FSharp.Text.Parsing.Accept(Microsoft.FSharp.Core.Operators.box _1))
                   )
                 : '_startMain));
# 280 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 36 "FunPar.fsy"
                                                               _1 
                   )
# 36 "FunPar.fsy"
                 : Absyn.expr));
# 291 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 40 "FunPar.fsy"
                                                               _1                     
                   )
# 40 "FunPar.fsy"
                 : Absyn.expr));
# 302 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 41 "FunPar.fsy"
                                                               _1                     
                   )
# 41 "FunPar.fsy"
                 : Absyn.expr));
# 313 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _6 = (let data = parseState.GetInput(6) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 42 "FunPar.fsy"
                                                               If(_2, _4, _6)         
                   )
# 42 "FunPar.fsy"
                 : Absyn.expr));
# 326 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 43 "FunPar.fsy"
                                                               Prim("-", CstI 0, _2)  
                   )
# 43 "FunPar.fsy"
                 : Absyn.expr));
# 337 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 44 "FunPar.fsy"
                                                               Prim("+",  _1, _3)     
                   )
# 44 "FunPar.fsy"
                 : Absyn.expr));
# 349 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 45 "FunPar.fsy"
                                                               Prim("-",  _1, _3)     
                   )
# 45 "FunPar.fsy"
                 : Absyn.expr));
# 361 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 46 "FunPar.fsy"
                                                               Prim("*",  _1, _3)     
                   )
# 46 "FunPar.fsy"
                 : Absyn.expr));
# 373 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 47 "FunPar.fsy"
                                                               Prim("/",  _1, _3)     
                   )
# 47 "FunPar.fsy"
                 : Absyn.expr));
# 385 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 48 "FunPar.fsy"
                                                               Prim("%",  _1, _3)     
                   )
# 48 "FunPar.fsy"
                 : Absyn.expr));
# 397 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 49 "FunPar.fsy"
                                                               Prim("=",  _1, _3)     
                   )
# 49 "FunPar.fsy"
                 : Absyn.expr));
# 409 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 50 "FunPar.fsy"
                                                               Prim("<>", _1, _3)     
                   )
# 50 "FunPar.fsy"
                 : Absyn.expr));
# 421 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 51 "FunPar.fsy"
                                                               Prim(">",  _1, _3)     
                   )
# 51 "FunPar.fsy"
                 : Absyn.expr));
# 433 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 52 "FunPar.fsy"
                                                               Prim("<",  _1, _3)     
                   )
# 52 "FunPar.fsy"
                 : Absyn.expr));
# 445 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 53 "FunPar.fsy"
                                                               Prim(">=", _1, _3)     
                   )
# 53 "FunPar.fsy"
                 : Absyn.expr));
# 457 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 54 "FunPar.fsy"
                                                               Prim("<=", _1, _3)     
                   )
# 54 "FunPar.fsy"
                 : Absyn.expr));
# 469 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 55 "FunPar.fsy"
                                                               Prim("|>", _1, _3)     
                   )
# 55 "FunPar.fsy"
                 : Absyn.expr));
# 481 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 56 "FunPar.fsy"
                                                               Prim(">>", _1, _3)     
                   )
# 56 "FunPar.fsy"
                 : Absyn.expr));
# 493 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 60 "FunPar.fsy"
                                                               _1                     
                   )
# 60 "FunPar.fsy"
                 : Absyn.expr));
# 504 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 61 "FunPar.fsy"
                                                               Var _1                 
                   )
# 61 "FunPar.fsy"
                 : Absyn.expr));
# 515 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _6 = (let data = parseState.GetInput(6) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 62 "FunPar.fsy"
                                                               Let(_2, _4, _6)        
                   )
# 62 "FunPar.fsy"
                 : Absyn.expr));
# 528 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 63 "FunPar.fsy"
                                                               Letfun(_2, _3, _5, _7) 
                   )
# 63 "FunPar.fsy"
                 : Absyn.expr));
# 542 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 64 "FunPar.fsy"
                                                               _2                     
                   )
# 64 "FunPar.fsy"
                 : Absyn.expr));
# 553 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 65 "FunPar.fsy"
                                                               Print(_2)              
                   )
# 65 "FunPar.fsy"
                 : Absyn.expr));
# 564 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 69 "FunPar.fsy"
                                                               Call(_1, _2)           
                   )
# 69 "FunPar.fsy"
                 : Absyn.expr));
# 576 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 70 "FunPar.fsy"
                                                               Call(_1, _2)           
                   )
# 70 "FunPar.fsy"
                 : Absyn.expr));
# 588 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : int)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 74 "FunPar.fsy"
                                                               CstI(_1)               
                   )
# 74 "FunPar.fsy"
                 : Absyn.expr));
# 599 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : bool)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 75 "FunPar.fsy"
                                                               CstB(_1)               
                   )
# 75 "FunPar.fsy"
                 : Absyn.expr));
|]
# 611 "FunPar.fs"
let tables () : FSharp.Text.Parsing.Tables<_> = 
  { reductions= _fsyacc_reductions ();
    endOfInputTag = _fsyacc_endOfInputTag;
    tagOfToken = tagOfToken;
    dataOfToken = _fsyacc_dataOfToken; 
    actionTableElements = _fsyacc_actionTableElements;
    actionTableRowOffsets = _fsyacc_actionTableRowOffsets;
    stateToProdIdxsTableElements = _fsyacc_stateToProdIdxsTableElements;
    stateToProdIdxsTableRowOffsets = _fsyacc_stateToProdIdxsTableRowOffsets;
    reductionSymbolCounts = _fsyacc_reductionSymbolCounts;
    immediateActions = _fsyacc_immediateActions;
    gotos = _fsyacc_gotos;
    sparseGotoTableRowOffsets = _fsyacc_sparseGotoTableRowOffsets;
    tagOfErrorTerminal = _fsyacc_tagOfErrorTerminal;
    parseError = (fun (ctxt:FSharp.Text.Parsing.ParseErrorContext<_>) -> 
                              match parse_error_rich with 
                              | Some f -> f ctxt
                              | None -> parse_error ctxt.Message);
    numTerminals = 32;
    productionToNonTerminalTable = _fsyacc_productionToNonTerminalTable  }
let engine lexer lexbuf startState = (tables ()).Interpret(lexer, lexbuf, startState)
let Main lexer lexbuf : Absyn.expr =
    Microsoft.FSharp.Core.Operators.unbox ((tables ()).Interpret(lexer, lexbuf, 0))
