using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Rigidbody ownerRB;
    public AudioSource shieldUpSound;
    public float totalShieldCharge;
    public float shieldCharge;
    public float shieldDepletionPerTick;
    public float shieldRechargePerTick;

    private void Awake()
    {
        shieldCharge = totalShieldCharge;
    }

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

    private void FixedUpdate()
    {
        if(GetShieldActive())
        {
            shieldCharge -= shieldDepletionPerTick;
            
            if(shieldCharge < shieldDepletionPerTick)
            {
                SetShieldActive(false);
            }
        }

        // This is bugged at the moment due to how shields are disabled resulting in this script not running. Not going to fix for the project.
        if (shieldCharge < totalShieldCharge - shieldRechargePerTick)
        {
            shieldCharge += shieldRechargePerTick;
        }
    }

    public void SetShieldActive(bool on)
    {
        if(on == true && shieldCharge >= shieldDepletionPerTick)
        {
            gameObject.SetActive(true);
            shieldUpSound.Play();
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    public bool GetShieldActive()
    {
        return gameObject.activeSelf;
    }
}
