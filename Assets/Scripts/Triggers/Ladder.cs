using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
    bool triggered;
	// Use this for initialization
	void Start () {
        triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.prof) && !triggered)
        {
            Switcher.instance.prof.GetComponent<Prof>().onLadder = true;
            triggered = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.prof))
        {
            Switcher.instance.prof.GetComponent<Prof>().onLadder = false;
            triggered = false;
        }
    }
}
