using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour {
    
    public Text CoinsText;
    public GameObject informationGame;
	public GameObject game;

    void Start()
    {
        CoinsText = GameObject.FindGameObjectWithTag("CoinsText").GetComponent<Text>();
        informationGame = GameObject.FindGameObjectWithTag("GameInformation");
        if(gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text == "Tour A")
            informationGame.GetComponent<GameScript>().unlockTower(gameObject.GetComponent<MatchButtonTower>().tower);

        if (gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text == "Tour B")
            informationGame.GetComponent<GameScript>().unlockTower(gameObject.GetComponent<MatchButtonTower>().tower);
    }

    void Update()
    {
        CoinsText.text = (getCoinsAmout() + "");
    }

    void Awake()
    {
        informationGame = GameObject.FindGameObjectWithTag("GameInformation");
    }

    public int getCoinsAmout()
    {
        return int.Parse(CoinsText.text);
    }

    public void AddCoins(int amountToAdd)
    {
        CoinsText.text = ("" + (int.Parse(CoinsText.text) + amountToAdd));
    }

    private bool isPurchasable()
    {
        if (gameObject.transform.childCount < 2)
            return false;
        else if (int.Parse(CoinsText.text) < int.Parse(gameObject.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text) && gameObject.transform.childCount > 2)
            return false;

        else return true;
    }

    public void Purchase()
    {
        if (isPurchasable())
        {
			informationGame.GetComponent<GameScript>().Coins -= int.Parse(gameObject.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text);
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);

            informationGame.GetComponent<GameScript>().unlockTower(gameObject.GetComponent<MatchButtonTower>().tower);
        }
    }
}
