using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CallTitle : MonoBehaviour
{
    void Start()
    {
        Invoke("Title", 5);
    }
    void Title()
    {
        SceneManager.LoadScene("Title");
    }
}