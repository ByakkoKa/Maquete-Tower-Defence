using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed;
    public int power;
    public float lifeSpan;
    public string enemyTag;
    public bool fliped;

    private void Start()
    {
        StartCoroutine("LifeSpan");

        if (fliped)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void FixedUpdate()
    {
        if (!fliped)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Time.deltaTime, 0);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == gameObject.tag || collision.gameObject.tag == "Dead")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == enemyTag)
        {
            collision.gameObject.SendMessage("TakeDamage", power);

            if (enemyTag != "Player")
            {
                if (GameManager.hitCount >= 999)
                {
                    GameManager.hitCount = 999;
                }
                else
                {
                    GameManager.hitCount++;
                }
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }

}
