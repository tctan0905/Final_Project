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
        public string name;
        public Transform[] enemywave;        
    }
    [SerializeField]
    public List<Transform> list_enemy = new List<Transform>();
    public Transform[] spawnPoint;

    public DemoWave[] _wavedemo;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    public float timeCoutdown;
    public int currentWave;

    public EnemyStage stage = EnemyStage.COUNTING;
    public Text WaveText;
    public float searchCountDown = 1f;
    private int index = 0;

    public GameObject checkGameScreen;
    public GameObject panelStartGame;
    private float timeStartGame = 4f;
    public Text timeStartGameString;
    public Text WonLoseTittle;

    MenuController replaygame;
    void Start()
    {
        currentWave = nextWave + 1;
        WaveText.text = "Wave: " + currentWave +"/4";
        timeCoutdown = 6f;
        checkGameScreen.SetActive(false);
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
            checkGameScreen.SetActive(true);
            WonLoseTittle.text = "YOU WON";


        }
        else
        {
            nextWave++;
            currentWave++;
            WaveText.text = "Wave: " + currentWave + "/4";
            index = 0;
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
        for(int i = 0;i< _waves.enemywave.Length;i++)
        {
            SpawnEnemy(_waves.enemywave);
            yield return new WaitForSeconds(2f);
        }
        stage = EnemyStage.WATTING;
        
        yield break;
    }
    void SpawnEnemy(Transform[] _enemy)
    {
        Transform _sp = spawnPoint[Random.Range(0, spawnPoint.Length)];
        Instantiate(_enemy[index], _sp.position, _sp.rotation);
        index++;

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
    public void Replay()
    {
        nextWave = 0;
        stage = EnemyStage.COUNTING;
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        //replaygame.GetComponent<MenuController>().OnButtonReplayClick();
        Debug.Log("Wave: " + nextWave.ToString());
        currentWave = nextWave + 1;
        WaveText.text = "Wave: " + currentWave + "/4";
        timeCoutdown = 6f;
        checkGameScreen.SetActive(false);
        panelStartGame.SetActive(true);
        timeStartGame = 4f;
        CountDownStartGame();

    }
}
