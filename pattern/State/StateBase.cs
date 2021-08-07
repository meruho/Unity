using UnityEngine;
using System.Collections;


public class StateBase : State
{
    //
    PlayerCtrl pc;
    //
    private StateBase() { }
    public override void Enter(MonoBehaviour target)
    {
        pc = (PlayerCtrl)target;
    }
    public override void Stay(MonoBehaviour target)
    {
        
    }
    public override void Exit(MonoBehaviour target)
    {
        
    }

    private static StateBase instance = null;
    public static StateBase inst()
    {
        if (instance == null)
        {
            instance = new StateBase();
        }
        return instance;
    }
}

