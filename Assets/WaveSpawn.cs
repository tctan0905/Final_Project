using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawn : MonoBehaviour
{
    public enum EnemyStage { SPAWNING, WATTING, COUNTING}
    [System.Serializable]
    public class DemoWave
    {
        public Transform enemywave;
        public int count;
        
    }
    [SerializeField]
    public List<Transform> list_enemy = new List<Transform>();
    public Transform[] spawnPoint;

    public EnemyHeathManager[] EHM;
    public DemoWave[] _wavedemo;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    public float timeCoutdown;

    public EnemyStage stage = EnemyStage.COUNTING;
    public Text WaveText;
    public float searchCountDown = 1f;

    public GameObject panelGame;
    public GameObject panelStartGame;
    private float timeStartGame = 6f;
    public Text timeStartGameString;
    
    void Start()
    {
        int currentWave = nextWave + 1;
        WaveText.text = "Wave: " + currentWave +"/4";
        timeCoutdown = timeBetweenWaves;
        panelGame.SetActive(false);
        panelStartGame.SetActive(true);
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
        CountDownStartGame();
        if (stage == EnemyStage.WATTING)
        {
            //Check enemy is alive  
            if(!EnemyisAlive())
            {
                WaveComplete();
            }
            else
            {
                return;
            }
        }
        if(timeCoutdown <= 0)
        {
            if(stage != EnemyStage.SPAWNING)
            {
                StartCoroutine(SpawnWave(_wavedemo[nextWave]));
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
        timeCoutdown = timeBetweenWaves;
        stage = EnemyStage.COUNTING;
        if(nextWave + 1  > _wavedemo.Length-1)
        {
            Debug.Log("ALL WAVES COMPLETE");
            stage = EnemyStage.WATTING;
            panelGame.SetActive(true);
            
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
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        
        return true;
    }

    IEnumerator SpawnWave(DemoWave _waves)
    {
        stage = EnemyStage.SPAWNING;
        for(int i = 0;i< _waves.count;i++)
        {
            SpawnEnemy(_waves.enemywave);
            yield return new WaitForSeconds(2f);
        }
        stage = EnemyStage.WATTING;
        
        yield break;
    }
    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoint[Random.Range(0, spawnPoint.Length)];
        Transform _ene = Instantiate(_enemy, _sp.position,_sp.rotation);
        list_enemy.Add(_ene);
    }
    public void CountDownStartGame()
    {
        timeStartGame  -= Time.deltaTime;
        int compileint = (int)timeStartGame;
        timeStartGameString.text = compileint.ToString();
        if(timeStartGame <0)
        {
            panelStartGame.SetActive(false);
        }
    }
}
