using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eulerAngEnum
{
    front,
    right,
    back,
    left,
    none
};

public class startUp : MonoBehaviour {
    private GameObject obj;

    private int[] eulerAngArr = { 170, 260, 350, 80 };
    private int numberOfSteps;
    private eulerAngEnum[] switchAngles = {
        eulerAngEnum.back
    };

    // Use this for initialization
    void Start () {
        numberOfSteps = switchAngles.Length + 1;
        makeSpheres();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles;
        double yAngle = v.y;

        //Debug.Log("swAng + 10    " + (eulerAngArr[(int)switchAngles[0]] + 10));
        //Debug.Log("swAng - 10    " + (eulerAngArr[(int)switchAngles[0]] - 10));
        //Debug.Log("yAng    " + yAngle);

        //Debug.Log(GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(0));
        if (!(GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(0) >= 65200))
        {
            Debug.Log((GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(0)));
            return;
        }Debug.Log("losers");
        GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().pauseVideo(0);
        if (yAngle >= eulerAngArr[(int)switchAngles[0]] - 10 && yAngle <= eulerAngArr[(int)switchAngles[0]] + 10 && GameObject.Find("Steps").transform.GetChild(0).gameObject.activeSelf)
        {
            GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().resumeVideo(1);
            GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().setLoopState(1, 1);
            GameObject.Find("Steps").transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void makeSpheres()
    {
        for (int i = 1; i < numberOfSteps + 1; i++)
        {
            obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.name = "step" + i;
            obj.transform.localScale = new Vector3(100, 100, 100);
            obj.AddComponent<HPV_Node>();
            if(i == 1)
                obj.GetComponent<HPV_Node>().filename = "1.1.hpv";
            if(i==2)
                obj.GetComponent<HPV_Node>().filename = "v1.hpv";

            Renderer myRenderer = obj.GetComponent<MeshRenderer>();
            myRenderer.material.shader = Shader.Find("Custom/HPV/Spherical");
            GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().pauseVideo(1);
            GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().setSpeed((byte)(i-1), 1.1988);
            obj.transform.parent = GameObject.Find("Steps").transform;
        }
    }
}
