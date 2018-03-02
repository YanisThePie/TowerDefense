using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {
	public int health;
    public string LevelText;
    public int Coins;
    public List<GameObject> towers;

    public GameObject HealthText;

    // Static singleton property
    public static GameScript Instance { get; private set; }

    void Awake()
    {
        // First we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }

        // Here we save our singleton instance
        Instance = this;

        DontDestroyOnLoad(gameObject);

    }

    public bool takeDamage(int amount) {
        HealthText = GameObject.FindGameObjectWithTag("Health");
        health -= amount;
		if (health <= 0) {
			health = 0;
            HealthText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Health : " + health);
            return true;
		}

        HealthText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Health : " + health);
        return false;
	}

    

    public void unlockTower(GameObject tower)
    {
        towers.Add(tower);
    }

    public List<GameObject> getListTowersAvailable()
    {
        return towers;
    }
}
