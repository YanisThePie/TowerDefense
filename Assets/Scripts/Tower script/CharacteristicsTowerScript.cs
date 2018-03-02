using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsTowerScript : MonoBehaviour {
	public int level;
	public float dgt;
	public int cout;
	public float cadence;
	public int maxLvl;
    public string towerName;
    public string effect;
    public int coutAmelioration;
    public int prixVente;

    public void UpgradeLevel() {
		if (level < maxLvl) {
			level++;
			dgt *= 1.5f;
            prixVente = prixVente * 2;
            coutAmelioration = coutAmelioration * 2;
        }
	}
}
