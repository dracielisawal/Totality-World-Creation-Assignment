using System.Collections.Generic;
using UnityEngine;
using JMRSDK.Toolkit;
using System.Collections;

public class HorizontalScrollController : MonoBehaviour
{

    [SerializeField]
    JMRNestedInfiniteScroll scrollView;

    [SerializeField]
    private int _width, itemCount;
    [SerializeField]
    private List<Sprite> Contents;


    private void Awake()
    {
        scrollView.OnFill += OnFillItem;
        scrollView.OnPull += OnPullLeft;

        SetScrollViewData();
        //StartCoroutine(WaitTillPoolingComplete());
    }

    private void OnPullLeft(JMRNestedInfiniteScroll.Direction obj)
    {
        if (itemCount < 40)
        {
            scrollView.ApplyDataTo(itemCount, itemCount + 10, JMRNestedInfiniteScroll.Direction.Right);
            itemCount += 10;
        }
    }

    IEnumerator WaitTillPoolingComplete()
    {
        while (scrollView.isPooling)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

    void OnFillItem(int index, GameObject item)
    {
        item.GetComponent<SampleControl>().img.sprite = Contents[Random.Range(0, Contents.Count)];
    }

    public void SetScrollViewData()
    {
        scrollView.InitData(itemCount);
    }

    private int GetItemWidth(int index)
    {
        return _width;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scrollView.RecycleAll();
            scrollView.Prefab = scrollView.GetPrefabFromPool(Random.Range(0, 3));
            scrollView.InitData(itemCount);
        }
    }

    private void OnDestroy()
    {
        scrollView.OnFill -= OnFillItem;
        scrollView.OnPull -= OnPullLeft;
    }
}
