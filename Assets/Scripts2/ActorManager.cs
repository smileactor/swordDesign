using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun.Demo.PunBasics;
using Photon.Pun;
using HedgehogTeam.EasyTouch;
using UnityEngine.SceneManagement;
public class ActorManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public PlayerInput pi;
    public ActorController ac;
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;


    public bool isAI;
    public bool isGoods;

    public MyTimer testtimer=new MyTimer();

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    void Awake()
    {
        if (!isAI && !isGoods)
        {
            ac = Bind<ActorController>(this.gameObject);
            bm = Bind<BattleManager>(this.gameObject);
            wm = Bind<WeaponManager>(this.gameObject);
            sm = Bind<StateManager>(this.gameObject);
            pi = GetComponent<PlayerInput>();
        }
        if (photonView.IsMine)
        {
            ActorManager.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);//在加载新场景时，别删除了角色
    }
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;//这个是什么？
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)//同步开火状态，不然就只有自己知道自己开火
        {
            // We own this player: send the others our data
            stream.SendNext(ac.pi.attack);
            stream.SendNext(sm.Health);
        }
        else
        {
            // Network player, receive data
            ac.pi.attack = (bool)stream.ReceiveNext();
            sm.Health = (float)stream.ReceiveNext();
        }
    }
    private T Bind<T>(GameObject go) where T : IActorManagerInterface//每次呼叫bind方法，则给一个类型T,一定是IActorManagerInterface
    {//确保am下所有管理者都初始化am,并且在go物件上绑上T组件
        T tempInstance;
        if (go == null)
        {
            return null;
        }
        tempInstance = go.GetComponent<T>();//在go物件上获取该组件
        if (tempInstance == null)
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;
        return tempInstance;
    }
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }
    void CalledOnLevelWasLoaded(int level)
    {
        // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))//射线检测没有地板则传送回去
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }
    }
    public override void OnDisable()
    {
        // Always call the base to remove callbacks
        base.OnDisable();
        print("UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;");
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
