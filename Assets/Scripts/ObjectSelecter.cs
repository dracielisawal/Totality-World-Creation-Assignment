using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "Moveableobj")
                {
                    Debug.Log(hitInfo.transform.gameObject.name);
                   PlacerObj.instance.activeObject = hitInfo.transform.gameObject;
                }

            }
            else
            {
                Debug.Log("No hit");
            }
            Debug.Log("Mouse is down");
        }
    }
}
