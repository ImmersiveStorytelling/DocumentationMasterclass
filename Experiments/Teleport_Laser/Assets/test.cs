using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Timers;

public class test : MonoBehaviour {

    public enum Film
    {
        film0,
        film1,
        film2,
        film3,
        film4,
        film5,
        film6
    }

    public Film _film = Film.film0;

    GameObject sphere,sphere2;
    GameObject mainCamEye;
    HPV_Manager mgr;

    void Start()
    {
        sphere = GameObject.Find("Sphere");
        sphere2 = GameObject.Find("Sphere3");

        Component c = sphere.GetComponent("HPV_Node");
        HPV_Node node = (HPV_Node)c;
        

        mainCamEye = GameObject.Find("Main Camera (eye)");
        mgr = (HPV_Manager)mainCamEye.GetComponent("HPV_Manager");
        mgr.pauseVideo(1);
    }
   
    void LateUpdate()
    {



        /*if (transform.rotation.eulerAngles.y > 50 && transform.rotation.eulerAngles.y < 80)
        {
            if (Camera.current != null)
            {
                //Debug.Log("move camera");
              

                   //sphere.transform.position = new Vector3(100, 100, 100);
                   //sphere2.transform.position = new Vector3(0, 0, 0);
                //_film = Film.film2;
            }


        }

        if (transform.rotation.eulerAngles.y > 0 && transform.rotation.eulerAngles.y < 50)
        {
            if (Camera.current != null)
            {
                //Debug.Log("move camera");
               
                //sphere.transform.position = new Vector3(0, 0, 0);
                //sphere2.transform.position = new Vector3(100, 100, 100);
                //_film = Film.film1;
                //mgr.resumeVideo(1);
            }

        }*/
    }
}
