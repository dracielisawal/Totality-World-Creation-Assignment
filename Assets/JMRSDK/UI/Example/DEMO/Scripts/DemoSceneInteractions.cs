using JMRSDK.InputModule;
using JMRSDK.UI;
using JMRSDK.Utilities;
using System;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class DemoSceneInteractions : MonoBehaviour/*, IBackHandler*/
{
    //string path;
    //StreamWriter writer;

    //public TMP_Text normalLogger;

    //private void Start()
    //{
    //    path = Application.persistentDataPath + "\\";
    //    JMRSDK.UI.Logger.logFromSystemUI += Log;
    //    JMRDebugLogs.logFromJMRSDK += Log;
    //    RefreshLogFile();
    //    JMRInputManager.Instance.AddGlobalListener(this.gameObject);
    //}

    //public void TEST_ShowRandomPopUp()
    //{
    //    JMRUIManager.QueueRandomPopupFromConfig?.Invoke();
    //}

    //public void TEST_CLoseVisiblePopup()
    //{
    //    JMRUIManager.ForceCloseRandomShownPopup?.Invoke();
    //}

    //public void TEST_TriggerNewError(int i)
    //{
    //    JMRUIManager.FireNewErrorType?.Invoke(i);
    //}

    //public void OnBackAction()
    //{
    //    JMRUIManager.ForceCloseRandomShownPopup?.Invoke();
    //}

    //public void TEST_ClearAllPopups()
    //{
    //    JMRUIManager.Cheat_RemoveAllPopups?.Invoke();
    //}
    //void Log(string log)
    //{
    //    MainThreadDispatcher.Execute(() =>
    //    {
    //        Debug.Log(log);
    //        if (normalLogger != null && normalLogger.gameObject.activeSelf)
    //        {
    //            LogToDisplay(log);
    //        }
    //        writer.Write("[" + DateTime.Now + "]: " + log + "\n");
    //    });
    //}

    //public void RefreshLogFile()
    //{
    //    string filename = "SystemUI_" + DateTime.Now.ToString().Replace(":", string.Empty) + "_Log.txt";
    //    filename = Regex.Replace(filename, @"[: -/\\]", string.Empty);
    //    if (writer != null)
    //    {
    //        writer.Close();
    //    }
    //    writer = new StreamWriter(path + filename, true);
    //    Debug.Log("MaddyTest: Logs: writing at : " + path);
    //    Log(" ############### New Log ############### ");
    //}

    //void LogToDisplay(string logs)
    //{
    //    normalLogger.text += "[" + DateTime.Now + "]: " + logs + "\n";

    //    //maybe unnecessary just cleaning text field if it goes above few characters
    //    if (normalLogger.text.Length > 5000)
    //    {
    //        normalLogger.text = normalLogger.text.Substring(normalLogger.text.Length / 2, normalLogger.text.Length / 2) + "\n\nClearing old logs \n\n";
    //    }

    //    Canvas.ForceUpdateCanvases();
    //    RectTransform rect = normalLogger.transform.parent.GetComponent<RectTransform>();
    //    if (rect.sizeDelta.y > 450)
    //    {
    //        normalLogger.transform.parent.GetComponent<RectTransform>().localPosition = new Vector3(rect.rect.x, rect.sizeDelta.y - 400);
    //    }
    //}


}
