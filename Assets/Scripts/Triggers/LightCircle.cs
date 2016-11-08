using UnityEngine;
using System.Collections;

public class LightCircle : MonoBehaviour {
    private bool triggered;
    private bool swapped;
    public bool activated;

	// Use this for initialization
	void Start () {
        triggered = false;
        swapped = false;
        activated = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (activated)
        {
            if (triggered & !swapped)
            {
                Switcher.instance.SetCurrentPlayer(Switcher.instance.profShadow);
                triggered = false;
                swapped = true;
            }
        } else
        {
            if (swapped)
            {
                StartCoroutine(SwitchTo(Switcher.instance.prof));
            }
        }
	}
    IEnumerator SwitchTo(GameObject o)
    {
        yield return new WaitForSeconds(0.02f);
        Switcher.instance.SetCurrentPlayer(o);
        swapped = false;
        triggered = false;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.prof))
        {
            if (gameObject.GetComponentInChildren<Collider2D>().bounds.Contains(other.bounds.min)
                && gameObject.GetComponentInChildren<Collider2D>().bounds.Contains(other.bounds.max))
                triggered = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.prof))
        {
            triggered = false;
        }
    }
}
