using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class EnemySimple : MonoBehaviour
{
    public LayerMask groundLayerMask;
    bool result;
    bool isGroundedLeft, isGroundedMiddle, isGroundedRight;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    float dirX;

    void Start()
    { 
        groundLayerMask = LayerMask.GetMask("ground");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        dirX = 2;
        anim = GetComponent<Animator>(); // ***
        bool walkingE = true;
    }

    // Update is called once per frame
    void Update()
    {
        

        //rb.linearVelocity = new Vector2(-2,  rb.linearVelocityY );

        // ground check


        isGroundedMiddle = RayCollisionCheck(0, 0);

         isGroundedLeft = RayCollisionCheck(-0.2f, 0);

         isGroundedRight = RayCollisionCheck(0.2f, 0);
        
        if (isGroundedRight == false && (dirX>0))
        {

            dirX = -2;
            print("walkingback");
            
            /* if (isGroundedRight == true)
             {
                 rb.linearVelocity = new Vector2(-2, rb.linearVelocityY);
             }
            */
        }
        



           if (isGroundedLeft == false && (dirX < 0))
           {
            
            dirX = 2;

           
             /*if (isGroundedRight == true)
            {
                rb.linearVelocity = new Vector2(2, rb.linearVelocityY);
            }
             */
           }

        rb.linearVelocityX = dirX;

        //isGrounded will contain TRUE if the ray hits something on the 'Ground' layer
        Flipsprite();
        

    }
    void Flipsprite()
    {
        if (dirX < -0.1f)
        {
            sr.flipX = true;
        }

        if (dirX > 0.1f)
        {
            sr.flipX = false;
        }
    }

    public bool RayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.5f; // length of raycast
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

    private void OnCollisionEnter(Collision collision)
    {
        

    }

}
