using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour, MovableInterface, ActivableInterface {
    public float speed;
    public bool activated;
    public bool horizontal;
    public float direction;
    private Rigidbody2D body;
    private bool sticky;
    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            Move();
        }
    }

    void Move()
    {
        if(horizontal)
            body.MovePosition(new Vector2(transform.position.x + speed * Time.deltaTime * direction, transform.position.y));
        else
            body.MovePosition(new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime * direction));
    }

    public void Flip()
    {
        direction = -direction;
    }

    public void Activate()
    {
        activated = true;
    }
    public void Desactivate()
    {
        activated = false;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        sticky = true;
    }
}
