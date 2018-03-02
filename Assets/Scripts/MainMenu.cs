using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void returnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("DemoMenu",LoadSceneMode.Single);
    }
}
