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
    public int hp;
    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
        childAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        LaunchShoot(1);
    }

    // Update is called once per frame
    void Update () {
	    
	}

    void LaunchShoot(int intensity)
    {
        if(intensity == 1)
        {
            shootRate = 2f;
            StartCoroutine(Shoot());
        }
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
            float x = Random.Range(-6, 0);
            Vector3 offset = new Vector3(x, 3, 0);
            poisonball.transform.position = gameObject.transform.position + offset;
            Instantiate(poisonball);
        }
    }
}
