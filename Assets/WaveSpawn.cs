using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawn : MonoBehaviour
{
    public enum EnemyStage { SPAWNING, WATTING, COUNTING}
    [SerializeField]
    //public List<GameObject> list_enemy1 = new List<GameObject>();
    //public List<GameObject> list_enemy2 = new List<GameObject>();
    //public List<GameObject> list_enemy3 = new List<GameObject>();
    //public List<GameObject> list_enemy4 = new List<GameObject>();
    public Transform[] spawnPoint;

    public EnemyHeathManager[] EHM;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    public float timeCoutdown;

    public EnemyStage stage = EnemyStage.COUNTING;
    public Text WaveText;
    public float searchCountDown = 1f;
    void Start()
    {
        int currentWave = nextWave + 1;
        WaveText.text = "Wave: " + currentWave +"/4";
        timeCoutdown = timeBetweenWaves;
    }
    //public EnemyHeathManager GetEnemy()
    //{
    //    //foreach(var enemys in list_enemy1)
    //    //{
    //    //    if(enemys.activeSelf)
    //    //    {
    //    //        return enemys;
    //    //    }

    //    //}

    //    //EnemyHeathManager enemy = new EnemyHeathManager()
    //    //{
    //    //    enemyPrefabs = Instance(_enemyPrefabs),
    //    //};
    //    //enemy
    //    return false;
    //}
    void Update()
    {
        if(stage == EnemyStage.WATTING)
        {
            if(!EnemyisAlive())
            {
                WaveComplete();
            }
            else
            {

            }
        }
        if(timeCoutdown <= 0)
        {
            if(stage != EnemyStage.SPAWNING)
            {
                StartCoroutine(SpawnWave(EHM[nextWave]));
            }
        }
        else
        {
            timeCoutdown -= Time.deltaTime;
        }
        //if(timeBetweenCoutdown <=0)
        //{

        //}
        //for(int i = 0;i< list_enemy1.Count;i++)
        //{
        //    GameObject ene = Instantiate(list_enemy1[i], cubecreate.position, Quaternion.identity);

        //}

    }
    void WaveComplete()
    {
        Debug.Log("Wave Complete");
        if(nextWave +1 > 4)
        {
            Debug.Log("ALL WAVES COMPLETE");
            
        }
        else
        {
            nextWave++;
        }
    }
    bool EnemyisAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <=0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                return false;
            }
        }
        
        return true;
    }

    IEnumerator SpawnWave(EnemyHeathManager _waveEnemy)
    {
        stage = EnemyStage.SPAWNING;
        for(int i = 0;i< 2;i++)
        {
            SpawnEnemy(_waveEnemy.enemys);
            yield return new WaitForSeconds(1f);
        }
        stage = EnemyStage.WATTING;
        
        yield break;
    }
    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoint[Random.Range(0, spawnPoint.Length)];
        Instantiate(_enemy, _sp.position,_sp.rotation);
    }
}
