using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer 
{
    public enum STATE//记录当前状态枚举变量
    {
        IDLE,//时钟闲置
        RUN,//时钟运行
        FINISHED//时钟完成
    }

    public STATE state;

    public float duration = 1.0f;//要计多长时间

    public float elapsedTime = 0;//延迟时间

    public void Tick()
    {
        switch (state)
        {
            case STATE.IDLE:
                break;
            case STATE.RUN:
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= duration)
                {
                    state=STATE.FINISHED;
                }
                break;
            case STATE.FINISHED:
                break;
            default:
                Debug.Log("Timer error");
                break;
        }
    }

    public void Go(float duration)
    {
        this.duration = duration;
        elapsedTime = 0.0f;
        state = STATE.RUN;
    }
}
