using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public float speed = 10.0f;
    public float rateOfFire = 0.3f;
    public float health = 100;
    protected BoundsCheck boundsCheck;

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

    // Use this for initialization
    void Start()
    {
        boundsCheck = gameObject.GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
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

    public virtual void Move()
    {
        Vector3 temp = pos;
        temp.y -= speed * Time.deltaTime;
        pos = temp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject projectileObj = collision.gameObject;
        if(projectileObj.tag == "HeroProjectile")
        {
            Destroy(projectileObj);
            Destroy(gameObject);
        }
        else
        {
            print("Enemy hit by non-hero projectile: " + projectileObj.name);
        }
    }
}
