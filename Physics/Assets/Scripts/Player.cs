using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Linked;
    [SerializeField]
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = Linked.GetComponent<Rigidbody>().velocity;
        Linked.GetComponent<Rigidbody>().velocity = body.velocity;
    }
}
