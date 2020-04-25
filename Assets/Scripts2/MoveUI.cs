using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MoveUI : MonoBehaviour,IDragHandler,IEndDragHandler
{
    public float maxRadius = 100f;//摇杆移动最大半径
    public Vector2 moveBackPos;//摇杆背景位置
    //hor,ver的属性访问器
    private float horizontal = 0;
    private float vertical = 0;

    public float Horizontal
    {
        get { return horizontal; }
    }

    public float Vertical
    {
        get { return vertical; }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        moveBackPos=transform.parent.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = transform.localPosition.x;
        vertical = transform.localPosition.y;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //背景和摇杆距离（向量）
        Vector2 oppsitionVec = eventData.position - moveBackPos;
        //向量长度
        float distance = Vector3.Magnitude(oppsitionVec);
        //限制移动距离在0到最大半径
        float radius = Mathf.Clamp(distance, 0, maxRadius);
        //移动位置
        transform.position = moveBackPos + oppsitionVec.normalized * radius;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //回复位置
        transform.position = moveBackPos;
        transform.localPosition = Vector3.zero;
    }
    
}
