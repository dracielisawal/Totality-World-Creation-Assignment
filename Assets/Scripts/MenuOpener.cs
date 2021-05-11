using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOpener : MonoBehaviour
{
    public GameObject objMenu;
    private bool _isMenuOpen = false;
   public void onbuttonclick()
    {
        if (_isMenuOpen == false)
        {
            openMenu();
            return;
        }
        else
        { closeMenu(); }


    } 
    void openMenu()
    {
        objMenu.SetActive(true);
        _isMenuOpen = true;
        Debug.Log("Menu Opened");
    } 
    void closeMenu()
    {
        objMenu.SetActive(false);
        _isMenuOpen = false;
        Debug.Log("Menu Closed");
    }
}
