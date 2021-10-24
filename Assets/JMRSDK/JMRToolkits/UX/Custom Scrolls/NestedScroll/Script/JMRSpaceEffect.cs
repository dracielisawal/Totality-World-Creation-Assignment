using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JMRSDK.Toolkit;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScrollRect), typeof(JMRNestedInfiniteScroll))]
public class JMRSpaceEffect : MonoBehaviour
{
    //#region Serialize Field
    //#endregion

    //#region Private Fields
    //private RectTransform j_Content;
    //private Dictionary<int, Vector2> j_Items = new Dictionary<int, Vector2>();
    //private int j_Counter = -1;
    //private ScrollRect j_ScrollRect;
    //private JMRNestedInfiniteScroll j_InfiniteScroll;
    //private int j_LeftPadding, j_RightPadding, j_ItemSpacing;
    //private bool iSLoaded;
    //private float j_ContentWidth = 0;
    //private float j_PrevSpaceFactor = 0;
    //private float j_Itemwidth;
    //#endregion



    //// Start is called before the first frame update
    //void Start()
    //{
    //    j_ScrollRect = GetComponent<ScrollRect>();
    //    j_InfiniteScroll = GetComponent<JMRNestedInfiniteScroll>();
    //    j_Content = j_ScrollRect.content;
    //    j_InfiniteScroll.OnFill += OnFill;
    //}

    //private void OnFill(int arg1, GameObject arg2)
    //{
    //    if (j_Items.ContainsKey(arg2.GetInstanceID()))
    //    {
    //        j_Items[arg2.GetInstanceID()] = arg2.GetComponent<RectTransform>().anchoredPosition;
    //    }
    //    else
    //    {
    //        j_Items.Add(arg2.GetInstanceID(), arg2.GetComponent<RectTransform>().anchoredPosition);
    //    }
    //}

    //public List<GameObject> itemsAsceding = new List<GameObject>();
    //private void SortItemsInAscendingOrder()
    //{
    //    GameObject currentItem = null;
    //    float minX = 0, maxX = 0;
    //    for (int i = 0; i < j_Content.childCount; i++)
    //    {
    //        currentItem = j_Content.GetChild(i).gameObject;
    //        if (currentItem.activeInHierarchy)
    //        {
    //            if (i == 0)
    //            {
    //                minX = maxX = currentItem.GetComponent<RectTransform>().anchoredPosition.x;
    //                itemsAsceding.Add(currentItem);
    //            }
    //            else
    //            {
    //                if (currentItem.GetComponent<RectTransform>().anchoredPosition.x <= minX)
    //                {
    //                    minX = currentItem.GetComponent<RectTransform>().anchoredPosition.x;
    //                    itemsAsceding.Insert(0, currentItem);
    //                }
    //                else if (currentItem.GetComponent<RectTransform>().anchoredPosition.x >= minX)
    //                {
    //                    maxX = currentItem.GetComponent<RectTransform>().anchoredPosition.x;
    //                    itemsAsceding.Add(currentItem);
    //                }
    //                else
    //                {
    //                    if (itemsAsceding.Count == 2)
    //                    {
    //                        itemsAsceding.Insert(1, currentItem);
    //                    }
    //                    else
    //                    {
    //                        int newIndex = 0;
    //                        for (int j = 1; j < itemsAsceding.Count - 1; j++)
    //                        {
    //                            if (currentItem.GetComponent<RectTransform>().anchoredPosition.x <= itemsAsceding[j].GetComponent<RectTransform>().anchoredPosition.x)
    //                            {
    //                                newIndex = j;
    //                                break;
    //                            }
    //                            else if (j == itemsAsceding.Count - 1)
    //                            {
    //                                newIndex = j + 1;
    //                            }
    //                        }
    //                        itemsAsceding.Insert(newIndex, currentItem);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //private void UpdateItemsLastPos()
    //{
    //    GameObject currentItem = null;
    //    for (int i = 0; i < j_Content.childCount; i++)
    //    {
    //        currentItem = j_Content.GetChild(i).gameObject;
    //        if (currentItem.activeInHierarchy)
    //        {
    //            int instanceID = currentItem.GetInstanceID();
    //            if (j_Items.ContainsKey(instanceID))
    //            {
    //                j_Items[currentItem.GetInstanceID()] = currentItem.GetComponent<RectTransform>().anchoredPosition;
    //            }
    //            else
    //            {
    //                j_Items.Add(currentItem.GetInstanceID(), currentItem.GetComponent<RectTransform>().anchoredPosition);
    //            }
    //        }
    //    }
    //}
    //float spaceFactor = 0, content_pos_x = 0, cntrl = 0, val = 0;
    //void LateUpdate()
    //{
    //    if (Mathf.Abs(j_ScrollRect.velocity.x) <= 0)
    //    {
    //        UpdateItemsLastPos();
    //        return;
    //    }
    //    SortItemsInAscendingOrder();

    //    spaceFactor = Mathf.Abs((j_ScrollRect.velocity.x * 0.02f));
    //    content_pos_x = Mathf.Abs(j_Content.anchoredPosition.x);

    //    for (int i =0;i<itemsAsceding.Count;i++)
    //    {
    //            RectTransform item = itemsAsceding[i].GetComponent < RectTransform>() ;

    //        cntrl = (item.anchoredPosition.x - content_pos_x);
    //        if (cntrl > 0)
    //        {
    //            val = Mathf.Floor(cntrl / item.sizeDelta.x);
    //            item.anchoredPosition = Vector2.Lerp(item.anchoredPosition, new Vector2(j_Items[item.gameObject.GetInstanceID()].x + (val * spaceFactor), item.anchoredPosition.y), 2 * Time.deltaTime);
    //        }
    //        }
    //}
}
