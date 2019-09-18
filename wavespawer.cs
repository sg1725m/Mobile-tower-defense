using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wavespawner : MonoBehaviour
{
    public waveinfo[] waves;
    
    public Transform[] spawnpoints;

    private float timeBetweenWaves = 6f;
    private float countdown = 5f;

    public Text wavecountdowntext;
    public Text waveCounter;

    public int waveIndex = 0;
    int waveLength = 0;
	public GameObject worldCanvas;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;

        }
        countdown -= Time.deltaTime;
        if(waveIndex == 0)
            if(wavecountdowntext != null)
                wavecountdowntext.text = countdown.ToString("F0");
        else if (wavecountdowntext != null)
        {
            wavecountdowntext.text = countdown.ToString("F0");
            if(worldCanvas)
			    worldCanvas.SetActive(false);
		}
        waveCounter.text = waveLength.ToString() + " / " + waves.Length.ToString();

    }

    IEnumerator SpawnWave()
    {
        if (waveLength == 0)
        {
            waveLength++;
        }
        waveinfo waveinfo = waves[waveIndex];
        timeBetweenWaves =  waveinfo.rate;
        for (int i = 0; i < waveinfo.count; i++)
        {
            for (int j = 0; j < waveinfo.enemies.Length; j++)
            {
                SpawnEnemy(waveinfo.enemies[j]);
            }
            yield return new WaitForSeconds( waveinfo.rate);
        }
        waveLength++;
        waveIndex++;
        if (waveIndex == waves.Length)
        {
            MyData.isWin = true;
            waveCounter.gameObject.SetActive(false);
            wavecountdowntext.gameObject.SetActive(false);
            this.enabled = false;
        }
    }
    void SpawnEnemy(GameObject enemy)
    {
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            Instantiate(enemy, spawnpoints[i].position, spawnpoints[i].rotation, spawnpoints[i]);
        }
        MyData.Enemiesalive += 2;
    }
}
