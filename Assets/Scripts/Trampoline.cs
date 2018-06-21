using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {

    public float thrust;


        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Throwable"))
        {
            Debug.Log("The ball is in the Trampoline zone");
            other.GetComponent<Rigidbody>().AddForce(transform.forward * -thrust, ForceMode.Impulse);
        }
        
            
        
                
    } 

    }

