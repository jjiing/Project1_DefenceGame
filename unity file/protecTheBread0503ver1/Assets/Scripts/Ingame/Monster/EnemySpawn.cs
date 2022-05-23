using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawn : MonoBehaviour
{
    
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyPrefab2;
    [SerializeField]
    private GameObject enemyPrefab3;
    [SerializeField]
    private GameObject enemyPrefab4;
    [SerializeField]
    private GameObject enemyPrefab5;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private GameObject complete;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private GameObject wave;
    private Text waveText;
    private List<Enemy> enemyList;
    private float delayTime = 7.0f;
    private int waveCount = 1;
    private bool isComplete = false;

    public InGameManager inGameManager;
    public Text GetStar;

    private void Awake()
    {
        waveText= wave.transform.GetChild(6).GetComponent<Text>();
        enemyList = new List<Enemy>();
        StartCoroutine("SpawnEnemy");
        waveCount = 1;
    }

    private void Update()
    {
        if (waveCount == 6 && enemyList.Count == 0 && !isComplete)
        {
            
            GameManager.instance.Diamond += 200;
            if (inGameManager.Life == 20)
                inGameManager.IngameStar = 3;
            else if (inGameManager.Life >= 10 && inGameManager.Life < 20)
                inGameManager.IngameStar = 2;
            else if (inGameManager.Life > 0 && inGameManager.Life < 10)
                inGameManager.IngameStar = 1;
            GameManager.instance.Star = inGameManager.IngameStar;

            GetStar.text = inGameManager.IngameStar.ToString();

            isComplete = true;
            complete.SetActive(true);

            Time.timeScale = 0;




        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(delayTime);
        wave.GetComponent<Image>().color = Color.white;
        waveText.color = Color.white;

        for (int i=0;i<6;i++)
        {
            wave.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
 
        waveCount++;
        waveText.text = "침공 " + waveCount.ToString() + "/5";
        wave.SetActive(true);
    }
    private IEnumerator SpawnEnemy()
    {
        //웨이브1
        for (int i = 0; i < 8; i++)
        {
            GameObject clone = Instantiate(enemyPrefab);
            Enemy enemy = clone.GetComponent<Enemy>();


            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);

            yield return new WaitForSeconds(spawnTime);
        }

        yield return Wait();

        //웨이브2
        for (int i = 0; i < 4; i++)
        {
            GameObject clone = Instantiate(enemyPrefab);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(spawnTime);
        }

        for (int i = 0; i < 7; i++)
        {
            GameObject clone = Instantiate(enemyPrefab2);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(spawnTime);
        }

        yield return Wait();

        //웨이브3
        for (int i = 0; i < 6; i++)
        {
            GameObject clone = Instantiate(enemyPrefab2);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(spawnTime);
        }
        for (int i = 0; i < 6; i++)
        {
            GameObject clone = Instantiate(enemyPrefab3);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(spawnTime);
        }

        yield return Wait();

        //웨이브4
        for (int i = 0; i < 6; i++)
        {
            GameObject clone = Instantiate(enemyPrefab3);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(spawnTime);
        }
        for (int i = 0; i < 6; i++)
        {
            GameObject clone = Instantiate(enemyPrefab4);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(spawnTime);
        }

        yield return Wait();

        //웨이브5
        for (int i = 0; i < 1; i++)
        {
            GameObject clone = Instantiate(enemyPrefab5);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(spawnTime);
        }

        waveCount++;
    }

    public void DestoryEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        Destroy(enemy.gameObject);
    }
}
