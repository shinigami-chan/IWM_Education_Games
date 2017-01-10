using UnityEngine;
using Random = System.Random;
using System;

public class Npc : MonoBehaviour
{
    private int playerID;
    private int points;
    private int meanResponseTime;

    Random rnd = new Random(Guid.NewGuid().GetHashCode());

    //correctness of the upcoming Npc answer (normal distributed)
    public bool isNpcAnswerCorrect()
    {
        float errorProbability = RandomFromDistribution.RandomNormalDistribution(0.2f, 0.05f);

        //old version: npc can be wrong
        //no error if errorProbability is negative
        //return rnd.Next(0,100)>errorProbability*100;

        //in this version the npc will always be correct
        return true;
    }

    public float getNpcResponseTime()
    {
        return RandomFromDistribution.RandomNormalDistribution(meanResponseTime,500);
    }

    public int getPoints()
    {
        return points;
    }

    public void setPoints(int receivedPoints)
    {
        points += receivedPoints;
    }

    public void setMeanResponseTime(int speedMode)
    {
        switch (speedMode)
        {
            case 3: meanResponseTime = 3000; break;
            case 2: meanResponseTime = 5000; break;
            case 1: meanResponseTime = 7000; break;
        }
    }

}
