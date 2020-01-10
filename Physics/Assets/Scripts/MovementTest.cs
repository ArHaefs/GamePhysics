using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    Vector3 movement;
    [SerializeField]
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void changeMovementForce(Vector3 vect)
    {
        movement += vect;
    }

    public Vector3 retMovement()
    {
        return movement;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(movement);
        body.AddForce(movement, ForceMode.Force);
    }
}
