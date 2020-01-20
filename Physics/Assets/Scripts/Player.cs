using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    LinkedMovementManager manager;

    public void PlayerRespawn()
    {
        manager.CutLinked();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000))
            {
                if (hit.transform.gameObject.GetComponent<MoveableCube>() && !hit.transform.gameObject.GetComponent<Player>())
                {
                    manager.SetLinked(hit.transform.gameObject.GetComponent<MoveableCube>());
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            manager.CutLinked();
        }
    }
}

