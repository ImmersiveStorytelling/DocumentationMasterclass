using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum SwitchType
{
    angle,
    time,
    both
}

public enum eulerAngEnum
{
    front,
    right,
    back,
    left,
    none
};


public class jsonTestScript : MonoBehaviour
{
    public int[] eulerAngArr = { 170, 260, 350, 80 };


    [Serializable]
    public class Shot
    {
        public string Filename;
        public SwitchType Switch;
        public bool Reverse;
        public int Startpoint;
        public eulerAngEnum[] SwitchAngles;
        public int SwitchTime;
        public int[] Orientation;
    }


    public List<Shot> ShotList;
    private int cacheAmount = 2;
    private bool lastStepStarted = false;
    private int numberOfShots;
    private HPV_Manager manager;
    private int step = 0;
    private bool shotStarted = false;
    private int anglesHit = 0;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>();
        makeSpheres(ShotList);
        numberOfShots = ShotList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        bool shotReady = false;
        Shot currentShot = ShotList[step];
        if (!shotStarted)
        {
            shotStarted = true;
            manager.resumeVideo((byte)step);
            setFilm((byte)step, currentShot.Startpoint, !currentShot.Reverse);
        }

        switch (currentShot.Switch)
        {
            case SwitchType.angle:
                shotReady = checkAngles(currentShot);
                break;
            case SwitchType.time:
                shotReady = checkTime(currentShot);
                break;
            case SwitchType.both:
                if (checkTime(currentShot))
                {
                    shotReady = checkAngles(currentShot);
                }
                break;
            default:
                break;
        }

        if (shotReady)
        {
            if ((step + cacheAmount) < numberOfShots)
                GameObject.Find("Steps").transform.GetChild(step + cacheAmount).gameObject.SetActive(true);

            GameObject.Find("Steps").transform.GetChild(step).gameObject.SetActive(false);
            shotStarted = false;
            if (step < numberOfShots - 2)
            {
                manager.pauseVideo((byte)step);
                step++;
            }
            else if (!lastStepStarted)
            {
                lastStepStarted = true;
            }
        }
    }


    private bool checkTime(Shot shot)
    {
        if (step ==6)
        {
            Debug.Log(manager.getPositionMs((byte)step));
        }
        if (shot.Reverse)
        {
            if (manager.getPositionMs((byte)step)<=shot.SwitchTime)
            {
                manager.pauseVideo((byte)step);
                return true;
            }
        }
        else
        {
            if (manager.getPositionMs((byte)step) >= shot.SwitchTime)
            {
                manager.pauseVideo((byte)step);
                return true;
            }
        }
        return false;
    }


    private bool checkAngles(Shot shot)
    {
        float yAngle = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles.y;
        eulerAngEnum[] toHitAngles = shot.SwitchAngles;
        
        if ((yAngle > (eulerAngArr[(int)toHitAngles[anglesHit]] - 10) && yAngle < (eulerAngArr[(int)toHitAngles[anglesHit]] + 10)))
        {
            anglesHit++;
        }
        if (anglesHit >= toHitAngles.Length)
        {
            anglesHit = 0;
            return true;
        }
        else
        {
            return false;
        }
    }



    private void makeSpheres(List<Shot> shotList)
    {
        for (int i = 0; i < shotList.Count; i++)
        {
            GameObject obj;
            obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.name = "shot" + i;
            obj.transform.localScale = new Vector3(100, 100, 100);
            obj.AddComponent<HPV_Node>();
            obj.GetComponent<HPV_Node>().filename = shotList[i].Filename;
            manager.setLoopState((byte)step, 0);
            manager.pauseVideo((byte)i);
            manager.setSpeed((byte)i, 1.1988);
            if (shotList[i].Orientation.Length == 3)
            {
                obj.transform.Rotate(new Vector3(shotList[i].Orientation[0], shotList[i].Orientation[1], shotList[i].Orientation[2]));
            }

            Renderer myRenderer = obj.GetComponent<MeshRenderer>();
            myRenderer.material.shader = Shader.Find("Custom/HPV/Spherical");

            if (i > 1)
            {
                if (i > 1)
                    obj.SetActive(false);
            }
            obj.transform.parent = GameObject.Find("Steps").transform;
        }
    }

    private void setFilm(byte node, int startPos, bool direction)
    {
        GameObject g = GameObject.Find("[CameraRig]");
        g.GetComponent<HPV_Manager>().seekToMs(node, startPos);
        g.GetComponent<HPV_Manager>().setDirection(node, direction);
    }
}
