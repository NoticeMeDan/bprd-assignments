## Exercise 8.1
Exercise 8.1 Download microc.zip from the book homepage, unpack it to a
folder MicroC, and build the micro-C compiler as explained in README.TXT
step (B).

### 8.1 (i)

- 8.1 (i) (a)
````
 compile "ex11";;
 val it : Machine.instr list =
  [LDARGS; CALL (1,"L1"); STOP; Label "L1"; INCSP 1; INCSP 1; INCSP 100; GETSP;
   CSTI 99; SUB; INCSP 100; GETSP; CSTI 99; SUB; INCSP 100; GETSP; CSTI 99;
   SUB; INCSP 100; GETSP; CSTI 99; SUB; GETBP; CSTI 2; ADD; CSTI 1; STI;
   INCSP -1; GOTO "L3"; Label "L2"; GETBP; CSTI 103; ADD; LDI; GETBP; CSTI 2;
   ADD; LDI; ADD; CSTI 0; STI; INCSP -1; GETBP; CSTI 2; ADD; GETBP; CSTI 2;
   ADD; LDI; CSTI 1; ADD; STI; INCSP -1; INCSP 0; Label "L3"; GETBP; CSTI 2;
   ADD; LDI; GETBP; CSTI 0; ADD; LDI; SWAP; LT; NOT; IFNZRO "L2"; GETBP;
   CSTI 2; ADD; CSTI 1; STI; INCSP -1; GOTO "L5"; Label "L4"; GETBP; CSTI 204;
   ADD; LDI; GETBP; CSTI 2; ADD; LDI; ADD; GETBP; CSTI 305; ADD; LDI; GETBP;
   CSTI 2; ADD; LDI; ADD; CSTI 0; STI; STI; INCSP -1; GETBP; CSTI 2; ADD; ...]
````

- 8.1 (i) (b)
````
  java Machine ex11.out 8
  ....
  7 5 3 1 6 8 2 4
  8 2 4 1 7 5 3 6
  8 2 5 3 1 7 4 6
  8 3 1 6 2 5 7 4
  8 4 1 3 6 2 7 5
  
  Ran 0.059 seconds
````
### 8.1 (ii)


## Exercise 8.3

## Exercise 8.4

## Exercise 8.5
Changes to `Absyn.fs`, `CLex.fsl`, `CPar.fsy` and `Comp.fs`.

## Exercise 8.6
Changes to `Absyn.fs`, `CLex.fsl`, `CPar.fsy` and `Comp.fs`.

The compilation scheme is really messy with tonnes of duplicates on the stack, but it should work.