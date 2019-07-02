using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int healPoint = 20;
    public Life lifeScript;
    private AudioSource sound01;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            lifeScript.Lifeup(healPoint);
            //sound01.PlayOneShot(sound01.clip);
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        sound01 = GetComponent<AudioSource>();
        lifeScript = GameObject.FindGameObjectWithTag("Hp").GetComponent<Life>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}