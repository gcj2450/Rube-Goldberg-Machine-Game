using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControllerInputManager : MonoBehaviour
{

    //Teleporter
    private LineRenderer laser;
    public GameObject teleportAimerObject;
    public Vector3 teleportLocation;
    public GameObject player;
    public LayerMask laserMask;
    public float yNudgeAmount;  // specific to teleport aimer height

    //Vive stuff
    public SteamVR_TrackedObject trackedObj;
    public SteamVR_TrackedObject trackedObjRight;
    public SteamVR_Controller.Device deviceLeft;
    public SteamVR_Controller.Device deviceRight;
    public GameObject LeftController;
    public GameObject RightController;

    //Hand things
    public float throwForce = 1.5f;

    //Object menu things
    public ObjectMenuManager objectMenuManager;
    public GameObject objectmenu;
    public float touchPoint;
    public bool hasSwipedLeft;
    public bool hasSwipedRight;
    public bool hasJustSelected;




    void Start()
    {
        laser = LeftController.GetComponentInChildren<LineRenderer>();
        trackedObj = LeftController.GetComponent<SteamVR_TrackedObject>();
        trackedObjRight = RightController.GetComponent<SteamVR_TrackedObject>();
        objectmenu.SetActive(false);
    }


    void Update()
    {
        //---------------------------------------------------------------------------TELEPORTATION------------------------------------------------------------------------------------------------

        deviceLeft = SteamVR_Controller.Input((int)trackedObj.index);
        deviceRight = SteamVR_Controller.Input((int)trackedObjRight.index); 


        if (deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            laser.gameObject.SetActive(true);
            teleportAimerObject.SetActive(true);

            laser.SetPosition(0, LeftController.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(LeftController.transform.position, LeftController.transform.forward, out hit, 15, laserMask))
            {
                //Debug.Log("The ray can collide with something");
                teleportLocation = hit.point;
                //Debug.Log("hit.point = " + hit.point);
                laser.SetPosition(1, teleportLocation);
                // aimer position
                teleportAimerObject.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudgeAmount, teleportLocation.z);
            }
            else
            {


                // Debug.Log("using the height raycast method");
                teleportLocation = new Vector3(LeftController.transform.forward.x * 15 + LeftController.transform.position.x, LeftController.transform.forward.y * 15 + LeftController.transform.position.y, LeftController.transform.forward.z * 15 + LeftController.transform.position.z);
                RaycastHit groundRay;
                if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 17, laserMask))
                {
                    //Debug.Log("Lasermask Condition met");
                    teleportLocation = new Vector3(LeftController.transform.forward.x * 15 + LeftController.transform.position.x, groundRay.point.y, LeftController.transform.forward.z * 15 + LeftController.transform.position.z);
                }
                else
                { teleportLocation = player.transform.position; }

                laser.SetPosition(1, LeftController.transform.forward * 15 + LeftController.transform.position);
                // aimer position
                teleportAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmount, 0);
            }

        }
        if (deviceLeft.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            laser.gameObject.SetActive(false);
            teleportAimerObject.SetActive(false);
            player.transform.position = new Vector3(teleportLocation.x, player.transform.position.y, teleportLocation.z);

        }

        //-------------------------------------------------------------MENU----------------------------------------------

        if (deviceRight.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            
            objectmenu.SetActive(true);
            touchPoint = deviceRight.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;

            if (touchPoint > 0.7f)
            {
                if (!hasJustSelected)
                {
                    objectMenuManager.MenuRight();
                    hasJustSelected = true;
                   // StartCoroutine("MenuCoolDown");
                }
            }
            else if (touchPoint < -0.7f)
            {
                if (!hasJustSelected)
                {
                    objectMenuManager.MenuLeft();
                    hasJustSelected = true;
                    //StartCoroutine("MenuCoolDown");
                }
                   
            } 

            if(touchPoint > -0.1f && touchPoint < 0.1f)
            {
                hasJustSelected = false;
            }

            if (deviceRight.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                SpawnObject();
            }
        }

        if (deviceRight.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            
            objectmenu.SetActive(false);
        }
    }

    void SpawnObject()
    {
        objectMenuManager.SpawnCurrentObject();
    }

    IEnumerator MenuCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        hasJustSelected = false;
    }
}
   


