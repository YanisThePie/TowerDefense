using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnStart : MonoBehaviour {
	
    public void LoadScene()
    {
        StartCoroutine("ChangeScene");
    }
    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("DemoMenu", LoadSceneMode.Single);
    }
}
