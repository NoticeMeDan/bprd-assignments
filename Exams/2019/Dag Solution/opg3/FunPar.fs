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
  | LCBRACK
  | RCBRACK
  | DOT
  | SEMI
  | EOF
  | LPAR
  | RPAR
  | EQ
  | NE
  | GT
  | LT
  | GE
  | LE
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
    | TOKEN_LCBRACK
    | TOKEN_RCBRACK
    | TOKEN_DOT
    | TOKEN_SEMI
    | TOKEN_EOF
    | TOKEN_LPAR
    | TOKEN_RPAR
    | TOKEN_EQ
    | TOKEN_NE
    | TOKEN_GT
    | TOKEN_LT
    | TOKEN_GE
    | TOKEN_LE
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
    | NONTERM_RecordList
    | NONTERM_Main
    | NONTERM_Expr
    | NONTERM_AtExpr
    | NONTERM_AppExpr
    | NONTERM_Const

// This function maps tokens to integer indexes
let tagOfToken (t:token) = 
  match t with
  | LCBRACK  -> 0 
  | RCBRACK  -> 1 
  | DOT  -> 2 
  | SEMI  -> 3 
  | EOF  -> 4 
  | LPAR  -> 5 
  | RPAR  -> 6 
  | EQ  -> 7 
  | NE  -> 8 
  | GT  -> 9 
  | LT  -> 10 
  | GE  -> 11 
  | LE  -> 12 
  | PLUS  -> 13 
  | MINUS  -> 14 
  | TIMES  -> 15 
  | DIV  -> 16 
  | MOD  -> 17 
  | ELSE  -> 18 
  | END  -> 19 
  | FALSE  -> 20 
  | IF  -> 21 
  | IN  -> 22 
  | LET  -> 23 
  | NOT  -> 24 
  | THEN  -> 25 
  | TRUE  -> 26 
  | CSTBOOL _ -> 27 
  | NAME _ -> 28 
  | CSTINT _ -> 29 

// This function maps integer indexes to symbolic token ids
let tokenTagToTokenId (tokenIdx:int) = 
  match tokenIdx with
  | 0 -> TOKEN_LCBRACK 
  | 1 -> TOKEN_RCBRACK 
  | 2 -> TOKEN_DOT 
  | 3 -> TOKEN_SEMI 
  | 4 -> TOKEN_EOF 
  | 5 -> TOKEN_LPAR 
  | 6 -> TOKEN_RPAR 
  | 7 -> TOKEN_EQ 
  | 8 -> TOKEN_NE 
  | 9 -> TOKEN_GT 
  | 10 -> TOKEN_LT 
  | 11 -> TOKEN_GE 
  | 12 -> TOKEN_LE 
  | 13 -> TOKEN_PLUS 
  | 14 -> TOKEN_MINUS 
  | 15 -> TOKEN_TIMES 
  | 16 -> TOKEN_DIV 
  | 17 -> TOKEN_MOD 
  | 18 -> TOKEN_ELSE 
  | 19 -> TOKEN_END 
  | 20 -> TOKEN_FALSE 
  | 21 -> TOKEN_IF 
  | 22 -> TOKEN_IN 
  | 23 -> TOKEN_LET 
  | 24 -> TOKEN_NOT 
  | 25 -> TOKEN_THEN 
  | 26 -> TOKEN_TRUE 
  | 27 -> TOKEN_CSTBOOL 
  | 28 -> TOKEN_NAME 
  | 29 -> TOKEN_CSTINT 
  | 32 -> TOKEN_end_of_input
  | 30 -> TOKEN_error
  | _ -> failwith "tokenTagToTokenId: bad token"

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
let prodIdxToNonTerminal (prodIdx:int) = 
  match prodIdx with
    | 0 -> NONTERM__startMain 
    | 1 -> NONTERM_RecordList 
    | 2 -> NONTERM_RecordList 
    | 3 -> NONTERM_Main 
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
    | 19 -> NONTERM_Expr 
    | 20 -> NONTERM_AtExpr 
    | 21 -> NONTERM_AtExpr 
    | 22 -> NONTERM_AtExpr 
    | 23 -> NONTERM_AtExpr 
    | 24 -> NONTERM_AtExpr 
    | 25 -> NONTERM_AtExpr 
    | 26 -> NONTERM_AtExpr 
    | 27 -> NONTERM_AppExpr 
    | 28 -> NONTERM_AppExpr 
    | 29 -> NONTERM_Const 
    | 30 -> NONTERM_Const 
    | _ -> failwith "prodIdxToNonTerminal: bad production index"

