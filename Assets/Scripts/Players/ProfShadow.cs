using UnityEngine;
using System.Collections;

public class ProfShadow : MonoBehaviour, PlayerInterface {

    public float MAX_SPEED;
    public float speed;
    public float jumpPower;
    public bool grounded;
    public float accel;
    public float decel;
    public bool pushing;
    public bool damaged;
    public bool disabled;
    public bool walled;
    public bool activating;
    private Rigidbody2D body;
    private Animator anim;
    private GroundCollider groundCollider;
    private WallCollider wallCollider;
    private SpriteRenderer render;
    private float key, lastKey;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        groundCollider = GetComponentInChildren<GroundCollider>();
        wallCollider = GetComponentInChildren<WallCollider>();
        anim.SetBool("dead", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!disabled && body != null)
        {
            grounded = groundCollider.grounded;
            walled = wallCollider.walled;
            //Animation utils
            anim.SetBool("activating", activating);
            anim.SetFloat("speed", speed);
            anim.SetBool("walled", walled);
            anim.SetBool("grounded", grounded);
            if (Switcher.instance.currentPlayer.Equals(gameObject))
            {
                if (!Switcher.instance.IsInsideLightLayer())
                {
                    Switcher.instance.EnableLightColliders(true);
                }
                else
                {
                    Switcher.instance.EnableLightColliders(false);
                }
            }
            //Vertical movement
            Jump();
            //Horizontal movement
            Move();
        } else
        {
            //Animation utils
            render.flipX = Switcher.instance.currentPlayer.GetComponent<SpriteRenderer>().flipX;
            anim.SetBool("activating", Switcher.instance.prof.GetComponent<Prof>().activating);
            anim.SetFloat("speed", Switcher.instance.prof.GetComponent<Prof>().speed);
            anim.SetBool("grounded", Switcher.instance.prof.GetComponent<Prof>().grounded);
            anim.SetBool("onLadder", Switcher.instance.prof.GetComponent<Prof>().onLadder);
            anim.SetBool("pushing", Switcher.instance.prof.GetComponent<Prof>().pushing);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.AddForce(new Vector3(0, jumpPower, 0), ForceMode2D.Impulse);
            grounded = false;
        }
        if (walled)
        {
            if (!GetComponent<SpriteRenderer>().flipX)
            {
                key = 1f;
            }
            else
            {
                key = -1f;
            }
            if (Input.GetAxisRaw("Horizontal") == key && Input.GetButtonDown("Horizontal") && Input.GetAxis("Vertical") > 0 && !grounded && key != lastKey)
            {
                body.velocity = (new Vector2(-body.velocity.x, Mathf.Abs(body.velocity.y)));
                body.AddForce(new Vector2(0, jumpPower / 2), ForceMode2D.Impulse);
                lastKey = key;
            }
        }       
    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        if ((h < 0) && (speed < MAX_SPEED))
        {
            render.flipX = false;
            speed = speed + accel * Time.deltaTime;
        }
        else if ((h > 0) && (speed < MAX_SPEED))
        {
            render.flipX = true;
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
    public void Enable()
    {
        if(gameObject.GetComponent<Rigidbody2D>() == null)
            body = gameObject.AddComponent<Rigidbody2D>();
        foreach(Collider2D coll in GetComponentsInChildren<Collider2D>())
        {
            coll.enabled = true;
        }
        disabled = false;
    }
    public void Disable()
    {
        Destroy(GetComponent<Rigidbody2D>());
        foreach (Collider2D coll in GetComponentsInChildren<Collider2D>())
        {
            coll.enabled = false;
        }
        disabled = true;
    }
    public void Idle()
    {
        speed = 0;
    }
}

