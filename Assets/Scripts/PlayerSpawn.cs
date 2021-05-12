using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject editButton;
    public GameObject worldcreatorui;
    public GameObject objectselector;
    public Transform spawnpos;
   
    
 public void oncreationDone()
    {
        player.transform.position = spawnpos.position;
        editButton.SetActive(true);
        PlacerObj.instance.activeObject = null;
        player.SetActive(true);
        worldcreatorui.SetActive(false);
        objectselector.SetActive(false);
        
        
    } 

    public void Editmap()
    {
        editButton.SetActive(false);
        player.SetActive(false);
        worldcreatorui.SetActive(true);
        objectselector.SetActive(true);

    }


}
