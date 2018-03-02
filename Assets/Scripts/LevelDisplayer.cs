using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDisplayer : MonoBehaviour
{

    public GameObject game;
    public GameObject CoinsText;

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameInformation");
    }

    public void LoadLevel()
    {
        switch (gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text)
        {
            case "Easy":
                //LevelText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Difficulty : <color=#22D01CFF>" + (gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text) + "</color>");
                game.GetComponent<GameScript>().LevelText = "Easy";
                break;

            case "Medium":
                //LevelText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Difficulty : <color=#FFB531FF>" + (gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text) + "</color>");
                game.GetComponent<GameScript>().LevelText = "Medium";
                break;

            case "Hard":
                //LevelText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Difficulty : <color=#F91D1DFF>" + (gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text) + "</color>");
                game.GetComponent<GameScript>().LevelText = "Hard";
                break;
        }
        StartCoroutine(ChooseLevelScene(gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text));
        int i;
        if (int.TryParse(CoinsText.GetComponent<Text>().text, out i))
            game.GetComponent<GameScript>().Coins = i;
        else
            Debug.Log("Unable to parse coin text");

        //WaveText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Wave : 1");
        //HealthText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Health : 10");
    }

    public IEnumerator ChooseLevelScene(string textLevel)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Demo-" + textLevel);
    }
}
