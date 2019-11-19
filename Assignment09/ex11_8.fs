open Icon;;

(*i*)
run (Every(Write(Prim("+",CstI 1,Prim("*",CstI 2,FromTo(1, 4))))));;
run (Every(Write(Prim("+",Prim("*",CstI 10, FromTo(2,4)),FromTo(1,2)))));;

(* ii *)
run (Write(Prim("<", CstI 50, Prim("*", CstI 7, FromTo(1,10)))));;
