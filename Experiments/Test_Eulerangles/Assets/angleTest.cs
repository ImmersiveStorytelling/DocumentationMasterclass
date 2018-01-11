using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angleTest : MonoBehaviour {

    private int rechtdoorMin = 45;
    private int rechtdoorMax = 135;
    private int rechtsMin1 = 135;
    private int rechtsMax1 = 180;
    private int rechtsMin2 = -180;
    private int rechtsMax = -135;
    private int achteruitMin = -135;
    private int achteruitMax = -45;
    private int linksMin1 = -45;
    private int linksMax1 = 0;
    private int linksMin2 = 0;
    private int linksMax2 = 45;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.rotation.eulerAngles.y > rechtdoorMin && transform.rotation.eulerAngles.y < rechtdoorMax)
            Debug.Log("Rechtdoor");
        if (transform.rotation.eulerAngles.y > achteruitMax && transform.rotation.eulerAngles.y < achteruitMin)
            Debug.Log("Achteruit");
        if ((transform.rotation.eulerAngles.y > rechtdoorMin && transform.rotation.eulerAngles.y < rechtdoorMax))
            Debug.Log("Rechtdoor");
    }
}
