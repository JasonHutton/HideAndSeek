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
    /*

    public void Fire()
    {
        if (Time.fixedTime > reloadIntervalTimer)
        {
            reloadIntervalTimer = Time.fixedTime + reloadTime;

            GameObject shell = Instantiate(shellPrefab, ownerRB.position, ownerRB.rotation) as GameObject;
            shell.gameObject.layer = this.gameObject.layer;
            Shell shellScript = shell.GetComponentInChildren<Shell>();
            shellScript.velocity = ownerRB.transform.forward * shellVelocity;
            shells.Add(shell);
        }*/

        /*
         * 
         * 
         *          if (Input.GetKeyDown(KeyCode.LeftShift))
             {
                 GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                 bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
             }
         * 
         * 
         * 
         * 
         * 
         * 
         *      var projectile : Transform;
         var bulletSpeed : float = 20;

         function Update () {
             // Put this in your update function
             if (Input.GetButtonDown("Fire1")) {

             // Instantiate the projectile at the position and rotation of this transform
             var clone : Transform;
             clone = Instantiate(projectile, transform.position, transform.rotation);

             // Add force to the cloned object in the object's forward direction
             clone.rigidbody.AddForce(clone.transform.forward * shootForce);
             }
         }
         * 
         * 
         * */
    //}
}
