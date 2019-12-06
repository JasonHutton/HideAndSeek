using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //public float shellVelocity;
    public float reloadTime; // How long it takes to reload.
    private float reloadIntervalTimer; // Timer for reloading.

    public GameObject shellPrefab;
    List<GameObject> shells;

    private Rigidbody ownerRB;

    // Start is called before the first frame update
    void Start()
    {
        shells = new List<GameObject>();
        ownerRB = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject go in shells)
        {
            Shell shellScript = go.GetComponentInChildren<Shell>();
            /*if(!shellScript.alive)
            {
                shells.Remove(go);
            }*/
        }
    }

    public void Fire()
    {
        if (Time.fixedTime > reloadIntervalTimer)
        {
            reloadIntervalTimer = Time.fixedTime + reloadTime;

            //gunSoundSource.Play();
            Shell shell = ObjectPooler.Instance.Pop("Shell").GetComponent<Shell>();
            shell.gameObject.layer = this.gameObject.layer;
            shell.Fire(ownerRB.transform.forward, ownerRB.position, ownerRB.rotation);
        }
    }

    public bool ReadyToFire()
    {
        if (Time.fixedTime > reloadIntervalTimer)
            return true;

        return false;
    }
}
