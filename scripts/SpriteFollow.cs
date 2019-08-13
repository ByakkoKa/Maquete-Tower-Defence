using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFollow : MonoBehaviour {

    public Sprite spriteSelect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.currentChar == null)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = GameManager.currentChar.GetComponent<SpriteRenderer>().sprite;
        }

        Vector3 mouseP = Input.mousePosition;
        mouseP.z = transform.position.z - Camera.main.transform.position.z;
        transform.position = Camera.main.ScreenToWorldPoint(mouseP);
	}
}
