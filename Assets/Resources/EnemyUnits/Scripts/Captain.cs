using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Unit
{
    private float parentSpeed;

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
        SpeedUp();
    }

    private void OnDisable()
    {
        SpeedDown();
    }
}
