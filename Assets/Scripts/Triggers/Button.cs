using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour {
    bool triggered = false;
    private List<ActivableInterface> activableTargets;
    public List<GameObject> targets;
    // Use this for initialization
    void Start () {
        activableTargets = new List<ActivableInterface>();
        if (targets.Count > 0)
        {
            foreach (GameObject target in targets)
            {
                activableTargets.Add(target.gameObject.GetComponent(typeof(ActivableInterface)) as ActivableInterface);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(triggered && Input.GetButtonDown("Action"))
        {
            StartCoroutine(TriggerAction());
            triggered = false;
            Debug.Log("Shoot");
        }
	}

    IEnumerator TriggerAction()
    {
        foreach (ActivableInterface a in activableTargets)
        {
            if (a != null)
            {
                if (!a.isActivated())
                {
                    a.Activate();
                }
                else
                {
                    a.Desactivate();
                }
            }
        }
        yield return new WaitForSeconds(1f);
        foreach (ActivableInterface a in activableTargets)
        {
            if (a != null)
            {
                if (!a.isActivated())
                {
                    a.Activate();
                }
                else
                {
                    a.Desactivate();
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            triggered = true;
    }
}
