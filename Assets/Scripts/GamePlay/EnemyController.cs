using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class EnemyController : MonoBehaviour
{
   
    private RaycastHit2D hit;
    private Vector2[] touches = new Vector2[5];

    [Header("Enemy reference")]
    public GameObject enemyPrefab;
    [SerializeField] private int damagePerEnemy = 10;
    [SerializeField] private float speedOffsetItem = 0.5f;
    
    [Header("UI Reference")]
    [SerializeField]PlayerHealthManager playerHealthManager;
    [SerializeField]PlayerScoreManager playerScoreManager;

    [Header("Spawned Objects")]
    private List<GameObject> enemies = new();
    private List<GameObject> items = new();

    [Header("GamePlay Variables")]
    private float spawnTimer = 3.0f;
    private int enemyCount = 0;
    private float enemySpeed = 1.0f;


    [Header("Enemy Spawn Pivots")]
    [SerializeField]private Camera playerCam;

    [Range(0f,90.0f)]
    [SerializeField]private float ranFovX = 45f;
    [Range(0f, 90.0f)]
    [SerializeField] private float ranFovY = 45f;
    [SerializeField]private float RanMinZ = 5;
    [SerializeField]private float RanMaxZ = 20f;

    public const float SpawnInterval = 1.0f;

    void Update()
    {
        spawnEnemyLoop();
        moveEnemyToPlayer();
        
    
        
    }

    private void spawnEnemyLoop()
    {
        spawnTimer -= Time.deltaTime;
        
        if (spawnTimer <= 0)
        {
            spawnTimer = SpawnInterval;

            Vector3 right = playerCam.transform.right;
            Vector3 up = Vector3.up;
            Vector3 forward = playerCam.transform.forward;

            float scaleValueFov = 0.5f * (RanMaxZ + RanMinZ);
            float alphaX = ranFovX / 90.0f;
            float betaY = ranFovY / 90.0f;
            float ranX = Random.Range(-right.x * alphaX, right.x * alphaX) * scaleValueFov;
            float ranZ = Random.Range(RanMinZ, RanMaxZ);
            float ranY = Random.Range(-up.y * betaY, up.y * betaY) * scaleValueFov;

            GameObject spawnedEnemy = Instantiate(enemyPrefab, new Vector3(ranX, ranY, ranZ), transform.rotation);
            spawnedEnemy.name = enemies.Count + " alpha";
            spawnedEnemy.transform.LookAt(playerCam.transform);
            //spawnedEnemy.transform.position = Vector3.MoveTowards(spawnedEnemy.transform.position, target.position, speed);

            enemies.Add(spawnedEnemy);
        }
    }

    private void moveEnemyToPlayer()
    {
        float speed = enemySpeed * Time.deltaTime;

        for (int i = 0; i < enemies.Count; i++)
        { 
            enemies[i].transform.position = Vector3.MoveTowards(enemies[i].transform.position, playerCam.transform.position, speed);
        }
    }

    public void DestroyEnemyCollidingToPlayer(GameObject enemy)
    {
        DestroyEnemy(enemy);
        playerHealthManager.decreaseHealth(damagePerEnemy);
    }

    public void DestroyEnemyWithPlayerShot(GameObject enemy, int scorePerEnemy)
    {

        DestroyEnemy(enemy);
        playerScoreManager.AddScore(scorePerEnemy);
    }

    public void RemoveAllEnemies()
    {
        for(int i = 0;i < enemies.Count;i++)
        {
            GameObject enemy = enemies[i];

            DestroyEnemy(enemy);
            
        }
    }

    public void AddMoreEnemies(int numOfEnemies)
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            Vector3 right = playerCam.transform.right;
            Vector3 up = Vector3.up;
            Vector3 forward=playerCam.transform.forward;

            float scaleValueFov = 2.5f * (RanMaxZ + RanMinZ);
            float alphaX = ranFovX / 90.0f;
            float betaY = ranFovY / 90.0f;
            float ranX = Random.Range(-right.x * alphaX, right.x * alphaX)*scaleValueFov;
            float ranZ = Random.Range(RanMinZ, RanMaxZ);
            float ranY = Random.Range(-up.y * betaY, up.y * betaY)*scaleValueFov;

            GameObject spawnedEnemy = Instantiate(enemyPrefab, new Vector3(ranX, ranY, ranZ), transform.rotation);
            spawnedEnemy.transform.LookAt(playerCam.transform);
          
            enemies.Add(spawnedEnemy);
        }
    }

    public void ApplyFastSpeedItem(int duration)
    {
        StartCoroutine(ApplyNewSpeedForSeconds(duration, enemySpeed + speedOffsetItem, enemySpeed));
    }

    public void ApplySlowSpeedItem(int duration)
    {
        if(enemySpeed - speedOffsetItem > 0.1f)
        {
            StartCoroutine(ApplyNewSpeedForSeconds(duration, enemySpeed - speedOffsetItem, enemySpeed));
        }
    }

    IEnumerator ApplyNewSpeedForSeconds(int seconds,float newSpeed,float defaultSpeed)
    {
        enemySpeed = newSpeed;
        Debug.Log("new speed " + newSpeed);
        yield return new WaitForSeconds(seconds);
        enemySpeed = defaultSpeed;

        Debug.Log("default speed " + defaultSpeed);
    }

    

    /// <summary>
    /// Destroy the specified enemy from enemy list
    /// and corresponding gameobject.
    /// It involves the process to shrink the List<Enemy>
    /// which removes null object to be removed.
    /// </summary>
    /// <param name="enemy"></param>
    private void DestroyEnemy(GameObject enemyToDestroy)
    {
        enemies.RemoveAll(enemy=> enemy ==enemyToDestroy);
        enemies.RemoveAll(enemy => enemy == null);

        Destroy(enemyToDestroy.gameObject);

    }

    private void PrintAllEnemies()
    {
        for(int i=0; i<enemies.Count;i++)
        {
            if (enemies[i] == null)
                Debug.Log(i + " null");
            else
                Debug.Log(i + " : " + enemies[i]);

        }
    }
}
