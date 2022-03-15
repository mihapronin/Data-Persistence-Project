using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField enteredName;
    public GameObject hintNameText;
    private string playerName;

    // Start is called before the first frame update
    void Start()
    {
        DataManager.instance.LoadPlayersList();
    }

    public void StartNew()
    {
        if (playerName != null && playerName != "")
        {
            DataManager.instance.currentPlayerName = playerName;
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Enter you name");
            StartCoroutine("BlinkTimer");
        }
    }

    IEnumerator BlinkTimer()
    {
        for (int i = 0; i < 6; i++)
        {
            hintNameText.SetActive(i % 2 == 0);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SetCurrentPlayerName()
    {
        playerName = enteredName.text;
    }
}
