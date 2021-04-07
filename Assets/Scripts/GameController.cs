using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public float startWait;
    public int enemyCount;
    public int currentEnemyCount;
    public int[] enemySpawnCount;
    public int levelCount;

    public int expEarned;

    public enum gameStates { StartLevel, Playing, Death, GameOver, BeatLevel, ViewingStats };
    public gameStates gameState = gameStates.Playing;

    public GameObject[] enemies;
    public GameObject player;

    private PlayerController playerCtrl;

    public GameObject floor;
    public Vector3 spawnValues;
    public float spawnWait;
    public Text currentLevelText;

    public GameObject gameOverCanvas;
    public GameObject mainCanvas;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (gameState == gameStates.Playing)
        {
            playerCtrl = player.GetComponent<PlayerController>();
            currentEnemyCount = enemySpawnCount[levelCount];
            StartCoroutine(SpawnWaves());
            UpdateLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case gameStates.StartLevel:
                StartCoroutine(SpawnWaves());
                break;
            case gameStates.Playing:
                if (!playerCtrl.isAlive)
                {
                    gameState = gameStates.Death;

                    mainCanvas.SetActive(false);
                    gameOverCanvas.SetActive(true);
                }
                else if (currentEnemyCount <= 0)
                {
                    gameState = gameStates.BeatLevel;

                    mainCanvas.SetActive(false);
                    //levelEnemyCount = enemyCount + 5;
                    levelCount++;
                    expEarned = Mathf.CeilToInt(levelCount / 2);
                    UpdateLevel();
                }
                break;
            case gameStates.Death:
                break;
            case gameStates.GameOver:
                break;
            case gameStates.BeatLevel:
                SceneManager.LoadScene("StatsScene");
                gameState = gameStates.ViewingStats;
                break;
            case gameStates.ViewingStats:

                break;
        }

    }

    IEnumerator SpawnWaves()
    {
        currentEnemyCount = enemySpawnCount[levelCount];
        yield return new WaitForSeconds(startWait);

        for (int i = 0; i < enemySpawnCount[levelCount]; i++)
        {
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            enemy.GetComponent<EnemyFollow>().player = player;

            float width = ((floor.GetComponent<MeshRenderer>().bounds.size.x) / 2) - 1;
            Vector3 spawnPosition = new Vector3(Random.Range(-width, width), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;

            Instantiate(enemy, spawnPosition, spawnRotation);

            yield return new WaitForSeconds(spawnWait);
        }
    }

    void UpdateLevel()
    {
        currentLevelText.text = "Round: " + (levelCount + 1);
    }
}
