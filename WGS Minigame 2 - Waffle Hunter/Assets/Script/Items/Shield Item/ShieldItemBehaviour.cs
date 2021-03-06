using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItemBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(DestroyItem(col));
        }
    }

    IEnumerator DestroyItem(Collider col)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(col.GetComponent<ShieldHandler>().shieldTime);

        Destroy(gameObject);
        
    }
}
