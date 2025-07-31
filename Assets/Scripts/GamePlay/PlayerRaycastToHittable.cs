
using System;
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

    [Header("Raycasting arguments")]
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private LayerMask itemLayerMask;
    [Range(0.1f,20.0f)]
    [SerializeField] private float maxDistance = 10f;


    private List<GameObject> items;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        items = new List<GameObject>();



    }


    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    RaycastOnTouch(t.position);
                }
            }
        }
    }

    private void RaycastOnTouch(Vector3 pos)
    {

  
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f,enemyLayerMask))
        {
            EnemyShootHandling(hit);
  
        }
        else if (Physics.Raycast(ray, out hit, 10f, itemLayerMask ))
        {
            ItemShootHandling(hit);
   
        }
    }
    private void EnemyShootHandling(RaycastHit hit)
    {
        int item_rand = UnityEngine.Random.Range(0, 4);
        GameObject item = null;

        switch (item_rand)
        {
            case 0:
                item = Instantiate(clearItemPrefab, hit.transform.position, transform.rotation);
                break;
            case 1:
                item = Instantiate(plusItemPrefab, hit.transform.position, transform.rotation);
                break;
            case 2:
                item = Instantiate(fastItemPrefab, hit.transform.position, transform.rotation);
                break;
            case 3:
                item = Instantiate(slowItemPrefab, hit.transform.position, transform.rotation);
                break;
        }
        items.Add(item);

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


            enemyController.AddMoreEnemies(numOfPlusEnemies);
           
        }
        else if (hit.collider.gameObject.CompareTag("Fast"))
        {

            enemyController.ApplyFastSpeedItem(duration);
         
        }
        else if (hit.collider.gameObject.CompareTag("Slow"))
        {

            enemyController.ApplySlowSpeedItem(duration);
          
        }
        Destroy(hit.collider.gameObject);

    }
}
