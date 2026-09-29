using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public LayerMask groundLayerMask;
    bool result;
    bool isGroundedLeft, isGroundedMiddle, isGroundedRight;
    bool seeWallRight , seeWallLeft;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    float dirX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        


        isGroundedMiddle = RayCollisionCheck(0, 0, Vector2.down);

        isGroundedLeft = RayCollisionCheck(-0.2f, 0, Vector2.down);

        isGroundedRight = RayCollisionCheck(0.2f, 0, Vector2.down);

         seeWallLeft = RayCollisionCheck(-0.2f, 0.2f, Vector2.left);

        seeWallRight = RayCollisionCheck(0.2f, 0.2f, Vector2.right);


        if (isGroundedRight == false || seeWallRight && (dirX > 0))
        {

            dirX = -2;
            print("walkingback");


            /* if (isGroundedRight == true)
             {
                 rb.linearVelocity = new Vector2(-2, rb.linearVelocityY);
             }
            */
        }
        if (isGroundedLeft == false || seeWallLeft && (dirX < 0))
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

    public bool RayCollisionCheck(float xoffs, float yoffs, Vector2 direction )
    {
        float rayLength = 0.25f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward starting at the sprite's position
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, direction, rayLength, groundLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            print("Player has collided with Ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray's position
        // You need to enable gizmos in th e editor to see these
        Debug.DrawRay(transform.position + offset, direction * rayLength, hitColor);
        return hitSomething;


    }

    private void OnCollisionEnter(Collision collision)
    {


    }

}
