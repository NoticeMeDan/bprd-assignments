# Exercise 7.1
    ```
    Prog
    [Fundec
       (None,"main",[(TypI, "n")],
        Block
          [Stmt
             (While
                (Prim2 (">",Access (AccVar "n"),CstI 0),
                 Block
                   [Stmt (Expr (Prim1 ("printi",Access (AccVar "n"))));
                    Stmt
                      (Expr
                         (Assign
                            (AccVar "n",Prim2 ("-",Access (AccVar "n"),CstI 1))))]));
           Stmt (Expr (Prim1 ("printc",CstI 10)))])]
    ```

    - Declarations:
	    Fundec 

    - Statements:
	    Block, Stmt, While, Expr

    - types:
	    TypI

	- expressions:
		Prim2, Access, Accvar, CstI, Prim1, Assign

# Exercise 7.2
    - /MicroC/ex7_2_i.c
    - /MicroC/ex7_2_ii.c
    - /MicroC/ex7_2_iii.c

# Exercise 7.3
    - /MicroC/CLex.fsl   ->   line: 31-32
    - /MicroC/CPar.fsy   ->   line: 17-18 and line: 104-105
    - /MicroC/ex7_3_i.c
    - /MicroC/ex7_3_ii.c
    - /MicroC/ex7_3_iii.c