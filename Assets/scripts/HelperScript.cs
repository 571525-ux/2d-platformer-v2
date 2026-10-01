using UnityEngine;

public class HelperScript : MonoBehaviour
{

    public void FlipSprite()
    { 
        // Get the SpriteRenderer component
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb.linearVelocityX < -0.1f)
        {
            sr.flipX = false;
        }

        if (rb.linearVelocityX > 0.1f)
        {
            sr.flipX = true;
        }
    }

    public void Hello()
    {
        print("hello");
    }

    //add more methods here

    public void destroyer()
    {
        Destroy(gameObject);
    }

}
