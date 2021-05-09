using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerObj : MonoBehaviour
{
    public GameObject objPrefab;
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
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

                if ( touch.phase == TouchPhase.Moved)
                {


                    activeObject.transform.position = new Vector3
                          (activeObject.transform.position.x + touch.deltaPosition.x * 0.01f,
                          activeObject.transform.position.y,
                          activeObject.transform.position.z + touch.deltaPosition.y * 0.01f);
                }
            }
        }

    }

    public void onObjSelected()
    {
        activeObject = Instantiate(objPrefab, origin);
        isObjActive = true;
    }
}
