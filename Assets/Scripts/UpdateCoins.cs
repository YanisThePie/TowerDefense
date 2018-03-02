using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCoins : MonoBehaviour {
	public GameObject game;
    public Text text;

	void Start()
	{
		game = GameObject.FindGameObjectWithTag ("GameInformation");
	}

	void Update () {
        //gameObject.GetComponent<TMPro.TextMeshProUGUI>().SetText(game.GetComponent<GameScript>().Coins + "");
        text.text = (game.GetComponent<GameScript>().Coins + "");
    }
}
