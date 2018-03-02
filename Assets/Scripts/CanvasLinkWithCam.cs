using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasLinkWithCam : MonoBehaviour {
    public GameObject raycastCamera;
    // Use this for initialization
    void Awake () {
        SceneManager.activeSceneChanged += LinkWithUi;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LinkWithUi(Scene previousScene, Scene newScene)
    {
        Debug.Log("scene switch");
        GameObject[] Canvas = GameObject.FindGameObjectsWithTag("Canvas");
        foreach (GameObject go in Canvas)
            go.GetComponent<Canvas>().worldCamera = raycastCamera.GetComponent<Camera>();
    }
}
