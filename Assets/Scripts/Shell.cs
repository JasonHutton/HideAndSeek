using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : Poolable
{
    // Start is called before the first frame update
    //public Vector3 velocity;
    private Rigidbody rb;
    //public bool alive;
    //private float shotTime;

    // old
    public float speed;
    public float lifetime;
    public int damage;
    private Coroutine despawnCoroutine;
    // old
    public string hitEffectKey;
    public string dieEffectKey;

    void Awake()//Start()
    {
        //alive = true;
        //shotTime = Time.fixedTime;
        rb = GetComponent<Rigidbody>();
        //rb.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.fixedTime > shotTime + 2)
            //Destroy(this.gameObject);
            //alive = false;
    }

    private void FixedUpdate()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (despawnCoroutine != null)
        {
            StopCoroutine(despawnCoroutine);
            despawnCoroutine = null;
        }

        // TODO: maybe damageable objects should have a tag? string comparison may be faster than getting component and null checking
        GameObject hitObj = col.gameObject;
        if(hitObj.CompareTag("Tank"))
        {
            Tank tank = hitObj.GetComponentInParent<Tank>();
            if (tank != null && !tank.shielded)
            {
                tank.InflictDamage(damage);
            }
        }

        Hit();
        ReturnToPool();
    }

    public void OnTriggerEnter(Collider other)
    {
        Shield shield = other.gameObject.GetComponent<Shield>();
        if (shield != null)
        {
            // Is it someone else's shot?
            if(shield.gameObject.layer != gameObject.layer)
            {
                if (despawnCoroutine != null)
                {
                    StopCoroutine(despawnCoroutine);
                    despawnCoroutine = null;
                }
                Die();
                ReturnToPool();
            }
        }
    }

    public void Fire(Vector3 direction, Vector3 position, Quaternion rotation)
    {
        rb.position = transform.position = position;
        rb.rotation = transform.rotation = rotation;
        rb.velocity = direction.normalized * speed;
        despawnCoroutine = StartCoroutine(DespawnTimer());

        //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // disappear after the bullet's lifetime is up and return it to its pool
    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    public void Die()
    {
        if (!string.IsNullOrEmpty(dieEffectKey))
        {
            EffectManager.Instance.PlayAt(dieEffectKey, transform.position, 1.0f);
        }
    }

    public void Hit()
    {
        if (!string.IsNullOrEmpty(hitEffectKey))
        {
            EffectManager.Instance.PlayAt(hitEffectKey, transform.position, 1.0f);
        }
    }
}