let _fsyacc_endOfInputTag = 32 
let _fsyacc_tagOfErrorTerminal = 30

// This function gets the name of a token as a string
let token_to_string (t:token) = 
  match t with 
  | LCBRACK  -> "LCBRACK" 
  | RCBRACK  -> "RCBRACK" 
  | DOT  -> "DOT" 
  | SEMI  -> "SEMI" 
  | EOF  -> "EOF" 
  | LPAR  -> "LPAR" 
  | RPAR  -> "RPAR" 
  | EQ  -> "EQ" 
  | NE  -> "NE" 
  | GT  -> "GT" 
  | LT  -> "LT" 
  | GE  -> "GE" 
  | LE  -> "LE" 
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
  | LCBRACK  -> (null : System.Object) 
  | RCBRACK  -> (null : System.Object) 
  | DOT  -> (null : System.Object) 
  | SEMI  -> (null : System.Object) 
  | EOF  -> (null : System.Object) 
  | LPAR  -> (null : System.Object) 
  | RPAR  -> (null : System.Object) 
  | EQ  -> (null : System.Object) 
  | NE  -> (null : System.Object) 
  | GT  -> (null : System.Object) 
  | LT  -> (null : System.Object) 
  | GE  -> (null : System.Object) 
  | LE  -> (null : System.Object) 
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
let _fsyacc_gotos = [| 0us; 65535us; 2us; 65535us; 5us; 6us; 57us; 61us; 1us; 65535us; 0us; 1us; 24us; 65535us; 0us; 7us; 3us; 4us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 37us; 19us; 38us; 20us; 39us; 21us; 40us; 22us; 41us; 23us; 42us; 24us; 43us; 25us; 44us; 26us; 45us; 27us; 46us; 28us; 47us; 29us; 54us; 30us; 55us; 31us; 59us; 32us; 63us; 33us; 66us; 34us; 67us; 35us; 69us; 36us; 26us; 65535us; 0us; 9us; 3us; 9us; 9us; 71us; 10us; 72us; 11us; 9us; 13us; 9us; 15us; 9us; 17us; 9us; 37us; 9us; 38us; 9us; 39us; 9us; 40us; 9us; 41us; 9us; 42us; 9us; 43us; 9us; 44us; 9us; 45us; 9us; 46us; 9us; 47us; 9us; 54us; 9us; 55us; 9us; 59us; 9us; 63us; 9us; 66us; 9us; 67us; 9us; 69us; 9us; 24us; 65535us; 0us; 10us; 3us; 10us; 11us; 10us; 13us; 10us; 15us; 10us; 17us; 10us; 37us; 10us; 38us; 10us; 39us; 10us; 40us; 10us; 41us; 10us; 42us; 10us; 43us; 10us; 44us; 10us; 45us; 10us; 46us; 10us; 47us; 10us; 54us; 10us; 55us; 10us; 59us; 10us; 63us; 10us; 66us; 10us; 67us; 10us; 69us; 10us; 26us; 65535us; 0us; 50us; 3us; 50us; 9us; 50us; 10us; 50us; 11us; 50us; 13us; 50us; 15us; 50us; 17us; 50us; 37us; 50us; 38us; 50us; 39us; 50us; 40us; 50us; 41us; 50us; 42us; 50us; 43us; 50us; 44us; 50us; 45us; 50us; 46us; 50us; 47us; 50us; 54us; 50us; 55us; 50us; 59us; 50us; 63us; 50us; 66us; 50us; 67us; 50us; 69us; 50us; |]
let _fsyacc_sparseGotoTableRowOffsets = [|0us; 1us; 4us; 6us; 31us; 58us; 83us; |]
let _fsyacc_stateToProdIdxsTableElements = [| 1us; 0us; 1us; 0us; 2us; 1us; 2us; 2us; 1us; 2us; 14us; 1us; 2us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 1us; 2us; 1us; 2us; 13us; 3us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 1us; 3us; 2us; 4us; 27us; 2us; 5us; 28us; 1us; 6us; 13us; 6us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 1us; 6us; 13us; 6us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 1us; 6us; 13us; 6us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 1us; 7us; 13us; 7us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 13us; 8us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 13us; 8us; 9us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 13us; 8us; 9us; 10us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 13us; 8us; 9us; 10us; 11us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 13us; 8us; 9us; 10us; 11us; 12us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 14us; 15us; 16us; 17us; 18us; 19us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 15us; 16us; 17us; 18us; 19us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 16us; 17us; 18us; 19us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 17us; 18us; 19us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 18us; 19us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 22us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 22us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 23us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 24us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 25us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 25us; 13us; 8us; 9us; 10us; 11us; 12us; 13us; 14us; 15us; 16us; 17us; 18us; 19us; 26us; 1us; 8us; 1us; 9us; 1us; 10us; 1us; 11us; 1us; 12us; 1us; 13us; 1us; 14us; 1us; 15us; 1us; 16us; 1us; 17us; 1us; 18us; 1us; 19us; 1us; 19us; 1us; 20us; 1us; 21us; 4us; 22us; 23us; 24us; 25us; 4us; 22us; 23us; 24us; 25us; 3us; 22us; 23us; 24us; 1us; 22us; 1us; 22us; 2us; 23us; 24us; 1us; 23us; 1us; 23us; 1us; 23us; 1us; 24us; 1us; 24us; 1us; 24us; 1us; 24us; 1us; 25us; 1us; 25us; 1us; 25us; 1us; 25us; 1us; 26us; 1us; 26us; 1us; 27us; 1us; 28us; 1us; 29us; 1us; 30us; |]
let _fsyacc_stateToProdIdxsTableRowOffsets = [|0us; 2us; 4us; 7us; 10us; 25us; 27us; 29us; 43us; 45us; 48us; 51us; 53us; 67us; 69us; 83us; 85us; 99us; 101us; 115us; 129us; 143us; 157us; 171us; 185us; 199us; 213us; 227us; 241us; 255us; 269us; 283us; 297us; 311us; 325us; 339us; 353us; 367us; 369us; 371us; 373us; 375us; 377us; 379us; 381us; 383us; 385us; 387us; 389us; 391us; 393us; 395us; 397us; 402us; 407us; 411us; 413us; 415us; 418us; 420us; 422us; 424us; 426us; 428us; 430us; 432us; 434us; 436us; 438us; 440us; 442us; 444us; 446us; 448us; 450us; |]
let _fsyacc_action_rows = 75
let _fsyacc_actionTableElements = [|7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 0us; 49152us; 1us; 32768us; 7us; 3us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 13us; 16385us; 2us; 48us; 3us; 5us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 1us; 32768us; 28us; 2us; 0us; 16386us; 13us; 32768us; 2us; 48us; 4us; 8us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 0us; 16387us; 5us; 16388us; 5us; 69us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 5us; 16389us; 5us; 69us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 13us; 32768us; 2us; 48us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 25us; 13us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 13us; 32768us; 2us; 48us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 18us; 15us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 12us; 16390us; 2us; 48us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 4us; 16391us; 2us; 48us; 15us; 39us; 16us; 40us; 17us; 41us; 4us; 16392us; 2us; 48us; 15us; 39us; 16us; 40us; 17us; 41us; 4us; 16393us; 2us; 48us; 15us; 39us; 16us; 40us; 17us; 41us; 1us; 16394us; 2us; 48us; 1us; 16395us; 2us; 48us; 1us; 16396us; 2us; 48us; 10us; 16397us; 2us; 48us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 10us; 16398us; 2us; 48us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 10us; 16399us; 2us; 48us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 10us; 16400us; 2us; 48us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 10us; 16401us; 2us; 48us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 10us; 16402us; 2us; 48us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 13us; 32768us; 2us; 48us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 22us; 55us; 13us; 32768us; 2us; 48us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 19us; 56us; 13us; 32768us; 2us; 48us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 19us; 60us; 13us; 32768us; 2us; 48us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 19us; 64us; 13us; 32768us; 2us; 48us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 22us; 67us; 13us; 32768us; 2us; 48us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 19us; 68us; 13us; 32768us; 2us; 48us; 6us; 70us; 7us; 42us; 8us; 43us; 9us; 44us; 10us; 45us; 11us; 46us; 12us; 47us; 13us; 37us; 14us; 38us; 15us; 39us; 16us; 40us; 17us; 41us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 1us; 32768us; 28us; 49us; 0us; 16403us; 0us; 16404us; 0us; 16405us; 1us; 32768us; 28us; 53us; 2us; 32768us; 7us; 54us; 28us; 65us; 8us; 32768us; 0us; 57us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 0us; 16406us; 2us; 32768us; 1us; 58us; 28us; 2us; 1us; 32768us; 22us; 59us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 0us; 16407us; 1us; 32768us; 1us; 62us; 1us; 32768us; 22us; 63us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 0us; 16408us; 1us; 32768us; 7us; 66us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 0us; 16409us; 7us; 32768us; 5us; 69us; 14us; 17us; 21us; 11us; 23us; 52us; 27us; 74us; 28us; 51us; 29us; 73us; 0us; 16410us; 0us; 16411us; 0us; 16412us; 0us; 16413us; 0us; 16414us; |]
let _fsyacc_actionTableRowOffsets = [|0us; 8us; 9us; 11us; 19us; 33us; 35us; 36us; 50us; 51us; 57us; 63us; 71us; 85us; 93us; 107us; 115us; 128us; 136us; 141us; 146us; 151us; 153us; 155us; 157us; 168us; 179us; 190us; 201us; 212us; 223us; 237us; 251us; 265us; 279us; 293us; 307us; 321us; 329us; 337us; 345us; 353us; 361us; 369us; 377us; 385us; 393us; 401us; 409us; 411us; 412us; 413us; 414us; 416us; 419us; 428us; 436us; 437us; 440us; 442us; 450us; 451us; 453us; 455us; 463us; 464us; 466us; 474us; 482us; 483us; 491us; 492us; 493us; 494us; 495us; |]
let _fsyacc_reductionSymbolCounts = [|1us; 3us; 5us; 2us; 1us; 1us; 6us; 2us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 1us; 1us; 7us; 8us; 9us; 8us; 3us; 2us; 2us; 1us; 1us; |]
let _fsyacc_productionToNonTerminalTable = [|0us; 1us; 1us; 2us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 4us; 4us; 4us; 4us; 4us; 4us; 4us; 5us; 5us; 6us; 6us; |]
let _fsyacc_immediateActions = [|65535us; 49152us; 65535us; 65535us; 65535us; 65535us; 16386us; 65535us; 16387us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16403us; 16404us; 16405us; 65535us; 65535us; 65535us; 65535us; 16406us; 65535us; 65535us; 65535us; 16407us; 65535us; 65535us; 65535us; 16408us; 65535us; 65535us; 65535us; 16409us; 65535us; 16410us; 16411us; 16412us; 16413us; 16414us; |]
let _fsyacc_reductions ()  =    [| 
# 280 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
                      raise (FSharp.Text.Parsing.Accept(Microsoft.FSharp.Core.Operators.box _1))
                   )
                 : '_startMain));
