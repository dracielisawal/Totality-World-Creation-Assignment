using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JMRSDK.InputModule;

public class MenuOpener : MonoBehaviour,IBackHandler
{
    public GameObject objMenu;
    private bool _isMenuOpen = false;

    private void Start() {
        JMRInputManager.Instance.AddGlobalListener(gameObject);
    }
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
public void OnBackAction() {
    Debug.Log("OnBackAction");
    closeMenu();
  }

    void openMenu()
    {
        objMenu.SetActive(true);
        _isMenuOpen = true;
        Debug.Log("Menu Opened");
    } 
   public void closeMenu()
    {
        objMenu.SetActive(false);
        _isMenuOpen = false;
        Debug.Log("Menu Closed");
    }

 

     
}
