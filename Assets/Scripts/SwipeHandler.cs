using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;
public class SwipeHandler : MonoBehaviour, ISwipeHandler
{
    public void OnSwipeCanceled(SwipeEventData eventData) {
    Debug.Log("OnSwipeCanceled");
  }
  
  public void OnSwipeCompleted(SwipeEventData eventData) {
    Debug.Log("OnSwipeCompleted");
  }
  
  public void OnSwipeDown(SwipeEventData eventData, float delta) {
    Debug.Log("OnSwipeDown");
     transform.Translate(3*delta, 0, 0);
  }
  
  public void OnSwipeLeft(SwipeEventData eventData, float delta) {
    Debug.Log("OnSwipeLeft");
  }
  
  public void OnSwipeRight(SwipeEventData eventData, float delta) {
    Debug.Log("OnSwipeRight");
  }
  
  public void OnSwipeStarted(SwipeEventData eventData) {
    Debug.Log("OnSwipeStarted");
  }
  
  public void OnSwipeUp(SwipeEventData eventData, float delta) {
    Debug.Log("OnSwipeUp");
    transform.Translate(3*delta, 0, 0);
  }
  
  public void OnSwipeUpdated(SwipeEventData eventData, Vector2 delta) {
    Debug.Log("OnSwipeUpdated");
  }
}
