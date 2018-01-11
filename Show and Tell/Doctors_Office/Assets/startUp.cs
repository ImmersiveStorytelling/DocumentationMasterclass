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

    //public int numberOfSteps;
    private int[] eulerAngArr = { 350, 80, 170, 260 };
    public int cacheAmount = 2;
    private int step = 0;
    private int numberOfSteps;
    private eulerAngEnum[] switchAngles = {
        eulerAngEnum.front
    };
    private bool spacePressed = false;
    //private AudioClip[] audioClips = new AudioClip[2];
    //AudioSource audioSource;

    // Use this for initialization
    void Start () {
        numberOfSteps = switchAngles.Length + 1;
        makeSpheres();

        /*for (int i = 0; i < audioClips.Length; i++)
        {
            audioClips[i] = Resources.Load<AudioClip>("Sounds/" + (i+1) + ".mp3");
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        audioSource.Play();*/
       
    }

    // Update is called once per frame
    void Update () {
        Vector3 v = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles;
        double yAngle = v.y;
        if (Input.GetKeyDown("space"))
        {
            spacePressed = true;
            Debug.Log("Gedrukt");
        }
        
        if (!spacePressed)
        {
            return;
        }

        if (!(GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().getPositionMs(0) >= 173480))
        {
            
            return;
        }

        if (yAngle > (eulerAngArr[(int)switchAngles[step]] - 10) && yAngle < (eulerAngArr[(int)switchAngles[step]] + 10))
        {
            if ((step + cacheAmount) < numberOfSteps)
            {
                GameObject.Find("Steps").transform.GetChild(step + cacheAmount).gameObject.SetActive(true);
            }


            Debug.Log(step);
            if (step < (numberOfSteps - 1))
            {
                GameObject.Find("Steps").transform.GetChild(step).gameObject.SetActive(false);
                if (step < numberOfSteps -2)
                {
                    GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().seekToPos(1, 0);
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

            GameObject.Find("[CameraRig]").GetComponent<HPV_Manager>().setSpeed((byte)(i - 1), 1.1988);

            Renderer myRenderer = obj.GetComponent<MeshRenderer>();
            myRenderer.material.shader = Shader.Find("Custom/HPV/Spherical");

            if (i > 1)
            {
                if (i > 2)
                    obj.SetActive(false);
            }
                

            obj.transform.parent = GameObject.Find("Steps").transform;
        }
    }
}
