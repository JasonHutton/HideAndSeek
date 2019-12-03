using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Rigidbody ownerRB;
    public AudioSource shieldUpSound;

    // Start is called before the first frame update
    void Start()
    {
        ownerRB = gameObject.transform.parent.GetComponentInChildren<Rigidbody>();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ownerRB != null && gameObject.activeSelf)
        {
            gameObject.transform.position = ownerRB.transform.position;
        }
    }

    public void SetShieldActive(bool on)
    {
        gameObject.SetActive(on);
        if(on)
        {
            shieldUpSound.Play();
        }
    }

    public bool GetShieldActive()
    {
        return gameObject.activeSelf;
    }
}
