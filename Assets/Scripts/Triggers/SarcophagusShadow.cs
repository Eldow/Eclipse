using UnityEngine;
using System.Collections;

public class SarcophagusShadow : MonoBehaviour {
    public bool empty;
    public bool colliding;
    public GameObject mummy;
    private Animator anim, parentAnim;
    private AudioSource sound;
	// Use this for initialization
	void Start () {
        empty = gameObject.transform.parent.gameObject.GetComponent<Sarcophagus>().empty;
        anim = gameObject.GetComponent<Animator>();
        parentAnim = gameObject.transform.parent.gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
        colliding = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (colliding && Input.GetButtonDown("Switch") && !empty)
        {
            sound.Play();
            StartCoroutine(SwitchToMummy());
            empty = true;
            gameObject.transform.parent.gameObject.GetComponent<Sarcophagus>().empty = true;
            colliding = false;
        }
	}
    IEnumerator SwitchToMummy()
    {
        anim.SetBool("open", true);
        parentAnim.SetBool("open", true);
        Switcher.instance.profShadow.SetActive(false);
        mummy.transform.position = gameObject.transform.parent.gameObject.transform.position;
        GameObject playerMummy = Instantiate(mummy);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("open", false);
        parentAnim.SetBool("open", false);
        Switcher.instance.SetCurrentPlayer(playerMummy);
        playerMummy.transform.GetChild(0).GetComponent<Animal>().entered = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.profShadow) && !colliding)
        {
            colliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.profShadow) && colliding)
        {
            colliding = false;
        }
    }
}
