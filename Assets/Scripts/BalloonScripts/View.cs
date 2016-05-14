using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class View : MonoBehaviour
{
    public Button button0;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;
    public Button button8;

    public GameObject balloonMain0;
    public GameObject balloonMain1;
    public GameObject balloonMain2;
    public GameObject balloonMain3;
    public GameObject balloonMain4;
    public GameObject balloonMain5;
    public GameObject balloonMain6;
    public GameObject balloonMain7;
    public GameObject balloonMain8;

    public List<Button> buttonList = new List<Button>();
    public List<GameObject> balloonMainList = new List<GameObject>();
    public List<Vector3> balloonPositionList = new List<Vector3>();

    public Text taskText;
    public Text playerScoreText;
    public Text playerNameText;
    public Text npcScoreText;

    public Color cWrong = Color.gray;
    public Color cRight = Color.white;

    public AudioSource balloonPoppingSound;

    public NavigationSystem navigationSystem;

    public View() { }

    void Awake()
    {
        //adding all buttons to the list
        buttonList.Add(button0);
        buttonList.Add(button1);
        buttonList.Add(button2);
        buttonList.Add(button3);
        buttonList.Add(button4);
        buttonList.Add(button5);
        buttonList.Add(button6);
        buttonList.Add(button7);
        buttonList.Add(button8);

        balloonMainList.Add(balloonMain0);
        balloonMainList.Add(balloonMain1);
        balloonMainList.Add(balloonMain2);
        balloonMainList.Add(balloonMain3);
        balloonMainList.Add(balloonMain4);
        balloonMainList.Add(balloonMain5);
        balloonMainList.Add(balloonMain6);
        balloonMainList.Add(balloonMain7);
        balloonMainList.Add(balloonMain8);

        //referencing GameObjects and UI elements

        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i] = GameObject.Find("Button" + i).GetComponent<Button>();
            balloonMainList[i] = GameObject.Find("BalloonMain" + i);
        }

        taskText = GameObject.Find("Task").GetComponent<Text>();
        playerNameText = GameObject.Find("Player").GetComponent<Text>();
        playerScoreText = GameObject.Find("PlayerScore").GetComponent<Text>();
        npcScoreText = GameObject.Find("NpcScore").GetComponent<Text>();

        balloonPoppingSound = GameObject.Find("Sound").GetComponent<AudioSource>();
    }

    public void setPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }

    public void setDisabledButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;  //copy
        cb.disabledColor = color;       //add change to the copy
        button.colors = cb;             //apply copy back
    }


    public void updateScoreDisplay(int newPoints, bool isPlayer)
    {
        if (isPlayer)
        {
            playerScoreText.text = "" + newPoints;
        }
        else
        {
            npcScoreText.text = "" + newPoints;
        }
    }   

    public void setButtonTexts(ArrayList options)
    {
        MathOption currentOption;
        for (int i = 0; i < options.Count; i++)
        {
            currentOption = ((MathOption)options[i]);
            buttonList[i].GetComponentInChildren<Text>().text = "" + currentOption.getAltSolution();
        }
    }

    public void setTaskText(string task)
    {
        taskText.text = task;
    }

    //sets all buttons interactable and its disabled color back to grey
    public void resetAllButtons()
    {
        ColorBlock cb;
        cb = buttonList[1].colors;

        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].interactable = true;
            balloonMainList[i].SetActive(true);
            GameObject.Find("Text" + i).GetComponent<Text>().enabled = true;

            cb.disabledColor = Color.grey;
            buttonList[i].colors = cb;

        }
    }

    public void disableAllButtons()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].interactable = false;
        }
    }

    public void makeBalloonMainInvisible(int i)
    {
        balloonMainList[i].SetActive(false);
        GameObject.Find("Text" + i).GetComponent<Text>().enabled = false;
    }

    public void makeBalloonMainVisible(int i)
    {
        balloonMainList[i].SetActive(true);
        GameObject.Find("Text" + i).GetComponent<Text>().enabled = true;
    }

    public void popBalloon(int i, Color color)
    {
        makeBalloonMainInvisible(i);
        ParticleSystem particles = GameObject.Find("ParticleSystem" + i).GetComponent<ParticleSystem>();
        particles.startColor = color;
        particles.Play();
        balloonPoppingSound.Play();
    }

    public void letBalloonFlyAway(int i)
    {
        GameObject button = GameObject.Find("Button" + i);
        Destroy(button.GetComponent("Floating"));
        button.AddComponent<FlyAway>();
    }

    public void popBalloonExceptIndex(int index, Color color)
    {
        for(int i=0; i < balloonMainList.Count; i++)
        {
            if (i != index)
            {
                popBalloon(i, color);
            }
        }
        
    }

    public void letBallonsFlyAwayExceptIndex(int index)
    {
        for (int i = 0; i < balloonMainList.Count; i++)
        {
            if (i != index)
            {
                letBalloonFlyAway(i);
            }
        }
    }

    public void resetAllBalloons()
    {
        for (int i = 0; i < balloonMainList.Count; i++)
        {
            GameObject button = GameObject.Find("Button" + i);
            Destroy(button.GetComponent("FlyAway"));
            button.transform.localPosition = new Vector3(0,0.05f,0);
            if (button.GetComponent("Floating") == null)
            {
                button.AddComponent<Floating>();
            }
        }
    }

}
