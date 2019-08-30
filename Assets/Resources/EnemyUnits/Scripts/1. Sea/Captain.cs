using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Unit
{
    private float parentSpeed;
    public BossPassive bossPassive;

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

    private void OnEnable()
    {
        SpeedUp(0.5f);
    }

    private void OnDisable()
    {
        SpeedDown(0.5f);
        bossPassive.Recovered();
    }

    
}
