using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour, ActivableInterface {
    public GameObject lightCircle;
	// Use this for initialization
	void Start () {
        lightCircle = gameObject.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Activate()
    {
        lightCircle.SetActive(true);
        lightCircle.GetComponent<LightCircle>().activated = true;
        Switcher.instance.SetupLightCircles();
    }

    public void Desactivate()
    {
        StartCoroutine(WaitForSwitch());
    }

    public bool isActivated()
    {
        if(lightCircle.activeSelf)
        {
            return true;
        } else
        {
            return false;
        }
    }
    IEnumerator WaitForSwitch()
    {
        lightCircle.GetComponent<LightCircle>().activated = false;
        yield return new WaitForSeconds(0.5f);
        lightCircle.SetActive(false);
    }

}
