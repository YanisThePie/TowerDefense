using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject panelToolTip;
    public GameObject damageText;
    public GameObject cadenceText;
    public GameObject effectsText;
    public GameObject tower;
void Start()
    {
        damageText = panelToolTip.transform.GetChild(0).GetChild(0).gameObject;
        cadenceText = panelToolTip.transform.GetChild(0).GetChild(1).gameObject;
        effectsText = panelToolTip.transform.GetChild(0).GetChild(2).gameObject;
        tower = gameObject.transform.GetComponent<MatchButtonTower>().tower;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        panelToolTip.SetActive(true);
        damageText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Damages : " + tower.GetComponent<CharacteristicsTowerScript>().dgt.ToString());
        cadenceText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Cadence : " + tower.GetComponent<CharacteristicsTowerScript>().cadence.ToString());
        effectsText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Effects : " + tower.GetComponent<CharacteristicsTowerScript>().effect.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panelToolTip.SetActive(false);
    }
}