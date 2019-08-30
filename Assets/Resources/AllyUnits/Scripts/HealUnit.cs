using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealUnit : Unit
{
    //parent의 rigidbody를 받아와서 콜리더의 값을 변경해주는 스크립트
    private void Awake()
    {
        DefaultSetting();
    }

    private void Update()
    {
        Loop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter(collision, "ER");
    }
}
