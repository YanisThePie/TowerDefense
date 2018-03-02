using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionPanelBuilder : MonoBehaviour {

    public GameObject informationGame;

    // Use this for initialization
    void Start () {
        informationGame = GameObject.FindGameObjectWithTag("GameInformation");
        
        for (int i = 0; i < informationGame.GetComponent<GameScript>().towers.Count; i++)
        {
            if (informationGame.GetComponent<GameScript>().towers[i].GetComponent<CharacteristicsTowerScript>().towerName == gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text)
            {
                gameObject.GetComponent<Button>().interactable = true;
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
