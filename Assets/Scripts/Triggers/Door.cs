using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public bool locked;
    private Collider2D coll;
    private Animator anim;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
        audio = gameObject.GetComponent<AudioSource>();
        locked = true;
        anim = GetComponent<Animator>();
        anim.SetBool("locked", locked);
    }

    public void Unlock()
    {
        locked = false;
    }
	
    IEnumerator ToNextStage()
    {
        anim.SetBool("locked", locked);
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("Stage").GetComponent<Stage>().NextStage();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.prof) && !locked)
        {
            audio.Play();
            StartCoroutine(ToNextStage());
        }
    }
}
