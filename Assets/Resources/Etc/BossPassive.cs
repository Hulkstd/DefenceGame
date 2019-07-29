using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPassive : MonoBehaviour
{
    public GameObject boss;


    public void Recovered()
    {
        StartCoroutine(Recover());
    }
    protected IEnumerator Recover()
    {
        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            count++;
            Debug.Log("count= " + count);
            if (count >= 20)
            {
                boss.SetActive(true);
                boss.transform.position = new Vector2(5.5f, 0);
                boss.GetComponent<Unit>().Reborn();
                break;
            }
        }
    }
}
