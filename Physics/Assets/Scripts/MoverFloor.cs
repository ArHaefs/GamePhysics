using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverFloor : MonoBehaviour
{

    [SerializeField]
    [Range(0, 100)]
    int acceleration;

    GameObject moving;
    Vector3 movement;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MoveableCube>())
        {
            movement = transform.forward * acceleration;
            collision.gameObject.GetComponent<MoveableCube>().ChangeMovementForce(movement, this.gameObject);
            moving = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<MoveableCube>() && moving != null)
        {
            movement = -transform.forward * acceleration;
            collision.gameObject.GetComponent<MoveableCube>().ChangeMovementForce(movement, null);
            moving = null;
        }
    }

    public void StopInfluence()
    {
        moving = null;
    }

}
