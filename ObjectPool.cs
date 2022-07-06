using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Sahab
{
    public class ObjectPool<T> where T : Component
    {
        private List<T> m_Pool;
        private T m_Prefab;

        public ObjectPool(T prefab, int amount)
        {
            m_Pool = new List<T>();
            if (!prefab)
            {
                return;
            }

            m_Prefab = prefab;

            for (int i = 0; i < amount; i++)
            {
                var tempObject = GameObject.Instantiate(prefab);
                tempObject.gameObject.SetActive(false);
                m_Pool.Add(tempObject);
            }
        }

        public T Spawn()
        {
            for (int i = 0; i < m_Pool.Count; i++)
            {
                if (m_Pool[i] != null && m_Pool[i].gameObject.activeInHierarchy == false)
                {
                    return m_Pool[i];
                }
            }
            return SpawnNewOne();
        }

        public T Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            for (int i = 0; i < m_Pool.Count; i++)
            {
                if (m_Pool[i] != null && m_Pool[i].gameObject.activeInHierarchy == false)
                {
                    m_Pool[i].transform.position = position;
                    m_Pool[i].transform.rotation = rotation;

                    if(parent)
                    {
                        m_Pool[i].transform.parent = parent;
                    }

                    m_Pool[i].gameObject.SetActive(true);

                    return m_Pool[i];
                }
            }

            T newObj = SpawnNewOne();
            newObj.transform.position = position;
            newObj.transform.rotation = rotation;

            if (parent)
            {
                newObj.transform.parent = parent;
            }

            newObj.gameObject.SetActive(true);
            return SpawnNewOne();
        }

        public void Release(T releasedObject) // or make the spawned object SetActive(false) on its own
        {
            releasedObject.gameObject.SetActive(false);
        }

        private T SpawnNewOne()
        {
            T t = GameObject.Instantiate(m_Prefab);
            m_Pool.Add(t);
            return t;
        }
    }
}
