using UnityEngine;
using System.Collections;

public class LeverBehaviour : MonoBehaviour {

    public bool activated = false;
    //Lever properties
    private Animator anim;
    private Animator shadowAnim;
    private bool inTrigger = false;
    //Needed for action : custom these as you want
    private GameObject player;
    private GameObject shadow;

	// Use this for initialization
	void Start () {
        //Lever Action : custom this as you want
        player = GameObject.Find("Prof");
        shadow = GameObject.Find("Prof Shadow");
        //Lever Animations
        anim = GetComponent<Animator>();
        shadowAnim = GameObject.Find("Lever Shadow").GetComponent<Animator>();
        PlayerSwitcher.StartWith(player);
    }
	
    //Update
    void Update()
    {
        //If player is colliding the trigger & pressing down key & active
        if (inTrigger && Input.GetKeyDown("down") && player.activeInHierarchy)
        {
            activated = !activated;
        }
        TriggerAction();
    }
    //Fixed Update
    void FixedUpdate()
    {
        //Link to animations parameters
        anim.SetBool("activated", activated);
        if(shadowAnim.isActiveAndEnabled)
            shadowAnim.SetBool("activated", activated);
    }
    //Lever's action : custom this as you want
    void TriggerAction()
    {
        if (activated)
        {
           PlayerSwitcher.Switch(player, shadow);
        }
    }
    //Events related - set inTrigger depending of player's presence
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = false;
        }
    }
}
