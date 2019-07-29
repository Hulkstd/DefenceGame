using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHealRange : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D rigibody;
    [SerializeField]
    protected Animator anim;
    private Unit unit;
    [SerializeField]
    protected int healPower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ER"))
        {
            Debug.Log(gameObject.name + ", " + collision.name);
            rigibody.angularDrag = 0;
            unit = collision.GetComponentInParent<Unit>();
            StartCoroutine(Healing());
        }
    }

    private IEnumerator Healing()
    {
        while(rigibody.angularDrag == 0)
        {
            anim.Play("Heal");
            unit.Healing(healPower);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
