using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    const float MAX_SPEED = 3;

    public float speed = 50f;
    public float jumpPower = 150f;
    public bool grounded;
    public float accel = 50f;
    public float decel = 100f;

    private Rigidbody2D body;
    private Animator anim;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    // Fixed Update
    void FixedUpdate()
    {
        anim.SetBool("grounded", grounded);
        anim.SetFloat("speed", speed);
        //Vertical movement
        Jump();
        //Horizontal movement
        Move();       
    }
    void Jump()
    {
        if (Input.GetKey("up"))
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
            GetComponent<SpriteRenderer>().flipX = true;
            speed = speed + accel * Time.deltaTime;
        }
        else if ((h > 0) && (speed < MAX_SPEED))
        {
            GetComponent<SpriteRenderer>().flipX = false;
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
}
