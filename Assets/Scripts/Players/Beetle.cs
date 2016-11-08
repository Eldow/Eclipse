using UnityEngine;
using System.Collections;

public class Beetle : MonoBehaviour, PlayerInterface {
    public float MAX_SPEED;
    public float speed;
    public float accel;
    public float decel;
    public bool grounded;
    public bool activating;

    public bool damaged;

    private Rigidbody2D body;
    private Animator anim;
    private Animator shadowAnim;
    private GroundCollider groundCollider;
    private SpriteRenderer shadowRender;
    private SpriteRenderer render;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        shadowAnim = GetComponentsInChildren<Animator>()[1];
        groundCollider = GetComponentInChildren<GroundCollider>();
        render = GetComponent<SpriteRenderer>();
        shadowRender = GetComponentsInChildren<SpriteRenderer>()[1];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = groundCollider.grounded;
        //Animation utils
        anim.SetFloat("speed", speed);
        anim.SetBool("grounded", grounded);
        shadowAnim.SetFloat("speed", speed);
        shadowAnim.SetBool("grounded", grounded);
        if (Switcher.instance.currentPlayer.Equals(gameObject) && !activating)
        {

            //Horizontal movement
            Move();
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        if ((h < 0) && (speed < MAX_SPEED))
        {
            render.flipX = false;
            shadowRender.flipX = false;
            speed = speed + accel * Time.deltaTime;
        }
        else if ((h > 0) && (speed < MAX_SPEED))
        {
            render.flipX = true;
            shadowRender.flipX = true;
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
}
