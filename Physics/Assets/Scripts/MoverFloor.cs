using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverFloor : MonoBehaviour
{

    [SerializeField]
    [Range(0, 100)]
    int acceleration;
    [SerializeField]
    bool resistance;
    [Range(0, 0.9f)]
    [SerializeField]
    float resistanceStrength;

    GameObject moving;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {

    }

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

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if(moving && resistance)
        {
            moving.GetComponent<MoveableCube>().movement -= moving.GetComponent<MoveableCube>().linkedMovement * resistanceStrength;
        }*/
    }
}
