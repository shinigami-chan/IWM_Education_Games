using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = System.Random;
using System;
using UnityEngine.SceneManagement;

public class MultiplicationController : MonoBehaviour
{
    Random rnd = new Random(Guid.NewGuid().GetHashCode());

    private View view;
    private Game game;
    readonly float ResetTimeBetweenRounds = 2.5f;

    // Use this for initialization
    void Awake()
    {
        //initialize game (+ npc) and view (need to be added to an object -> using camera since it exists through the whole game)
        game = GameObject.Find("Main Camera").AddComponent<Game>();
        view = GameObject.Find("Main Camera").AddComponent<View>();
        game.setNpc(GameObject.Find("Main Camera").AddComponent<Npc>());

        game.getPlayer().setPlayerName(PlayerPrefs.GetString("PlayerName"));
        view.setPlayerName(game.getPlayer().getPlayerName());
        game.getNpc().setMeanResponseTime(PlayerPrefs.GetInt("SpeedMode"));
        //load first round
        loadNewRound();
    }

    public void loadNewRound()
    {
        //all rounds have been played
        if (game.loadCurrentQuest() == null)
        {
            prepareEndOfGame();
        }
        else
        {
            prepareAndStartNewRound();
        }
    }

    private void prepareEndOfGame()
    {
        game.getReactionData().creatingCsvFile("test_data.csv");
        PlayerPrefs.SetInt("PlayerPoints", game.getPlayer().getPoints());
        SceneManager.LoadScene("end_screen");
    }

    private void prepareAndStartNewRound()
    {
        view.resetAllButtons();
        view.resetAllBalloons();
        view.setTaskText(game.getCurrentQuest().getProblem().stringProblemTask());
        view.setButtonTexts(game.getCurrentQuest().getOptions());
        StartCoroutine(startNpcBehaviour());
        game.getReactionData().addTimeStampLoadedRound(DateTime.Now);
    }


    //update points in class Player or Npc and in view
    public void updatePoints(int recievedPoints, bool player)
    {
        if (player)
        {
            game.getPlayer().setPoints(recievedPoints);
            view.updateScoreDisplay(game.getPlayer().getPoints(),true);
        }
        else
        {
            game.getNpc().setPoints(recievedPoints);
            view.updateScoreDisplay(game.getNpc().getPoints(),false);
        }
    }

    /// <summary>
    /// Chooses a button depending on a correctness probability and sets it to not interactable.
    /// Reaction time is normal distributed and depends on the chosen difficulty mode.
    /// <para /> correct answer: all balloons except for the chosen one fly away
    /// <para /> incorrect answer: not used in this version
    /// </summary>
    public IEnumerator startNpcBehaviour()
    {
        //normal distributed response time
        yield return new WaitForSeconds(game.getNpc().getNpcResponseTime()/1000);

        //100% correct answers in this version
        //bool npcAnswersCorrect = game.getNpc().isNpcAnswerCorrect();

        int buttonIndex = rnd.Next(0, 8);
        while (view.buttonList[buttonIndex].interactable == false ||
            !((MathOption)game.getCurrentQuest().getOptions()[buttonIndex]).getIsCorrect())
        {
            buttonIndex = rnd.Next(0, 8);
        }
        
        view.buttonList[buttonIndex].interactable = false;

        if (((MathOption)game.getCurrentQuest().getOptions()[buttonIndex]).getIsCorrect())
            {
                correctAnswered(buttonIndex,false);
            }
        else
        {
            //incorrectAnswered(buttonIndex,false); //no incorrect answers in this Version
        }
    }

    /// <summary>
    /// Sets clicked button to not interactable and checks whether the answer is correct or not.
    /// <para /> correct answer: time stamp is saved and all balloons except for the clicked one pop
    /// <para /> incorrect answer: time stamp is saved and the clicked balloon pops
    /// </summary>
    public void triggerEventsAfterPlayersAnswer(Button button)
    {
        button.interactable = false;
        int buttonIndex = view.buttonList.IndexOf(button);
        MathOption clickedOption = (MathOption)game.getCurrentQuest().getOptions()[buttonIndex];
        clickedOption.setIsClicked();

        if (clickedOption.getIsCorrect())
        {
            correctAnswered(buttonIndex,true);
        }
        else
        {
            incorrectAnswered(buttonIndex,true);
        }
    }

    private void correctAnswered(int buttonIndex, bool isPlayer)
    {
        if (isPlayer)
        {
            StopAllCoroutines(); //prevents any intersection with Npc

            game.getReactionData().addTimeStampClickedRight(DateTime.Now);

            Debug.Log(game.getCurrentQuest().getProblem().stringProblemTask() + " = " + game.getCurrentQuest().getProblem().getSolution() + " correct answer given");
            view.setDisabledButtonColor(view.buttonList[buttonIndex], view.cRight);
            view.popBalloonExceptIndex(buttonIndex, Color.white);
        }
        else
        {
            view.letBallonsFlyAwayExceptIndex(buttonIndex);
            view.setDisabledButtonColor(view.buttonList[buttonIndex], view.cRight);
        }
        updatePoints(10, isPlayer);
        view.disableAllButtons();

        Invoke("loadNewRound", ResetTimeBetweenRounds);
    }

    private void incorrectAnswered(int buttonIndex, bool isPlayer)
    {
        if (isPlayer)
        {
            game.getReactionData().addTimeStampClickedWrong(DateTime.Now);
            Debug.Log(game.getCurrentQuest().getProblem().stringProblemTask() + " = " + game.getCurrentQuest().getProblem().getSolution() + " wrong answer given");

            view.popBalloon(buttonIndex, Color.white);
            view.setDisabledButtonColor(view.buttonList[buttonIndex], view.cWrong);
        }
        else
        {
            view.popBalloon(buttonIndex, new Color(230, 0, 255, 188));
            view.setDisabledButtonColor(view.buttonList[buttonIndex], view.cWrong);
            StartCoroutine(startNpcBehaviour());
        }
    }
}
