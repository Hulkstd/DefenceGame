using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class CoolDown : MonoBehaviour
{
    [SerializeField]
    public Text CostText;
    [SerializeField]
    public Image Image;
    [SerializeField]
    public ETier Tier;
    [SerializeField]
    public ECost Cost;

    public bool CoolOver = true;

    public int GetCost()
    {
        return (int)Cost;
    }
    
    public IEnumerator Cooldown()
    {
        float CoolTime = 0;

        CoolOver = false;

        yield return new WaitUntil(() =>
        {
            CoolTime += Time.deltaTime;

            Image.fillAmount = CoolTime / (float)Tier;

            return CoolTime >= (float)Tier;
        }
        );

        /*while (true) // 20 번에 걸처 쿨 감소 이펙트 니가 원하는거 사용해 그리고 나중에 최적화 생각하려면 이거해
        {
            yield return new WaitForSeconds(0.05f * (int)Tier);

            CoolTime += 0.05f;

            Image.fillAmount = CoolTime;

            if(CoolTime >= 1)
            {
                break;
            }
        }*/

        CoolOver = true;
    }
}