using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar : MonoBehaviour {

    public GameObject prefabChar;
    public LayerMask[] layerSpawn = new LayerMask[2]; 
    private GameObject spriteChar;
    private bool canSelect = true;

	void FixedUpdate () {

        if (GameManager.points < prefabChar.GetComponent<BaseChar>().valor || !canSelect)
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
	}

    private void OnMouseDown() {
        if (canSelect && (GameManager.points >= prefabChar.GetComponent<BaseChar>().valor)
            && GameManager.currentChar == null)
        {
            GameManager.currentChar = prefabChar;
            spriteChar = (GameObject)Instantiate(GameManager.currentSprite, 
                                                 transform.position, Quaternion.identity);
            spriteChar.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
    }

    private void OnMouseUp() {

        if (GameManager.currentChar != null && GameManager.dragLocation[0].GetComponent<SummonIndcator>().mouseOn ||
            GameManager.currentChar != null && GameManager.dragLocation[1].GetComponent<SummonIndcator>().mouseOn)
        {       
            if (GameManager.dragLocation[0].GetComponent<SummonIndcator>().mouseOn)
            {
                GameManager.currentChar.GetComponent<BaseChar>().yourLayer = layerSpawn[0];
                GameManager.currentChar.GetComponent<BaseChar>().size = 1.2f;
                GameManager.currentChar.GetComponent<Renderer>().sortingOrder = 5;
                Instantiate(GameManager.currentChar,
                            GameManager.dragLocation[0].GetComponent<SummonIndcator>().spawnLocation.transform.position, 
                            Quaternion.identity);
                
            }
            else if (GameManager.dragLocation[1].GetComponent<SummonIndcator>().mouseOn)
            {
                GameManager.currentChar.GetComponent<BaseChar>().yourLayer = layerSpawn[1];
                GameManager.currentChar.GetComponent<BaseChar>().size = 1.1f;
                GameManager.currentChar.GetComponent<Renderer>().sortingOrder = 4;
                Instantiate(GameManager.currentChar, 
                            GameManager.dragLocation[1].GetComponent<SummonIndcator>().spawnLocation.transform.position, 
                            Quaternion.identity);                
            }

            GameManager.points -= GameManager.currentChar.GetComponent<BaseChar>().valor;
            StartRecharge();

        }
        else
        {
            Destroy(spriteChar);
            GameManager.currentChar = null; 
        }

    }

    private IEnumerator WaitTime() {
        yield return new WaitForSeconds(prefabChar.GetComponent<BaseChar>().timeRecharge);
        canSelect = true;
    }

    public void StartRecharge() {
        canSelect = false;
        Destroy(spriteChar);
        GameManager.currentChar = null;
        StartCoroutine("WaitTime");
    }
}
