using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMonsterData : MonoBehaviour
{
    #region ClassAndEnum
    public enum EnemyType
    {
        Pirate = 0,
        PirateSpear = 1,
        UndeadPirate = 2,
        FreshUndead = 3,
        Captain = 4
    }

    public class MonsterData
    {
        public EnemyType EnemyTy;
        public int Count;
        public List<int> Times;

        public MonsterData()
        {
            Times = new List<int>();
        }
    }
    #endregion

    private int Time;
    private List<MonsterData> MonsterDatas;//몬스터 총 관리 변수
    private static readonly string MonsterDataPath = @"EnemyUnits/Prefabs";
    public static int StageNumber = 1;
    public static readonly Vector2[] PosArr = { new Vector2(5, 2), new Vector2(5, 1), new Vector2(5, 0), new Vector2(5, -1), new Vector2(5, -2) };
    
    public void SpawnMonster(MonsterData Md)
    {
        Transform Enemy = Instantiate(Resources.Load<Transform>(MonsterDataPath + GetMonsterDataPath(Md.EnemyTy))); // 만들고
        Enemy.position = PosArr[Random.Range(0, 4)]; // 랜덤 배치
    }
    
    private EnemyType ToEnemy(int num) // int.ToEnemy
    {
        switch (num)
        {
            case 0: return EnemyType.Pirate;
            case 1: return EnemyType.PirateSpear;
            case 2: return EnemyType.UndeadPirate;
            case 3: return EnemyType.FreshUndead;
            case 4: return EnemyType.Captain;
            default: return EnemyType.Pirate;
        }
    }
    private string GetMonsterDataPath(EnemyType EType) // 경로 변환
    {
        switch (EType)
        {
            case EnemyType.Pirate: return "Pirate";
            case EnemyType.PirateSpear: return "PirateSpear";
            case EnemyType.UndeadPirate: return "UndeadPirate";
            case EnemyType.FreshUndead: return "FreshUndead";
            case EnemyType.Captain: return "Captain";
            default: return "Pirate";
        }
    }

    public void LoadData()
    {
        MonsterDatas = new List<MonsterData>();
        string[] strs = File.ReadAllLines(@"Resources/StageData/Stage" + StageNumber);
        string buffer = "";
        int Count;
        
        foreach (string str in strs)
        {
            MonsterData Md = new MonsterData();
            Count = 0;

            foreach (char ch in str)
            {
                if (ch == ' ')
                {
                    if (Count == 0) { Md.EnemyTy = ToEnemy(int.Parse(buffer)); }
                    else if (Count == 1) { Md.Count = int.Parse(buffer); }
                    else { Md.Times.Add(int.Parse(buffer)); }

                    Count++;
                    buffer = "";
                }
                buffer += ch;
            }
            MonsterDatas.Add(Md);
        }
    }
    
    private IEnumerator MonsterSpawnCoroutine()
    {
        while (true)
        {
            if (MonsterDatas[0].Times.Count == 0)
            {
                MonsterDatas.RemoveAt(0);
            }

            if (MonsterDatas[0].Times[0] == Time)
            {
                SpawnMonster(MonsterDatas[0]);
                MonsterDatas[0].Times.RemoveAt(0);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void StartGame()
    {
        LoadData();
        StartCoroutine("MonsterSpawnCoroutine");
    }

    private void Awake()
    {
        StartGame();
    }
}
