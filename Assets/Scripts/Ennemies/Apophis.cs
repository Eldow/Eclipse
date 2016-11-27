using UnityEngine;
using System.Collections;

public class Apophis : MonoBehaviour {
    public bool firing;
    public GameObject poisonball;
    public float shootRate;
    private Animator anim;
    private Animator childAnim;
    private Coroutine shootingRoutine;
    private AudioSource sound;
    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
        childAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        if (firing)
        {
            shootingRoutine = StartCoroutine(Shoot());
        }
    }

    // Update is called once per frame
    void Update () {
	    
	}

    IEnumerator Shoot()
    {
        while (true)
        {
            anim.Play("idle");
            childAnim.Play("idle");
            yield return new WaitForSeconds(shootRate);
            anim.Play("shoot_attack");
            childAnim.Play("shoot_attack");
            sound.Play();
            Vector3 offset = new Vector3(0, 0, 0);
            if (poisonball.GetComponent<Fireball>().vertical)
            {
                if (poisonball.GetComponent<Fireball>().flip > 0)
                {
                    offset.y = 1.8f;
                }
                else
                {
                    offset.y = -1.8f;
                }
            }
            else
            {
                if (poisonball.GetComponent<Fireball>().flip > 0)
                {
                    offset.x = 1.8f;
                }
                else
                {
                    offset.x = -1.8f;
                }
            }
            poisonball.transform.position = gameObject.transform.position + offset;
            Instantiate(poisonball);
        }
    }
}
