using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    private float direction = 1;

    private void Awake()
    {
        direction = Random.Range(0, 2);
        speed *= 0.7f;
    }

    public override void Move()
    {
        Vector3 temp = pos;

        if (direction == 0)
        {
            temp.x += speed * Time.deltaTime;
        }
        else if(direction == 1)
        {
            temp.x -= speed * Time.deltaTime;
        }

        pos = temp;
        base.Move();
    }

    
}
