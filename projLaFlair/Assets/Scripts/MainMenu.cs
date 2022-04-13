using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    /*Entire menu class created. In here are scene managements
     * in here are buyables.*/
    // Start is called before the first frame update
    public GameObject holder;
    public MouseLook mouseLook;
    public PlayerMovement pm;
    public Ability ability;
    private void Awake()
    {
        if(holder != null)
        {
            holder.SetActive(false);
        }
    }
    //start game button
    public void PlayGame()
    {
        SceneManager.LoadScene("minigame");
        Time.timeScale = 1;
       // StartMovementAndHideCursor();
    }
    //exit application button
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        //pause button
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (holder.activeInHierarchy) Unpause();
            else Pause();
            
        }
    }
    //buy new powerup gravity inside in-game purchase menu
    public void BuyGravity()
    {
        if(!ability.gravityEnabled)
        {
            if (ability.essenceCount >= 2)
            {
                ability.essenceCount -= 2;
                ability.gravityEnabled = true;
            }
        }
        else return;
    }
    //buy new powerup goo inside in-game purchase menu
    public void BuyGoo()
    {
        if(!ability.gooEnabled)
        {
            if (ability.essenceCount >= 2)
            {
                ability.gooEnabled = true;
                ability.essenceCount -= 2;
            }
        }
        else return;
        
    }
    //end the game using enough currency
    public void BuyableEnding()
    {
        if(ability.essenceCount >= 8)
        {
            EndGame();
        }
    }
    //loads menu from button press
    public void MainMenuScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //loads end menu from button press
    public void EndGame()
    {
        SceneManager.LoadScene("YouWin");
        Time.timeScale = 1;
    }
    //pauses game, but sets scale to 0.1 for time slow mechanic. 
    //edit, didnt need bottom two methods to stop movement, this does that anyway. Am dumb.
    public void Pause()
    {       
        holder.SetActive(true);
        Time.timeScale = 0.01f;
        StopMovementAndShowCursor();
    }
    //unpauses game
    public void Unpause()
    {
        holder.SetActive(false);
        Time.timeScale = 1;
        StartMovementAndHideCursor();
    }
    //allows for cursor implementation and movement changes.
    private void StopMovementAndShowCursor()
    {
        mouseLook.enableCameraMovement = false;
        mouseLook.lockAndHideCursor = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pm.playerCanMove = false;
    }
    private void StartMovementAndHideCursor()
    {
        mouseLook.enableCameraMovement = true;
        mouseLook.lockAndHideCursor = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pm.playerCanMove = true;
    }
}
