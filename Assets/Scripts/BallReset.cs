using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {


   public Material ruinedBall;
   public Material activeBall;
   public Vector3 startPos;
   public Rigidbody rb;
   public GameObject[] collectables;

    public GameObject goal;

    public bool ballGrab = false;
    public bool outSafeZone = false;
   
        

	void Start () {
        startPos = gameObject.transform.position;
        collectables = GameObject.FindGameObjectsWithTag("Collectable"); 
    }
	

	void Update () {

        if ((outSafeZone) && (ballGrab))
        {
            Debug.Log("Game is void");
            gameObject.GetComponent<Renderer>().material = ruinedBall;
            goal.layer = 9;

        } 
            
      
      

    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            Debug.Log("Leaving safezone");
            outSafeZone = true;
        }
    }

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Balls on the floor");
            goal.layer = 9;
            gameObject.tag = "Untagged";
            GameManager.score = 0;
            gameObject.GetComponent<Renderer>().material = ruinedBall;
            QuickReset();

        }
        else
        {
            if (col.gameObject.CompareTag("Collectable"))
            {
                Debug.Log("scored a point");
                GameManager.score++;
                col.gameObject.SetActive(false);
                Debug.Log("score is: " + GameManager.score);
            }
        }

    }

    IEnumerator Fail()
    {
        
        yield return new WaitForSeconds(1);
        Debug.Log("coroutine fired");
        gameObject.transform.position = startPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        gameObject.GetComponent<Renderer>().material = activeBall;
        foreach (GameObject star in collectables)
        {
            star.SetActive(true);

        }
        ballGrab = false;
        outSafeZone = false;
        goal.layer = 0;
        gameObject.tag = "Throwable";

    }

    void QuickReset()
    {
        
        StartCoroutine("Fail");

    }

}




//-------------Will need to make a collision detector to enable a collider which stops the ball from being taken off the platform 

