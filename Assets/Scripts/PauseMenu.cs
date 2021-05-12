using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
   public GameObject pausemenu;
    private bool _isPauseMenuOpen;
    public PlayerSpawn spawner;
 
    
   public void pauseMenuopener()
    {
        if (_isPauseMenuOpen == false)
        {
            pausemenu.SetActive(true);
            _isPauseMenuOpen = true;
            return;
        }
        else
        {
            pausemenu.SetActive(false);
            _isPauseMenuOpen = false;
        }
    } 

    public void exitapp()
    {
        Application.Quit();
    } 

    public void respawn()
    {
        spawner.oncreationDone();
    }
}
