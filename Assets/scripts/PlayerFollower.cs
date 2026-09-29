using UnityEngine;

public class PlayerFollower : MonoBehaviour

    
{
    public GameObject player;
    SpriteRenderer sr;
    float enemyPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        print("playyer x pos is" + player.transform.position.x);

        Flipsprite();
        
    }
    void Flipsprite()
    {
       if(player.transform.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        if (player.transform.position.x> transform.position.x)
        {
            sr.flipX = false;
        }

    }

}
