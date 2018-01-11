using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum eulerAngEnum
//{
//    front,
//    right,
//    back,
//    left,
//    none
//};

public class startUp : MonoBehaviour {
    private GameObject obj;

    private int[] eulerAngArr = { 170, 260, 350, 80 };
    private int[] endOfFilm = { 4000, 0, 0, 55000 };

    public int cacheAmount;
    private int step = 0;
    private int numberOfShots;

    //private bool step1Finished = false;
    private bool charlotteSeen = false;
    private bool step4Finished = false;
    private bool step5Finished = false;
    private bool step6Finished = false;
    private bool step7Finished = false;
    private bool lastStepStarted = false;

    private double yAngle = 0;

    private eulerAngEnum[] switchAngles = {
        eulerAngEnum.none,
        eulerAngEnum.front,
        eulerAngEnum.front,
        eulerAngEnum.front, //upsidedown
        eulerAngEnum.none, //timer donker
        eulerAngEnum.back, //upsidedown
        eulerAngEnum.none, //front
        eulerAngEnum.front //timer donker
    };
    
    void Start () {
        numberOfShots = switchAngles.Length + 1;
        makeSpheres();
	}
	
	void Update () {
        yAngle = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles.y;

        if (GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(0) == 8000)
        {
            if ((step + cacheAmount) < numberOfShots)
            {
                GameObject.Find("Steps").transform.GetChild(step + cacheAmount).gameObject.SetActive(true);
            }
            if (step < (numberOfShots - 1))
            {
                GameObject.Find("Steps").transform.GetChild(step).gameObject.SetActive(false);
                if (step < numberOfShots - 2)
                {
                    step++;
                    setFilm((byte)step, 0, true);
                }
            }
        }

        /*if (GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(0) >= 17240) //reverse video
        {
            step1Finished = true;
        }*/

        if (GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(3) >= 45000)
        {
            step4Finished = true;
        }

        if (GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(4) >= 100000)
        {
            if ((step + cacheAmount) < numberOfShots)
            {
                GameObject.Find("Steps").transform.GetChild(step + cacheAmount).gameObject.SetActive(true);
            }
            if (step < (numberOfShots - 1))
            {
                GameObject.Find("Steps").transform.GetChild(step).gameObject.SetActive(false);
                if (step < numberOfShots - 2)
                {
                    step++;
                    setFilm((byte)step, 5000, true);
                }
            }
        }
        
        if (GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(5) >= 65000)
        {
            step5Finished = true;
        }

        if (GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(6) >= 107000)
        {
            step6Finished = true;
        }

        if (GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(7) >= 50000)
        {
            if (!GameObject.Find("Steps").transform.GetChild(6).gameObject.activeSelf)
            {
                step7Finished = true;
            }
        }

        if (step == 2)
        {
            if (yAngle > (eulerAngArr[2] - 10) && yAngle < (eulerAngArr[2] + 10))
            {
                charlotteSeen = true;
            }
        }

        if (switchAngles[step] != eulerAngEnum.none)
        {
            Debug.Log(GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(1));
            /*if (step == 0)
            {
                if (step1Finished)
                {
                    step++;
                    setFilm((byte)step, 0, true);
                }
            }
            else */if(step == 2)
            {
                if(charlotteSeen)
                {
                    checkAngles();
                    setFilm(3, 18000, true);
                }
            }
            else if(step == 3)
            {
                if(step4Finished)
                {
                    checkAngles();
                }
            }
            else if(step == 5)
            {
                if (step5Finished)
                {
                    checkAngles();
                }
            }
            else if (step == 6)
            {
                if (step6Finished)
                {
                    //checkAngles();
                    step++;
                    setFilm((byte)step, 0, true);
                }
            }
            else if(step == 7)
            {
                if (step7Finished)
                {
                    checkAngles();
                }
            }
            else
            {
                checkAngles();
            }
        }

        //set begin point of films
        if(GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs((byte)step) == 0)
        {
            switch(step)
            {
                case 0: setFilm((byte)step, 101640, false); break;
                case 3: setFilm((byte)step, 18000, true); break;
                case 4: setFilm((byte)step, 18000, true); break;
            }
        }
    }

    private void makeSpheres()
    {
        for (int i = 1; i < numberOfShots + 1; i++)
        {
            obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.name = "shot" + i;
            obj.transform.localScale = new Vector3(100, 100, 100);
            obj.AddComponent<HPV_Node>();
            obj.GetComponent<HPV_Node>().filename = i + ".hpv";
            if (i == 5)
            {
                obj.transform.Rotate(new Vector3(180, 180, 0));
            }
            if (i == 7)
            {
                obj.transform.Rotate(new Vector3(180, 0, 0));
            }

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

    private void checkAngles()
    {
        if (yAngle > (eulerAngArr[(int)switchAngles[step]] - 10) && yAngle < (eulerAngArr[(int)switchAngles[step]] + 10))
        {
            if ((step + cacheAmount) < numberOfShots)
                GameObject.Find("Steps").transform.GetChild(step + cacheAmount).gameObject.SetActive(true);

            //if (step < (numberOfShots - 1))
            //{
                GameObject.Find("Steps").transform.GetChild(step).gameObject.SetActive(false);
                if (step < numberOfShots - 2)
                {
                    step++;
                    setFilm((byte)step, 0, true);
                }
                else if (!lastStepStarted)
                {
                    lastStepStarted = true;
                    setFilm((byte)(step+1), 0, true);
                }
            //}
        }
    }

    private void setFilm(byte node, int startPos, bool direction)
    {
        GameObject g = GameObject.Find("[CameraRig]");
        g.GetComponent<HPV_Manager>().seekToMs(node, startPos);
        g.GetComponent<HPV_Manager>().setDirection(node, direction);
    }
}
