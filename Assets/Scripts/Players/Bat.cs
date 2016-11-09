using UnityEngine;
using System.Collections;

public class Bat : MonoBehaviour, PlayerInterface {
    public float MAX_SPEED;
    public float speed;
    public float verticalSpeed;
    public float accel;
    public float decel;
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
        //Animation utils
        anim.SetBool("activating", activating);
        shadowAnim.SetBool("activating", activating);
        if (Switcher.instance.currentPlayer.Equals(gameObject))
        {
            Fly();
        }
    }

    void Fly()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
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
        if ((v < 0) && (verticalSpeed < MAX_SPEED))
        {
            verticalSpeed = verticalSpeed + accel * Time.deltaTime;
        }
        else if ((v > 0) && (verticalSpeed < MAX_SPEED))
        {
            verticalSpeed = verticalSpeed + accel * Time.deltaTime;
        }
        else
        {
            if (Mathf.Abs(verticalSpeed) > decel * Time.deltaTime)
                verticalSpeed = verticalSpeed - decel * Time.deltaTime;
            else
                verticalSpeed = 0;
        }
        body.position = new Vector2(body.position.x + speed * Time.deltaTime * h, body.position.y + verticalSpeed * Time.deltaTime * v);
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
}