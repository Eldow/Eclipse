using UnityEngine;
using System.Collections;

public class Poisonball : MonoBehaviour
{
    public int flip;
    public bool vertical;
    public float speed;
    private SpriteRenderer render;
    public float angle;
    private Rigidbody2D body;
    // Use this for initialization
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        render = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(TimedDeath());
    }



    IEnumerator TimedDeath()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject.transform.parent.gameObject);
    }
    IEnumerator BreakBlock(GameObject o)
    {
        render.enabled = false;
        o.GetComponent<Animator>().SetBool("broken", true);
        yield return new WaitForSeconds(0.45f);
        o.SetActive(false);
        Destroy(gameObject.transform.parent.gameObject);
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
        }
        else
        {
            if (other.gameObject.layer.Equals(8))
            {
                render.enabled = false;
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
