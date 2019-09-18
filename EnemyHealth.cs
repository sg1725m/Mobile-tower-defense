using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public int hp;
    int maxHp;
    public Image health;
    public int coins;
	public GameObject enemyvfx;
    public GameObject coin;
    int rand;
	Vector3 CoinOffset = new Vector3(0, 0.35f, 0);
    
	void Start () {
        maxHp = hp;
        rand = Random.Range(2, 3);
    }

    void Update()
    {
        health.fillAmount = Mathf.Lerp(health.fillAmount, (float)hp / (float)maxHp, Time.deltaTime);
    }
    

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            print(rand);

            if (dynamicwaves.isEasy)
            {
                rand = Random.Range(6, 8);
            }
            else if (dynamicwaves.isMedium)
            {
                rand = Random.Range(2, 3);
            }
            else if (dynamicwaves.isHard)
            {
                rand = Random.Range(1, 2);
            }

            hp = 0;
            for (int i = 0; i < rand; i++)
            {
                Instantiate(coin, transform.position + CoinOffset, Quaternion.identity);
            }
			Instantiate(enemyvfx, transform.position, Quaternion.identity);
            MyData.Enemiesalive--;
            Destroy(gameObject);
        }
    }
}
