using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
    public int flip;
    public bool vertical;
    public float speed;
    private SpriteRenderer render;
    //private Rigidbody2D body;
	// Use this for initialization
	void Start () {
        //body = gameObject.GetComponent<Rigidbody2D>();
        render = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!vertical)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * flip, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime * flip);
    }
    IEnumerator BreakBlock(GameObject o)
    {
        render.enabled = false;
        o.GetComponent<Animator>().SetBool("broken", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(o);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ennemy"))
        {
            // Here, play a sound and an animation depending on what is hit
            render.enabled = false;
            Switcher.instance.KillPlayer(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Breakable"))
        {
            StartCoroutine(BreakBlock(other.gameObject));
        } else
        {
            Destroy(gameObject);
        }
    }
}
