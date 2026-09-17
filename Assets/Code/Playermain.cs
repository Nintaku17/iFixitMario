using UnityEngine;
using UnityEngine.SceneManagement;


public class Playermain : MonoBehaviour
{
    public enum PlayerState
    {
        Idle = 0,
        Walking = 1,
        Hurt = 2,
        Jumping = 3,
        Dead = 4
    }

    public enum CurrentWeapon
    {
        
        PolarStar = 0,
        Fireball = 1,
        RocketLauncher = 2
        
        
        
    }
    
    
    
    public PlayerState state = PlayerState.Idle;
    public CurrentWeapon cw = CurrentWeapon.PolarStar;
    
    public float speed = 10;
    public float jump = 5;
    public float gravity = 3;
    public bool isGrounded;
   // public Transform groundCheck;
    public bool facingleft = true;
    public Rigidbody2D RB;
    public SpriteRenderer SR;
    public bool daeth = false;
    public float Delay = 2;
    
    public AudioSource AS;
    public AudioClip FAK;
    public AudioClip Track;
    
    void Start()
    {
        
        SR= GetComponent<SpriteRenderer>();
        RB= GetComponent<Rigidbody2D>();
       //groundCheck= GetComponent<Transform>();
       AS= GetComponent<AudioSource>();
        
    }


    
    

    void Update()
    {
        
        //AS.clip = Track;
        
        //AS.Play();
        
        if (transform.position.y < -4)
        {

            Reset();

        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
           // Invoke("Reset", Delay);
           Reset();
           
        }
       
        if (daeth)  return;
        
       Movement();
       
     
        
        
        SR.flipX = facingleft;
        
        
        
      
        
        
        SetState(state);
        // end of void update    
    }
    // outside of void update


    public void SetState(PlayerState PlayerSet)
    {
        state = PlayerSet;

        if (PlayerSet == PlayerState.Idle)
        {
            SR.color = Color.blue;
        }

        if (PlayerSet == PlayerState.Walking)
        {
            SR.color = Color.green;
        }

        if (PlayerSet == PlayerState.Hurt)
        {
            SR.color = Color.yellow;
        }

        if (PlayerSet == PlayerState.Jumping)
        {
            SR.color = Color.magenta;
        }

        if (PlayerSet == PlayerState.Dead)
        {
            SR.color = Color.black;
           // RB.gravityScale = 0;
           //AS.PlayOneShot(FAK);
        }
        
        
        
    }

    public void Movement()
    {
        
        Vector2 vel = RB.linearVelocity;
        
        if (Input.GetKey(KeyCode.D))
        { 
            vel.x = speed;
            facingleft = false;
            state = PlayerState.Walking;
        }
        else if (Input.GetKey(KeyCode.A))
        { 
            vel.x = -speed;
            facingleft = true;
            state = PlayerState.Walking;
        }
        else
        { 
            vel.x = 0;
            state = PlayerState.Idle;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            vel.y = jump;
            state = PlayerState.Jumping;


        }
        RB.linearVelocity = vel;
    }

    public void NoMovement()
    {
        daeth  = true;
        RB.linearVelocity = Vector2.zero;
        state = PlayerState.Dead;
      
        
        
    }
    
    public bool CanJump()
    {
        return isGrounded;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {  
        isGrounded = true;
        if (other.gameObject.CompareTag("Evil"))
        {
            
            SR.color = Color.black;
            NoMovement();
            Invoke("Reset", Delay);
            
        }
        
        if (other.gameObject.CompareTag("Hazard"))
        {
            
            SR.color = Color.black;
            NoMovement();
            Invoke("Reset", Delay);
            
        }
        

       
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }
    
    void Reset()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.gameObject.CompareTag("Ihop"))
        {
            SceneManager.LoadScene("WinScreen");
        }
        
        
    }
}
