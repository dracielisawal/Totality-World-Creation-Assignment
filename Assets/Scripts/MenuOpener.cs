using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOpener : MonoBehaviour
{
    public GameObject objMenu;
    private bool _isMenuOpen;
   public void onbuttonclick()
    {
        if (_isMenuOpen)
            openMenu();


    } 
    void openMenu()
    {
        objMenu.SetActive(true);
    } 
    void closeMenu()
    {
        objMenu.SetActive(false);
    }
}
