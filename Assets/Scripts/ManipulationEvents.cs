using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationEvents : MonoBehaviour
{
    public void onManipulationStart()
    {
        
    }
    public void onManipulationComplete()
    {
       PlacerObj.instance.activeObject = this.gameObject;
       Debug.LogError("Manipulation Complete, Active Object == " + PlacerObj.instance.activeObject );

    }
}
