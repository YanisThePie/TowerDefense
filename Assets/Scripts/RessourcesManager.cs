using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourcesManager : MonoBehaviour {

    public Text RessourceText;
    public GameObject tower;

	void Start()
	{
		RessourceText = GameObject.FindGameObjectWithTag ("Ressource").GetComponent<Text>();
	}

    private bool isPurchasable()
    {
        if (int.Parse(RessourceText.text) < int.Parse(gameObject.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text))
            return false;

        else return true;
    }

    private bool isUpgradable()
    {
        if (int.Parse(RessourceText.text) < int.Parse(gameObject.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text )*-1)
            return false;

        else return true;
    }

    public void BuyTower()
    {
        if (isPurchasable())
        {
            RessourceText.text = ("" + (int.Parse(RessourceText.text) - int.Parse(gameObject.transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text)));
        }
    }

    public void addRessources(int amount)
    {
        RessourceText.text = ("" + (int.Parse(RessourceText.text) + amount));
    }

    public void UpgradeTower()
    {
        if (isUpgradable())
        {
            RessourceText.text = ("" + (int.Parse(RessourceText.text) + int.Parse(gameObject.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text)));
            tower.GetComponent<CharacteristicsTowerScript>().UpgradeLevel();
            tower.GetComponent<OnClickEventTower>().UpdateInfos();
            tower.GetComponent<OnClickEventTower>().ShowButtonUpgrade();
        }
    }

    public void SellTower()
    {
        RessourceText.text = ("" + (int.Parse(RessourceText.text) + int.Parse(gameObject.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text)));
        //Detruit le panel
		gameObject.transform.parent.parent.gameObject.GetComponent<Canvas>().enabled = false;
        Destroy(tower);
    }
}
