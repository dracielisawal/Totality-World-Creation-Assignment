using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject worldcreatorui;
    public GameObject objectselector;
   
    
 public void oncreationDone()
    {
        PlacerObj.instance.activeObject = null;
        player.SetActive(true);
        worldcreatorui.SetActive(false);
        objectselector.SetActive(false);
        
    }
   

}
