using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public int health;
    private bool alive;
    public string dieEffectKey;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfShouldDie())
        {
            Die();
        }
    }

    public void InflictDamage(int value)
    {
        if (alive)
        {
            health -= value;
        }

        if(CheckIfShouldDie())
        {
            Die();
        }
    }

    bool CheckIfShouldDie()
    {
        if (alive && health <= 0)
            return true;
        else
            return false;
    }

    void Die()
    {
        alive = false;
        EffectManager.Instance.PlayAt(dieEffectKey, transform.position, 1.0f);
        Destroy(gameObject);
    }
}
