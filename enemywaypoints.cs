using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemywaypoints : MonoBehaviour
{

    public PlayerHealth player;

    public bool isAttacking;

    float timer;
    public float attackSpeed;

    public Transform head;

    Animator anim;

    //int currentWP = 0;
    public float rotSpeed = 0.2f;
    public float speed = 3f;
    //  float accuracyWP = 5.0f;
    Vector3 direction;

    GameObject[] Waypoints;
    int waypointID;
    bool isEnd;
    int minDmg, maxDmg;

    void Start()
    {
        minDmg = GetComponent<Damage>().minDamage;
        maxDmg = GetComponent<Damage>().maxDamage;
        anim = GetComponent<Animator>();
        Waypoints = transform.parent.GetComponent<Waypoint>().Waypoints;
    }

    void Update()
    {
        direction.y = 0;

        if (FindClosestPlayer() && Vector3.Distance(FindClosestPlayer().transform.position, transform.position) < 2f)
        {
            direction = FindClosestPlayer().transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);


            if (direction.magnitude > 1)
            {

                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }
            else 
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
                player = FindClosestPlayer().GetComponentInParent<PlayerHealth>();
                timer += Time.deltaTime;
                if (timer >= attackSpeed)
                {
                    player.TakeDamage(Random.Range(minDmg, maxDmg));
                    timer = 0;
                }
                else 
                {
                    if (player.hp <= 0)
                    {
                        anim.SetBool("isAttacking", false);
                        anim.SetBool("isWalking", true);
                    }
                   
                }
            }
        }
        else if (FindClosestTree() && Vector3.Distance(FindClosestTree().transform.GetChild(0).position, transform.position) < 5f)
        {
            anim.SetBool("isWalking", true);
            //if (Vector3.Distance(FindClosestTree().transform.position, transform.position) < accuracyWP)
            //{

            //    currentWP++;
            //    if (currentWP >= waypoints.Length)
            //    {
            //        currentWP = 0;
            //        return;
            //    }
            //}
            direction = FindClosestTree().transform.GetChild(0).position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            if (Vector3.Distance(FindClosestTree().transform.GetChild(0).position, transform.position) < 0.7f)
            {
                FindClosestTree().GetComponent<TreeHealth>().TakeDamage(100);
                Destroy(gameObject);
            }
        }
        else if (!isEnd && Vector3.Distance(Waypoints[waypointID].transform.position, transform.position) > .5f)
        {
            anim.SetBool("isWalking", true);

            if (Waypoints[waypointID].transform != null)
            {
                direction = Waypoints[waypointID].transform.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            }
        }
        else if (!isEnd)
        {
            if (waypointID >= Waypoints.Length - 1)
            {
                isEnd = true;
            }
            else
                waypointID++;
        }
        else if (FindClosestTree())
        {
            anim.SetBool("isWalking", true);
            //if (Vector3.Distance(FindClosestTree().transform.position, transform.position) < accuracyWP)
            //{

            //    currentWP++;
            //    if (currentWP >= waypoints.Length)
            //    {
            //        currentWP = 0;
            //        return;
            //    }
            //}
            direction = FindClosestTree().transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            if (Vector3.Distance(FindClosestTree().transform.position, transform.position) < 0.5f)
            {
                FindClosestTree().GetComponent<TreeHealth>().TakeDamage(100);
                Destroy(gameObject);
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
    }

    public GameObject FindClosestPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
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

    public GameObject FindClosestTree()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Tree");
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
}

