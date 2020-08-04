using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public GameManager gameManager;

    [Header("Input Parameters")]
    
    public float maxSpeed;
    [Range(0f, 50f)]
    public float jumpPower;
    public float addSpeed;

    private int direction;
    private float scale;
    private float speed;
    private int jumpCount;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        direction = 1;
        scale = 1;
        speed = 1;
        jumpCount = 0;
    }
    void Update()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) && jumpCount <= 1)
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                animator.SetBool("isJump", true);
                jumpCount++;
            }
        }

    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(direction * speed, rigid.velocity.y);
        rigid.AddForce(Vector2.right * rigid.velocity * 5, ForceMode2D.Impulse);
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position + Vector2.down * scale, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector3.down, 1f, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f * scale)
                {
                    animator.SetBool("isJump", false);
                    jumpCount = 0;
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Spike") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                gameManager.Die(this);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            if (collision.gameObject.tag == "Item")
            {
                gameManager.CountUp();

                collision.gameObject.GetComponent<Cheese>().Hide();
            }
        }
    }

    public void Giant(float ratio)
    {
        Time.timeScale = 0;
        StartCoroutine("WaitTransform");
        Time.timeScale = 1;
        transform.localScale = new Vector3(ratio, ratio, 1f);
        scale = ratio;
    }

    public void Turn()
    {
        direction *= -1;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public void SpeedUp()
    {
        speed *= addSpeed;
    }

    IEnumerator WaitTransform()
    {
        int countTime = 0;
        while (countTime < 10)
        {
            if (countTime % 2 == 0)
            {
                spriteRenderer.color = new Color32(255, 255, 255, 50);
            }
            else
            {
                spriteRenderer.color = new Color32(255, 255, 255, 180);
            }
            yield return new WaitForSeconds(0.1f);
            countTime++;
        }
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        yield return null;
    }
}
