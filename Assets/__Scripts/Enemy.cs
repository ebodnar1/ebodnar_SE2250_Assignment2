using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    //Fields with information on the enemy
    public float speed = 10.0f;
    protected BoundsCheck boundsCheck;

    //Attribute for the position of the enemy object
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    //Initialize boundsCheck object
    void Start()
    {
        boundsCheck = gameObject.GetComponent<BoundsCheck>();
    }

    //Call the move function and check boundaries, destroying the enemy object if it is out of bounds
    void Update()
    {
        Move();

        if (boundsCheck != null && boundsCheck.offBottom)
        {
            Destroy(gameObject);
        }
        if (boundsCheck != null && boundsCheck.offLeft || boundsCheck != null && boundsCheck.offRight)
        {
            Destroy(gameObject);
        }
    }

    //Move downwards according to the enemy speed using the pos attribute
    public virtual void Move()
    {
        Vector3 temp = pos;
        temp.y -= speed * Time.deltaTime;
        pos = temp;
    }

    //Not required - if an enemy is hit by a hero projectile, destroy the enemy and the projectile
    private void OnCollisionEnter(Collision collision)
    {
        GameObject projectileObj = collision.gameObject;
        if(projectileObj.tag == "HeroProjectile")
        {
            Destroy(projectileObj);
            Destroy(gameObject);
        }
    }
}
