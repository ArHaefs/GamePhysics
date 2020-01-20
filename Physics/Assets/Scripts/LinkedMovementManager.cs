using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedMovementManager : MonoBehaviour
{
    public MoveableCube player;
    public MoveableCube moveableCube;
    bool isLinked = false;
    bool oneGrounded = false;

    public void SetLinked(MoveableCube newCube)
    {
        moveableCube = newCube;
        isLinked = true;
        player.isLinked = true;
        moveableCube.isLinked = true;
        player.player = player.gameObject;
        moveableCube.player = player.gameObject;
        if (moveableCube.grounded)
        {
            player.SetLinkedGrounded(true, player.gameObject);
            oneGrounded = true;
        }
        else if (player.grounded)
        {
            moveableCube.SetLinkedGrounded(true, player.gameObject);
            oneGrounded = true;
        }
    }

    public void CutLinked()
    {
        player.isLinked = false;
        moveableCube.isLinked = false;
        player.SetLinkedGrounded(false, player.gameObject);
        moveableCube.SetLinkedGrounded(false, player.gameObject);
        player.player = null;
        moveableCube.player = null;
        moveableCube = null;
        isLinked = false;
    }

    void FixedUpdate()
    {
        if (isLinked)
        {
            player.CheckIfGrounded();
            moveableCube.CheckIfGrounded();
            if (oneGrounded == true)
            {
                if (moveableCube.GetLinkedGrounded() && !player.grounded)
                {
                    moveableCube.SetLinkedGrounded(false, player.gameObject);
                    oneGrounded = false;
                }
                else if (player.GetLinkedGrounded() && !moveableCube.grounded)
                {
                    player.SetLinkedGrounded(false, player.gameObject);
                    oneGrounded = false;
                }
            }
            if (oneGrounded == false)
            {
                if (moveableCube.grounded)
                {
                    player.SetLinkedGrounded(true, player.gameObject);
                    oneGrounded = true;
                }
                else if (player.grounded)
                {
                    moveableCube.SetLinkedGrounded(true, player.gameObject);
                    oneGrounded = true;
                }
            }
            NewVelocity(player.body.velocity, moveableCube.body.velocity);

            Vector3 combinedMovement = player.movement + moveableCube.movement;
            player.body.AddForce(combinedMovement * 10, ForceMode.Force);
            player.linkedMovement = combinedMovement;
            moveableCube.body.AddForce(combinedMovement * 10, ForceMode.Force);
            moveableCube.linkedMovement = combinedMovement;
        }
    }

    void NewVelocity(Vector3 velocityPlayer, Vector3 velocityCube)
    {
        float x = velocityPlayer.x;
        float y = velocityPlayer.y;
        float z = velocityPlayer.z;

        if (Mathf.Abs(x) > Mathf.Abs(velocityCube.x)) x = velocityCube.x;
        if (Mathf.Abs(z) > Mathf.Abs(velocityCube.z)) z = velocityCube.z;
        y = FindCorrectY(velocityPlayer.y, velocityCube.y);

        Vector3 newVelocity = new Vector3(x, y, z);
        player.body.velocity = newVelocity;
        moveableCube.body.velocity = newVelocity;
    }

    float FindCorrectY(float velocityPlayerY, float velocityCubeY)
    {
        float y = 0;

        if (moveableCube.grounded && !player.grounded) y = velocityCubeY;
        else if (!moveableCube.grounded && player.grounded) y = velocityPlayerY;
        else if (Mathf.Abs(velocityPlayerY) <= Mathf.Abs(velocityCubeY)) y = velocityPlayerY;
        return y;
    }
}
