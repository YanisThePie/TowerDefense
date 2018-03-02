using System.Collections;
using System.Collections.Generic;
using HoloToolkit;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour {
    public GameObject EditModePointer;
    public GameObject obj;

    public void InstantiateTower ()
    {
        EditModePointer.SetActive(true);
        foreach (Transform child in EditModePointer.transform)
        {
            Destroy(child.gameObject);
        }
        obj = this.gameObject.GetComponent<MatchButtonTower>().tower;
        EditModePointer.GetComponent<Pointer>().tower = obj;

        var customObj = Instantiate(obj, new Vector3(0,0,0), Quaternion.identity, EditModePointer.transform);
    
        customObj.transform.localPosition = new Vector3(0, 0, 0);
        customObj.GetComponent<AttackTowerScript>().enabled = false;
        customObj.GetComponent<OnClickEventTower>().enabled = false;
    }
}