# 289 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 36 "FunPar.fsy"
                                                               [(_1, _3)]    
                   )
# 36 "FunPar.fsy"
                 : 'RecordList));
# 301 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : 'RecordList)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 37 "FunPar.fsy"
                                                               (_1, _3)::_5  
                   )
# 37 "FunPar.fsy"
                 : 'RecordList));
# 314 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 42 "FunPar.fsy"
                                                               _1 
                   )
# 42 "FunPar.fsy"
                 : Absyn.expr));
# 325 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 46 "FunPar.fsy"
                                                               _1                     
                   )
# 46 "FunPar.fsy"
                 : Absyn.expr));
# 336 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 47 "FunPar.fsy"
                                                               _1                     
                   )
# 47 "FunPar.fsy"
                 : Absyn.expr));
# 347 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _6 = (let data = parseState.GetInput(6) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 48 "FunPar.fsy"
                                                               If(_2, _4, _6)         
                   )
# 48 "FunPar.fsy"
                 : Absyn.expr));
# 360 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 49 "FunPar.fsy"
                                                               Prim("-", CstI 0, _2)  
                   )
# 49 "FunPar.fsy"
                 : Absyn.expr));
# 371 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 50 "FunPar.fsy"
                                                               Prim("+",  _1, _3)     
                   )
