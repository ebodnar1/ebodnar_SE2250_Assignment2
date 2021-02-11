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

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x = horizontal * speed * Time.deltaTime;
        pos.y = vertical * speed * Time.deltaTime;
        transform.position += new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime, 0);

        transform.rotation = Quaternion.Euler(vertical * pitchMult, horizontal * rollMult, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Transform rootT = collision.gameObject.transform.root;
        GameObject gameObj = rootT.gameObject;
        print("Triggered: " + gameObj.name);

        if(gameObj == lastTriggeredGameObj)
        {
            return;
        }
        lastTriggeredGameObj = gameObj;

        if(gameObj.tag == "Enemy")
        {
            Destroy(gameObj);
            Destroy(this.gameObject);
            Main.S.DelayedRestart(gameRestartDelay);
        }
        else
        {
            print("Triggered by non-enemy: " + gameObj.name);
        }
    }

    private void TempFire()
    {
        GameObject projGameObj = Instantiate(projectilePrefab);
        projGameObj.transform.position = transform.position;
        Rigidbody rigidBody = projGameObj.GetComponent<Rigidbody>();
        rigidBody.velocity = Vector3.up * projectileSpeed;
    }
}
