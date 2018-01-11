using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angleScript : MonoBehaviour
{
    public int cacheAmount;
    private int[] eulerArr;
    private int step;
    private startUp startUpScript;
    private eulerAngEnum[] switchAngles = {
        eulerAngEnum.back,
        eulerAngEnum.left,
        eulerAngEnum.back,
        eulerAngEnum.left,
        eulerAngEnum.right,
        eulerAngEnum.left,
        eulerAngEnum.front,
        eulerAngEnum.left,
        eulerAngEnum.back,
        eulerAngEnum.back,
        eulerAngEnum.left,
        eulerAngEnum.right,
        eulerAngEnum.front,
        eulerAngEnum.back,
        eulerAngEnum.front,
        eulerAngEnum.back,//from here test code
        eulerAngEnum.front,
        eulerAngEnum.back,
        eulerAngEnum.front
    };
    // Use this for initialization
    void Start()
    {
        startUpScript = GetComponent<startUp>();
        eulerArr = startUpScript.eulerAngArr;
        step = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameObject.Find("Steps").transform.GetChild(0).gameObject.activeSelf == false)
        //{
        //Vector3 v = GameObject.Find("Camera (eye)").transform.rotation.eulerAngles;
        //double yAngle = v.y;
        //if (yAngle > (eulerArr[(int)switchAngles[step]] - 10) && yAngle < (eulerArr[(int)switchAngles[step]] + 10))
        //{
        //    if ((step + cacheAmount) < startUpScript.numberOfSteps)
        //    {
        //        GameObject.Find("Steps").transform.GetChild(step + cacheAmount).gameObject.SetActive(true);
        //    }


        //    Debug.Log(step);
        //    if (step < (startUpScript.numberOfSteps - 1))
        //    {
        //        GameObject.Find("Steps").transform.GetChild(step).gameObject.SetActive(false);
        //        step++;
        //    }
        //}
        //}
    }
}
