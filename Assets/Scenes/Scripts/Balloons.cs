using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloons : MonoBehaviour
{
    void Start()
    {
        
    }

    public Sprite ropeSprite;

    internal Transform ropeTrans;
    // Update is called once per frame
    GameObject makeRope()
    {
        GameObject ropeGO = new GameObject("rope");
        var ropeRend = ropeGO.AddComponent<SpriteRenderer>();

        ropeRend.sprite = ropeSprite;

        return ropeGO;
    }
    private void OnEnable()
    {
        var ropeGO = makeRope();
        ropeTrans = ropeGO.transform;
    }

    private void OnDisable()
    {
        if (ropeTrans != null)
        {
            Destroy(ropeTrans.gameObject);
        }
    }

    private void Update()
    {
        // Setup Position
        //AnchoredJoint2D springStart = GetComponent<AnchoredJoint2D>();
        //Vector3 startPoint = springStart;
        SpringJoint2D spring = GetComponent<SpringJoint2D>();


        Vector3 startPoint = transform.TransformPoint(spring.anchor);
        //Vector3 startPoint = spring.anchor;
        //Vector3 BalloonAnchor;
        //BalloonAnchor = springStart.connectedAnchor;
        //AnchoredJoint2D BalloonAnchor = GetComponent<BaseBalloon.connectedAnchor>(); // HW: This should use the anchor point of the spring.
        Vector3 endPoint;

        if (spring.connectedBody == null)
        {
            endPoint = spring.connectedAnchor;
        }

        else
        {
            var pc = DynamicPlayerController.g_singleton;
            endPoint = pc.transform.TransformPoint(spring.connectedAnchor);
        }

        var vecToEnd = endPoint - startPoint;
        var vecHalfToEnd = vecToEnd * 0.5f;
        var ropePosition = startPoint + vecHalfToEnd;
        ropeTrans.position = ropePosition;

        //Setup Rotation

        var vecToStart = -vecToEnd;
        float angleRad = Mathf.Atan2(vecToStart.y, vecToStart.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angleDeg, Vector3.forward);
        ropeTrans.rotation = rotation;

        //Setup Scale

        Vector3 newScale = new Vector3(
            vecToEnd.magnitude,   // x
            0.25f,   // y
            1.0f);   // z 
        ropeTrans.localScale = newScale;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DynamicPlayerController player = collision.GetComponent<DynamicPlayerController>();

        if (player == null)
        {
            return;
        }

        SpringJoint2D balloonSpringJoint = this.GetComponent<SpringJoint2D>();
        if (balloonSpringJoint.connectedBody != null)
        {
            return;
        }

        Rigidbody2D playerRigidBody = player.GetComponent<Rigidbody2D>();
        
        balloonSpringJoint.connectedBody = playerRigidBody;
        balloonSpringJoint.connectedAnchor = Vector2.up * 0.5f;


    }
}