# 50 "FunPar.fsy"
                 : Absyn.expr));
# 383 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 51 "FunPar.fsy"
                                                               Prim("-",  _1, _3)     
                   )
# 51 "FunPar.fsy"
                 : Absyn.expr));
# 395 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 52 "FunPar.fsy"
                                                               Prim("*",  _1, _3)     
                   )
# 52 "FunPar.fsy"
                 : Absyn.expr));
# 407 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 53 "FunPar.fsy"
                                                               Prim("/",  _1, _3)     
                   )
# 53 "FunPar.fsy"
                 : Absyn.expr));
# 419 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 54 "FunPar.fsy"
                                                               Prim("%",  _1, _3)     
                   )
# 54 "FunPar.fsy"
                 : Absyn.expr));
# 431 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 55 "FunPar.fsy"
                                                               Prim("=",  _1, _3)     
                   )
# 55 "FunPar.fsy"
                 : Absyn.expr));
# 443 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 56 "FunPar.fsy"
                                                               Prim("<>", _1, _3)     
                   )
# 56 "FunPar.fsy"
                 : Absyn.expr));
# 455 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 57 "FunPar.fsy"
                                                               Prim(">",  _1, _3)     
                   )
# 57 "FunPar.fsy"
                 : Absyn.expr));
# 467 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 58 "FunPar.fsy"
                                                               Prim("<",  _1, _3)     
                   )
