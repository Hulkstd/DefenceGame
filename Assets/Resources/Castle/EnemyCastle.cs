using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastle : Castle
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter(collision, "AR");
    }


    public override void GameEnd()
    {
        Debug.Log("Game over..");
    }
}
