using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;

    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2.0f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40.0f;

    private GameObject lastTriggeredGameObj = null;

    //Awake function with Singleton
    private void Awake()
    {
        if(S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign secong Hero.S!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        //User Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //ALlows for movement vertically and horizontally using transform.position
        Vector3 pos = transform.position;
        pos.x = horizontal * speed * Time.deltaTime;
        pos.y = vertical * speed * Time.deltaTime;
        transform.position += new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime, 0);

        //Ship tilting
        transform.rotation = Quaternion.Euler(vertical * pitchMult, horizontal * rollMult, 0);

        //Fire on space bar click (not required)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }

    //When there is a trigger collision
    private void OnTriggerEnter(Collider collision)
    {
        Transform rootT = collision.gameObject.transform.root;
        GameObject gameObj = rootT.gameObject;

        //So the player doesn't continuously trigger the same collider
        if(gameObj == lastTriggeredGameObj)
        {
            return;
        }
        lastTriggeredGameObj = gameObj;

        //If the player collides with an enemy ship, destroy the enemy and the player
        if(gameObj.tag == "Enemy")
        {
            Destroy(gameObj);
            Destroy(this.gameObject);
            Main.S.DelayedRestart(gameRestartDelay);
        }
    }

    //Firing method (not required)
    private void TempFire()
    {
        GameObject projGameObj = Instantiate(projectilePrefab);
        projGameObj.transform.position = transform.position;
        Rigidbody rigidBody = projGameObj.GetComponent<Rigidbody>();
        rigidBody.velocity = Vector3.up * projectileSpeed;
    }
}
