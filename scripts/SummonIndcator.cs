using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonIndcator : MonoBehaviour {

    public bool mouseOn;
    public GameObject effect;
    public Transform spawnLocation;

    private bool showeffect = false;
    private GameObject effectInstance;

    void FixedUpdate() {

        if (GameManager.currentChar != null && !showeffect)
        {
            effectInstance = (GameObject)Instantiate(effect, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            showeffect = true;
        }
        if (GameManager.currentChar == null && showeffect) {
            Destroy(effectInstance);
            showeffect = false;
        }
    }

    void OnMouseOver() {
        mouseOn = true;
    }

    void OnMouseExit() {
        mouseOn = false;
    }

}
