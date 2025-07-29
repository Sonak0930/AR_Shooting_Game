using UnityEngine;
using UnityEngine.Events;
public class PlayerCollisionBox : MonoBehaviour
{
    [SerializeField] private string tagForEnemy = "Enemy";
    [SerializeField] private EnemyController enemyController;


    public UnityEvent onPlayerCollided;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagForEnemy))
        {
            enemyController.DestroyEnemyCollidingToPlayer(other.gameObject);
        }
    }


}
