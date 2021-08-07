using UnityEngine;
using System.Collections;

public abstract class State
{
    public State() { }
    public abstract void Enter(MonoBehaviour target);
    public abstract void Stay(MonoBehaviour target);
    public abstract void Exit(MonoBehaviour target);
    //추상클래스
    //진입,ING,탈출
}
