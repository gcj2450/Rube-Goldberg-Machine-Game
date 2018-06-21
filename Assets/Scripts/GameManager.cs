using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public static int score = 0;

    public int targetScore = 2;

    public static int currentLevel = 0;

    public int MaxPlank;
    public int MaxRamp;
    public int MaxFan;
    public int MaxTramp;

    public string[] levelNames = new string[4] { "Level1", "Level2", "Level3", "Level4" };


    public GameObject finish;
    public GameObject[] IntroItems;
    

    public BallReset ballscript;



    void Update()
    {
        if ((currentLevel == 0) && (ballscript.ballGrab == true))
        {
            foreach (GameObject item in IntroItems)
            {
                item.gameObject.SetActive(false);
            }
        } 

        if(currentLevel > 0)
        {
            foreach (GameObject item in IntroItems)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

        void Start(){

         //finish = GameObject.FindWithTag("Finish");
         IntroItems = GameObject.FindGameObjectsWithTag("Intro");
            

            if (currentLevel == 0)
        {
            MaxPlank = 2;
            MaxRamp = 1;
            MaxFan = 1;
            MaxTramp = 1;

        } else if (currentLevel == 1)
        {
            MaxPlank = 4;
            MaxRamp = 2;
            MaxFan = 1;
            MaxTramp = 2;
        }
        else if (currentLevel == 2)
        {
            MaxPlank = 7;
            MaxRamp = 2;
            MaxFan = 2;
            MaxTramp = 3;
        }
        else if (currentLevel == 3)
        {
            targetScore = 3;
            MaxPlank = 5;
            MaxRamp = 3;
            MaxFan = 4;
            MaxTramp = 4;
        }

    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Throwable"))
        {
            Debug.Log("collision successful");
            if (score == targetScore)
            {
                if (currentLevel == 3)
                {
                    Debug.Log("showing finish UI");
                    finish.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("here comes the next level");
                    StartCoroutine("EndLevelCoolDown");
                    currentLevel = (currentLevel + 1); // next level
                    SteamVR_LoadLevel.Begin(levelNames[currentLevel]);
                    score = 0;
                }
            }
            
        }
    }

    IEnumerator EndLevelCoolDown()
    {
       
        yield return new WaitForSeconds(2);
    }



}
