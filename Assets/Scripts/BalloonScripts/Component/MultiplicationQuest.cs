using System;
using System.Collections;
using Random = System.Random;


//TODO @ creatingOptions(): creating primes as solutions

public class MultiplicationQuest
{
    Random rnd = new Random(Guid.NewGuid().GetHashCode());

    private MathProblem problem; //mathematical problem, contains term and correct solution
    private ArrayList options; //contains solution, its validity and if the option is already clicked
    readonly int AmountOfOptions = 9;

    public MultiplicationQuest()
    {
        this.problem = createRndMultProblem();
        options = createOptions();
    }

    //creates a random multiplication problem
    private MathProblem createRndMultProblem()
    {
        int t1 = rnd.Next(2, 10);
        int t2 = rnd.Next(2, 10);
        return MathProblem.createMultProblem(t1, t2);
    }

    //creates options following this pattern:
    //1 correct option (co), co+10, co-10, co+1,co-1,
    //each 2 from same mult table as term1 and term2
    private ArrayList createOptions()
    {
        float solution = problem.getSolution();
        int t1 = (int)problem.getTerm1();
        int t2 = (int)problem.getTerm2();
        ArrayList incorrectSolutions = new ArrayList();
        ArrayList newOptions = new ArrayList();

        MathOption correctOption = new MathOption(solution, true);
        newOptions.Add(correctOption);

        //co+10
        incorrectSolutions.Add((int)solution + 10);

        //co-10
        if (((int)solution) - 10 > 0)
        {
            incorrectSolutions.Add((int)solution - 10);
        }
        //TODO: what happens here?
        else
        {
            incorrectSolutions.Add((int)solution + 7);
        }

        //co+1
        incorrectSolutions.Add((int)solution + 1);

        //co-1
        if (((int)solution) - 1 > 0)
        {
            incorrectSolutions.Add((int)solution - 1);
        }
        //TODO: what happens here? +2?
        else
        {
            incorrectSolutions.Add((int)solution + 2);
        }

        int incorrectSolution = (int)solution + 10;
        for (int i=0; i<2; i++)
        {
            while (incorrectSolutions.Contains(incorrectSolution) || incorrectSolution == solution)
            {
                incorrectSolution = t1 * rnd.Next(1,10);
            }
            incorrectSolutions.Add(incorrectSolution);
            while (incorrectSolutions.Contains(incorrectSolution) || incorrectSolution == solution)
            {
                incorrectSolution = t2 * rnd.Next(1, 10);
            }
            incorrectSolutions.Add(incorrectSolution);
        }

        for (int i=0; i < incorrectSolutions.Count; i++)
        {
            newOptions.Add(new MathOption((int)incorrectSolutions[i]));
        }

        //giving the correct answer a random index
        int index = rnd.Next(0, 8);
        MathOption transAnswer = (MathOption)newOptions[index];
        newOptions[0] = transAnswer;
        newOptions[index] = correctOption;

        return newOptions;
    }

    public ArrayList getOptions()
    {
        return options;
    }

    public MathProblem getProblem()
    {
        return problem;
    }

}
