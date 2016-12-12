using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prof : MonoBehaviour, PlayerInterface
{

    public float MAX_SPEED;
    public float speed;
    public float accel;
    public float decel;
    public float jumpPower;
    public bool grounded;
    public bool asleep;
    public bool pushing;
    public bool damaged;
    public bool activating;
    public bool onLadder;

    private Rigidbody2D body;
    private Animator anim;
    private GroundCollider groundCollider;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCollider = GetComponentInChildren<GroundCollider>();
        anim.SetBool("dead", false);
    }
    void Update()
    {
        //Animation utils
        anim.SetFloat("speed", speed);
        anim.SetBool("grounded", grounded);
        anim.SetBool("asleep", asleep);
        anim.SetBool("activating", activating);
        anim.SetBool("onLadder", onLadder);
        anim.SetBool("pushing", pushing);
        if (Switcher.instance.currentPlayer.Equals(gameObject) && !activating && !asleep)
        {
            if (!onLadder)
            {
                body.gravityScale = 1f;
                grounded = groundCollider.grounded;
                //Vertical movement
                Jump();
            }
            else
            {
                body.gravityScale = 0f;
                MoveOnLadder();
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Switcher.instance.currentPlayer.Equals(gameObject) && !activating && !asleep)
        {
            if (!onLadder)
            {
                body.gravityScale = 1f;
                grounded = groundCollider.grounded;
                //Horizontal movement
                Move();
            }
            else
            {
                body.gravityScale = 0f;
                MoveOnLadder();
            }
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            grounded = false;
        }
    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        if ((h < 0) && (speed < MAX_SPEED))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            speed = speed + accel * Time.deltaTime;
        }
        else if ((h > 0) && (speed < MAX_SPEED))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            speed = speed + accel * Time.deltaTime;
        }
        else
        {
            if (Mathf.Abs(speed) > decel * Time.deltaTime)
                speed = speed - decel * Time.deltaTime;
            else
                speed = 0;
        }
        if (asleep)
        {
            speed = 0;
        }
        //body.position = new Vector2(body.position.x + speed * Time.deltaTime * h, body.position.y);
        body.velocity = new Vector2(speed * h, body.velocity.y);
    }

    void MoveOnLadder()
    {
        float v = Input.GetAxis("Vertical");
        speed = speed + 1f * Time.deltaTime;
        body.position = new Vector2(body.position.x, body.position.y + speed * Time.deltaTime * v);
    }
    public void Activate()
    {
        StartCoroutine(ActivateAnim());
    }
    IEnumerator ActivateAnim()
    {
        activating = true;
        yield return new WaitForSeconds(0.5f);
        activating = false;
    }
    public void Desactivate()
    {
        activating = false;
    }

    public void Idle()
    {
        speed = 0;
    }
    public void Enable()
    {
        asleep = false;
        grounded = true;
    }
    public void Disable()
    {
        asleep = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            pushing = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            pushing = false;
        }
    }
}
