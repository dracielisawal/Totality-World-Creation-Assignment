using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using JMRSDK.InputModule;
using System.Collections;
namespace JMRSDK.Toolkit
{
    public class JMRCustomScrollRect : ScrollRect, ISwipeHandler
    {
        private ScrollRect j_ParentScroll;
        private PointerEventData j_PntrData;
        private Vector2 j_TempVelocity;
        private bool isVerticalScroll;

        protected override void Start()
        {
            base.Start();
            if (!Application.isPlaying)
                return;
            j_ParentScroll = GetScrollParent(transform);
            CreatePointerEventData();
        }

        private void CreatePointerEventData()
        {
            if (j_PntrData != null) { return; }
            StartCoroutine(WaitTillEventSystemLoads());
        }

        IEnumerator WaitTillEventSystemLoads()
        {
            while (!EventSystem.current)
            {
                yield return new WaitForEndOfFrame();
            }
            if (j_PntrData != null) { yield break; }
            j_PntrData = new PointerEventData(EventSystem.current);
            j_PntrData.Reset();
        }

        ScrollRect GetScrollParent(Transform t)
        {
            if (t.parent != null)
            {
                ScrollRect scroll = t.parent.GetComponent<ScrollRect>();
                if (scroll != null) { return scroll; }
                else return GetScrollParent(t.parent);
            }
            return null;
        }

        public override void OnScroll(PointerEventData data)
        {
            return;
        }

        private void Update()
        {
            if (!Application.isPlaying)
                return;

            velocity = Vector2.Lerp(velocity, Vector2.zero, scrollSensitivity * Time.deltaTime);
        }

        public void ProcessScroll(bool isXAxis, float eventData)
        {
           

            isVerticalScroll = !isXAxis;
            j_TempVelocity.x = isXAxis ? eventData : 0;
            j_TempVelocity.y = !isXAxis ? eventData : 0;

            if (j_ParentScroll != null && ((j_ParentScroll.vertical && isVerticalScroll) || (j_ParentScroll.horizontal && !isVerticalScroll)))
            {
                j_ParentScroll.velocity = j_TempVelocity * j_ParentScroll.scrollSensitivity * 3000;
            }
            else
            {
                velocity = j_TempVelocity * scrollSensitivity * 3000;
            }
           
        }

        public void OnSwipeLeft(SwipeEventData eventData, float value)
        {
            ProcessScroll(true, value);
        }

        public void OnSwipeRight(SwipeEventData eventData, float value)
        {
            ProcessScroll(true, value);
        }

        public void OnSwipeUp(SwipeEventData eventData, float value)
        {
            ProcessScroll(false, value);
        }

        public void OnSwipeDown(SwipeEventData eventData, float value)
        {
            ProcessScroll(false, value);
        }

        public void OnSwipeStarted(SwipeEventData eventData)
        {

        }

        public void OnSwipeUpdated(SwipeEventData eventData, Vector2 swipeData)
        {

        }

        public void OnSwipeCompleted(SwipeEventData eventData)
        {

        }

        public void OnSwipeCanceled(SwipeEventData eventData)
        {

        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            base.OnInitializePotentialDrag(eventData);
            if (j_ParentScroll != null)
            {
                j_ParentScroll.OnInitializePotentialDrag(eventData);
            }
        }

        Vector2 dragDelta;
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            if (j_ParentScroll != null)
            {
                j_ParentScroll.OnBeginDrag(eventData);
            }
            dragDelta = eventData.position;
        }

        private bool isHorizontal = true;
        public override void OnDrag(PointerEventData eventData)
        {
            isHorizontal = DesideIsHoriZontalScroll(eventData);
            if ((isHorizontal && horizontal) || (vertical && !isHorizontal))
            {
                base.OnDrag(eventData);
                if (j_ParentScroll != null)
                    j_ParentScroll.velocity = Vector2.zero;
            }
            else if (j_ParentScroll != null)
            {
                j_ParentScroll.OnDrag(eventData);
                velocity = Vector2.zero;
            }
        }

        private bool isPrevHorizontal = true;
        private float horizontalDrag = 0,verticalDrag=0;
        private float dragOffset = 30;
        private bool DesideIsHoriZontalScroll(PointerEventData eventData)
        {
            horizontalDrag = Mathf.Abs(eventData.position.x - dragDelta.x);
            verticalDrag = Mathf.Abs(eventData.position.y - dragDelta.y);
            isPrevHorizontal = horizontalDrag > verticalDrag ? !isPrevHorizontal && (horizontalDrag > 0.025f) ? true : isPrevHorizontal : (isPrevHorizontal && verticalDrag > 0.025f) ? false : isPrevHorizontal;  //(horizontalDrag >= verticalDrag ? (!isPrevHorizontal ? ((horizontalDrag > dragOffset) ? true : isPrevHorizontal) : isPrevHorizontal) : (isPrevHorizontal ? ((verticalDrag > dragOffset) ? false : isPrevHorizontal):isPrevHorizontal));
            dragDelta = eventData.position;
            return isPrevHorizontal;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            if (j_ParentScroll != null)
            {
                j_ParentScroll.OnEndDrag(eventData);
            }
        }
    }
}