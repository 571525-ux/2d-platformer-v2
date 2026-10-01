using UnityEngine;
using UnityEngine.InputSystem;

public class playermovement : MonoBehaviour
{
    //declare the variables
    public LayerMask groundLayerMask;
    bool result;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction crouchAction;
    Rigidbody2D rb;
    SpriteRenderer sr;
    bool isGrounded;
    Animator anim;  // ***
    HelperScript helper;

    public float speed = 5.0f;

    void Start()
    {
        helper = gameObject.AddComponent<HelperScript>();
        //initialise the variables
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        //required: a 2D Rigidbody component attached to the Game Object
        rb = GetComponent<Rigidbody2D>();          //initialise the rigidbody component
        anim = GetComponent<Animator>(); // ***
        sr = GetComponent<SpriteRenderer>();
        isGrounded = false;
        groundLayerMask = LayerMask.GetMask("ground");
    }

    

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            // helper.FlipSprite(true);
            helper.destroyer();
        }


        if (rb.linearVelocityX != 0)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }

        //ground check
        isGrounded = RayCollisionCheck(0, 0);
        //isGrounded will contain TRUE if the ray hits something on the 'Ground' layer

        // read the x-axis and output it to the rigidbody
        Vector2 moveVel = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveVel.x * 4, rb.linearVelocity.y);
        Jump();
        helper.FlipSprite();
        Crouch();
        
    }
    




    public bool RayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.25f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward starting at the sprite's position
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength, groundLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            print("Player has collided with Ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray's position
        // You need to enable gizmos in th e editor to see these
        Debug.DrawRay(transform.position + offset, Vector2.down * rayLength, hitColor);
        return hitSomething;
    }



    void Jump()
    {
        if(jumpAction.WasPressedThisFrame() && (isGrounded == true))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 5);

        }


    }

    void Crouch()
    {
        if (crouchAction.WasPressedThisFrame())
        {
            
            
            anim.SetBool("crouching", true);
           
            Vector2 moveVel = moveAction.ReadValue<Vector2>();
            rb.linearVelocity = new Vector2(moveVel.x * 2, rb.linearVelocity.y);
            print("crouching");
        }
        else if (crouchAction.WasReleasedThisFrame())
        {
            
            
           anim.SetBool("crouching", false);
            
        }

    }
    
    

}
