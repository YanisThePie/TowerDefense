using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {
    
	public void restartGame () {
        Time.timeScale = 1;
        GameObject game = GameObject.FindGameObjectWithTag("GameInformation");
        if(game.GetComponent<GameScript>().LevelText == "Easy")
            SceneManager.LoadScene("Demo-Easy", LoadSceneMode.Single);

        else if (game.GetComponent<GameScript>().LevelText == "Medium")
            SceneManager.LoadScene("Demo-Easy", LoadSceneMode.Single);

        else
            SceneManager.LoadScene("Demo-Easy", LoadSceneMode.Single);
    }
}
