using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOnClick : MonoBehaviour {
    public GameObject objectToDisplay;
    public GameObject objectToHide;
    public GameObject LevelManager;
    public GameObject mainCanvas;
    private GameObject raycastCamera;
    bool UserHasTapped = false;
	// Use this for initialization
	void Start () {
		
	}

    IEnumerator DisplayMapMesh (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        var meshs = objectToDisplay.GetComponentsInChildren<MeshRenderer>();
        mainCanvas.SetActive(true);
        GameObject[] Canvas = GameObject.FindGameObjectsWithTag("Canvas");
        raycastCamera = GameObject.FindGameObjectWithTag("RaycastCamera");
        foreach (GameObject go in Canvas)
            go.GetComponent<Canvas>().worldCamera = raycastCamera.GetComponent<Camera>();
        foreach (MeshRenderer mesh in meshs)
        {
            if (!mesh.enabled)
            {
                mesh.enabled = true;
            }
        }
        GameObject.FindGameObjectWithTag("Spawner").GetComponent<WaveSpawn>().WhenCanvasIsActive();
    }

    // Update is called once per frame
    void Update () {
		if (this.GetComponent <HoloToolkit.Unity.InputModule.TapToPlace>().IsBeingPlaced == false && UserHasTapped == false)
        {
            UserHasTapped = true;
            objectToDisplay.SetActive(true);
            objectToHide.SetActive(false);
            LevelManager.GetComponent<MapGenerator>().GenerateMap();
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine("DisplayMapMesh", 2.0);
                this.enabled = false;
        }
    }
}
