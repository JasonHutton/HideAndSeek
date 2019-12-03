using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    /// <summary>
    /// Public singleton instance.
    /// </summary>
    public static EffectManager Instance { get; private set; }

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
    }

    /// <summary>
    /// Plays an effect runner at a given location.
    /// </summary>
    /// <param name="key">Object pooler key.</param>
    /// <param name="pos">Position to play at.</param>
    /// <param name="particleScale">Size of particle effect.</param>
    public void PlayAt(string key, Vector2 pos, float particleScale)
    {
        EffectRunner e = ObjectPooler.Instance.Pop(key).GetComponent<EffectRunner>();
        e.transform.position = pos;
        e.Play(particleScale);
    }
}
