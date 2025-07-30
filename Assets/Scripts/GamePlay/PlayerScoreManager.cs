using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour
{

    [SerializeField]private GameObject scoreReference;
    [SerializeField]private int score = 0;
    [SerializeField]private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI scoreUI;
    private Text scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = scoreReference.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = score + "";
    }

    private void OnEnable()
    {
        gameOverUI.SetActive(false);
        scoreUI.text = "";
        score = 0;
    }
    public void DisplayGameOverScore()
    {
        gameOverUI.SetActive(true);
        scoreUI.text = "" + score;
    }
}
