using UnityEngine;
using JMRSDK.Toolkit;
using System;

public class VerticalScrollController : MonoBehaviour
{

    [SerializeField]
    JMRNestedInfiniteScroll scrollView;

    [SerializeField]
    private int _height,itemCount;

    private void Awake()
    {

        scrollView.InitData(itemCount);
    }

    private void OnEnable()
    {
        scrollView.OnFill += OnFillItem;
        scrollView.OnPull += OnPullDown;
        scrollView.OnInitializationComplete += OnInitializationComplete;
    }

    private void OnInitializationComplete()
    {
    }

    private void OnPullDown(JMRNestedInfiniteScroll.Direction obj)
    {
        if (itemCount < 40)
        {
            scrollView.ApplyDataTo(itemCount, itemCount + 10, JMRNestedInfiniteScroll.Direction.Bottom);
            itemCount += 10;
        }
    }

    void OnFillItem(int index, GameObject item)
    {
        // item.GetComponent<HorizontalScrollController>().SetText(index.ToString());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            OnPullDown(JMRNestedInfiniteScroll.Direction.Bottom);
    }

    public void SetScrollViewData()
    {
    }

    private int GetItemHeight(int index)
    {
        return _height;
    }

    private void OnDisable()
    {
        scrollView.OnFill -= OnFillItem;
        scrollView.OnPull -= OnPullDown;
        scrollView.OnInitializationComplete -= OnInitializationComplete;
    }
}
