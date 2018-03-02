using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWhenPlaced : MonoBehaviour {

void hideThis ()
    {
        this.gameObject.SetActive(false);
    }
}
