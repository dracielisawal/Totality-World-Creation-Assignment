using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelecter : MonoBehaviour
{
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
               
                if (hitInfo.transform.gameObject.tag == "Interactable")
                {
                    Debug.Log(hitInfo.transform.gameObject.name);
                    //test.text = "sahi jagah lagi";
                   PlacerObj.instance.activeObject = hitInfo.transform.gameObject;
                }

            }
            else
            {
              Debug.Log( "No hit");
            }
            Debug.Log("Mouse is down");
        }
    }
}
