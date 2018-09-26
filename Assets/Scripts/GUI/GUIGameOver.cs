using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIGameOver : MonoBehaviour {


    public void GameOver()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickInBackground()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
