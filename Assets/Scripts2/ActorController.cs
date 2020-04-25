using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Com.MyCompany.MyGame;
public class ActorController : IActorManagerInterface
{
    public Animator anim;
    public PlayerInput pi;
    public MoveUI touch;
    public float Speed = 5.0f;
    public float rotSpeed = 0.2f;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        pi = GetComponent<PlayerInput>();
        touch = GameManager.Instance.InsMoveUI().GetComponentInChildren<MoveUI>();
    }

    // Update is called once per frame
    void Update()
    {
        moveJostick();
        //print(touch.transform.position.x);
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)//如果该地区角色不是自己本地实例化角色，则返回。（因为我们要在离线模式下测试，所以为了方便测试，还要检测下是否在连接状态下）
        {
            return;
        }
        if (!anim)
        {
            return;
        }
        // deal with Jumping
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);//获得第一层base layer
        if (pi.attack)
        {
            Attack();
        }
        if (pi.defense)
        {
            Defense();
        }
        if (pi.dodgy)
        {
            Dodgy();
        }
    }


    public void Attack()
    {
        anim.SetTrigger("attack");
        pi.attack = false;
    }
    public void Defense()
    {
        anim.SetTrigger("defense");
        pi.defense = false;
    }
    public void Dodgy()
    {
        anim.SetTrigger("dodgy");
        pi.dodgy = false;
    }
    public void moveJostick()
    {
        Vector3 moveDirection = new Vector3(touch.Horizontal, 0, touch.Vertical);
        if (moveDirection != Vector3.zero)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * rotSpeed);
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        }
    }
}
