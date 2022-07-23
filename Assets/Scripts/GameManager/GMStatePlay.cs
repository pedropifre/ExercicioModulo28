
using Pedro.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GMStatePlay : StateBase
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Debug.Log("plaey can run");
    }
}
