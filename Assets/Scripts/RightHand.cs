using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RightHand : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    public float throwForce = 1.5f;

    public BallReset ballReset;

    public GameObject RightToolTipsOculus;
    public GameObject RightToolTipsVive;

    public GameObject toolTips;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        if (XRDevice.model.Contains("ive"))
        {
            toolTips = RightToolTipsVive; 
        }
        else if (XRDevice.model.Contains("culus"))
        {
            toolTips = RightToolTipsOculus;
        }
    }
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {

            toolTips.SetActive(true);

        }
        else if (device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            toolTips.SetActive(false);
        }
    }

    //---------------------------------------------------------------------------------------------RIGHT-GRABBING--------------------------------------------------------
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Throwable") || col.gameObject.CompareTag("Moveable"))
            //Debug.Log("I can pick this up");
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    GrabObject(col);
                if (col.gameObject.CompareTag("Throwable"))
                {
                    ballReset.ballGrab = true;
                }
                
                }

             else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                DropObject(col);
                if (col.gameObject.CompareTag("Throwable"))
                {
                    ballReset.ballGrab = false;
                }
            }
            
        }
    }

    void GrabObject(Collider coli)
    {
        coli.transform.SetParent(gameObject.transform);
        coli.GetComponent<Rigidbody>().isKinematic = true;
        //device.TriggerHapticPulse(2000);
        
    }

    void DropObject(Collider coli)
    {

        if (coli.gameObject.CompareTag("Throwable"))
        {

            coli.transform.SetParent(null);
            Rigidbody rigidbody = coli.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.velocity = device.velocity * throwForce;
            rigidbody.angularVelocity = device.angularVelocity;

        }
        else
        {
            coli.transform.SetParent(null);
        }

        
    }


    //---------------------------------------------------RIGHT MENU---------------------------------------------------------------------------

}

