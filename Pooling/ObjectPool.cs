using System.Collections;
using System.Collections.Generic;
using Physics.Influences;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    protected T _prefab { get; }
    protected Transform _activeParent { get; }
    protected Transform _inactiveParent { get; }
    protected Queue<T> _pool { get; } = new();

    public ObjectPool(
        T prefab,
        int count,
        int maxCount,
        Transform activeParent,
        Transform inactiveParent
    )
    {
        _prefab = prefab;
        _activeParent = activeParent;
        _inactiveParent = inactiveParent;

        for (int i = 0; i < count; i++)
        {
            Create();
        }
    }

    protected T Create()
    {
        T instance = Object.Instantiate(_prefab, _inactiveParent);
        instance.gameObject.SetActive(false);
        return instance;
    }

    public void Return(T instance)
    {
        if (instance.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }

        if (instance.TryGetComponent(out Contacts contacts))
        {
            contacts.Clear();
        }

        instance.gameObject.SetActive(false);
        instance.transform.SetParent(_inactiveParent, false);
        _pool.Enqueue(instance);
    }

    public void Return(
        T instance,
        MonoBehaviour runner,
        float delay
    )
    {
        runner.StartCoroutine(ReturnAfterDelay(instance, delay));
    }

    protected IEnumerator ReturnAfterDelay(T instance, float delay)
    {
        yield return new WaitForSeconds(delay);
        Return(instance);
    }

    public T Get(
        Vector3 position,
        Quaternion rotation,
        Transform parent = null)
    {
        T instance = _pool.Count > 0
            ? _pool.Dequeue()
            : Create();

        Transform targetParent = parent != null
            ? parent
            : _activeParent;

        instance.transform.SetParent(targetParent, false);
        instance.transform.SetPositionAndRotation(position, rotation);

        instance.gameObject.SetActive(true);

        return instance;
    }

    public void Clear()
    {
        while (_pool.Count > 0)
        {
            T instance = _pool.Dequeue();
            if (instance != null)
            {
                Object.Destroy(instance.gameObject);
            }
        }
    }
}
