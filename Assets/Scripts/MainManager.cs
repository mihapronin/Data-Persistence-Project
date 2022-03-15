using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick brickPrefab;
    public Rigidbody ball;
    public int lineCount = 6;

    //public Text scoreText;
    //public Text bestScoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI theBestScoreText;
    public GameObject gameOverText;
    
    private bool m_Started = false;
    private bool m_GameOver = false;
    private int m_Points;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        BestScore();

        AddPoint(0);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                ball.transform.SetParent(null);
                ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
            
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void BestScore()
    {
        if (DataManager.instance.playersList.Count != 0)
        {
            theBestScoreText.text = $"The best : {DataManager.instance.playersList[0].playerName} Score : {DataManager.instance.playersList[0].playerScore}";
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        currentScoreText.text = $"You : {DataManager.instance.currentPlayerName} Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        gameOverText.SetActive(true);

        DataManager.instance.playersList.Add(new Player(DataManager.instance.currentPlayerName, m_Points));
        DataManager.instance.playersList.Sort();
        DataManager.instance.playersList.Reverse();
    }
}
