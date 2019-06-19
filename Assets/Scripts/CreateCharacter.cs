using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    public static CreateCharacter Instance { get; private set; }

    public bool StillHoldOn;

    [SerializeField]
    private List<Sprite> CharacterSprites;
    [SerializeField]
    public List<AllyUnit> UnitList;
    [SerializeField]
    private Transform SpawnCharacter;
    [SerializeField]
    private GameObject[] Characters;
    [SerializeField]
    private Image MovableObject;
    [SerializeField]
    private Font font;
    private Transform SpawnPointsParent;

    private Color whenSpawn = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    private Color whenNotSpawn = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    private Vector2[] SpawnPoints = new Vector2[5];

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BasicSetting();
    }

    private void BasicSetting()
    {
        SpawnPointsParent = GameObject.Find("SpawnPoints").transform;

        int i;

        for(i=0; i < SpawnPointsParent.childCount; ++i)
        {
            SpawnPoints[i] = SpawnPointsParent.GetChild(i).position;
        }

        if(UnitList.Count == 0)
        {
            UnitList.Add(AllyUnit.전사);
        }

        i = 0;

        while(SpawnCharacter.childCount != i)
        {
            Destroy(SpawnCharacter.GetChild(i++).gameObject);
        }

        UnitList.Sort();

        i = 0;

        foreach(AllyUnit index in UnitList)
        {
            CreateSpawnButton(i++, index);
        }
    }

    private void CreateSpawnButton(int index, AllyUnit unit)
    {
        GameObject gameObject = new GameObject("Unit" + index);
        GameObject gameObject1 = new GameObject("Cost");

        gameObject.transform.SetParent(SpawnCharacter);
        gameObject1.transform.SetParent(gameObject.transform);

        Button button = gameObject.AddComponent<Button>();
        Image image = gameObject.AddComponent<Image>();
        HoldonEvent holdonEvent = gameObject.AddComponent<HoldonEvent>();
        CoolDown coolDown = gameObject.AddComponent<CoolDown>();
        Text costText = gameObject1.AddComponent<Text>();

        image.rectTransform.sizeDelta = new Vector2(150, 150);
        image.rectTransform.localPosition = new Vector3(-860 + 160 * index, 0, 0);
        image.rectTransform.localScale = new Vector3(-1, 1, 1);

        costText.rectTransform.sizeDelta = new Vector2(150, 40);
        costText.rectTransform.localPosition = new Vector3(0, -80);
        costText.rectTransform.localScale = new Vector3(-1, 1, 1);
        costText.resizeTextForBestFit = true;
        costText.resizeTextMaxSize = 300;
        costText.resizeTextMinSize = 14;
        costText.font = font;
        costText.alignment = TextAnchor.MiddleCenter;
        costText.text = ((int)EnumTransporter.AllyUnitToECost(unit)).ToString();
        costText.color = Color.yellow;

        image.sprite = CharacterSprites[(int)unit];
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Radial360;
        image.fillOrigin = 0;
        image.fillAmount = 1;
        image.fillClockwise = true;

        holdonEvent.CharacterNum = (int)unit;
        holdonEvent.CoolTime = coolDown;
        holdonEvent.createCharacter = this;

        coolDown.CostText = costText;
        coolDown.Image = image;
        coolDown.Tier = EnumTransporter.AllyUnitToETier(unit);
        coolDown.Cost = EnumTransporter.AllyUnitToECost(unit);
        coolDown.CoolOver = true;
    }

    public void CreateObject(int Num, CoolDown Object)
    {
        MovableObject.color = whenSpawn;
        MovableObject.sprite = CharacterSprites[Num];
        MovableObject.transform.position = Input.mousePosition;
        StillHoldOn = true;

        StartCoroutine(MoveObject(Num, Object));
    }

    IEnumerator MoveObject(int Num, CoolDown Object)
    {
        while(StillHoldOn)
        {
            yield return new WaitForFixedUpdate();

            MovableObject.transform.position = Input.mousePosition;
        }

        MovableObject.color = whenNotSpawn;

        float MinDistance = float.MaxValue;
        Vector2 Minvec = new Vector2();

        foreach (Vector2 vec in SpawnPoints)
        {
            float Distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(MovableObject.transform.position), vec);
            
            if (Distance < MinDistance)
            {
                Minvec = vec;
                MinDistance = Distance;
            }
        }
        if(Object.GetCost() <= CoinSystem.Coin)
        {
            CoinSystem.SpawnObject(Object.GetCost());
            StartCoroutine(Object.Cooldown());
            Instantiate(Characters[Num], Minvec, new Quaternion());
        }
    }
}

