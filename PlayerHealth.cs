using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int hp;
    int maxHp;
    public Image health;
    public bool isPanda;

    void Start()
    {
        maxHp = hp;
    }

    void Update()
    {
        health.fillAmount = Mathf.Lerp(health.fillAmount, (float)hp / (float)maxHp, Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.transform.tag == "enemy" && !isPanda)
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(GetComponent<Damage>().ReturnDamage());
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            Destroy(gameObject);
        }
    }
}
