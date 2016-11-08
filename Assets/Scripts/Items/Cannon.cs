using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour, ActivableInterface {
    public bool firing;
    public GameObject fireball;
    public float shootRate;
    private Animator anim;
    private Animator childAnim;
    private Coroutine shootingRoutine;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        childAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        if (firing)
        {
            shootingRoutine = StartCoroutine(Shoot());
        }
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("firing", firing);
        childAnim.SetBool("firing", firing);
	}

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootRate);
            Instantiate(fireball);
        }
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
