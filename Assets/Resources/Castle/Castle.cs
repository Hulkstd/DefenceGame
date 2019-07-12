using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float OriginalHP;
    public float hp;

    private bool isAttacked;


    protected virtual void TriggerEnter(Collider2D coll, string tag)
    {
        //Debug.Log(gameObject.name + "충돌함" + coll.name);
        if(coll.gameObject.CompareTag(tag))
        {
<<<<<<< HEAD
            Debug.Log("Castle" + gameObject.name + "충돌함" + coll.name);
            //StartCoroutine(Attacked((int)coll.transform.parent.GetComponent<Rigidbody2D>().mass));
            Unit unit = coll.GetComponentInParent<Unit>();
            StartCoroutine(Attacked((int)unit.GetRigidbody2D().mass));
=======
            Debug.Log(gameObject.name + "충돌함" + coll.name);
            StartCoroutine(Attacked((int)coll.transform.parent.GetComponent<Rigidbody2D>().mass));
>>>>>>> e99bd78576bf01ffd54a61ed09703d92632853de
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
