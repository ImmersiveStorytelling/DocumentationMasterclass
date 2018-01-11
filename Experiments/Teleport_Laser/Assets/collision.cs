using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour
{
    private GameObject sphereSetActive;
    private GameObject sphereSetUnActive;
    private GameObject sphereNextUnActive;

    private void Start()
    {
        sphereSetActive = GameObject.Find("/Spheres").transform.Find("Sphere3").gameObject;
        sphereSetUnActive = GameObject.Find("/Spheres").transform.Find("Sphere").gameObject;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "Laser")
        {
            switch (sphereSetUnActive.GetComponent<HPV_Node>().getFileName().Remove(0,16).Substring(0,1))
            {
                case "1": sphereSetUnActive = GameObject.Find("/Spheres").transform.Find("Sphere").gameObject; ; sphereSetActive = GameObject.Find("/Spheres").transform.Find("Sphere3").gameObject; sphereNextUnActive = GameObject.Find("/Spheres").transform.Find("Sphere3").gameObject; break;
                case "2": sphereSetUnActive = GameObject.Find("/Spheres").transform.Find("Sphere3").gameObject; sphereSetActive = GameObject.Find("/Spheres").transform.Find("Sphere4").gameObject; sphereNextUnActive = GameObject.Find("/Spheres").transform.Find("Sphere4").gameObject; break;
                case "3": sphereSetUnActive = GameObject.Find("/Spheres").transform.Find("Sphere4").gameObject; sphereSetActive = GameObject.Find("/Spheres").transform.Find("Sphere5").gameObject; sphereNextUnActive = GameObject.Find("/Spheres").transform.Find("Sphere5").gameObject; break;
            }
            gameObject.SetActive(false);
            sphereSetUnActive.SetActive(false);
            sphereSetActive.SetActive(true);

            sphereSetUnActive = sphereNextUnActive;
        }   
    }
}
