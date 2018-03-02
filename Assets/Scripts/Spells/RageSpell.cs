using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageSpell : MonoBehaviour {

    public GameObject ManaText;
    public int Cost;
    public float CoolDown;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canUseSpell())
        {
            activeSpell();
            ManaText.GetComponent<TMPro.TextMeshProUGUI>().SetText("" + (int.Parse(ManaText.GetComponent<TMPro.TextMeshProUGUI>().text) - Cost));
            StartCoroutine(Wait());
        }
    }

    private bool canUseSpell()
    {
        if (int.Parse(ManaText.GetComponent<TMPro.TextMeshProUGUI>().text) > Cost)
            return true;

        return false;
    }

    public void activeSpell()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        gameObject.GetComponent<CharacteristicsTowerScript>().dgt = gameObject.GetComponent<CharacteristicsTowerScript>().dgt + (gameObject.GetComponent<CharacteristicsTowerScript>().dgt/2);
        gameObject.GetComponent<CharacteristicsTowerScript>().cadence = gameObject.GetComponent<CharacteristicsTowerScript>().cadence + gameObject.GetComponent<CharacteristicsTowerScript>().cadence;
    }

    public void unactiveSpell()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.GetComponent<CharacteristicsTowerScript>().dgt = gameObject.GetComponent<CharacteristicsTowerScript>().dgt * (2/3);
        gameObject.GetComponent<CharacteristicsTowerScript>().cadence = gameObject.GetComponent<CharacteristicsTowerScript>().cadence - gameObject.GetComponent<CharacteristicsTowerScript>().cadence;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(15.0f);
        unactiveSpell();
    }
}