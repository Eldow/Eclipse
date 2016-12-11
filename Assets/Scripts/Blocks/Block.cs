using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour, MovableInterface, ActivableInterface {
    public float speed;
    public bool activated;
    public bool horizontal;
    public float direction;
    public bool once;
    private int count;
    private Rigidbody2D body;
    private Vector2 positionMover;
    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        if(body != null)
            body.gravityScale = 0f;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            Move();
        }
    }

    public bool IsIgnoringFlippers()
    {
        return false;
    }
    void Move()
    {
        if (horizontal)
            positionMover = new Vector2(transform.parent.position.x + speed * Time.deltaTime * direction, transform.parent.position.y);
        else
            positionMover = new Vector2(transform.parent.position.x, transform.parent.position.y + speed * Time.deltaTime * direction);
        transform.parent.position = (positionMover);
    }

    public void Flip()
    {
        direction = -direction;
    }

    public bool isActivated()
    {
        return activated;
    }
    public void Activate()
    {
        if (once)
        {
            if (count < 1)
            {
                activated = true;
                count++;
            }
        }
        else
        {
            activated = true;
        }
    }
    public void Desactivate()
    {
        activated = false;
    }

    public bool Once()
    {
        return once;
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Sticky"))
        {
            other.transform.parent = transform.parent;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Sticky"))
        {
            other.transform.parent = null;
        }
    }
}
