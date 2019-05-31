using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D Rigibody2D;
    [SerializeField]
    protected float Speed;
    protected float OriginalSpeed;
    protected static float SpeedWeight = 0;
    [SerializeField]
    protected int Shield;
    [SerializeField]
    protected int Hp; 
    protected int OriginalHp;
    [SerializeField]
    protected int Attack; // mass
    [SerializeField]
    protected UnityEngine.UI.Image HealthBoxPref;
    protected UnityEngine.UI.Image HealthBox;
    [SerializeField]
    protected Transform HealthBoxUI;
    [SerializeField]
    protected IEnumerator atkcorutin;
    [SerializeField]
    protected BoxCollider2D Collider2D;
    [SerializeField]
    protected bool isattacked;

    protected virtual void DefaultSetting()
    {
        Collider2D.size = new Vector2(Rigibody2D.drag, Collider2D.size.y);//콜리더 사이즈를 drag(사정거리)에 맞게 적용
        Speed = Rigibody2D.angularDrag;// 실제 이동 스피드 적용
        Attack = (int)Rigibody2D.mass; //attack은 mass  

        HealthBoxUI = GameObject.FindGameObjectWithTag("HealthBoxUI").transform; //HP바 적용
        HealthBox = Instantiate(HealthBoxPref, HealthBoxUI, false);              

        OriginalHp = Hp; // HP는 밖에서 설정해 주자 제발 진짜로
        OriginalSpeed = Rigibody2D.angularDrag;//상수값 OriginalSpeed 생성
    }

    protected virtual void Loop()
    {
        //가비지 콜렉터로 처리하는게 오래 걸려서 setatcive로 비활성화 처리 하는게 메모리로써는 많이 들지만 렉이 안걸림
        transform.Translate(new Vector3(Speed * -gameObject.transform.localScale.x, 0, 0) * Time.deltaTime);
        if (Rigibody2D.angularDrag != 0 && gameObject.tag == "Enemy")
            Speed = Rigibody2D.angularDrag + SpeedWeight;
        else
            Speed = Rigibody2D.angularDrag;

        HealthBox.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 0.1f); //hp바 움직이는 코드
        HealthBox.fillAmount = (float)Hp / OriginalHp;
    }

    protected virtual void Active()
    {
        //TODO 특수능력
    }

    protected virtual void Deactivate()
    {
        //특수 능력 종료
    }

    public void dead()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator Attacked(int damage)//공격받는 코루틴
    {
        isattacked = true;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(!isattacked)
            {
                break;
            }
            Hp -= damage;
<<<<<<< HEAD
            //Debug.Log(gameObject.name + "가 공격받고 있다!");

        }
=======

        }       
>>>>>>> 0853628d333901ed5b3c1defa5ae408d924881da
    }

    public IEnumerator CheckHP(Unit unit)
    {
        while (true)
        {
            if (Hp <= 0)
            {
                unit.Rigibody2D.angularDrag = unit.OriginalSpeed;//충돌 객체의 스피드 원래대로 돌려놈
                unit.isattacked = false;//충돌 객체의 공격 여부 false로 지정
                Invoke("dead", 0.2f);
            }
            if (!isattacked)
            {
                break;
            }            
            yield return new WaitForEndOfFrame();
        }
    }

    public virtual void TriggerEnter(Collider2D collision, string tag)
    {
        if(collision.gameObject.CompareTag(tag))
        {
            Unit unit = collision.GetComponentInParent<Unit>();//충돌하는 객체의 unit클래스를 불러오기
            atkcorutin = Attacked(unit.Attack);//충돌 객체의 공격력,객체를 받아서 Attacked코루틴 호출
<<<<<<< HEAD

            Debug.Log(gameObject.name + ", " + unit.name);

=======
>>>>>>> 0853628d333901ed5b3c1defa5ae408d924881da
            StartCoroutine(atkcorutin);
            StartCoroutine(CheckHP(unit));
        }
    }

    public virtual void SpeedUp()
    {
        SpeedWeight += 0.5f;
    }
    public virtual void SpeedDown()
    {
        SpeedWeight -= 0.5f;
    }
}
