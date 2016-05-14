public class MathOption : Option
{
    private float altSolution;          //alternative solution, can be correct or incorrect
    private bool isCorrect = false;
    private bool isClicked = false;

    public MathOption(float altSolution)
    {
        this.altSolution = altSolution;
    }

    public MathOption(float altSolution, bool isCorrect)
    {
        this.altSolution = altSolution;
        this.isCorrect = isCorrect;
    }

    public void setIsClicked()
    {
        isClicked = true;
    }

    public bool getIsClicked()
    {
        return isClicked;
    }

    public float getAltSolution()
    {
        return altSolution;
    }

    public bool getIsCorrect()
    {
        return isCorrect;
    }
}
