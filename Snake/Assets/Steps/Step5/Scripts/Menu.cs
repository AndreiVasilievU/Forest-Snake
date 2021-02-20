using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    GameObject GO;
    GameObject gameOver;
    GameObject plane;

    Animator anim;
    void Start()
    {
        GO = GameObject.Find("Snake");
        GO.GetComponent<SnakeController>();

        gameOver = GameObject.Find("GameOver");
        anim = gameOver.GetComponent<Animator>();

        plane = GameObject.Find("Panel");
        
    }
    public void Yes()
    {
        GO.SendMessage("RebootSnake");
    }

    // Update is called once per frame
    public void No()
    {
        plane.SetActive(false);
        anim.SetBool("isPanel", true);
    }
}
