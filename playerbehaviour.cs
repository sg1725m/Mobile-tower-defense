using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerbehaviour : MonoBehaviour {
    
    static Animator anim;
    public float speed = 2f;
    public float range = 3f;
    float dist;
    Vector3 startPos;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        startPos = transform.position;
    }
    
    void Update()
    {
        if (FindClosestEnemy())
        {
            dist = Vector3.Distance(transform.position, FindClosestEnemy().transform.position);
        }

        if (dist <= range && FindClosestEnemy())
        {
            Vector3 direction = FindClosestEnemy().transform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                       Quaternion.LookRotation(direction), 0.1f);

            if (direction.magnitude > 1f)
            {
                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
            }
            else if (dist >= .5f)
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", false);
            }
        }
        else if(!FindClosestEnemy())
        {
            Vector3 direction = startPos - transform.position;

            if (direction.magnitude > .5f)
            {
                direction.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                           Quaternion.LookRotation(direction), 0.1f);

                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
            }
            else if (direction.magnitude <= .1f)
            {
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
            }
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public string opponent;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
