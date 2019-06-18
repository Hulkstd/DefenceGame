using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCastle : Castle
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter(collision, "ER");
    }


    public override void GameEnd()
    {
        Debug.Log("Game Clear!");
    }
}
