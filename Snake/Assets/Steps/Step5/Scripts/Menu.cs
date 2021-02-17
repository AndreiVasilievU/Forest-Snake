using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    GameObject GO;
    public void Yes()
    {
        GO = GameObject.Find("Snake");
        GO.GetComponent<SnakeController>();
        GO.SendMessage("RebootSnake");
    }

    // Update is called once per frame
    public void No()
    {
        SceneManager.LoadScene(0);
    }
}
