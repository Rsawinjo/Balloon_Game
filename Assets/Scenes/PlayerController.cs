using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
     * // 1) Impliment color change on button press "G" Switch back and forth
        // 2) Make a new component that changes it scale's magnitude between
        //    0.5f and 5.0f. (Ie the object's grows and skrinks like a heart beat)
        // 3) Make a new component that changes it's sprite color gradually between 2 different color. (Also try and use a public Gradient)
        // 4) Make a new component that changes it's spriteRenderer's sprite
        //    change between multiple different images like a flip book or animated sprite.
        //    See "public Sprite[] mySprites;"
        // 5) Make a new component that rotates an object constantly about the z axis.
        //    See "Quaternion.AngleAxis(123, Vector3.zero);
        //
    */

    // Update is called once per frame
    void Update()
    {
        
        bool wantUp = Input.GetKey(KeyCode.UpArrow);
        bool wantDown = Input.GetKey(KeyCode.DownArrow);
        bool wantLeft = Input.GetKey(KeyCode.LeftArrow);
        bool wantRight = Input.GetKey(KeyCode.RightArrow);        

        if (wantUp)
        {
            Transform trans = GetComponent<Transform>();
            Vector3 pos = trans.position;

            pos.y += 0.02f;

            trans.position = pos;
        }

        if (wantDown)
        {
            Transform trans = GetComponent<Transform>();
            Vector3 pos = trans.position;

            pos.y -= 0.02f;

            trans.position = pos;
        }

        if (wantLeft)
        {
            Transform trans = GetComponent<Transform>();
            Vector3 pos = trans.position;

            pos.x -= 0.02f;

            trans.position = pos;
        }

        if (wantRight)
        {
            Transform trans = GetComponent<Transform>();
            Vector3 pos = trans.position;

            pos.x += 0.02f;

            trans.position = pos;
        }
    }
}
