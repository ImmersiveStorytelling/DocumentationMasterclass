using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    SteamVR_TrackedController controller;

    // Use this for initialization
    void Start () {

        controller = GetComponent<SteamVR_TrackedController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<SteamVR_TrackedController>();
        }
        controller.TriggerClicked += Controller_TriggerClicked;
        controller.PadClicked += Controller_PadClicked;
        
    }

    private void Controller_PadClicked(object sender, ClickedEventArgs e)
    {
        Debug.Log("Fired");
    }

    private void Controller_TriggerClicked(object sender, ClickedEventArgs e)
    {
        Debug.Log("Fired");

    }

    // Update is called once per frame
    void Update () {
		
	}
}
