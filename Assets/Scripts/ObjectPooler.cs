using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    /// <summary>
    /// Public singleton instance.
    /// </summary>
    public static ObjectPooler Instance { get; private set; }
    [SerializeField]
    private PoolType[] poolTypes;
    private Dictionary<string, ObjectPool> dict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dict = new Dictionary<string, ObjectPool>();
        foreach (PoolType type in poolTypes)
        {
            ObjectPool pool = new ObjectPool
            {
                prefab = type.prefab,
                stack = new Stack<Poolable>(),
                shouldExpand = type.shouldExpand
            };

            dict.Add(type.name, pool);

            for (int i = 0; i < type.initialCount; i++)
            {
                Poolable obj = Instantiate(type.prefab);
                obj.PoolName = type.name;
                obj.transform.parent = transform;
                obj.gameObject.SetActive(false);

                pool.stack.Push(obj);
            }
        }
    }

    /// <summary>
    /// Push a poolable object into its pool and remove it from the scene.
    /// </summary>
    /// <param name="poolName">Tag of the poolable object's pool.</param>
    /// <param name="obj">Poolable object to return.</param>
    public void Push(string poolName, Poolable obj)
    {
        ObjectPool pool;
        if (dict.TryGetValue(poolName, out pool))
        {
            obj.gameObject.SetActive(false);
            pool.stack.Push(obj);
            pool.activeCount--;
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogErrorFormat("Attempted to push a type not in the dictionary: " + poolName);
        }
#endif
    }

    /// <summary>
    /// Pop a poolable object from its pool and add it to the scene.
    /// </summary>
    /// <param name="poolName">Tag of the poolable object's pool.</param>
    /// <returns>A poolable object of the requested type.</returns>
    public Poolable Pop(string poolName)
    {
        ObjectPool pool;
        Poolable obj = null;
        if (dict.TryGetValue(poolName, out pool))
        {
            pool.activeCount++;
            if (pool.stack.Count > 0)
            {
                obj = pool.stack.Pop();
                obj.gameObject.SetActive(true);
            }
            else if (pool.shouldExpand)
            {
                obj = Instantiate(pool.prefab);
                obj.PoolName = poolName;
                obj.transform.parent = transform;
            }
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogErrorFormat("Attempted to pop a type not in the dictionary: " + poolName);
        }
#endif

        return obj;
    }

    /// <summary>
    /// Returns the count of poolable objects belonging to a pool active in the scene.
    /// </summary>
    /// <param name="poolName">Tag of the poolable object's pool.</param>
    /// <returns>Number of poolable objects belonging to a pool active in the scene.</returns>
	public int ActiveCount(string poolName)
    {
        ObjectPool pool;
        if (dict.TryGetValue(poolName, out pool))
        {
            return pool.activeCount;
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogErrorFormat("Attempted to count a type not in the dictionary: " + poolName);
        }
#endif
        return 0;
    }

    /// <summary>
    /// Returns the prefab used to instantiate poolable objects of the given object pool.
    /// </summary>
    /// <param name="poolName">Tag of the poolable object's pool.</param>
    /// <returns></returns>
    public Poolable GetPrefab(string poolName)
    {
        ObjectPool pool;
        if (dict.TryGetValue(poolName, out pool))
        {
            return pool.prefab;
        }

        return null;
    }

    private class ObjectPool
    {
        public Poolable prefab;
        public Stack<Poolable> stack;
        public bool shouldExpand;
        public int activeCount;
    }

    [Serializable]
    private struct PoolType
    {
        public string name;
        public Poolable prefab;
        public int initialCount;
        public bool shouldExpand;
    }
}

public class Poolable : MonoBehaviour
{
    public string PoolName
    {
        get
        {
            return poolName;
        }
        set
        {
            poolName = poolName ?? value;
        }
    }
    private string poolName;

    /// <summary>
    /// Returns the poolable object to its object pool, and removes it from the scene.
    /// </summary>
    public void ReturnToPool()
    {
        ObjectPooler.Instance.Push(poolName, this);
    }
}
