using UnityEngine;
using System.Collections;

public class Sarcophagus : MonoBehaviour {
    private bool triggered;
    public bool empty;
    private bool hidden;
    private Animator anim, childAnim;
    private AudioSource sound;
    // Use this for initialization
    void Start () {
        triggered = false;
        hidden = false;
        anim = gameObject.GetComponent<Animator>();
        childAnim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("empty", empty);
        childAnim.SetBool("empty", empty);
	    if(triggered && Input.GetButtonDown("Action"))
        {
            triggered = false;
            sound.Play();
            if (empty && !hidden)
            {
                //Hide the prof
                hidden = true;
                StartCoroutine(HideInside());

            } else if(!empty && !hidden)
            {
                //Show mummy & close
                StartCoroutine(ShowAndClose());
            } else
            {
                //Go out 
                hidden = false;
                StartCoroutine(GoOutside());
            }
        }
	}
    IEnumerator HideInside()
    {
        anim.SetBool("open", true);
        childAnim.SetBool("open", true);
        Switcher.instance.prof.GetComponent<Prof>().Activate();
        yield return new WaitForSeconds(0.5f);
        Switcher.instance.prof.SetActive(false);
        Switcher.instance.profShadow.SetActive(false);
        anim.SetBool("open", false);
        childAnim.SetBool("open", false);
        triggered = true;
    }

    IEnumerator ShowAndClose()
    {
        anim.SetBool("open", true);
        childAnim.SetBool("open", true);
        Switcher.instance.prof.GetComponent<Prof>().Activate();
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("open", false);
        childAnim.SetBool("open", false);
        triggered = true;
    }

    IEnumerator GoOutside()
    {
        anim.SetBool("open", true);
        childAnim.SetBool("open", true);
        Switcher.instance.prof.SetActive(true);
        Switcher.instance.profShadow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("open", false);
        childAnim.SetBool("open", false);
        triggered = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.prof) && !triggered)
        {
            triggered = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.Equals(Switcher.instance.prof) && triggered)
            triggered = false;
    }
}
