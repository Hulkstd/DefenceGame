using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFreshUndead : Unit
{
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
        TriggerEnter(collision, "AR");
    }
}