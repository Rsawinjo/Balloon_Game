using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theMover : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float moveSpeed;
    public float distanceTravelledForward;
    public bool isActive;
    public bool isMovingFoward;
    private void FixedUpdate()
    {
        if (isActive == false) return;

        Vector3 forwardVector = endPosition - startPosition;
        Vector3 backwardVector = -forwardVector;

        Vector3 forwardDirection = forwardVector.normalized;
        Vector3 backwardDirection = backwardVector.normalized;

        float dt = Time.fixedDeltaTime;

        float distanceTravelledThisFrame = moveSpeed * dt;

        Vector3 forwardMovement = forwardDirection * distanceTravelledThisFrame;
        Vector3 backwardMovement = backwardDirection * distanceTravelledThisFrame;
        Vector3 position = transform.position;

        if (isMovingFoward)
        {
            transform.position = position + forwardMovement;
            distanceTravelledForward += distanceTravelledThisFrame;
        }
        else // move backward
        {
            transform.position = position + backwardMovement;
            distanceTravelledForward -= distanceTravelledThisFrame;
        }
        if (distanceTravelledForward > forwardVector.magnitude) // reached end?
        {
            isMovingFoward = false;
        }
        if (distanceTravelledForward < 0.0f) // reached start?
        {
            isMovingFoward = true;
        }
    }

    private void OnDrawGizmos()
    {
        Color drawColor = Color.blue;
        DrawMovementLine(drawColor);
    }

    private void OnDrawGizmosSelected()
    {
        Color drawColor = Color.white;
        DrawMovementLine(drawColor);
    }

    void DrawMovementLine(Color drawColor)
    {
        var WorldPos = transform.position;

        Gizmos.color = drawColor;
        Gizmos.DrawLine(WorldPos + startPosition, WorldPos + endPosition);

    }

}