using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChar : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer sprite;
    private float velocity, xScale;
    private bool canAttack = false, isAttacking = false;

    //public static int lifeVerify;

    [Header("Orientation")]
    public float size;
    public bool fliped;

    [Header("Status")]
    public int health;
    public int valor;
    public float speed;
    public int timeRecharge;

    [Header("Attack Variebles")]
    public Transform attackSensor;
    public Vector2 sensorTransform;
    public Transform attackCollision;
    public float hitRadius;
    public float attackDelay;
    public int attackPower;
    public GameObject bulletsType;
    

    [Header("Layer e Tags")]
    public string teamTag;
    public string enemyTag;
    public LayerMask yourLayer;

    [Header("Verification")]
    public Transform groundCheck;
    public bool isGround;

    private void Start()
    {
        SpawnVerification();

        velocity = speed * Time.deltaTime;

        if (fliped)
        {
            xScale = -1;
        }
        else
        {
            xScale = 1;
        }

        rb2d = GetComponent<Rigidbody2D> ();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator> ();
        transform.localScale = new Vector3(size * xScale * transform.localScale.x,
                                           size * transform.localScale.y,
                                           size * transform.localScale.z);
        gameObject.layer = (int)Mathf.Log(yourLayer.value, 2);
        gameObject.tag = teamTag;
    }

    private void FixedUpdate()       
    {
        GroundCheck();
        AttackSensor();

        if (canAttack && !isAttacking && health > 0)
        {
            AttackTrigger();
        }
        if (!fliped) { 
            rb2d.velocity = new Vector2(velocity, 0);
        }
        else
        {
            rb2d.velocity = new Vector2(-velocity, 0);
        }

        anim.SetFloat("Speed", Mathf.Abs(velocity));

        if (isAttacking || health <= 0)
        {
            velocity = 0;
        }
        else
        {
            velocity = speed * Time.deltaTime;
        }

    }

    private void SpawnVerification()
    {
        Collider2D[] charspawn = Physics2D.OverlapCapsuleAll(GetComponent<CapsuleCollider2D>().transform.position,
                                     GetComponent<CapsuleCollider2D>().size,
                                     GetComponent<CapsuleCollider2D>().direction, 0);
        for (int i = 0; i < charspawn.Length; i++)
        {
            if (charspawn[i].gameObject.tag == teamTag)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), charspawn[i].GetComponent<Collider2D>());
            }
        }
    }

    private void GroundCheck()
    {
        Collider2D groundCollider = Physics2D.OverlapArea(new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y),
                                 new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y),
                                 yourLayer);

        if (groundCollider != null && groundCollider.gameObject.tag == "Ground")
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    private void AttackSensor()
    {
        Collider2D[] sensorCollider = Physics2D.OverlapBoxAll(attackSensor.transform.position, sensorTransform, 0, yourLayer);

        for (int i = 0; i < sensorCollider.Length; i++)
        {
            if (sensorCollider[i].gameObject.tag == enemyTag)
            {
                canAttack = true;
                velocity = 0;
                return;
            }
            else
            {
                canAttack = false;
            }
        }
    }

    private void AttackTrigger()
    {
       anim.SetTrigger("Attack");
       isAttacking = true;
       canAttack = false;
       StartCoroutine("WaitAttack");
    }

    public void AttackHit()
    {
        Collider2D[] enemiesAttack = Physics2D.OverlapCircleAll(attackCollision.position, hitRadius, yourLayer);

            for (int i = 0; i < enemiesAttack.Length; i++)
            {
                if(enemiesAttack[i].gameObject.tag == enemyTag){
                    enemiesAttack[i].SendMessage("TakeDamage", attackPower);
                    if (teamTag == "Player")
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
                }
            }
    }

    public void SpawnShoot()
    {
        bulletsType.gameObject.layer = (int)Mathf.Log(yourLayer.value, 2);
        bulletsType.GetComponent<Bullets>().power = attackPower;
        bulletsType.GetComponent<Bullets>().enemyTag = enemyTag;
        bulletsType.GetComponent<Bullets>().fliped = fliped;
        Instantiate(bulletsType, attackCollision.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) {

            gameObject.tag = "Dead";
            anim.SetTrigger("Dead");

        }
        else
        {
            StartCoroutine("DamageSprite");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackSensor.position, sensorTransform);
        Gizmos.DrawWireSphere(attackCollision.position, hitRadius);
        Gizmos.DrawWireSphere(groundCheck.position, 0.04f);
    }

    private IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    private IEnumerator DamageSprite()
    {
        for (float i = 0; i < 0.2f; i+= 0.2f)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
        }
               
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == gameObject.tag || collision.gameObject.tag == "Dead")
        {
           Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

    }

    public void SelfDestory()
    {
        Destroy(gameObject);
    }
 
}