# 58 "FunPar.fsy"
                 : Absyn.expr));
# 479 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 59 "FunPar.fsy"
                                                               Prim(">=", _1, _3)     
                   )
# 59 "FunPar.fsy"
                 : Absyn.expr));
# 491 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 60 "FunPar.fsy"
                                                               Prim("<=", _1, _3)     
                   )
# 60 "FunPar.fsy"
                 : Absyn.expr));
# 503 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 61 "FunPar.fsy"
                                                               Field(_1, _3)          
                   )
# 61 "FunPar.fsy"
                 : Absyn.expr));
# 515 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 65 "FunPar.fsy"
                                                                             _1                     
                   )
# 65 "FunPar.fsy"
                 : Absyn.expr));
# 526 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 66 "FunPar.fsy"
                                                                             Var _1                 
                   )
# 66 "FunPar.fsy"
                 : Absyn.expr));
# 537 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _6 = (let data = parseState.GetInput(6) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 67 "FunPar.fsy"
                                                                             Let(_2, _4, _6)        
                   )
# 67 "FunPar.fsy"
                 : Absyn.expr));
# 550 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 68 "FunPar.fsy"
                                                                             Let(_2, Record([]), _7) 
                   )
# 68 "FunPar.fsy"
                 : Absyn.expr));
# 562 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : 'RecordList)) in
            let _8 = (let data = parseState.GetInput(8) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 69 "FunPar.fsy"
                                                                               Let(_2, Record(_5), _8) 
                   )
# 69 "FunPar.fsy"
                 : Absyn.expr));
# 575 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 70 "FunPar.fsy"
                                                                             Letfun(_2, _3, _5, _7) 
                   )
# 70 "FunPar.fsy"
                 : Absyn.expr));
# 589 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 71 "FunPar.fsy"
                                                                             _2                     
                   )
# 71 "FunPar.fsy"
                 : Absyn.expr));
# 600 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 75 "FunPar.fsy"
                                                               Call(_1, _2)           
                   )
# 75 "FunPar.fsy"
                 : Absyn.expr));
# 612 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : Absyn.expr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 76 "FunPar.fsy"
                                                               Call(_1, _2)           
                   )
# 76 "FunPar.fsy"
                 : Absyn.expr));
# 624 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : int)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 80 "FunPar.fsy"
                                                               CstI(_1)               
                   )
# 80 "FunPar.fsy"
                 : Absyn.expr));
# 635 "FunPar.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : bool)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 81 "FunPar.fsy"
                                                               CstB(_1)               
                   )
# 81 "FunPar.fsy"
                 : Absyn.expr));
|]
# 647 "FunPar.fs"
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
    numTerminals = 33;
    productionToNonTerminalTable = _fsyacc_productionToNonTerminalTable  }
let engine lexer lexbuf startState = (tables ()).Interpret(lexer, lexbuf, startState)
let Main lexer lexbuf : Absyn.expr =
    Microsoft.FSharp.Core.Operators.unbox ((tables ()).Interpret(lexer, lexbuf, 0))