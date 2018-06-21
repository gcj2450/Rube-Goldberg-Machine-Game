using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBehaviour : MonoBehaviour {

    public float thrust;


        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Throwable"))
            Debug.Log("The ball is in the fan zone");
            Rigidbody rb;

        if (rb=  other.GetComponent<Rigidbody>())
            rb.AddForce(transform.forward * -thrust, ForceMode.Acceleration);
    } 

    }

