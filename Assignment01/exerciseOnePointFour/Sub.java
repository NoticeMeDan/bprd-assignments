import java.util.Dictionary;

public class Sub extends Binop {
    private final Expr e1, e2;

    public Sub(Expr e1, Expr e2) {
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
    public int eval(Dictionary<String, Integer> env) { return e1.eval(env) - e2.eval(env); }

    @Override
    public Expr simplify() {

        if (e1.equals(e2)) return new CstI(0);

        CstI c2 = (CstI) e2;
        return c2 != null && c2.getI() == 0 ? e1.simplify() :
                new Sub(e1.simplify(), e2.simplify());
    }

    @Override
    public String toString() { return toString("-"); }
}
