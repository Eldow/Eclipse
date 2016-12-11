using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour, ActivableInterface {
    public bool firing;
    public GameObject fireball;
    public float shootRate;
    private Animator anim;
    private Animator childAnim;
    private Coroutine shootingRoutine;
    private AudioSource sound;
    public bool shoot;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
        childAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        if (firing)
        {
            shootingRoutine = StartCoroutine(Shoot());
        }
        shoot = false;
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("firing", shoot);
        childAnim.SetBool("firing", shoot);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            shoot = false;
            sound.Play();
            Vector3 offset = new Vector3(0, 0, 0);
            if (fireball.GetComponent<Fireball>().vertical)
            {
                if(fireball.GetComponent<Fireball>().flip > 0)
                {
                    offset.y = 1.8f;
                } else
                {
                    offset.y = -1.8f;
                }
            } else
            {
                if (fireball.GetComponent<Fireball>().flip > 0)
                {
                    offset.x = 1.8f;
                }
                else
                {
                    offset.x = -1.8f;
                }
            }
            fireball.transform.position = gameObject.transform.position + offset;
            Instantiate(fireball);
            yield return new WaitForSeconds(shootRate - 0.5f);
            shoot = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
    public bool isActivated()
    {
        return firing;
    }
    public void Activate()
    {
        firing = true;
        shootingRoutine = StartCoroutine(Shoot());
    }

    public void Desactivate()
    {
        firing = false;
        StopCoroutine(shootingRoutine);
    }
}
