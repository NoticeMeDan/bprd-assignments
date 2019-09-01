import java.util.Dictionary;

public class CstI extends Expr {

    public int i;

    public CstI(int i) { this.i = i; }

    public int getI() {
        return i;
    }

    @Override
    public int eval(Dictionary<String, Integer> env) { return this.i; }

    @Override
    public Expr simplify() { return this; }

    @Override
    public String toString() { return Integer.toString(this.i); }
}