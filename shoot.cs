using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {


    private Transform target;

    [Header ("Attributes")]
    public float range = 10f;
     public float fireRate = 1f;
    private float firecountDown = 0f;

    

    [Header ("unity Setup field")]
    public Transform parttorotate;
    public string enemyTag = "enemy";
    public float turnspeed = 5.0f;

    //to fire
    public GameObject bulletPrefab;
    public Transform firepoint;

	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null)
            return;

        //lock target
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(parttorotate.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        parttorotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);

        if (firecountDown <= 0f)
        {
            Shoot();
            firecountDown = 1f / fireRate;
        }

        firecountDown -= Time.deltaTime;

	}
     void Shoot()
    {
       GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        bulley bullet = bulletGO.GetComponent<bulley>();

        if (bullet != null)
            bullet.Seek(target);
    }

     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
