using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPhysics : MonoBehaviour                   // HW MAKE COINS PLAY SOUND EFFECT!!!
{
    // Start is called before the first frame update

    public float weight = 0.1f;
    public int value = 5;
    void OnTriggerEnter2D(Collider2D collision)
    {


        DynamicPlayerController player = collision.GetComponent<DynamicPlayerController>();
        
        if (player == null)
        {            
            return;
        }

        player.pickupGold(this);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<AudioSource>().Play();

    }

    // Update is called once per frame

    
    
    
   
}
