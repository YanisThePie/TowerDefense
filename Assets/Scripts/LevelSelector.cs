using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {

    public GameObject textLevel;
    public GameObject textWave;
    public GameObject textPV;

    public void InitiGamePanel()
    {
        textLevel.GetComponent<TMPro.TextMeshProUGUI>().SetText("Level : " + gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text);
        textWave.GetComponent<TMPro.TextMeshProUGUI>().SetText("Wave : 1");
        textPV.GetComponent<TMPro.TextMeshProUGUI>().SetText("PV : 10");
    }

}