public class EnumTransporter
{
    public static ETier AllyUnitToETier(AllyUnit unit)
    {
        int a = (int)unit;

        if(a >= 0 && a <= 10)
        {
            return ETier.Tier1;
        }
        else if (a >= 11 && a <= 16)
        {
            return ETier.Tier2;
        }
        else if (a >= 17 && a <= 24)
        {
            return ETier.Tier3;
        }

        return ETier.None;
    }

    public static ECost AllyUnitToECost(AllyUnit unit)
    {
        switch(unit)
        {
            case AllyUnit.전사:
                {
                    return ECost.Warrior;
                }
                
            case AllyUnit.쌍검사:
                {
                    return ECost.SSanggumsa;
                }
                
            case AllyUnit.무사:
                {
                    return ECost.Musa;
                }
                
            case AllyUnit.창병:
                {
                    return ECost.ChangBeong;
                }

            case AllyUnit.장창병:
                {
                    return ECost.JangChangBeong;
                }

            case AllyUnit.투창병:
                {
                    return ECost.TuChangBeong;
                }

            case AllyUnit.기마병:
                {
                    return ECost.GiMaBeong;
                }

            case AllyUnit.경기병:
                {
                    return ECost.GeongGiBeong;
                }

            case AllyUnit.기사:
                {
                    return ECost.GiSa;
                }

            case AllyUnit.해머병:
                {
                    return ECost.HammerBeong;
                }

            case AllyUnit.철퇴병:
                {
                    return ECost.凸TeauBeong;
                }

            case AllyUnit.방패병:
                {
                    return ECost.BangPeBeong;
                }

            case AllyUnit.기갑병:
                {
                    return ECost.GiGabBeong;
                }

            case AllyUnit.사제:
                {
                    return ECost.Trash;
                }

            case AllyUnit.고위사제:
                {
                    return ECost.GoWiiTrash;
                }

            case AllyUnit.암살자:
                {
                    return ECost.AmSalJa;
                }

            case AllyUnit.어쎄신:
                {
                    return ECost.Assesin;
                }

            case AllyUnit.귀족:
                {
                    return ECost.GuiJok;
                }

            case AllyUnit.국왕:
                {
                    return ECost.GukWang;
                }

            case AllyUnit.성기사:
                {
                    return ECost.SeongGiSa;
                }

            case AllyUnit.영웅:
                {
                    return ECost.AU;
                }

            case AllyUnit.대장군:
                {
                    return ECost.DaeJangGun;
                }

            case AllyUnit.총사령관:
                {
                    return ECost.GunSaRyeongGuan;
                }
        }
        return ECost.None;
    }
}

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

public enum AllyUnit : short
{
    전사 = 0,
    쌍검사 = 1,
    무사 = 2,
    창병 = 3,
    장창병 = 4,
    투창병 = 5,
    기마병 = 6,
    경기병 = 7,
    중기병 = 8,
    기사 = 9,
    해머병 = 10,
    철퇴병 = 11,
    방패병 = 12,
    기갑병 = 13,
    사제 = 14,
    고위사제 = 15,
    암살자 = 16,
    어쎄신 = 17,
    귀족 = 18,
    국왕 = 19,
    성기사 = 20,
    영웅 = 21,
    대장군 = 22,
    총사령관 = 23,
    아무것도아님 = -1
}
