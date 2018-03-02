using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelEasy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("DEV Thomas - Towers and units", LoadSceneMode.Additive);
    }
	
}
