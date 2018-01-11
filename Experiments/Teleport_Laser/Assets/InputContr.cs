using UnityEngine;
using System.Collections;
using Valve.VR;

public class InputContr : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    private SteamVR_TrackedController contr;

    public GameObject sphere1, sphere2, mainCamEye;

    HPV_Manager mgr;

    void Start()
    {
        Component c = sphere1.GetComponent("HPV_Node");
        HPV_Node node = (HPV_Node)c;

        mgr = (HPV_Manager)mainCamEye.GetComponent("HPV_Manager");
        mgr.pauseVideo(0);

        trackedObj = GetComponent<SteamVR_TrackedObject>();
        contr = GetComponent<SteamVR_TrackedController>();
    }

    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = (device.GetAxis(EVRButtonId.k_EButton_Axis0));
            if (touchpad.y > 0.7f)
            {
                Debug.Log("pressed up");
                mgr.setDirection(0,true);
                mgr.resumeVideo(0);
            }

            else if (touchpad.y < -0.7f)
            {
                Debug.Log("pressed down");
                mgr.setDirection(0, false);
                mgr.resumeVideo(0);
            }

            if (touchpad.x > 0.7f)
            {
                Debug.Log("pressed right");
            }

            if (touchpad.x < -0.7f)
            {
                Debug.Log("pressed left");
            }
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("press release");
            mgr.pauseVideo(0);
        }
    }
}
