using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    public bool StillHoldOn;

    [SerializeField]
    private Sprite[] CharacterSprites;
    [SerializeField]
    private Button[] CharacterButtons;
    [SerializeField]
    private GameObject[] Characters;
    [SerializeField]
    private Image MovableObject;
    [SerializeField]
    private Transform SpawnPointsParent;

    private Color whenSpawn = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    private Color whenNotSpawn = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    private Vector2[] SpawnPoints = new Vector2[5];

    private void Start()
    {
        for(int i=0; i < SpawnPointsParent.childCount; ++i)
        {
            SpawnPoints[i] = SpawnPointsParent.GetChild(i).position;
        }
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
