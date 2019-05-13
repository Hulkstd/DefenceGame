using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBasicElement : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
