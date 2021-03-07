using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This was not a required component
public class Projectile : MonoBehaviour
{
    private BoundsCheck boundsCheck;
    public Rigidbody rb;

    //Initialize BoundsCheck and Rigidbody fields
    private void Awake()
    {
        boundsCheck = GetComponent<BoundsCheck>();
        rb = GetComponent<Rigidbody>();
    }

    //If the projectile goes off the top of the screen, destroy it
    void Update()
    {
        if (boundsCheck.offTop)
        {
            Destroy(gameObject);
        }
    }
}
