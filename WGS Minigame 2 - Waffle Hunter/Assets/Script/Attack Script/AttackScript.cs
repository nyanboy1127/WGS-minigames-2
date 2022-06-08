using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{   
    public float desiredCooldown;
    public float cooldown;
    public float rayDistance;
    public float rayHeight;
    public LayerMask enemyMask;
    public bool canAttack;



    Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            cooldown = 0;
            canAttack = true;
        }

        if (Input.GetMouseButtonDown(0) && cooldown <= 0 && !CheckPlatform.isAndroid)
        {
            PlayerAttack();
        }

    }

    public void PlayerAttack()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
            Invoke("ActivateController", 1.1f);
        }
    }

    IEnumerator Attack()
    {
        Debug.Log("Attack");
        cooldown = desiredCooldown;
        canAttack = false;
        _anim.SetTrigger("Attack");
        GetComponent<PlayerController>().enabled = false;


        Ray ray = new Ray (new Vector3(transform.position.x, transform.position.y - rayHeight, transform.position.z), transform.TransformDirection(Vector3.forward * rayDistance));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, enemyMask))
        {
            if (!hit.transform.GetComponent<ShieldHandler>().shieldActivated)
            {
                yield return new WaitForSeconds(.5f);
                Debug.Log("Hit Player");
                hit.transform.GetComponent<WaffleHandler>().DecreaseWaffle();
            }
            else
            {
                Debug.Log("Shielded");
                hit.transform.GetComponent<ShieldHandler>().shieldActivated = false;
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y - rayHeight, transform.position.z), transform.TransformDirection(Vector3.forward * rayDistance));
    }

    void ActivateController()
    {
        GetComponent<PlayerController>().enabled = true;
    }
}
