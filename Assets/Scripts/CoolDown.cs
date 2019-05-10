using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ETier : int
{
    Tier1 = 3,
    Tier2 = 10,
    Tier3 = 20,
    None = -1
}

public enum ECost : int
{
    Warrior = 5,
    SSanggumsa = 5,
    Musa = 8,
    ChangBeong = 7,
    JangChangBeong = 7,
    TuChangBeong = 10,
    GiMaBeong = 10,
    GeongGiBeong = 10,
    JungGiBeong = 10,
    GiSa = 17,
    HammerBeong = 20,
    凸TeauBeong = 25,
    BangPeBeong = 15,
    GiGabBeong = 20,
    Trash = 20,
    GoWiiTrash = 30,
    AmSalJa = 30,
    Assesin = 40,
    GuiJok = 30,
    GukWang = 30,
    SeongGiSa = 40,
    AU = 50,
    DaeJangGun = 20,
    GunSaRyeongGuan = 20,
    None = -1
}

public enum ESkill : int
{
    SaGiJeungJin = 20,
    AmSal = 10,
    HongSu = 30,
    PokDong = 20,
    None = -1
}

[SerializeField]
public class CoolDown : MonoBehaviour
{
    [SerializeField]
    private Image Image;
    [SerializeField]
    private ETier Tier;
    [SerializeField]
    private ECost Cost;

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