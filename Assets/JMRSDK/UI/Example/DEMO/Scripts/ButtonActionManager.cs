using JMRSDK.Toolkit;
using JMRSDK.UI;
using JMRSDK.UI.Mediator;
using UnityEngine;

/// <summary>
/// Will set specific actions on spawned buttons in UI as per ID
/// </summary>
public class ButtonActionManager : MonoBehaviour
{
    //private string id;

    //private void OnDisable()
    //{
    //    UnRegisterAction(id);
    //}

    //public void RegisterAction(string ID)
    //{
    //    id = ID;

    //    GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() =>
    //    {
    //        JMRUIManager.QueueTestPopUp?.Invoke(ID);
    //    });
    //}

    //public void UnRegisterAction(string ID)
    //{
    //    GetComponent<JMRUIPrimaryButton>().OnClick.RemoveListener(() =>
    //    {
    //        JMRUIManager.QueueTestPopUp?.Invoke(ID);
    //    });
    //}
}
