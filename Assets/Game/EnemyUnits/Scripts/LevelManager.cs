using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies
{
    GameObject Type;
    int Count;

//    Enemies(Types types, intCount)
}
public enum EnemyType { Pirate = 0, }

public class LevelManager : MonoBehaviour
{
    public Transform[] points; // enemy가 생성될 Points
    public List<Enemies> enemies = new List<Enemies>(); // enemy 프리팹
    public int enemyNum; // 몇 명의 enemy가 나오는지

    private void Start()
    {
        StartCoroutine(CreateEnemy());
    }


    IEnumerator CreateEnemy()
    {
        while (enemyNum > 0)
        {
            int idx = Random.Range(0, points.Length);

 //           Instantiate(enemies[0], points[idx]);
            //Debug.Log("Enemy created on" + points[idx].name);
            enemyNum--;
            yield return new WaitForSeconds(2f);
        }
    }

}
