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
        Debug.Log("Game Over....");

        //아군 성 체력 다 닳면 어떻게 할지 정하기
    }
}
