using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DeathScreen : MonoBehaviour
{
    public GameObject holder;
    public Text deathQuote;
    public bool deathActivated;
    private string[] deathQuotes = {"Maybe next time, use your abilities, mate.",
                                    "The birds will never chirp again.",
                                    "Call someone you love. They miss you.",
                                    "Extravagant!",
                                    "You could not resist the call.",
                                    "The essence has consumed you. You will be Cthulhu kin.",
                                    "Welcome to the hotel Flynn. Such a lovely place.",
                                    "Toniiiiiiight, we are deaaaaaaddddd. So let's set the world on fire?",
                                    "Take me out, Jim. Don't be such a deadbeat.",
                                    "Please excuse my dear aunt Sally. She only has one arm.",
                                    "Tentacles!",
                                    "She said to come over there was nobody home. I get there and guess what? THERE WAS NOBODY HOME.",
                                    "Die Rise was the best map." };
    //class is purely a menu class solely for the death screen.
    //takes random quote and puts it out, restarts or quits the game.
    private void Awake()
    {
        deathActivated = false;
        if (holder != null)
        {
            holder.SetActive(false);
        }
    }
    public void GameOver()
    {
            deathActivated = true;
            int randomNumber = Random.Range(1, deathQuotes.Length);
            deathQuote.text = deathQuotes[randomNumber];
            holder.SetActive(true);
        Time.timeScale = .01f;
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
