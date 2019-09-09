public class Main {
    public static void main(String[] args) {

        Expr e = new Add(new CstI(17), new Var("z"));
        System.out.println(e);

        Expr e1 = new Sub( new CstI(84), new Add( new CstI(21), new CstI(21) ));

        Expr e2 = new Add( new Var("a"), new Sub( new Mul( new Var("b"), new Var("c")), new Var("d")));

        Expr e3 = new Mul(new Var("x"), new Add(new CstI(20), new Var("y")));

        System.out.println(e1);
        System.out.println(e2);
        System.out.println(e3);
    }
}
