using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    public GameObject[] spawnLocation = new GameObject[2];
    public GameObject[] enemyTypes;
    public LayerMask[] layerSpawn = new LayerMask[2];

    public List<GameObject> spawnNumber = new List<GameObject>();

    private int maxSpawnLoops, level;
    private float spawnTime;

    private void Awake()
    {
        spawnTime = 10f;
        level = 1;
        maxSpawnLoops = 10;
        SummonEnemy();
    }


    public void SummonEnemy()
    {
        GameManager.currentEnemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)]);
        spawnNumber.Add(GameManager.currentEnemy);
        GameManager.currentEnemy.GetComponent<BaseChar>().teamTag = "Enemy";
        GameManager.currentEnemy.GetComponent<BaseChar>().enemyTag = "Player";
        GameManager.currentEnemy.GetComponent<BaseChar>().fliped = true;

        int layer = Random.Range(0, layerSpawn.Length);

        GameManager.currentEnemy.GetComponent<BaseChar>().yourLayer = layerSpawn[layer];

        if (layer == 0)
        {

            GameManager.currentEnemy.GetComponent<BaseChar>().size = 1.2f;
            GameManager.currentEnemy.GetComponent<Renderer>().sortingOrder = 5;
        }
        else
        {
            GameManager.currentEnemy.GetComponent<BaseChar>().size = 1.1f;
            GameManager.currentEnemy.GetComponent<Renderer>().sortingOrder = 4;
        }

        GameManager.currentEnemy.transform.position = new Vector3(spawnLocation[layer].transform.position.x,
                                                                  spawnLocation[layer].transform.position.y,
                                                                     0);
        GameManager.currentEnemy = null;
        LevelCheck();
    }

    void LevelCheck()
    {
        if (maxSpawnLoops > spawnNumber.Count)
        {
            StartCoroutine("SpawnTime");
        }
        else
        {
            for (int i = 0; i < spawnNumber.Count; i++)
            {
                if (spawnNumber[i] != null)
                {
                    StartCoroutine("VerifyEnemies");
                    return;
                }
                else
                {
                    if (i == spawnNumber.Count - 1) {

                        level++;
                        spawnNumber = new List<GameObject>();

                        switch (level)
                        { case 1:
                                maxSpawnLoops = 20;
                                spawnTime = 5f;
                                break;
                            case 2:
                                maxSpawnLoops = 30;
                                spawnTime = 4f;
                                break;
                            case 3:
                                maxSpawnLoops = 40;
                                spawnTime = 3f;
                                break;
                            case 4:
                                maxSpawnLoops = 50;
                                spawnTime = 2f;
                                break;
                            default:
                                maxSpawnLoops = 100;
                                spawnTime = 1f;
                                break;
                        }

                        SummonEnemy();
                    }
   
                }
            }
        }
    }

    private IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(spawnTime);
        SummonEnemy();
    }
    private IEnumerator VerifyEnemies()
    {
        yield return new WaitForSeconds(1f);
        LevelCheck();
    }
}
