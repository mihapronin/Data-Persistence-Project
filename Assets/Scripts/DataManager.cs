using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public string currentPlayerName;
    public List<Player> playersList;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SavePlayersList()
    {
        SavedPlayersList data = new SavedPlayersList();
        data.players = playersList;

        string jsonData = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/playerslist.json", jsonData);
    }

    public void LoadPlayersList()
    {
        string path = Application.persistentDataPath + "/playerslist.json";

        if (File.Exists(path))
        {
            string jsonAdata = File.ReadAllText(path);
            SavedPlayersList data = JsonUtility.FromJson<SavedPlayersList>(jsonAdata);

            playersList = data.players;
        }
    }
}

class SaveTestData
{
    public string playerName;
}

[System.Serializable]
class SavedPlayersList
{
    public List<Player> players;
}

[System.Serializable]
public class Player : IComparable<Player>
{
    public string playerName;
    public int playerScore;

    public Player(string newPlayerName, int newPlayerScore)
    {
        playerName = newPlayerName;
        playerScore = newPlayerScore;
    }

    public int CompareTo(Player other)
    {
        if (other == null)
        {
            return 1;
        }
        return playerScore - other.playerScore;
    }
}


