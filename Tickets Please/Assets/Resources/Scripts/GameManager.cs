using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int lives = 3;

    private void Update()
    {
        //remember to always have a quit game button!
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //go to the game over screen if you get 3 mistakes in a day
        if(lives < 1)
        {
            SceneManager.LoadScene(1);
        }
    }

    //makes the player loose a life
    public void LooseLife()
    {
        lives--;
    }

    //Restes the players lifetotal to 3
    public void ResetLives()
    {
        lives = 3;
    }
}
