using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    private GameObject fireBall;
   
    private GameObject fireBallTemp;
    [SerializeField]
    private float fireBallSpeed = 0;

    private Animator anim;

    private GameObject player;

    [SerializeField]
    private float attackCoolDown = 0;
    private float nextAttack;

    [SerializeField]
    private AudioClip swordSound;

    [SerializeField]
    private AudioClip spellSound;

    private AudioSource audioSource;

    private int playerFace = 1;

    private bool attackButton = false;
    private bool spellButton = false;

    public bool AttackButton
    {
        get { return attackButton; }
        set { attackButton = value; }
    }

    public bool SpellButton
    {
        get { return spellButton; }
        set { spellButton = value; }
    }

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        //if ((Input.GetButtonDown("Fire1") && Time.time > proximoAtaque))
        if ((attackButton == true && Time.time > nextAttack))
        {
            attackButton = false;
            Attacking();
        }

        if ((spellButton == true && Time.time > nextAttack))
        {
            spellButton = false;
            Fireball();
        }
    }

    void Attacking()
    {
        audioSource.clip = swordSound;
        audioSource.Play();

        anim.SetTrigger("Attack");
        nextAttack = Time.time + attackCoolDown;
    }

    void Fireball()
    {
        if (player.transform.localScale.x < 0)
        {
            playerFace = -1;
        }        
        else
        {
            playerFace = 1;
            
        }

        audioSource.clip = spellSound;
        audioSource.Play();

        anim.SetTrigger("Skill");
        fireBallTemp = GameObject.Instantiate(fireBall);
        fireBallTemp.transform.position = new Vector3(player.transform.position.x + (1f * playerFace), player.transform.position.y - 0.2f,
                                                    player.transform.position.z);
    
        fireBallTemp.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        attackButton = false;
        fireBallTemp.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(fireBallSpeed * playerFace, fireBallTemp.GetComponent<Rigidbody2D>().velocity.y);

        Destroy(fireBallTemp, 0.8f);
        

    }
}
