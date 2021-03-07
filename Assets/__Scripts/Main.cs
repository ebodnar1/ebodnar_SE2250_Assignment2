using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;

    //Fields for enemy spawning and bounds checking
    public GameObject[] prefabEnemies;
    public float enemySpawnsPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;
    private BoundsCheck boundsCheck;

    //Instantiate fields and invoke the SpawnEnemy function on awakening
    private void Awake()
    {
        S = this;
        boundsCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnsPerSecond);
    }

     
    public void SpawnEnemy()
    {
        //Spawn a random enemy based on the array of enemy prefabs
        int index = Random.Range(0, prefabEnemies.Length);
        GameObject gameObj = Instantiate(prefabEnemies[index]);

        //Add padding
        float enemyPadding = enemyDefaultPadding;
        if(gameObj.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(gameObj.GetComponent<BoundsCheck>().radius);
        }

        //Check the bounds of the game screen and use that to determine a random spawning position
        Vector3 pos = new Vector3(0,0,0);
        float xMin = -boundsCheck.camWidth + enemyPadding;
        float xMax = boundsCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = boundsCheck.camHeight + enemyPadding;
        gameObj.transform.position = pos;

        //Invoke the spawning function again recursively
        Invoke("SpawnEnemy", 1f / enemySpawnsPerSecond);
    }

    //Restart the scene upon a delay
    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    //Restart the scene
    public void Restart()
    {
        SceneManager.LoadScene("_Scene_0");
    }
}
