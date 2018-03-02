using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	public GameObject target;
	public Transform startMarker;
	public Transform endMarker;
	public float delta = 0f;
	public float damage;
	public float speed;
	public GameObject collisionEffect;
	//public GameObject tower;
	// Use this for initialization
	void Start () {
		startMarker = transform;

	}
	
	// Update is called once per frame
	void Update () {
		if (target == null)
			Destroy (gameObject);
		else {
			if (delta < 0.5) {
				delta += Time.deltaTime*speed;
			} else {

				if (target.GetComponent<UnitScript> ().takeDamage (damage)) {
					foreach (GameObject tower in target.GetComponent<UnitScript>().towers.ToArray())
						foreach (GameObject enemy in tower.GetComponent<AttackTowerScript>().enemiesInRange.ToArray())
							if (enemy == target) {
								tower.GetComponent<AttackTowerScript> ().enemiesInRange.Remove (enemy);   
								if (enemy == target)
									tower.GetComponent<AttackTowerScript> ().target = null;
							}
				}
				GameObject effect=Instantiate (collisionEffect, transform.position, transform.rotation);
				Destroy (effect,1f);
				Destroy (gameObject);
			}
			if (target != null)
				transform.position = Vector3.Lerp (startMarker.position, target.transform.position, delta);
			else
				Destroy (gameObject);
		}
	}
}
