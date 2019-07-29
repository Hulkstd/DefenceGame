using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyCastle : Castle
{
    public Image hpImage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter(collision, "ER");
    }
    private void Update()
    {
        hpImage.fillAmount = hp / OriginalHP;
    }

    public override void GameEnd()
    {
        Debug.Log("Game Over....");

        //아군 성 체력 다 닳면 어떻게 할지 정하기
    }
}
