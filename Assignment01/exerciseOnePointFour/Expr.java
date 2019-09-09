import java.util.Dictionary;

public abstract class Expr {

    public abstract int eval(Dictionary<String, Integer> env);

    public abstract Expr simplify();

    public abstract String toString();
}