using UnityEngine;
using System.Collections;

public class LeverBehaviour : MonoBehaviour {

    public bool activated = false;
    bool inTrigger = false;
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
    }
	
    //Update
    void Update()
    {
        anim.SetBool("activated", activated);
        if (inTrigger && Input.GetKeyDown("down"))
        {
            activated = !activated;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = false;
        }
    }
}
