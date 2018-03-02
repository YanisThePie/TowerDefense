using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitScript : MonoBehaviour
{
    public float priority;
    public float currentHealth;
    public float maxHealth;
    public GameObject[] positions;
    public GameObject currentPositionToGo;
    public int indexPosition;
    public float delta;
    public float speed;
    public Vector3 positionInitiale;
    public List<GameObject> towers;
    public int comptPosition;
    public int nbPositions;
    public GameObject spawn;
    public int damage;
    public GameObject game;
    public string spawner;
    public int wave;
    public int rewards;
    public Text ressourcesText;
    public GameObject WinLoseText;
	public GameObject CoinsText;

    public RectTransform healthBar;
    public float currentHealthPercent;
    public bool isSlow;
    public float HealthBarWidth;


    // Use this for initialization
    void Start()
    {
        ressourcesText = GameObject.FindGameObjectWithTag("Ressource").GetComponent<Text>();
        WinLoseText = GameObject.FindGameObjectWithTag("WinLose");
		WinLoseText.transform.parent.parent.gameObject.GetComponent<Canvas>().enabled = false;
        HealthBarWidth = healthBar.GetComponent<RectTransform>().rect.width;
        isSlow = false;
        positions = GameObject.FindGameObjectsWithTag(spawner);
		GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawner");
		for (int i = 0; i < spawns.Length; i++)
		{
			if (spawns [i].name == spawner)
				spawn = spawns [i];
		}
        game = GameObject.FindGameObjectWithTag("GameInformation");
        nbPositions = positions.Length;
        indexPosition = 1;
        switchPosition(indexPosition);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(currentPositionToGo.transform);
        calculPriority();
        healthBar.sizeDelta = new Vector2(HealthBarWidth * (currentHealthPercent / 100), healthBar.sizeDelta.y);
        //Vector3 dir = currentPositionToGo.transform.position - transform.position;
        if (Time.timeScale == 1.0f)
        {

            transform.position = Vector3.MoveTowards(transform.position, currentPositionToGo.transform.position, speed * Time.deltaTime);
        }
            //transform.position += transform.forward * Time.deltaTime * speed;
        //transform.Translate(dir.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentPositionToGo.transform.position) <= 0.005f)
        {
            indexPosition++;
            switchPosition(indexPosition);
        }
    }

    public bool takeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {            
            ressourcesText.text = ((int.Parse(ressourcesText.text) + rewards) + "");
            rewards = 0;
			spawn.GetComponent<WaveSpawn> ().nbEnemy--;
			if (spawn.GetComponent<WaveSpawn> ().nbEnemy == 0)
				spawn.GetComponent<WaveSpawn> ().win ();
            Destroy(gameObject);
            return true;
        }
        else
        {
            currentHealthPercent = currentHealth * 100 / maxHealth;
            return false;
        }
    }

    void switchPosition(int indexPosition)
    {
        if (comptPosition < nbPositions)
        {
            foreach (GameObject position in positions)
                if (position.name == "Position" + indexPosition)
                {
                    currentPositionToGo = position;
                }

            positionInitiale = transform.position;
            comptPosition++;
        }
        else
        {
            foreach (GameObject tower in towers.ToArray())
            {

                if (tower.GetComponent<AttackTowerScript>().enabled && tower != null)
                {
                    foreach (GameObject enemy in tower.GetComponent<AttackTowerScript>().enemiesInRange.ToArray())
                        if (enemy == gameObject)
                        {
                            tower.GetComponent<AttackTowerScript>().enemiesInRange.Remove(enemy);
                            if (enemy == gameObject)
                                tower.GetComponent<AttackTowerScript>().target = null;
                        }

                }
            }
            if (game != null && game.GetComponent<GameScript>().takeDamage(damage)) {
				WinLoseText.transform.parent.parent.gameObject.GetComponent<Canvas>().enabled = true;
                WinLoseText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Loss !");
                Time.timeScale = 0;
                game.GetComponent<GameScript>().health = 10;
				int rewardCoins = (Random.Range (5, 11) * wave);
				game.GetComponent<GameScript>().Coins += rewardCoins;
                CoinsText = GameObject.FindGameObjectWithTag("Coins");
                CoinsText.GetComponent<TMPro.TextMeshProUGUI> ().SetText ("+" + rewardCoins);
            }

			if (spawn.GetComponent<WaveSpawn> ().nbEnemy == 0)
				spawn.GetComponent<WaveSpawn> ().win ();
            Destroy(gameObject);
        }
    }

    public void AddTower(GameObject tower)
    {
        towers.Add(tower);
    }

    public void RemoveTower(GameObject tower)
    {
        towers.Remove(tower);
    }

    public void Slow()
    {
        if (!isSlow)
        {
            StartCoroutine("SlowCoroutine");
        }
    }

    public void calculPriority()
    {
        float distance = 0;
        GameObject oldPosition = null;
        for (int i = comptPosition; i < nbPositions + 1; i++)
        {
            if (i == comptPosition)
            {
                distance = Vector3.Distance(transform.position, currentPositionToGo.transform.position);
                oldPosition = currentPositionToGo;
            }
            else
            {
                foreach (GameObject position in positions)
                    if (position.name == "Position" + i)
                    {
                        distance = distance + Vector3.Distance(oldPosition.transform.position, position.transform.position);
                        oldPosition = position;
                    }
            }
        }
        /*if (distance == 0)
			distance = Vector3.Distance (transform.position, currentPositionToGo.transform.position)+10000;*/
        priority = distance;

        /*
		if (comptPosition == 1) {
			priority =100- (Vector3.Distance (transform.position, currentPositionToGo.transform.position) / Vector3.Distance (spawn.transform.position, currentPositionToGo.transform.position)) * 100;
		} else {
			priority = (comptPosition - 1) * 100+(100-(Vector3.Distance (transform.position, currentPositionToGo.transform.position) / Vector3.Distance (spawn.transform.position, currentPositionToGo.transform.position)) * 100);

		}*/

    }

    IEnumerator SlowCoroutine()
    {
        speed /= 2;
        isSlow = true;
        yield return new WaitForSeconds(1);
        speed *= 2;
        isSlow = false;
    }
}