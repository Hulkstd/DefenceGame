using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Druid : Unit
{
    [SerializeField]
    protected int heal;
    private int healedUnits;

    private void Awake()
    {
        Rigibody2D.mass = 0;
        DefaultSetting();
        healedUnits = 0;
    }

    private void Update()
    {
        Loop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter(collision, "AR");
        if(collision.CompareTag("ER"))
        {
            StartCoroutine(Healed(collision.GetComponentInParent<Unit>()));
        }
    }

    public IEnumerator Healed(Unit targetUnit)
    {
        healedUnits += 1;
        while(true)
        {
            if(targetUnit.GetHP() > 0)
            {
                healedUnits -= 1;
            }
        }
    }
}
