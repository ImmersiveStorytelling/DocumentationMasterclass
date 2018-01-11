using UnityEngine;
using Valve.VR;

public class InputContr : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    private SteamVR_TrackedController contr;
    private HPV_Manager mgr;

    public GameObject sphere1, sphere2, sphere3, mainCamEye;

    private int numberOfFramesBeweeg = 0;

    void Start()
    {
        mgr = (HPV_Manager)mainCamEye.GetComponent("HPV_Manager");

        trackedObj = GetComponent<SteamVR_TrackedObject>();
        contr = GetComponent<SteamVR_TrackedController>();
    }

    void Update()
    {
        if(sphere2.activeInHierarchy)
        {
            mgr.setLoopState(1, 0);
            numberOfFramesBeweeg = mgr.getNumberOfFrames(1);
            if(numberOfFramesBeweeg == mgr.getCurrentFrameNum(1)+2)
            {
                sphere2.SetActive(false);
                mgr.seekToMs(1, 0);              
            }
        }
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = (device.GetAxis(EVRButtonId.k_EButton_Axis0));
            if (touchpad.y > 0.7f) //up
                sphere1.SetActive(false);

            else if (touchpad.y < -0.7f) { } //down

            if (touchpad.x > 0.7f) { } //right

            if (touchpad.x < -0.7f) //left
            {
                mgr.seekToMs(1, 0);
                sphere2.SetActive(true);
                sphere3.SetActive(true);
                sphere1.SetActive(true);
            }
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) { } //release
    }
}
