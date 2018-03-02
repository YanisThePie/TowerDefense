using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class Pointer : MonoBehaviour, IInputClickHandler {

    public Material CanPlaceTower;
    public Material CannotPlaceTower;
    public List <Material> matTab;
    public GameObject tower;
    
    // public Transform Gaze;
    RaycastHit hit;
	// Update is called once per frame
	void Update () {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 25.0f))
        {
            this.transform.position = hit.transform.position;
        }
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "BusyForObj")
        {
            var render = this.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer mesh in render)
            {
                mesh.material = CannotPlaceTower;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int i = 0;

        if (other.tag == "MapCube")
        {
            Renderer[] render = this.GetComponentsInChildren<Renderer>();
            foreach (Renderer mesh in render)
            {
                if (mesh.material)
                {
                    mesh.material = CanPlaceTower;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BusyForObj")
        {
            int i = 0;

            Renderer[] render = this.GetComponentsInChildren<Renderer>();
            foreach (Renderer mesh in render)
            {
                    mesh.material = CanPlaceTower;
            }
        }
    }
    public virtual void OnInputClicked(HoloToolkit.Unity.InputModule.InputClickedEventData eventData)
    {

            GameObject map = GameObject.FindWithTag("Map");
            List<GameObject> ennemies = this.transform.GetChild(0).gameObject.GetComponent<AttackTowerScript>().enemiesInRange;
            clearEnemies(ennemies);
            Destroy(this.transform.GetChild(0).gameObject);
            GameObject tmpObj = Instantiate(tower, this.transform.position, Quaternion.identity, map.transform);

            tmpObj.tag = "BusyForObj";
            int i = 0;

            var render = this.GetComponentsInChildren<Renderer>();
            this.gameObject.SetActive(false);
    }

    public void clearEnemies(List<GameObject> enemies)
    {
            foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<UnitScript>().RemoveTower(this.transform.GetChild(0).gameObject);
        }
    }
}
