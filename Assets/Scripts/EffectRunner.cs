using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRunner : Poolable
{
    private AudioSource sound;
    private ParticleSystem particle;
    private Vector3 initialScale;

    void Awake()
    {
        gameObject.SetActive(false);

        sound = GetComponent<AudioSource>();

        Transform t = transform.Find("ExplosionEffect");
        if (t != null)
        {
            particle = t.GetComponent<ParticleSystem>();
            initialScale = t.localScale;
        }
    }

    public void Play(float particleScale)
    {
        gameObject.SetActive(true);
        if (sound != null) sound.Play();

        if (particle != null)
        {
            particle.transform.localScale = initialScale * particleScale;
            particle.Play();
        }

        StartCoroutine(DestroyWhenComplete());
    }

    IEnumerator DestroyWhenComplete()
    {
        float delay = 0.0f;
        if (particle != null)
        {
            delay = particle.main.duration;
        }
        if (sound != null && delay < sound.clip.length)
            delay = sound.clip.length;

        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }
}
