using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickEventTower : MonoBehaviour, IPointerDownHandler {

    public GameObject PanelUpgrade;

	void Start()
	{
		PanelUpgrade = GameObject.FindGameObjectWithTag ("UpgradePanel");
	}

    private bool ShowCanvas()
    {
		if(PanelUpgrade.transform.parent.gameObject.GetComponent<Canvas>().enabled)
        return false;

        return true;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Hello");
		PanelUpgrade.transform.GetChild(1).GetComponent<RessourcesManager>().tower = gameObject;
		PanelUpgrade.transform.GetChild(0).GetComponent<RessourcesManager>().tower = gameObject;

		PanelUpgrade.transform.parent.gameObject.GetComponent<Canvas>().enabled = ShowCanvas();
		PanelUpgrade.transform.parent.position = gameObject.transform.position + new Vector3(0, 0.25f, 0);
        ShowButtonUpgrade();
        UpdateInfos();

    }

    public void UpdateInfos()
    {
		PanelUpgrade.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().SetText(gameObject.GetComponent<CharacteristicsTowerScript>().towerName);
		PanelUpgrade.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("Level " + gameObject.GetComponent<CharacteristicsTowerScript>().level);
		PanelUpgrade.transform.GetChild(0).GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().SetText("-" + gameObject.GetComponent<CharacteristicsTowerScript>().coutAmelioration);
		PanelUpgrade.transform.GetChild(1).GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().SetText("+" + gameObject.GetComponent<CharacteristicsTowerScript>().prixVente);
    }

    public void ShowButtonUpgrade()
    {
        if (gameObject.GetComponent<CharacteristicsTowerScript>().level == gameObject.GetComponent<CharacteristicsTowerScript>().maxLvl)
			PanelUpgrade.transform.GetChild(0).gameObject.SetActive(false);
        else
			PanelUpgrade.transform.GetChild(0).gameObject.SetActive(true);
    }
}
