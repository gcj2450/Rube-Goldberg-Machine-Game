using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBlades : MonoBehaviour {

    private int speed = -100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime * 2f);
		
	}
}
