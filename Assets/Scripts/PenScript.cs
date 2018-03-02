using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenScript : MonoBehaviour {
    Material MaterialToPaint;
	// Use this for initialization
	void Start () {
        MaterialToPaint = GameObject.Find("LevelManager").GetComponent<MapGenerator>().negativePrefab.GetComponent<Renderer>().sharedMaterial;
        Debug.Log(MaterialToPaint.name);
	}

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
            other.GetComponent<Renderer>().material = MaterialToPaint;
            other.tag = "BusyForObj";
    }
}
