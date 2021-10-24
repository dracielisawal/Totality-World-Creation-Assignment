using JMRSDK.Toolkit.UI;
using JMRSDK.UI;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will spawn buttons on UI depending on number of IDs in container
/// </summary>
public class UIButtonSpawner : MonoBehaviour
{
    //public Transform buttonHolder;

    //public GameObject buttonPrefab;
    //public void CreateButtons()
    //{
    //    foreach (Transform item in buttonHolder.transform)
    //    {
    //        Destroy(item.gameObject);
    //    }
    //    List<string> errorCodes = JMRUIManager.GetAllPopupCodesInConfig();

    //    Debug.Log(errorCodes.Count);

    //    errorCodes.ForEach(x =>
    //   {
    //       Debug.Log(x);
    //   });

    //    errorCodes.ForEach(x =>
    //    {
    //        var obj = Instantiate(buttonPrefab, buttonHolder.transform) as GameObject;
    //        obj.GetComponent<JMRThemeConfigHelper>().SetText(x);
    //        obj.GetComponent<ButtonActionManager>().RegisterAction(x);
    //        obj.SetActive(true);
    //    });
    //}
}