using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableCube : MonoBehaviour
{
    public Vector3 movement;
    public Vector3 linkedMovement;
    [SerializeField]
    public Rigidbody body;
    float previousDownVelocity = 1;
    public bool isLinked = false;
    public bool grounded = false;
    public bool linkedGrounded = false;
    public GameObject player;

    public Vector3 respawnPoint;

    GameObject movedBy;
    Vector3 movedByVector;
    void Start()
    {
        respawnPoint = body.transform.position;
    }

    public void ChangeMovementForce(Vector3 vect, GameObject mover)
    {
        if (mover != movedBy && movedBy != null && mover != null && movedByVector == vect)
        {
            movedBy.SendMessage("StopInfluence");
            movement -= movedByVector;
            movedBy = null;
        }

        movedByVector = vect;
        movedBy = mover;
        movement += vect;
    }

    public Vector3 RetMovement()
    {
        return movement;
    }

    public void SetLinkedGrounded(bool newState, GameObject player)
    {
        if (newState)
        {
            linkedGrounded = true;
            body.useGravity = false;
        }
        else if (!newState)
        {
            linkedGrounded = false;
            body.useGravity = true;
        }
        player = this.player;
    }

    public bool GetLinkedGrounded()
    {
        return linkedGrounded;
    }

    void FixedUpdate()
    {

        if (!isLinked)
        {
            CheckIfGrounded();
            body.AddForce(movement * 10, ForceMode.Force);
        }
        if (body.transform.position.y < -10)
        {
            Respawn();
        }
    }

    public void CheckIfGrounded()
    {
        if (!linkedGrounded)
        {
            if (!movedBy) grounded = false;
            if (Mathf.Approximately(previousDownVelocity, 0) && Mathf.Approximately(0, body.velocity.y)) grounded = true;
        }
        previousDownVelocity = body.velocity.y;
    }

    void Respawn()
    {
        if (isLinked) player.BroadcastMessage("PlayerRespawn");
        body.velocity *= 0;
        previousDownVelocity = 1;
        movement *= 0;
        player = null;
        body.position = respawnPoint;
    }
}
