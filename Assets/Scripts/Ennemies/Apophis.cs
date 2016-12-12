using UnityEngine;
using System.Collections;

public class Apophis : MonoBehaviour, ActivableInterface {
    public bool firing;
    public GameObject poisonball;
    public float shootRate;
    private Animator anim;
    private Animator childAnim;
    private Coroutine shootingRoutine;
    private AudioSource sound;
    public int hp;
    public Sprite apoHead;
    // Use this for initialization
    void Start()
    {
        hp = 100;
        anim = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
        childAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        TextLogger.instance.SetSpriteAndText(apoHead, "YOU ARE TOO LATE, NOW FACE MY FINAL FORM");
    }

    IEnumerator EndGame()
    {
        TextLogger.instance.SetSpriteAndText(apoHead, "AAAAARGH NOOOOOOO I WAS GOING TO RULE THE WORLD ");
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        yield return new WaitForSeconds(2f);
        TextLogger.instance.SetSpriteAndText(apoHead, "I WILL COME BACK");
        yield return new WaitForSeconds(2f);
        Stage.instance.NextStage();
    }
    IEnumerator LaunchShoot(int intensity)
    {
        while (true)
        {
            anim.Play("shoot_attack");
            childAnim.Play("shoot_attack");
            if (intensity == 1)
            {
                var startAngle = -Mathf.FloorToInt((5 - 1) / 2) * 30;
                for (var i = 0; i < 5; i++, startAngle += 30)
                {
                    Instantiate(poisonball, transform.position - new Vector3(3f, 0, 0), Quaternion.AngleAxis(startAngle, transform.forward) * transform.rotation);
                }
                yield return new WaitForSeconds(10f);
            }
            if (intensity == 2)
            {
                var startAngle = -Mathf.FloorToInt((5 - 1) / 2) * 30;
                for (var i = 0; i < 5; i++, startAngle += 30)
                {
                    Instantiate(poisonball, transform.position - new Vector3(3f, 0, 0), Quaternion.AngleAxis(startAngle, transform.forward) * transform.rotation);
                }
                yield return new WaitForSeconds(5f);
            }
            if (intensity == 3)
            {
                var startAngle = -Mathf.FloorToInt((5 - 1) / 2) * 30;
                for (var i = 0; i < 5; i++, startAngle += 30)
                {
                    Instantiate(poisonball, transform.position - new Vector3(3f, 0, 0), Quaternion.AngleAxis(startAngle, transform.forward) * transform.rotation);
                }
                yield return new WaitForSeconds(2.5f);
            }
        }
    }
    public void TakeHit()
    {
        hp -= 34;
        if (hp > 60)
        {
            TextLogger.instance.SetSpriteAndText(apoHead, "MISERABLE HUMAN, HOW CAN YOU POSSIBLY THINK YOU WILL DEFEAT ME ?!");
            StartCoroutine(TailAttack());
        }
        else if (hp > 30)
        {
            TextLogger.instance.SetSpriteAndText(apoHead, "NOW FACE MY TRUE POWER");
            StartCoroutine(TailAttack());
        }
        else if (hp <= 0)
        {
            StopCoroutine(shootingRoutine);
            StartCoroutine(EndGame());
        }

    }

    IEnumerator TailAttack()
    {
        anim.Play("tail_attack");
        childAnim.Play("tail_attack");
        yield return new WaitForSeconds(2f);
    }
    public void Activate()
    {
        firing = true;
        if (hp > 66)
            shootingRoutine = StartCoroutine(LaunchShoot(1));
        else if(hp > 33)
            shootingRoutine = StartCoroutine(LaunchShoot(2));
        else if (hp > 0)
            shootingRoutine = StartCoroutine(LaunchShoot(3));
    }
    public void Desactivate()
    {
        StopCoroutine(shootingRoutine);
        firing = false;
    }
    public bool isActivated()
    {
        return firing;
    }
}
