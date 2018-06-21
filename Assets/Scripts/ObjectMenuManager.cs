using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {

  

    public List<GameObject> objectList; // handled automatically at start
    public List<GameObject> objectPrefabList; //Set manually in inspector and MUST match order of scene menu objects

    public int currentObject = 0;

    // local count of instantiated objects
    private int SpawnedPlanks = 0;
    private int SpawnedRamps = 0;
    private int SpawnedFans = 0;
    private int SpawnedTramps = 0;

    //reference to Gamemanager.
    public GameManager gameManager;

	// Use this for initialization
	void Start () {
        // this populates the list
        foreach (Transform child in transform) 
        {
            objectList.Add(child.gameObject);
        }
		
	}
    public void SpawnCurrentObject()
    {
        if ((currentObject == 0) && (SpawnedPlanks < gameManager.MaxPlank))
        {
            Spawn();
            SpawnedPlanks++;
        }
        else if ((currentObject == 1) && (SpawnedRamps < gameManager.MaxRamp))
        {
            Spawn();
            SpawnedRamps++;
        }
        else if ((currentObject == 2) && (SpawnedFans < gameManager.MaxFan))
        {
            Spawn();
            SpawnedFans++;
        }
        else if ((currentObject == 3) && (SpawnedTramps < gameManager.MaxTramp))
        {
            Spawn();
            SpawnedTramps++;
        }
       
    }

	public void MenuLeft()
    {
        objectList[currentObject].SetActive(false);
        currentObject--;
        if (currentObject < 0)
        {
            currentObject = objectList.Count - 1;
        }
        objectList[currentObject].SetActive(true);
    }
    public void MenuRight()
    {
        objectList[currentObject].SetActive(false);
        currentObject++;
        if(currentObject > objectList.Count -1)
        {
            currentObject = 0;
        }
        objectList[currentObject].SetActive(true);
    }

    public void Spawn()
    {
        Instantiate(objectPrefabList[currentObject], transform.position, objectList[currentObject].transform.rotation);
    }

}
