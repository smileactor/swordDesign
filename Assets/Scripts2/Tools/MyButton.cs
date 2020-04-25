using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton
{

    public bool IsPressing;
    public bool OnReleased;
    public bool OnPressed;
    public bool IsExtending;
    public bool IsDelaying;

    private bool curState=false;
    private bool lastState = false;

    public MyTimer extTimer = new MyTimer();
    private MyTimer delayTimer = new MyTimer();

    public float extendingDuration = 0.3f;
    public float delayingDuration = 1.0f;

    public void Tick(bool input)
    {
        extTimer.Tick();
        delayTimer.Tick();
        curState = input;

        IsPressing = curState;//是否按压就是等于当前按钮状态 
        IsDelaying = false;
        IsExtending = false;
        OnReleased = false;
        OnPressed = false;
        if (curState != lastState && curState)
        {
            OnPressed = true;
            delayTimer.Go(delayingDuration);
        }
        else if (curState != lastState && !curState)
        {
            OnReleased = true;
            extTimer.Go(extendingDuration);
        }
        lastState = curState;
        if (extTimer.state == MyTimer.STATE.RUN)//进行计时时，处于IsExtending状态
        {
            IsExtending = true;
        }

        if (delayTimer.state == MyTimer.STATE.RUN)
        {
            IsDelaying = true;
        }
    }
    //public void StartTimer(MyTimer myTimer,float duration)
    //{
    //    myTimer.Go(duration);
    //}
}
