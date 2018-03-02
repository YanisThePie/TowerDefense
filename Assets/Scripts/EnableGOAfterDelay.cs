using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGOAfterDelay : MonoBehaviour {

    public GameObject ObjectToEnable;
	// Use this for initialization
	void Start () {
        StartCoroutine("EnableGO");
    }
    public IEnumerator EnableGO()
    {
        yield return new WaitForSeconds(5);
        ObjectToEnable.SetActive(true);
    }
}
