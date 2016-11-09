using UnityEngine;
using System.Collections;

public class Mummy : MonoBehaviour, PlayerInterface {

    public float MAX_SPEED;
    public float speed;
    public float accel;
    public float decel;
    public float jumpPower;
    public bool grounded;
    public bool damaged;
    public bool activating;

    private Rigidbody2D body;
    private Animator anim, childAnim;
    private SpriteRenderer render, childRender;
    private GroundCollider groundCollider;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        childAnim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        childRender = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        groundCollider = GetComponentInChildren<GroundCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Animation utils
        anim.SetFloat("speed", speed);
        anim.SetBool("grounded", grounded);
        anim.SetBool("activating", activating);
        childAnim.SetFloat("speed", speed);
        childAnim.SetBool("grounded", grounded);
        childAnim.SetBool("activating", activating);
        if (Switcher.instance.currentPlayer.Equals(gameObject) && !activating)
        {
            grounded = groundCollider.grounded;
            //Vertical movement
            Jump();
            //Horizontal movement
            Move();
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                body.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                grounded = false;
            }
        }
    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        if ((h < 0) && (speed < MAX_SPEED))
        {
            render.flipX = false;
            childRender.flipX = false;
            speed = speed + accel * Time.deltaTime;
        }
        else if ((h > 0) && (speed < MAX_SPEED))
        {
            render.flipX = true;
            childRender.flipX = true;
            speed = speed + accel * Time.deltaTime;
        }
        else
        {
            if (Mathf.Abs(speed) > decel * Time.deltaTime)
                speed = speed - decel * Time.deltaTime;
            else
                speed = 0;
        }
        body.position = new Vector2(body.position.x + speed * Time.deltaTime * h, body.position.y);
    }
    public void Activate()
    {
        activating = true;
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
        GetComponent<Collider2D>().enabled = true;
        grounded = true;
    }
    public void Disable()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
