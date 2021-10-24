// ----------------------------------------------------------------------------
// The MIT License
// Copyright (c) 2018-2020 Mopsicus <mail@mopsicus.ru>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JMRSDK.Toolkit
{

    /// <summary>
    /// Infinite scroller for long lists
    /// </summary>
    public class JMRNestedInfiniteScroll : ScrollItemPool
    {

        public enum Direction
        {
            None,
            Up,
            Bottom,
            Right,
            Left
        }

        public struct InfiniteScrollItemData
        {
            public int index;
            public RectTransform transform;
            public Vector2 indexPos;


            InfiniteScrollItemData(int index, RectTransform transform, Vector2 indexPos)
            {
                this.index = index;
                this.transform = transform;
                this.indexPos = indexPos;
            }

        }
        public Action<int, GameObject> OnFill = delegate { };
        public Action<Direction> OnPull = delegate { };
        public Action OnInitializationComplete = delegate { };
        public GameObject Prefab = null;
        public GameObject[] poolPrefabs;
        public int TopPadding = 10, BottomPadding = 10, ItemSpacing = 2;
        public int LeftPadding = 10, RightPadding = 10;
        public bool IsPullBottom = false;
        public bool IsPullLeft = false;
        public float PullValue = 2f;

        /// <summary>
        /// Delegate for heights
        /// </summary>
        public delegate int HeightItem(int index);

        /// <summary>
        /// Event for get item height
        /// </summary>
        public event HeightItem OnHeight;

        /// <summary>
        /// Delegate for widths
        /// </summary>
        public delegate int WidthtItem(int index);

        /// <summary>
        /// Event for get item width
        /// </summary>
        public event HeightItem OnWidth;

        /// <summary>
        /// Type of scroller
        /// </summary>
        [HideInInspector]
        public int Type;


        public ScrollRect scrollRect { get; private set; }
        public Direction currentScrollDirection { get; private set; }

        public bool isScrolling { get; private set; }
        public List<InfiniteScrollItemData> ActiveItemsSorted = new List<InfiniteScrollItemData>();

        public Action OnScrollUpdate;
        [SerializeField, Range(0, 10)]
        private int fillCount;
        public int ItemCount = 8;
        public bool isValidInfiniteScroll = false;
        public bool isCustomInit = true;
        private float curr_width = 0, curr_height = 0;
        private Action OnItemsSpawnComplete;
        [SerializeField, Range(0, 100)]
        public float ExtraSpace = 0;

        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            if (scrollRect.horizontal)
            {
                IsPullBottom = false;
            }
            else
            {
                IsPullLeft = false;
            }
            if (!IsValidInfiniteScroll())
            {
                return;
            }

            OnItemPoolingComplete = PoolComplete;
            foreach (GameObject spawnObject in poolPrefabs)
            {
                PoolObjects(spawnObject, fillCount);
            }

            if (!isCustomInit)
            {
                InitData(ItemCount);
            }
        }

        private void PoolComplete()
        {
            scrollRect.enabled = true;
            OnItemsSpawnComplete?.Invoke();
            OnInitializationComplete?.Invoke();
            OnItemsSpawnComplete = null;
        }

        private void Update()
        {
            if (!IsValidInfiniteScroll())
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Prefab = poolPrefabs[UnityEngine.Random.Range(0, poolPrefabs.Length)];
                InitData(ActiveItemsSorted.Count);
            }

            if (scrollRect.velocity.magnitude <= 0)
            {
                isScrolling = false;
                return;
            }

            isScrolling = true;
            currentScrollDirection = SetScrollDirection();
            ViewUpdate(currentScrollDirection);

            if (ExtraSpace > 0)
            {
                //AdjustSpace();
            }

            OnScrollUpdate?.Invoke();
        }

        private bool IsValidInfiniteScroll()
        {
            if (fillCount == 0)
            {
                isValidInfiniteScroll = false;
                return isValidInfiniteScroll;
            }

            if (scrollRect == null)
            {
                scrollRect = GetComponent<ScrollRect>();
            }

            if ((scrollRect.vertical && scrollRect.horizontal) || (!scrollRect.vertical && !scrollRect.horizontal))
            {
                isValidInfiniteScroll = false;
                return isValidInfiniteScroll;
            }
            else
            {
                isValidInfiniteScroll = true;
                return isValidInfiniteScroll;
            }
        }

        public void InitData(int itemCount)
        {
            if (!IsValidInfiniteScroll())
            {
                return;
            }

            if (ActiveItemsSorted.Count > 0)
            {
                RecycleAll();
            }

            List<GameObject> pooledItems = GetPooledObject(Prefab);
            if (pooledItems == null || pooledItems.Count < this.fillCount)
            {
                scrollRect.enabled = false;
                int spawnCount = this.fillCount;
                if (pooledItems != null && pooledItems.Count < this.fillCount)
                {
                    spawnCount = this.fillCount - pooledItems.Count;
                }
                if (OnItemsSpawnComplete == null)
                {
                    OnItemsSpawnComplete = () => InitData(itemCount);
                }
                PoolObjects(Prefab, spawnCount);
            }

            if (isPooling)
            {
                OnItemsSpawnComplete = () => InitData(itemCount);
                return;
            }

            this.ItemCount = itemCount;
            int fillCount = ItemCount < this.fillCount ? ItemCount : this.fillCount;
            scrollRect.content.anchoredPosition = Vector2.zero;
            ActiveItemsSorted.Clear();
            Vector3 startLocalPos = Vector3.zero;
            for (int i = 0; i < fillCount; i++)
            {
                RectTransform currItem = pooledItems[i].GetComponent<RectTransform>();
                if (i == 0)
                {
                    curr_width = currItem.sizeDelta.x;
                    curr_height = currItem.sizeDelta.y;
                    if (scrollRect.vertical)
                    {
                        startLocalPos = (Vector3.up * (TopPadding + curr_height * 0.5f)) + (Vector3.right * (curr_width * -0.5f));
                    }
                    else
                    {
                        startLocalPos = (Vector3.right * (LeftPadding + curr_width * 0.5f)) + (Vector3.up * (curr_height * -0.5f));
                    }
                    SetupContainer(curr_width, curr_height);
                }
                currItem.SetParent(scrollRect.content, false);
                currItem.localScale = Vector3.one;
                currItem.anchorMin = currItem.anchorMax = Vector2.up;
                currItem.pivot = Vector2.one * 0.5f;
                currItem.localRotation = Quaternion.identity;
                currItem.localPosition = (startLocalPos + ((scrollRect.vertical ? Vector3.up : Vector3.right) * (i * (ItemSpacing + (scrollRect.vertical ? curr_height : curr_width))))) * (scrollRect.vertical ? -1 : 1);

                ActiveItemsSorted.Add(new InfiniteScrollItemData() { index = i, transform = currItem, indexPos = currItem.localPosition });
            }
            for (int i = 0; i < fillCount; i++)
            {
                RectTransform currItem = pooledItems[i].GetComponent<RectTransform>();
                OnFill?.Invoke(i, currItem.gameObject);
                currItem.gameObject.SetActive(true);
            }
        }

        public void AdjustSpace()
        {
            Vector2 startPos = Vector2.zero;
            for (int i = 0; i < ActiveItemsSorted.Count; i++)
            {
                int centerIndex = ActiveItemsSorted[(int)(ActiveItemsSorted.Count * 0.5f)].index;
                if (i == 0)
                {
                    int index_diff = (centerIndex - ActiveItemsSorted[i].index);
                    startPos = ActiveItemsSorted[i].indexPos + (Vector2.up * ((index_diff) * (ExtraSpace)));
                }
                else if (centerIndex == ActiveItemsSorted[i].index)
                {
                    startPos = ActiveItemsSorted[i].indexPos;
                }
                else
                {
                    startPos -= Vector2.up * (curr_height + ItemSpacing + ExtraSpace);
                }
                if (centerIndex != ActiveItemsSorted[i].index)
                {
                    ActiveItemsSorted[i].transform.localPosition = startPos;
                }
            }
        }

        private void SetupContainer(float width, float height)
        {
            scrollRect.content.anchorMin = scrollRect.content.anchorMax = scrollRect.content.pivot = Vector2.up;

            Vector2 size = Vector2.zero;
            if (scrollRect.vertical)
            {
                size = (Vector2.right * width) + (Vector2.up * (((ItemSpacing + height) * ItemCount) - ItemSpacing + TopPadding + BottomPadding));
            }
            else
            {
                size = (Vector2.right * ((ItemSpacing * (ItemCount - 1)) + (width * ItemCount) + LeftPadding + RightPadding)) + (Vector2.up * height);
            }
            scrollRect.content.sizeDelta = size;
        }

        public void ViewUpdate(Direction currentScrollDirection)
        {
            switch (currentScrollDirection)
            {
                case Direction.Up:
                case Direction.Left:
                    HandleUpOrLeftScroll(currentScrollDirection);
                    break;
                case Direction.Bottom:
                case Direction.Right:
                    HandleDownAndRight(currentScrollDirection);
                    break;
            }
        }

        private void HandleUpOrLeftScroll(Direction currentScrollDirection)
        {
            float checkPos = 0;
            switch (currentScrollDirection)
            {
                case Direction.Up:
                    checkPos = scrollRect.content.anchoredPosition.y - ((fillCount - 1) * ExtraSpace) + (ActiveItemsSorted[0].transform.localPosition.y - curr_height * 0.5f - TopPadding);
                    break;
                case Direction.Left:
                    checkPos = (ActiveItemsSorted[0].transform.localPosition.x + curr_width * 0.5f + LeftPadding) - scrollRect.content.anchoredPosition.x * -1;
                    break;
            }


            InfiniteScrollItemData lastItemData = ActiveItemsSorted[ActiveItemsSorted.Count - 1];
            if ((scrollRect.vertical ? (checkPos > 0) ? true : false : (checkPos < 0) ? true : false) && lastItemData.index < ItemCount - 1)
            {
                InfiniteScrollItemData currItemData = ActiveItemsSorted[0];
                if (scrollRect.vertical)
                {
                    currItemData.transform.localPosition = lastItemData.indexPos - Vector2.up * (ItemSpacing + curr_height);
                }
                else
                {
                    currItemData.transform.localPosition = lastItemData.indexPos + Vector2.right * (ItemSpacing + curr_width);
                }
                ActiveItemsSorted.RemoveAt(0);
                currItemData.index = lastItemData.index + 1;
                currItemData.indexPos = currItemData.transform.localPosition;
                ActiveItemsSorted.Add(currItemData);
                OnFill?.Invoke(currItemData.index, currItemData.transform.gameObject);

                if ((IsPullBottom || IsPullLeft) && (currentScrollDirection == Direction.Up || currentScrollDirection == Direction.Left))
                {
                    if (ItemCount - currItemData.index <= PullValue + 1)
                    {
                        OnPull(currentScrollDirection);
                    }
                }
            }
        }

        public void ApplyDataTo(int count, int newCount, Direction direction)
        {
            if (direction == Direction.Right || direction == Direction.Bottom)
            {
                if (newCount > ItemCount)
                {
                    ItemCount = newCount;
                    SetupContainer(curr_width, curr_height);
                }
            }
        }

        private void HandleDownAndRight(Direction currentScrollDirection)
        {
            float checkPos = 0;
            switch (currentScrollDirection)
            {
                case Direction.Bottom:
                    checkPos = scrollRect.content.anchoredPosition.y + scrollRect.viewport.rect.height + ((fillCount - 1) * ExtraSpace) + (ActiveItemsSorted[ActiveItemsSorted.Count - 1].transform.localPosition.y + curr_height * 0.5f + TopPadding);
                    break;
                case Direction.Right:
                    checkPos = (ActiveItemsSorted[ActiveItemsSorted.Count - 1].transform.localPosition.x - curr_width * 0.5f - LeftPadding) - ((scrollRect.content.anchoredPosition.x + scrollRect.viewport.rect.width * -1) * -1);
                    break;
            }


            InfiniteScrollItemData firstItemData = ActiveItemsSorted[0];
            if ((scrollRect.vertical ? (checkPos < 0) ? true : false : (checkPos > 0) ? true : false) && firstItemData.index > 0)
            {
                InfiniteScrollItemData currItemData = ActiveItemsSorted[ActiveItemsSorted.Count - 1];
                if (scrollRect.vertical)
                {
                    currItemData.transform.localPosition = firstItemData.indexPos + Vector2.up * (ItemSpacing + curr_height);
                }
                else
                {
                    currItemData.transform.localPosition = firstItemData.indexPos - Vector2.right * (ItemSpacing + curr_width);
                }
                ActiveItemsSorted.RemoveAt(ActiveItemsSorted.Count - 1);
                currItemData.index = firstItemData.index - 1;
                currItemData.indexPos = currItemData.transform.localPosition;
                ActiveItemsSorted.Insert(0, currItemData);
                OnFill?.Invoke(currItemData.index, currItemData.transform.gameObject);
            }
        }

        private Direction SetScrollDirection()
        {
            Vector2 currentVelocityNormalized = scrollRect.velocity.normalized;
            if (currentVelocityNormalized == Vector2.up)
            {
                return Direction.Up;
            }
            else if (currentVelocityNormalized == Vector2.down)
            {
                return Direction.Bottom;
            }
            else if (currentVelocityNormalized == Vector2.right)
            {
                return Direction.Right;
            }
            else if (currentVelocityNormalized == Vector2.left)
            {
                return Direction.Left;
            }

            return currentScrollDirection;
        }

        public GameObject[] GetViews()
        {
            GameObject[] tempArray = new GameObject[ActiveItemsSorted.Count];
            for (int i = 0; i < ActiveItemsSorted.Count; i++)
            {
                tempArray[i] = ActiveItemsSorted[i].transform.gameObject;
            }

            return tempArray;
        }

        public GameObject GetPrefabFromPool(int index)
        {
            if (poolPrefabs != null && poolPrefabs.Length > index)
            {
                return poolPrefabs[index];
            }
            return null;
        }

        public int GetTotalItemCOunt()
        {
            return ActiveItemsSorted.Count;
        }
    }
}