using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject yourTower;
    public GameObject enemyTower;

    public static int yourHealth;
    public static int enemyHealth;
    public Image healthBar;
    private int startHealth;

    public Text pointCounter;
    public Text hitCounter;
    public Text bonusShow;
    public GameObject[] boxSpawn;
    public GameObject spriteObject;

    public static GameObject currentChar, currentEnemy, currentSprite;
    public static GameObject[] dragLocation;
    public static int points;
    public static Vector3 instantiatePointPlayer = new Vector3(-7, 7, 0), instantiatePointEnemy = new Vector3(-7, -7, 0);

    private float rechargeSpeed;
    public static int hitCount;
    private bool hitTimeStart = false;


    void Awake () {

        dragLocation = boxSpawn;
        currentSprite = spriteObject;
        points = 0;
        rechargeSpeed = 0.2f;
        IncrementPoints();
        startHealth = yourTower.GetComponent<Tower>().health;
        hitCount = 0;

    }
	
	void FixedUpdate () {

        if (points > 9999)
        {
            points = 9999;
        }

        pointCounter.text = points.ToString();

        if (hitCount > 1)
        {
            hitCounter.text = hitCount.ToString() + " hits";
        }
        else
        {
            hitCounter.text = null;
        }

        if(hitCount > 1 && !hitTimeStart){
            StartCoroutine(HitTime(hitCount));
        }

        healthBar.fillAmount = (float)yourTower.GetComponent<Tower>().health / startHealth;

        if(yourHealth <= 0 || enemyHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

    }

    void IncrementPoints()
    {

        if (points < 9999)
        {

            if (hitCount >= 20 && hitCount < 40)
            {
                points += 2;
                bonusShow.text = "x2";
            }
            else if (hitCount >= 40 && hitCount < 60)
            {
                points += 4;
                bonusShow.text = "x4";
            }
            else if (hitCount >= 60 && hitCount < 80)
            {
                points += 6;
                bonusShow.text = "x6";
            }
            else if (hitCount >= 80 && hitCount < 100)
            {
                points += 8;
                bonusShow.text = "x8";
            }
            else if (hitCount >= 100)
            {
                points += 10;
                bonusShow.text = "x10";
            }
            else
            {
                points++;
                bonusShow.text = null;
            }
        }

        StartCoroutine("RechargePoints");

    }

    private IEnumerator RechargePoints()
    {
        yield return new WaitForSeconds(rechargeSpeed);
        IncrementPoints();
    }

    private IEnumerator HitTime(int count)
    {
        hitTimeStart = true;
        yield return new WaitForSeconds(1.5f);
        if (count == hitCount)
        {
            hitCount = 0;
        }
        hitTimeStart = false;
    }
}
