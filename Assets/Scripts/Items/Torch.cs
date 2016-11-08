using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour, ActivableInterface {
    private GameObject lightCircle;
	// Use this for initialization
	void Start () {
        lightCircle = gameObject.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Activate()
    {
        lightCircle.GetComponent<LightCircle>().activated = true;
        lightCircle.SetActive(true);
    }

    public void Desactivate()
    {
        StartCoroutine(WaitForSwitch());
    }

    IEnumerator WaitForSwitch()
    {
        lightCircle.GetComponent<LightCircle>().activated = false;
        yield return new WaitForSeconds(0.5f);
        lightCircle.SetActive(false);
    }

}
