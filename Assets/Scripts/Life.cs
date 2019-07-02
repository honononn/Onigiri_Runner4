using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    RectTransform rt;

    public GameObject Player;
    public GameObject explosion;
    //public Text gameOverText;
    private bool gameover = false;

    // Use this for initialization
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }



    public void LifeDown(int ap)
    {
        rt.sizeDelta -= new Vector2(0, ap);
    }

    public void Lifeup(int hp)
    {
        rt.sizeDelta += new Vector2(0, hp);

        if (rt.sizeDelta.y > 240f)
        {
            rt.sizeDelta = new Vector2(51f, 240f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (rt.sizeDelta.y <= 0)
        {
            if (gameover == false)
            {
                Instantiate(explosion, Player.transform.position + new Vector3(0, 1, 0), Player.transform.rotation);
            }
            

        }
        if (gameover)
        {
            //gameOverText.enabled = true;
            CallGameOver();
        }
    }

    public void GameOver()
    {
        gameover = true;
        Destroy(Player);
        Invoke("CallGameOver", 5);
    }
    void CallGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    void CallTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
