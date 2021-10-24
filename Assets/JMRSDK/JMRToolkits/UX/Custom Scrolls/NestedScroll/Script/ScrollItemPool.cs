using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JMRSDK.Toolkit
{
    public class ScrollItemPool : MonoBehaviour
    {
        struct PoolObjectData
        {
            public string key;
            public GameObject poolObject;
            public int count;
        }

        public bool isPooling = false;
        protected Action OnItemPoolingComplete = null;
        private Queue<PoolObjectData> itemsToBePooled = new Queue<PoolObjectData>();
        private Dictionary<string, List<GameObject>> objectPool = new Dictionary<string, List<GameObject>>();
        private Transform poolParent = null;
        private Coroutine currentCoroutine = null;

        protected virtual void OnEnable()
        {
            if (!poolParent)
            {
                SpawnPoolParent();
            }
            if (isPooling)
            {
                StopAllCoroutines();
                StartCoroutine(StartObjectPooling(true));
            }
            else if (itemsToBePooled.Count > 0)
            {
                StopAllCoroutines();
                StartCoroutine(StartObjectPooling());
            }
        }

        private void OnDisable()
        {
            currentCoroutine = null;
        }

        private void SpawnPoolParent()
        {
            if (!poolParent)
            {
                poolParent = new GameObject("PoolParent").transform;
                poolParent.transform.parent = transform;
                poolParent.gameObject.SetActive(false);
            }
        }

        public void PoolObjects(GameObject type, int count)
        {
            if (!poolParent)
            {
                SpawnPoolParent();
            }

            foreach (PoolObjectData item in itemsToBePooled)
            {
                if (item.key == type.name)
                {
                    return;
                }
            }

            itemsToBePooled.Enqueue(new PoolObjectData() { key = type.name, poolObject = type, count = count });
            if (!isPooling)
            {
                StartCoroutine(StartObjectPooling());
            }
        }

        private int currSpawnIndex = 0;
        private PoolObjectData poolObjectData = default;
        IEnumerator StartObjectPooling(bool resumePool = false)
        {
            if (!isPooling)
            {
                currSpawnIndex = 0;
            }
            isPooling = true;
            if (!resumePool)
            {
                poolObjectData = itemsToBePooled.Dequeue();
            }
            for (int i = currSpawnIndex; i < poolObjectData.count; i++)
            {
                if (objectPool.ContainsKey(poolObjectData.key))
                {
                    List<GameObject> objects = objectPool[poolObjectData.key];
                    GameObject obj = Instantiate(poolObjectData.poolObject);
                    obj.SetActive(false);
                    obj.transform.SetParent(poolParent,false);
                    objects.Add(obj);
                }
                else
                {
                    GameObject obj = Instantiate(poolObjectData.poolObject);
                    obj.SetActive(false);
                    obj.transform.SetParent(poolParent, false);
                    objectPool.Add(poolObjectData.key, new List<GameObject>() { obj });
                }
                currSpawnIndex++;
                yield return new WaitForEndOfFrame();
            }
            if (itemsToBePooled.Count > 0)
            {
                isPooling = false;
                poolObjectData = default;
                StartCoroutine(StartObjectPooling());
            }
            else
            {
                isPooling = false;
                poolObjectData = default;
                OnItemPoolingComplete?.Invoke();
            }
        }

        public void RecycleAll()
        {
            foreach (KeyValuePair<string, List<GameObject>> pool in objectPool)
            {
                foreach (GameObject item in pool.Value)
                {
                    item.transform.parent = poolParent;
                }
            }
        }

        public List<GameObject> GetPooledObject(GameObject type)
        {
            if (objectPool.ContainsKey(type.name))
            {
                return objectPool[type.name];
            }
            else
            {
                return null;
            }
        }
    }
}
