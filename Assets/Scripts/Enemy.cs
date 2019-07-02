using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb2D;
    public int speed = -3;

    public GameObject explosion;
    public GameObject item;
    public int attackPoint = 10;
    public Life lifescript;
    private Life lifeScript;

    public const string MAIN_CAMERA_TAG_NAME = "MainCamera";
    private bool _isRendered = false;
    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        lifescript = GameObject.FindGameObjectWithTag("Hp").GetComponent<Life>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRendered)
        {
            rb2D.velocity = new Vector2(speed, rb2D.velocity.y);

        }

        if (gameObject.transform.position.y < Camera.main.transform.position.y - 8 || gameObject.transform.position.x < Camera.main.transform.position.x - 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_isRendered)
        {
            if (col.tag == "Bullet")
            {
                Destroy(gameObject);
                Instantiate(explosion, transform.position, transform.rotation);
                if (Random.Range(0, 4) == 0)
                {
                    Instantiate(item, transform.position, transform.rotation);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            lifescript.LifeDown(attackPoint);
        }
    }

    private void OnWillRenderObject()
    {
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            _isRendered = true;
        }
    }
}
