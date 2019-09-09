import java.util.Dictionary;

public class Var extends Expr {

    public String x;

    public Var(String x) { this.x = x; }

    public String getX() {
        return x;
    }

    @Override
    public int eval(Dictionary<String, Integer> env) {
        try {
            return env.get(this.x);
        } catch (NullPointerException e) {
            System.out.println("No value has been defined for variable: " + this.x);
        }
        return 0;
    }

    @Override
    public Expr simplify() { return this; }

    @Override
    public String toString() { return this.x; }
}