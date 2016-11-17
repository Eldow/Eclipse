using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Plate : MonoBehaviour {
    private bool triggered;
    public List<GameObject> targets;
    private List<ActivableInterface> activableTargets;
    private Animator parentAnim, childAnim;
    private Plate childPlate;
    private Animator anim;
    private Collider2D pressingCollider;
    public bool activated;
    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        if (gameObject.layer.Equals(9))
            parentAnim = gameObject.transform.parent.gameObject.GetComponent<Animator>();
        if (gameObject.layer.Equals(8))
            childAnim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        activableTargets = new List<ActivableInterface>();
        foreach (GameObject target in targets)
        {
            activableTargets.Add(target.gameObject.GetComponent(typeof(ActivableInterface)) as ActivableInterface);
        }
        activated = false;
    }

    //Update
    void Update()
    {

    }
    //Lever's action : custom this as you want
    IEnumerator TriggerAction()
    {
        yield return new WaitForSeconds(0.2f);
        //Here do the action - switch light etc.
        foreach (ActivableInterface a in activableTargets)
        {
            if (activated)
            {
                a.Activate();
            }
            else
            {
                a.Desactivate();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated)
        {
            if (gameObject.layer.Equals(9) && !other.gameObject.Equals(Switcher.instance.prof))
            {
                if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
                {
                    activated = true;
                    pressingCollider = other;
                    StartCoroutine(TriggerAction());
                    anim.SetBool("activated", activated);
                    parentAnim.SetBool("activated", activated);
                }
            }
            else
            {
                activated = true;
                pressingCollider = other;
                StartCoroutine(TriggerAction());
                anim.SetBool("activated", activated);
                childAnim.SetBool("activated", activated);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (activated && other.Equals(pressingCollider))
        {
            if (gameObject.layer.Equals(9) && !other.gameObject.Equals(Switcher.instance.profShadow))
            {
                if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
                {
                    activated = false;
                    StartCoroutine(TriggerAction());
                    anim.SetBool("activated", activated);
                    parentAnim.SetBool("activated", activated);
                }
            }
            else if (other.gameObject.Equals(Switcher.instance.prof))
            {
                activated = false;
                StartCoroutine(TriggerAction());
                anim.SetBool("activated", activated);
                childAnim.SetBool("activated", activated);
            }
        }
    }
}
