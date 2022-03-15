using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TopListUIHandler : MonoBehaviour
{
    public TextMeshProUGUI topListNames;
    public TextMeshProUGUI topListScore;

    void Start()
    {
        TopListToScreen();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void TopListToScreen()
    {
        string topListScreenNames = "";
        string topListScreenScore = "";
        int n = 0;

        if (DataManager.instance.playersList.Count != 0)
        {
            foreach (Player player in DataManager.instance.playersList.GetRange(0, DataManager.instance.playersList.Count))
            {
                n++;
                topListScreenNames += n + ". " + player.playerName + "\r\n";
                topListScreenScore += player.playerScore + "\r\n";
            }
        }

        topListNames.text = topListScreenNames;
        topListScore.text = topListScreenScore;
    }
}
