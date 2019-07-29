using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCastle : Castle
{

    public Image hpImage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter(collision, "AR");
    }

    private void Update()
    {
        hpImage.fillAmount = hp / OriginalHP;
    }

    public override void GameEnd()
    {
        Debug.Log("Game Clear!");
        //적 성 체력 다 닳으면 어케할지 정하셈
    }
}
