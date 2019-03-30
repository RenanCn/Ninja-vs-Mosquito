using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private Animator anim;
    private bool jump = false;
    private bool jumpAgain = false;
    private bool inGround = false;
    private Transform groundCheck;

    [SerializeField]
    private AudioClip jumpSound;
    private AudioSource audioSource;

    private bool jumpButton;
    private int moveButton;
    private float h;

    public bool JumpButton
    {
        get { return jumpButton; }
        set { jumpButton = value; }

    }

    public int MoveButton
    {
        get { return moveButton; }
        set { moveButton = value; }
    }

    public float H
    {
        get { return h; }
        set { h = value; }
    }


    // Use this for initialization
    void Start () {

        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        /* Controle do pulo */
        inGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if ((Input.GetButtonDown("Jump") && inGround) || (jumpButton == true && inGround))
        {

            jump = true;
            anim.SetTrigger("Jumped");

            audioSource.clip = jumpSound;
            audioSource.Play();

        }

    }

    private void FixedUpdate()
    {
        

        /* Controle da movimentação */
        // float h;
        h = moveButton;
        //h = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("Velocity", Mathf.Abs(h));

        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            Flip();
        }

        if (jump)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            jump = false;
            jumpButton = false; 
            jumpAgain = true;
        }

        /* Double Jump */
        if (jumpAgain == true && (jumpButton == true || Input.GetButtonDown("Jump")))
        {
            audioSource.Play();
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("Jumped");
            jump = false;
            jumpButton = false;
            jumpAgain = false;
        }

        jumpButton = false;
    }

    /* Controle de direção */
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }



}
