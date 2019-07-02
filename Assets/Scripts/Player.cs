using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    public float jumpPower = 300;
    public LayerMask groundLayer;
    public Life lifeScript;

    public GameObject mainCamera;
    Rigidbody2D rb2D;
    private Animator anim;

    private bool isGrounded = true;

    public float maxSpeed = 12f;


    private bool facingRight = true;
    private Renderer renderer;


    private bool gameClear = false;
    public Text clearText;
    [Range(0, 1)] public float sliding = 0.9f;

    public GameObject bullet;
    // Use this for initialization

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
    }
    void Update()
    {
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 1, transform.position - transform.up * 0.05f, groundLayer);
        if (!gameClear)
        {

            if (Input.GetKeyDown("space"))
            {
                if (isGrounded == false)//ここ変えたよ！！！　isDroundがいけなかった
                {

                    anim.SetBool("Dash", false);
                    anim.SetTrigger("Jump");

                    isGrounded = false;

                    rb2D.AddForce(Vector2.up * jumpPower);
                }
            }
        }

        float veIY = rb2D.velocity.y;

        bool Jumping = veIY > 0.1f ? true : false;

        bool Falling = veIY < -0.1f ? true : false;

        anim.SetBool("isJumping", Jumping);
        anim.SetBool("isFalling", Falling);


        if (!gameClear)
        {
            if (Input.GetKeyDown("x"))
            {
                anim.SetTrigger("Shot");
                Instantiate(bullet, transform.position + new Vector3(0f, 0.1f, 0f), transform.rotation);
            }
            if (gameObject.transform.position.y < Camera.main.transform.position.y - 8)
            {
                lifeScript.GameOver();
            }
        }


    }


    void FixedUpdate()
    {
        if (!gameClear)
        {
            float x = Input.GetAxisRaw("Horizontal");
            Vector2 v = GetComponent<Rigidbody2D>().velocity;

            if (x != 0)
            {
                if (rb2D.velocity.x < maxSpeed)
                {
                    rb2D.velocity = new Vector2(x * speed, v.y);


                    /* Vector2 temp = transform.localScale;
                     temp.x = x;
                     transform.localScale = temp;*  これがいけなかった！！*/

                    if ((x > 0 && !facingRight) || (x < 0 && facingRight))
                    {
                        facingRight = (x > 0);
                        transform.localScale = new Vector3((facingRight ? 3 : -3), 3, 3);
                    }

                    anim.SetBool("Dash", true);
                }
                if (Mathf.Abs(rb2D.velocity.x) > maxSpeed)
                {
                    rb2D.velocity = new Vector2(Mathf.Sign(rb2D.velocity.x) * maxSpeed, rb2D.velocity.y);
                }

                if (transform.position.x > mainCamera.transform.position.x - 4)
                {
                    Vector3 cameraPos = mainCamera.transform.position;

                    cameraPos.x = transform.position.x + 4;
                    mainCamera.transform.position = cameraPos;


                }
                Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

                Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

                Vector2 pos = transform.position;
                pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x);
                transform.position = pos;
            }
            else
            {
                rb2D.velocity = new Vector2(0, v.y);
                anim.SetBool("Dash", false);
            }
        }
        else
        {
            clearText.enabled = true;
            anim.SetBool("Dash", true);
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            Invoke("CallTitle", 5);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ClearZone")
        {
            gameClear = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine("Damage");
        }
    }

    void CallTitle()
    {
        SceneManager.LoadScene("Title");
    }
    IEnumerator Damage()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        int count = 10;
        while (count > 0)
        {
            renderer.material.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.05f);
            renderer.material.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.05f);

            count--;
        }

        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
