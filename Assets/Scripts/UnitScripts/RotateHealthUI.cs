using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealthUI : MonoBehaviour {
	public GameObject camera;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (Camera.main.transform);
	}
}
