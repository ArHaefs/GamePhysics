using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverFloor : MonoBehaviour
{

    [SerializeField]
    [Range(-10, 10)]
    int downAndUp, leftAndRight;

    GameObject Moving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MovementTest>())
        {
            collision.gameObject.GetComponent<MovementTest>().changeMovementForce(new Vector3(leftAndRight, 0, downAndUp));
            Moving = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<MovementTest>())
        {
            collision.gameObject.GetComponent<MovementTest>().changeMovementForce(new Vector3(-leftAndRight, 0, -downAndUp));
            Moving = null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
