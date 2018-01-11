using UnityEngine;
using Valve.VR;

public class InputContr : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    private SteamVR_TrackedController contr;
    private HPV_Manager mgr;
    private GameObject[] arr = new GameObject[3];

    public GameObject sphere1, sphere2, sphere3, mainCamEye;

    void Start()
    {
        mgr = (HPV_Manager)mainCamEye.GetComponent("HPV_Manager");

        trackedObj = GetComponent<SteamVR_TrackedObject>();
        contr = GetComponent<SteamVR_TrackedController>();
        arr[0] = sphere1;
        arr[1] = sphere2;
        arr[2] = sphere3;
    }

    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = (device.GetAxis(EVRButtonId.k_EButton_Axis0));
            if (touchpad.y > 0.7f) //up
                setSphere(sphere2);

            else if (touchpad.y < -0.7f) { } //down

            if (touchpad.x > 0.7f) //right
                setSphere(sphere3);

            if (touchpad.x < -0.7f) //left
                setSphere(sphere1);
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) { } //release
    }

    void setSphere(GameObject obj)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            if(obj != arr[i])
                arr[i].SetActive(false);
            else
                arr[i].SetActive(true);
        }
    }
}
