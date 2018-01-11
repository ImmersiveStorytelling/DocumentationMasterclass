using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eulerAngEnum
{
    front,
    right,
    back,
    left
};

public class startUp : MonoBehaviour {
    private GameObject obj;

    //public int numberOfSteps;
    public int[] eulerAngArr = { 350, 80, 170, 260 };
    public int cacheAmount = 2;
    public int delay = 10;
    private float prevTime;
    private int step = 0;
    private int numberOfSteps;
    public eulerAngEnum[] switchAngles = {
        eulerAngEnum.back,
        eulerAngEnum.back,
        eulerAngEnum.front,
        eulerAngEnum.left,
        eulerAngEnum.right,
        eulerAngEnum.left,
        eulerAngEnum.back,
        eulerAngEnum.left,
        eulerAngEnum.back,
        eulerAngEnum.back,
        eulerAngEnum.left,
        eulerAngEnum.back,
        eulerAngEnum.front,
        eulerAngEnum.back,
        eulerAngEnum.front,
        eulerAngEnum.back
    };

    // Use this for initialization
    void Start () {
        numberOfSteps = switchAngles.Length + 1;
        makeSpheres();
        prevTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time < prevTime + delay)
        {
            return;
        }
        Vector3 v = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles;
        double yAngle = v.y;
        if (yAngle > (eulerAngArr[(int)switchAngles[step]] - 10) && yAngle < (eulerAngArr[(int)switchAngles[step]] + 10))
        {
            if ((step + cacheAmount) < numberOfSteps)
            {
                GameObject.Find("Steps").transform.GetChild(step + cacheAmount).gameObject.SetActive(true);
            }


            Debug.Log(step);
            if (step < (numberOfSteps - 1))
            {
                prevTime = Time.time;
                GameObject.Find("Steps").transform.GetChild(step).gameObject.SetActive(false);
                if (step < numberOfSteps -2)
                {
                    step++;
                }
            }
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
            obj.GetComponent<HPV_Node>().filename = i + ".hpv";

            Renderer myRenderer = obj.GetComponent<MeshRenderer>();
            myRenderer.material.shader = Shader.Find("Custom/HPV/Spherical");

            if (i > 1)
            {
                if (i > 3)
                    obj.SetActive(false);
            }
                

            obj.transform.parent = GameObject.Find("Steps").transform;
        }
    }
}
