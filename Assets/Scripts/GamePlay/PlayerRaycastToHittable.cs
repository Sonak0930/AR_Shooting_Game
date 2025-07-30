using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerRaycastToHittable : MonoBehaviour
{
    [Header("Reference to Player/Enemy")]
    [SerializeField] private PlayerScoreManager playerScoreManager;
    [SerializeField] private EnemyController enemyController;

    [Header("Item Effect Constants")]
    [SerializeField] private int scorePerEnemy = 10;
    [SerializeField] private int numOfPlusEnemies = 10;
    [SerializeField] private int duration = 3;

    [Header("Enemy and Item Prefabs")]
    [SerializeField] private GameObject clearItemPrefab;
    [SerializeField] private GameObject slowItemPrefab;
    [SerializeField] private GameObject fastItemPrefab;
    [SerializeField] private GameObject plusItemPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField]private LayerMask enemyLayerMask;
    [SerializeField]private LayerMask itemLayerMask;



    private List<GameObject> items;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        items= new List<GameObject>();
    }


    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (Input.GetTouch(t.fingerId).phase == TouchPhase.Began)
                {
                    checkTouch(Input.GetTouch(0).position);
                }
            }

            void checkTouch(Vector3 pos)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, enemyLayerMask))
                {
                    if (hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        EnemyShootHandling(hit);
                    }
                }
                if (Physics.Raycast(ray, out hit, itemLayerMask))
                {
                    
                }
            }
        }
    }

    private void EnemyShootHandling(RaycastHit hit)
    {
        int item_rand = Random.Range(1, 101);
        if (item_rand >= 1 && item_rand <= 10)
        {
            items.Add(Instantiate(clearItemPrefab, hit.transform.position, transform.rotation));
        }
        else if (item_rand >= 11 && item_rand <= 20)
        {
            items.Add(Instantiate(plusItemPrefab, hit.transform.position, transform.rotation));
        }
        else if (item_rand >= 21 && item_rand <= 30)
        {
            items.Add(Instantiate(fastItemPrefab, hit.transform.position, transform.rotation));
        }
        else if (item_rand >= 31 && item_rand <= 40)
        {
            items.Add(Instantiate(slowItemPrefab, hit.transform.position, transform.rotation));
        }
        enemyController.DestroyEnemyWithPlayerShot(hit.collider.gameObject, scorePerEnemy);
       
    }

    private void ItemShootHandling(RaycastHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Clear"))
        {
            enemyController.RemoveAllEnemies();
                    
        }
        else if (hit.collider.gameObject.CompareTag("Plus"))
        {
            Destroy(hit.collider.gameObject);

            enemyController.AddMoreEnemies(numOfPlusEnemies);
        }
        else if (hit.collider.gameObject.CompareTag("Fast"))
        {
            Destroy(hit.collider.gameObject);
            enemyController.ApplyFastSpeedItem(duration);
        }
        else if (hit.collider.gameObject.CompareTag("Slow"))
        {
            Destroy(hit.collider.gameObject);
            enemyController.ApplySlowSpeedItem(duration);
        }
    }
}
