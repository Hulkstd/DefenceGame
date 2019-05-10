using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAttackRange : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D RB;//몸체의 Rigidbody2D
    [SerializeField]
    protected Animator Animator;
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ally"))
        {
            RB.angularDrag = 0;
            StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking()
    {
        while (RB.angularDrag == 0)
        {
            Animator.Play("Attack");
            yield return new WaitForSeconds(1f);
        }
    }
}
