using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{

    public float radius = 2f;
    public float camWidth;
    public float camHeight;
    public bool isOnScreen = true;
    public bool keepOnScreen = true;
    public bool offLeft, offRight, offTop, offBottom;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        offLeft = offRight = offTop = offBottom = false;
        Vector3 pos = transform.position;
        isOnScreen = true;

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offTop = true;
        }
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offBottom = true;
        }

        isOnScreen = !(offTop || offBottom || offRight || offLeft);
        if(keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offLeft = offRight = offTop = offBottom = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
