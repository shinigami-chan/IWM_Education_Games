public class MathProblem {
    private float term1;
    private float term2;
    private string operation;
    private float solution;


    public MathProblem() { }

    private MathProblem(float term1, float term2, string operation)
    {
        this.term1 = term1;
        this.term2 = term2;
        this.operation = operation;
        solution = evaluateMathProblem();
    }

    public static MathProblem createMultProblem(float term1, float term2)
    {
        return new MathProblem(term1, term2, "*");
    }

    public static MathProblem createSumProblem(float term1, float term2)
    {
        return new MathProblem(term1, term2, "+");
    }

    public static MathProblem createDivProblem(float term1, float term2)
    {
        return new MathProblem(term1, term2, "/");
    }

    public static MathProblem createSubMathProblem(float term1, float term2)
    {
        return new MathProblem(term1, term2, "-");
    }


    private float evaluateMathProblem()
    {
        switch (operation)
        {
            case "*": return term1 * term2;
            case "+": return term1 + term2;
            case "-": return term1 - term2;
            case "/": return term1 / term2;
            default: throw new System.Exception("invalid operation");
        }
    }

    protected string MathProblemToString()
    {
        return term1 + " " + operation + " " + term2;
    }

    public float getTerm1()
    {
        return term1;
    }

    public float getTerm2()
    {
        return term2;
    }

    public string getOperation()
    {
        return operation;
    }

    public float getSolution()
    {
        return solution;
    }

    public string stringProblemTask()
    {
        return term1 + operation + term2;
    }

    public void printProblemSolution()
    {
  
    }
}

