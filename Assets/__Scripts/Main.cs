using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;

    public GameObject[] prefabEnemies;
    public float enemySpawnsPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;
    private BoundsCheck boundsCheck;

    private void Awake()
    {
        S = this;
        boundsCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnsPerSecond);
    }

    public void SpawnEnemy()
    {

        int index = Random.Range(0, prefabEnemies.Length);
        GameObject gameObj = Instantiate(prefabEnemies[index]);

        float enemyPadding = enemyDefaultPadding;
        if(gameObj.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(gameObj.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = new Vector3(0,0,0);
        float xMin = -boundsCheck.camWidth + enemyPadding;
        float xMax = boundsCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = boundsCheck.camHeight + enemyPadding;
        gameObj.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnsPerSecond);
    }

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene("_Scene_0");
    }
}
