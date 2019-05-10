using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class HoldonEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField]
    private int CharacterNum;
    [SerializeField]
    private CreateCharacter createCharacter;
    [SerializeField]
    private CoolDown CoolTime;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!createCharacter.StillHoldOn && CoolTime.CoolOver)
        {
            createCharacter.CreateObject(CharacterNum, CoolTime);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        createCharacter.StillHoldOn = false;

        /* 위치 계산 및 소환 */
    }
}
