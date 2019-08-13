using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int health;
    private int startHealth;
    public string teamTag;
    public Image healthBar;


    private SpriteRenderer sprite;

    void Awake()
    {
        gameObject.tag = teamTag;
        startHealth = health;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (teamTag == "Player") {
            GameManager.yourHealth = health;
        }
        if (teamTag == "Enemy")
        {
            GameManager.enemyHealth = health;
            healthBar.fillAmount = (float)health / startHealth;
        }
    }

    public IEnumerator DamageSprite()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
        }

    }
}
