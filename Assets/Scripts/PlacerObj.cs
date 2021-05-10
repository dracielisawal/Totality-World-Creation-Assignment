using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerObj : MonoBehaviour
{
    public GameObject[] objPrefab;
    public GameObject activeObject;
    public Transform origin;
    public bool isObjActive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeObject !=null && isObjActive)
        {  

            
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

                if ( touch.phase == TouchPhase.Moved)
                {


                    activeObject.transform.position = new Vector3
                          (activeObject.transform.position.x + touch.deltaPosition.x * 0.1f,
                          activeObject.transform.position.y,
                          activeObject.transform.position.z + touch.deltaPosition.y * 0.1f);
                }
            }
        }

    } 

    public void OnRotate()   //simple rotation by 90 degree on Y axis
    {  
        if(activeObject != null)
        activeObject.transform.Rotate(0, -90, 0);
    } 


    public void onObjSelected(int index)
    {
        activeObject = Instantiate(objPrefab[index], origin);
        isObjActive = true;
    }

    public void onObjectPlaced()
    {
        activeObject = null; 
    }
}
