using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck boundsCheck;
    public Rigidbody rb;


    private void Awake()
    {
        boundsCheck = GetComponent<BoundsCheck>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boundsCheck.offTop)
        {
            Destroy(gameObject);
        }
    }
}
