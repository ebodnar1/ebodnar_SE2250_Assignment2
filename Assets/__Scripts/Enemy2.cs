using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    private float direction = 1;

    //Determine a random direction
    private void Awake()
    {
        direction = Random.Range(0, 2);
        speed *= 0.7f;
    }

    //Overrides the Enemy Move function
    public override void Move()
    {
        Vector3 temp = pos;

        //If the direction number is 0, move the enemy horizontally to the right
        if (direction == 0)
        {
            temp.x += speed * Time.deltaTime;
        }
        //If the direction number is 1, move the enemy horizontally to the left
        else if (direction == 1)
        {
            temp.x -= speed * Time.deltaTime;
        }

        //Update the position of the enemy
        pos = temp;
        //Call the base Move function so the enemy moves downwards as well as horizontally so as to move at a 45° angle
        base.Move();
    }

    
}
