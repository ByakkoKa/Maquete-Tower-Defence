using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerParts : MonoBehaviour
{
    public string teamTag;
    public GameObject tower;

    void Awake()
    {
        gameObject.tag = teamTag;
    }

    private void FixedUpdate()
    {

    }

    public void TakeDamage(int damage)
    {
        tower.GetComponent<Tower>().health -= damage;
        StartCoroutine(tower.GetComponent<Tower>().DamageSprite());
    }
}
