using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSTSAttaque : FSMState<TSTStateInfo>
{
    public override void doState(ref TSTStateInfo infos)
    {
        Debug.Log("Je suis à sa poursuite !");
    }
}
