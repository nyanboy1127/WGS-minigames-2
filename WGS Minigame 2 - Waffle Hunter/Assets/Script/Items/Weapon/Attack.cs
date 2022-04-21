using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float range = 5f;


    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range));

        if (Physics.Raycast(theRay, out RaycastHit hit, range))
        {

            if (Input.GetButtonDown("Fire1"))
            {
                if (hit.collider.tag == "Player")
                {
                    print("Attack player");
                    gameObject.SetActive(false);
                }
                else
                {
                    print("Belum kena player");
                }
            }
        }
    }
}