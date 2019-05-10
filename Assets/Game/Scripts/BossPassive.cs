using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BossPassive : MonoBehaviour
{
    Unit speedSetter = new Unit();

    private void OnEnable()
    {
        speedSetter.SpeedUp();
    }

    private void OnDisable()
    {
        speedSetter.SpeedDown();
    }


}
