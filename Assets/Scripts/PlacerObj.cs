using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerObj : MonoBehaviour
{
    public static PlacerObj instance = null;
    public GameObject[] objPrefab;
    public GameObject activeObject;
    public Transform origin;
    public GameObject activemenu;
    public bool isObjActive;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(activeObject !=null)
        {
            activemenu.SetActive(true);
            
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
        else
        {
            activemenu.SetActive(false);
        }

    } 

    public void OnRotate()   //simple rotation by 90 degree on Y axis
    {  
        if(activeObject != null)
        activeObject.transform.Rotate(0, -90, 0);
    } 
    public void OnDelete()
    {
        if (activeObject != null)
        {
            Debug.Log("Deleted");
            Destroy(activeObject);
            activeObject = null;
        }
        
    }
    public void Onplaced()
    {
        if (activeObject != null)
        { 
            
            Debug.Log("Placed");
            activeObject = null;
        }
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
