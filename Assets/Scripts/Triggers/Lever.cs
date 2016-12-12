using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lever : MonoBehaviour {

    public bool activated = false;
    private Animator anim;
    private bool triggered = false;
    private PlayerInterface player;
    private List<ActivableInterface> activableTargets;
    public List<GameObject> targets;
    private Lever linkedLever;
    private GameObject currentLever;
    private AudioSource sound;
    public bool once = false;
    private bool used = false;

    // Use this for initialization
    void Start()
    {
        used = false;
        anim = GetComponent<Animator>();
        if (gameObject.layer == 8)
        {
            linkedLever = gameObject.transform.GetChild(0).gameObject.GetComponent<Lever>();
        }
        else
        {
            linkedLever = gameObject.transform.parent.gameObject.GetComponent<Lever>();
        }
        activableTargets = new List<ActivableInterface>();
        if(targets.Count > 0)
        {
            foreach (GameObject target in targets)
            {
                activableTargets.Add(target.gameObject.GetComponent(typeof(ActivableInterface)) as ActivableInterface);
            }
        }
        sound = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        //Link to animations parameters
        anim.SetBool("activated", activated);
        linkedLever.GetComponent<Animator>().SetBool("activated", activated);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.currentPlayer))
        {
            player = other.gameObject.GetComponent(typeof(PlayerInterface)) as PlayerInterface;
            //If player is colliding the trigger & pressing down key
            if (triggered && Input.GetButtonDown("Action"))
            {
                sound.Play();
                triggered = false;
                activated = !activated;
                linkedLever.activated = activated;
                StartCoroutine(TriggerAction());
            }
        }
    }
    //Lever's action : custom this as you want
    IEnumerator TriggerAction()
    {
        if(player != null && (!once || (once && !used)))
        {
            player.Activate();
            yield return new WaitForSeconds(0.4f);
            player.Desactivate();
            //Here do the action - switch light etc.
            foreach (ActivableInterface a in activableTargets)
            {
                if (a !=null)
                {
                    if (!a.isActivated())
                    {
                        a.Activate();
                    }
                    else
                    {
                        a.Desactivate();
                    }
                }
            }
            used = true;
        }
        triggered = true;
    }
    //Events related - set inTrigger depending of player's presence
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.currentPlayer) && !triggered)
        {
            triggered = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.currentPlayer))
        {
            triggered = false;
        }
    }
}
