using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private ReactionData reactionData = new ReactionData();
    private Player player;
    private Npc npc;
    readonly int MaxRounds = 5;
    private int currentRound = 0;
    private MultiplicationQuest currentQuest;
    private ArrayList quests = new ArrayList();

    public Game()
    {           
        player = new Player();
        addQuests();
    }

    public void addQuests()
    {
        for (int i = 0; i < MaxRounds; i++)
        {
            MultiplicationQuest questToBeAdded = new MultiplicationQuest();
            float term1 = questToBeAdded.getProblem().getTerm1();
            float term2 = questToBeAdded.getProblem().getTerm2();
            while (doesQuestAlreadyExist(term1, term2, true))
            {
                questToBeAdded = new MultiplicationQuest();
                term1 = questToBeAdded.getProblem().getTerm1();
                term2 = questToBeAdded.getProblem().getTerm2();
            }
            quests.Add(questToBeAdded);
        }
    }

    public bool doesQuestAlreadyExist(float term1, float term2, bool isCommutative)
    {
        foreach(MultiplicationQuest quest in quests)
        {
            if (isCommutative && ((quest.getProblem().getTerm1() == term1) && (quest.getProblem().getTerm2() == term2))
                    || ((quest.getProblem().getTerm1() == term2) && (quest.getProblem().getTerm2() == term1)))
            {
                return true;
            }
            else if(!isCommutative && ((quest.getProblem().getTerm1() == term1) && (quest.getProblem().getTerm2() == term2)))
            {
                return true;
            }
        }
        return false;
    }

    public MultiplicationQuest loadCurrentQuest()
    {
        if (currentRound >= MaxRounds)
        {
            return null;
        }
        else
        {
            currentQuest = (MultiplicationQuest)quests[currentRound];
            currentRound++;
            return currentQuest;
        }
    }

    public int getMaxRounds()
    {
        return MaxRounds;
    }

    public Player getPlayer()
    {
        return player;
    }

    public Npc getNpc()
    {
        return npc;
    }

    public void setNpc(Npc npc)
    {
        this.npc = npc;
    }

    public ReactionData getReactionData()
    {
        return reactionData;
    }

    public MultiplicationQuest getCurrentQuest()
    {
        return currentQuest;
    }
}
