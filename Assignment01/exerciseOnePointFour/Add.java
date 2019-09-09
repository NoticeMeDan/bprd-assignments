import java.util.Dictionary;

public class Add extends Binop {
    private final Expr e1, e2;

    public Add(Expr e1, Expr e2) {
        super(e1,e2);
        this.e1 = super.getE1();
        this.e2 = super.getE2();
    }

    @Override
    public Expr getE1() {
        return e1;
    }

    @Override
    public Expr getE2() {
        return e2;
    }

    @Override
    public int eval(Dictionary<String, Integer> env) { return e1.eval(env) + e2.eval(env); }

    @Override
    public Expr simplify() {
        CstI c1 = (CstI) e1;

        if (c1 != null && c1.getI() == 0) return e2.simplify();
        CstI c2 = (CstI) e2;

        if (c2 != null && c2.getI() == 0) return e1.simplify();
        return new Add(e1.simplify(), e2.simplify());
    }

    @Override
    public String toString() { return toString("+"); }
}