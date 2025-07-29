using ProjectTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private GameObject healthReference;
    [SerializeField] private int health = 100;
    private Text healthText;
    
    // Start is called before the first frame update
    void Start()
    {
        healthText = healthReference.GetComponent<Text>();
        healthText.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Debug.Log("Hi");
            health-=10;
            healthText.text = health.ToString();
        }
    }

    public void decreaseHealth(int damage)
    {
        if(this.health-damage<=0)
        {
            GameState.RoundWonHandler round 
        }
        this.health -= damage;
    }

    public void increaseHealth(int health)
    {
        this.health += health;
    }
}
