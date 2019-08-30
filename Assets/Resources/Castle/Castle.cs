using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float OriginalHP;
    public float hp;
    private bool isAttacked;


    private void Update()
    {
        
    }

    protected virtual void TriggerEnter(Collider2D coll, string tag)
    {
        //Debug.Log(gameObject.name + "충돌함" + coll.name);
        if(coll.gameObject.CompareTag(tag))
        {
            Debug.Log("Castle" + gameObject.name + "충돌함" + coll.name);
            Unit unit = coll.GetComponentInParent<Unit>();
            unit.castleAttack = true;
            StartCoroutine(Attacked((int)unit.GetRigidbody2D().mass));
            StartCoroutine(CheckHP());
        }
    }
    public IEnumerator Attacked(int damage)
    {
        isAttacked = true;

        while(true)
        {
            yield return new WaitForSeconds(1f);
            if (!isAttacked)
                break;
            hp -= damage;

        }
    }

    public IEnumerator CheckHP()
    {
        while(true)
        {
            if (!isAttacked)
                break;

            if(hp <= 0)
            {
                GameEnd();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public virtual void GameEnd()
    {
        // Game over, clear
    }

}
