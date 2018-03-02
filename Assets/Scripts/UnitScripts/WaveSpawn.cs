using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour {
	public enum SpawnState {SPAWNING,WAITING,COUNTING};
    public int currentWave = 0;
	public GameObject WaveText;

	[System.Serializable]
	public class Wave {
		public GameObject enemy;
		public int count;
		public float rate;
		public float timeBetweenWaves;
		public int numWave;

        public Wave(GameObject enemy, int count, float rate, float timeBetweenWaves, int numWave)
        {
            this.enemy = enemy;
            this.count = count;
            this.rate = rate;
            this.timeBetweenWaves = timeBetweenWaves;
            this.numWave = numWave;
        }
    }

    public GameObject lightEnemy;
    public GameObject baseEnemy;
    public GameObject heavyEnemy;
    public List<Wave> waves;
	private int nextWave=0;
    public string difficulty;
	public float waveCountdown;
	private SpawnState state = SpawnState.COUNTING;
	public int comptWaves = 0;
	public GameObject WinLoseText;
	public GameObject CoinsText;
	public GameObject game;

	public int nbEnemy;

	// Use this for initialization
	void Start () {
		waveCountdown = 5f;
        GenerateWaves(difficulty);
		game = GameObject.FindGameObjectWithTag("GameInformation");
    }

    public void WhenCanvasIsActive ()
    {
        WaveText = GameObject.FindGameObjectWithTag("Wave");
        CoinsText = GameObject.FindGameObjectWithTag("Coins");
        WinLoseText = GameObject.FindGameObjectWithTag("WinLose");
        WinLoseText.transform.parent.parent.gameObject.GetComponent<Canvas>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
		if (waveCountdown <= 0) {
			if (state != SpawnState.SPAWNING) {
				if (comptWaves < waves.Count) {
					StartCoroutine (SpawnWave (waves [comptWaves]));
					comptWaves++;
				}
			}
		} else {
			waveCountdown -= Time.deltaTime;
		}
	}

	IEnumerator SpawnWave(Wave _wave) {
		state = SpawnState.SPAWNING;
		if (_wave.timeBetweenWaves == 20)
		{
			currentWave = _wave.numWave+1;
			WaveText.GetComponent<TMPro.TextMeshProUGUI> ().SetText ("Wave : " + currentWave);
		}
		for (int i = 0; i < _wave.count; i++) {
			SpawnEnemy (_wave.enemy, _wave.numWave);
            yield return new WaitForSeconds(1f / _wave.rate);
		}
        state = SpawnState.WAITING;
		waveCountdown = _wave.timeBetweenWaves;
		yield break;
	}

	public void SpawnEnemy(GameObject enemy, int numWave) {
		GameObject clone;
		clone = Instantiate (enemy, transform.position, transform.rotation) as GameObject;
		clone.GetComponent<UnitScript> ().spawner = gameObject.name;
		clone.GetComponent<UnitScript> ().wave = numWave;
		clone.GetComponent<UnitScript> ().maxHealth += (clone.GetComponent<UnitScript> ().maxHealth/100) *20 *(numWave-1);
		clone.GetComponent<UnitScript> ().currentHealth = clone.GetComponent<UnitScript> ().maxHealth;
    }

    public void GenerateWaves(string difficulty)
    {
        int nbrWave = 0;
        if(difficulty == "Easy")
        {
            nbrWave = 5;
        }

        else if (difficulty == "Medium")
        {
            nbrWave = 10;
        }

        else if (difficulty == "Hard")
        {
            nbrWave = 20;
        }

        for(int i = 0 ; i < nbrWave; i++)
        {
            Debug.Log("i = " + i);
            List<Wave> listWave = RandomWave(i+1, difficulty);
            Debug.Log(listWave);
            for (int j = 0; j < listWave.Count; j++)
            {
                waves.Add(listWave[j]);
				nbEnemy += listWave [j].count;
            }
        }
    }

    public List<Wave> RandomWave(int numWave, string difficulty)
    {
        List<Wave> listWave = new List<Wave>();
        int nbTypeUnit;
        int nbSubwave = 7;

        if (numWave < 3)
        {
            nbTypeUnit = 2;
        }
        else
            nbTypeUnit = 3;

        for (int i = 0; i < nbSubwave; i++)
        {
            string typeEnemy = RandomTypeEnemy(nbTypeUnit);
            Wave wave = RandomSubWave(i, numWave, typeEnemy);
            listWave.Add(wave);
        }



        return listWave;
    }


    private string RandomTypeEnemy(int nbTypeUnit)
    {
        //0 = light, 1 = base, 2 = heavy
        int random = Random.Range(0, nbTypeUnit);

        if (random == 0)
            return "LightUnit";
        else if (random == 1)
            return "BaseUnit";
        else
            return "HeavyUnit";
    }

    private Wave RandomSubWave(int currentSubWave, int numWave, string typeEnemy)
    {
        GameObject enemy = null;
        int count = 0;
        float rate = 0;
        float timeBetweenWaves = 0;

        if (typeEnemy == "LightUnit")
        {
            enemy = lightEnemy;
            count = Random.Range(1 + (numWave / 2), 2*numWave);
            rate = 2 + (numWave * 0.1f);
            
        }
        else if (typeEnemy == "BaseUnit")
        {
            enemy = baseEnemy;
            count = (int) Random.Range(1 + (numWave/3), 1.5f*numWave);
            rate = 1.5f + (numWave * 0.05f);
        }
        else
        {
            enemy = heavyEnemy;
            count = Random.Range(1 + (numWave /4), numWave);
            rate = 1 + (numWave * 0.025f);
        }

        if (currentSubWave == 6)
            timeBetweenWaves = 20;
        else
            timeBetweenWaves = Random.Range(0, 6);

        int nbrWave = numWave;

        return new Wave(enemy, count, rate, timeBetweenWaves, numWave);
    }

	public void win()
	{
		WinLoseText.transform.parent.parent.gameObject.GetComponent<Canvas> ().enabled = true;
		WinLoseText.GetComponent<TMPro.TextMeshProUGUI> ().SetText ("Win !");
		Time.timeScale = 0;
		game.GetComponent<GameScript> ().health = 10;
		int rewardCoins = 0;

		if(difficulty == "Easy")
			rewardCoins = (Random.Range (80, 100));
		else if(difficulty == "Medium")
			rewardCoins = (Random.Range (120, 150));
		else
			rewardCoins = (Random.Range (250, 300));
		
		game.GetComponent<GameScript> ().Coins += rewardCoins;
		CoinsText.GetComponent<TMPro.TextMeshProUGUI> ().SetText ("+" + rewardCoins);
	}
}
