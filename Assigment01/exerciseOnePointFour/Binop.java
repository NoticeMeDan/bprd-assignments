public abstract class Binop extends Expr {
    public Expr e1;
    public Expr e2;

    protected Binop(Expr e1, Expr e2) {
        this.e1 = e1;
        this.e2 = e2;
    }

    public Expr getE1() {
        return e1;
    }

    public Expr getE2() {
        return e2;
    }

    protected String toString(String s) { return "( " + this.e1.toString() + " " + s + " " + this.e2.toString() + " )"; }
}