using UnityEngine;
using System.Collections;

public class Cultist : MonoBehaviour, MovableInterface {
    public float speed;
    public bool attacking;
    public float h;
    public bool once;
    private Animator anim, childAnim;
    private SpriteRenderer render, childRender;
    private Rigidbody2D body;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        childAnim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        render = gameObject.GetComponent<SpriteRenderer>();
        childRender = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        body = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("speed", speed);
        anim.SetBool("attacking", attacking);
        childAnim.SetFloat("speed", speed);
        childAnim.SetBool("attacking", attacking);
        if(!attacking)
            Move();
	}

    void Move()
    {
        body.position = new Vector2(transform.position.x + speed * Time.deltaTime * h, transform.position.y);
    }

    public bool Once()
    {
        return once;
    }
    IEnumerator AnimateAttack(GameObject o)
    {
        yield return new WaitForSeconds(0.5f);
        Switcher.instance.KillPlayer(o);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.currentPlayer))
        {
            attacking = true;
            StartCoroutine(AnimateAttack(other.gameObject));
        }
    }
    public void Flip()
    {
        h = -h;
        render.flipX = !render.flipX;
        childRender.flipX = !childRender.flipX;
    }
}
