using UnityEngine;
using System.Collections;

public class Poisonball : MonoBehaviour
{
    public int flip;
    public bool vertical;
    public float speed = 5f;
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

    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + 0.05f * gameObject.transform.up;
    }

    IEnumerator TimedDeath()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
    }
}
