using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class Playermain : MonoBehaviour
{
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    public static int Fixed_items;

    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;

    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public float meleeLength = 1.5f;

    private Rigidbody2D myRB;

    private Animator anim;
    [HideInInspector]
    public int numLives;
    public float horiz;
Vector2 hi;


    public bool canMove;

    public AudioSource jumpsound;
    public AudioSource attackNoise;
    public AudioSource getHitNoise;
    public AudioSource boostNoise;
    public AudioSource collectSound;
    public AudioSource blockSound;

    [HideInInspector]
    public Vector3 spawnPoint;
    private float originalspeed;
    private float grourem = 0;
    private float grourtime = 0.2f;//0.142069f;
    private bool jumpedddd = false;
    private bool triggeredd = false;
    private bool m_FacingRight = true;
    private Ethan ethan;


    public void Awake()
    {
        ethan = new Ethan();

        ethan.Player.Move.performed += ctx => hi = ctx.ReadValue<Vector2>();
        ethan.Player.Move.canceled += ctx => hi.x = 0;
        ethan.Player.Jump.performed += ctx => Jump();
        ethan.Player.Jump.canceled += ctx => unJump();

        ethan.Player.Interact.performed += ctx => runtown();
        ethan.Player.Interact.canceled += ctx => runup();
       // ethan.Player.Crouch.performed += ctx => Block();
       //ethan.Player.Crouch.canceled += ctx => unBlock();



    }
    private void OnEnable()
    {
        ethan.Player.Enable();
    }
    private void OnDisable()
    {
        ethan.Player.Disable();
    }

    void Start()
    {
        horiz=hi.x;
        originalspeed = moveSpeed;
        spawnPoint = transform.position;
        myRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canMove = true;

    }
    public void Jump()
    {
        if (grourem > 0 && jumpedddd == false)
        {
            if(grourem<=10){
            jumpedddd = true;
            }
            grourem -= 9;

            myRB.linearVelocity += new Vector2(0, jumpForce);
            //jumpsound.Play();
        }
        
    }
    public void unJump()
    {
        jumpedddd = true;

    }
    public void runtown()
    {
        moveSpeed = runSpeed;
    }
    public void runup()
    {
        moveSpeed = originalspeed;
    }


    void Update()
    {

        horiz=hi.x;
        //move = Input.GetAxisRaw("Horizontal");
        //Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        //Jump
        grourem -= Time.deltaTime;
        if(MathF.Abs(myRB.linearVelocity.y)<=0.05f)
        {
            
            jumpedddd = false;
            if (jumpedddd == false)
            {
                grourem = grourtime;
            }

        }
       // print(myRB.linearVelocity.y);
        //run
        

        //block
        //if (Input.GetKey(block) && isGrounded)
        //{
        //    anim.SetBool("DragonIdle", false);
        //    anim.SetBool("Blocking", true);
        //}
        //if (Input.GetKeyUp(block))
        //{
        //    anim.SetBool("DragonIdle", true);

        //    anim.SetBool("Blocking", false);
        //}

        //if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Block") == true)
        //{
        //}

        //Attack
        //if (Input.GetKeyDown(attack))//&& isGrounded)
        //{
          
        //}

        //Animation
        //anim.SetFloat("Velocity", Mathf.Abs(myRB.linearVelocity.x));
        anim.SetBool("Grounded", isGrounded);

  

    }

    public void Attack()
    {
        RaycastHit2D hit;
        if (transform.localScale.x < 0)
        {
            hit = Physics2D.Raycast(new Vector3(transform.position.x - .15f, transform.position.y, transform.position.z), -transform.right);
        }
        else
        {
            hit = Physics2D.Raycast(new Vector3(transform.position.x + .15f, transform.position.y, transform.position.z), transform.right);
        }

        if (hit.collider != null)
        {
            print(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Enemy")
            {
                if (Mathf.Abs(hit.collider.gameObject.transform.position.x - transform.position.x) < meleeLength && Mathf.Abs(hit.collider.gameObject.transform.position.y - transform.position.y) < meleeLength)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis(horizontalAxis);




        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle") == true)
        {
            myRB.linearVelocity = new Vector2(0, myRB.linearVelocity.y);
        }
        if (/*GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DragonShoot") == false && */anim.GetBool("dead") == false)
        {
            Move();

            //Move right
            /* if (Input.GetKey(right) && canMove)
             {
                 myRB.linearVelocity = new Vector2(moveSpeed, myRB.linearVelocity.y);

                 if (transform.localScale.x < 0)
                 {
                     Flip();
                 }
             }

             //Move left
             else if (Input.GetKey(left) && canMove)
             {
                 myRB.linearVelocity = new Vector2(-moveSpeed, myRB.linearVelocity.y);

                 if (transform.localScale.x > 0)
                 {
                     Flip();
                 }

             }
             */
        }

        //Clean up movement
        else
        {
            if (isGrounded)
            {
                //myRB.linearVelocity = new Vector2(0, myRB.linearVelocity.y);
            }

        }

        //Physics

        if (isGrounded)
        {
            SetGroundedPhysics();

        }
        else if (!isGrounded)
        {
            SetAirbornePhysics();
        }

    }
    void Move()
    {
        if (Mathf.Abs(myRB.linearVelocity.x) >= 14)
        {
            myRB.linearVelocity = new Vector2((myRB.linearVelocity.normalized * 14f).x,myRB.linearVelocity.y);
        }
        myRB.linearVelocity += new Vector2(horiz * moveSpeed*Time.deltaTime*5f,0);
       
        if (horiz < 0 && m_FacingRight)
        {
            Flip();
        }
        if (horiz > 0 && !m_FacingRight)
        {
            Flip();
        }

    }

    void Flip()
    {
        m_FacingRight = !m_FacingRight;

        //transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);



    }
    public void Playcollectsound()
    {
        collectSound.Play();
    }

    public void SetCanMoveTrue()
    {
        canMove = true;
        Debug.Log("SetCanMoveTrue" + canMove.ToString());
    }

    public void SetCanMoveFalse()
    {
        canMove = false;
        Debug.Log("SetCanMoveFalse" + canMove.ToString());
    }

    private void SetGroundedPhysics()
    {
        myRB.mass = 1.25f;
        myRB.linearDamping = 0;
        myRB.angularDamping = 0.05f;
        myRB.gravityScale = 4f;

    }

    private void SetAirbornePhysics()
    {
        myRB.mass = 1.2f;
        myRB.linearDamping = 0;
        myRB.angularDamping = 0.05f;
        myRB.gravityScale = 3.5f;

    }

    public void Die()
    {
        Debug.Log("Dying");
        //gameObject.SetActive(false);
        transform.position = spawnPoint;

    }

       void Reset() //reset the scene the player is in
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other) //for the win con
    {
        if (other.gameObject.CompareTag("WinTile"))
        {
            
            SceneManager.LoadScene("WinScreen");
        }
    }
    


      private void OnCollisionEnter2D(Collision2D other) //killboxes basically whenever you die to one u will turn red, stop ur movement via the dead state and then reset the level
    {  
        isGrounded = true;
        if (other.gameObject.CompareTag("Evil"))
        {
 Reset();
        }
        if (other.gameObject.CompareTag("Hazard"))
        {
 Reset();

        }
    }
}