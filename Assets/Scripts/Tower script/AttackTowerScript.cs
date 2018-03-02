using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTowerScript : MonoBehaviour {
	public GameObject target;
	public GameObject rotatePiece;
	public List<GameObject> enemiesInRange;
	public GameObject bullet;
	public GameObject[] bulletPosition;
	public bool canAttack;
	public int nbBullets;
    private AudioSource ValidateAudioSource;
    public AudioClip Shot;
	// Use this for initialization
	void Start () {
		enemiesInRange = new List<GameObject> ();
		canAttack = true;
        ValidateAudioSource = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
		if (target != null && GetComponent<CharacteristicsTowerScript> ().effect=="-") {
			rotatePiece.transform.LookAt (target.transform);
			Attack ();
		}
		else if (target != null && GetComponent<CharacteristicsTowerScript> ().effect=="Slow") {
			SlowEnnemies ();
		}
        else {

			if (enemiesInRange.Count > 0) {
				enemiesInRange.Sort (ComparePriority);
				target = enemiesInRange [0];
			}
		}


	}

	void OnTriggerEnter( Collider obj){
		if(obj.tag == "Enemy"){
			enemiesInRange.Add(obj.gameObject);
			obj.gameObject.GetComponent<UnitScript> ().AddTower (gameObject);

		}
	}


	void OnTriggerExit(Collider obj){
		if(obj.tag == "Enemy"){
			//we need to remove this guy from the enemiesInRangearray
			//and this is the dumbest and easiest way of doing that:
			foreach (GameObject enemy in enemiesInRange.ToArray())
				if (enemy == obj.gameObject) {
					enemy.GetComponent<UnitScript> ().RemoveTower (gameObject);
					enemiesInRange.Remove (enemy);   
					if (enemy == target)
						target = null;
				}
		}
	}

	private int ComparePriority(GameObject a, GameObject b) {
		float pA = a.GetComponent<UnitScript>().priority;
		float pB = b.GetComponent<UnitScript>().priority;
		if(pA >= pB) {
			return 1;
		}
		else {
			return -1;
		}
	}

	public void Attack() {
		if (canAttack == true) {
			GameObject clone;
			if (nbBullets == 2) {
				clone = Instantiate (bullet, bulletPosition [0].transform.position, bulletPosition [0].transform.rotation) as GameObject;
				clone.GetComponent<BulletScript> ().target = target;
				clone.GetComponent<BulletScript> ().damage = GetComponent<CharacteristicsTowerScript> ().dgt;
				clone = Instantiate (bullet, bulletPosition [1].transform.position, bulletPosition [1].transform.rotation) as GameObject;
				clone.GetComponent<BulletScript> ().target = target;
				clone.GetComponent<BulletScript> ().damage = 0;
			} else {
				clone = Instantiate (bullet, bulletPosition[0].transform.position, bulletPosition[0].transform.rotation) as GameObject;
				clone.GetComponent<BulletScript> ().target = target;
				clone.GetComponent<BulletScript> ().damage = GetComponent<CharacteristicsTowerScript> ().dgt;
			}
            ValidateAudioSource.PlayOneShot(Shot, 0.5F);
            StartCoroutine ("CadenceCoroutine");
			canAttack = false;
		}


	}

	public void SlowEnnemies() {
		foreach (GameObject enemy in enemiesInRange.ToArray()) {
			enemy.GetComponent<UnitScript> ().Slow ();
		}
	}


	IEnumerator CadenceCoroutine() {

		yield return new WaitForSeconds(GetComponent<CharacteristicsTowerScript>().cadence);
		canAttack = true;
	}

}
