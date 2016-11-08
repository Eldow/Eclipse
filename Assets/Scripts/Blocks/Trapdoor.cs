using UnityEngine;
using System.Collections;

public class Trapdoor : MonoBehaviour, ActivableInterface {
    private Animator anim;
    private Animator childAnim;
    private Collider2D coll;
    private Collider2D childColl;
    bool activated;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        childAnim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        coll = gameObject.GetComponent<Collider2D>();
        childColl = gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>();
        activated = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Activate()
    {
        activated = false;
        anim.SetBool("activated", activated);
        childAnim.SetBool("activated", activated);
        coll.enabled = true;
        childColl.enabled = true;
    }
    public void Desactivate()
    {
        activated = true;
        anim.SetBool("activated", activated);
        childAnim.SetBool("activated", activated);
        coll.enabled = false;
        childColl.enabled = false;
    }
}
