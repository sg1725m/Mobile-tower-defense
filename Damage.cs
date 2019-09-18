using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    public int minDamage, minMedium, minHard;
    public int maxDamage, maxMedium, maxHard;

    public bool enemy;


    int startDamage, startMaxDamage;
	// Use this for initialization
	void Start () {
        startDamage = minDamage;
        startMaxDamage = maxDamage;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy)
        {
            if(dynamicwaves.isEasy)
            {
                minDamage = startDamage;
                maxDamage = startMaxDamage;
            }
            else if(dynamicwaves.isMedium)
            {
                minDamage = minMedium;
                maxDamage = maxMedium;
            }
            else if (dynamicwaves.isHard)
            {
                minDamage = minHard;
                maxDamage = maxHard;
            }
        }
	}

    public int ReturnDamage()
    {
        int damage = Random.Range(minDamage, maxDamage);

        return damage;
    }
}
