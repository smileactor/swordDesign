using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;


namespace Com.MyCompany.MyGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields
        public static GameManager Instance;

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;
        public GameObject ControlCanvas;
        #endregion
        void Awake()
        {

        }

        #region MonoBehaviour CallBacks
        void Start()
        {
            #region NetWorkIns
            Instance = this;
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                if (ActorManager.LocalPlayerInstance == null)//本地玩家为空时，才进行生成  
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);//通过姓名找玩家预制体
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }
            //if (PhotonNetwork.IsConnected)
            //{
            //    GameObject.Instantiate(ControlCanvas);
            //}
            #endregion
            
        }
        #endregion

        #region Photon Callbacks
        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }
        #endregion


        #region Public Methods


        public void LeaveRoom()//离开房间的回调函数
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion

        #region Private Methods
        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)//就是告诉别人只需要主客户端加载场景即可，其他人不需要（比如地图加载一个，别人只需要把角色加进来就行而不是再加载一份地图
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);//利用这个字符串结合加载场景表示该场景可以容纳多少人
        }
        #endregion

        
        public GameObject InsMoveUI()
        {
            return GameObject.Instantiate(Resources.Load("Prefabs/moveCanvas")) as GameObject;
        }
        
        public T Bind<T>(GameObject go)
        {
            T tempT;
            tempT = go.GetComponent<T>();
            return tempT;
        }
        public void CheckSingle()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);//GM无法被破坏
                return;
            }
            Destroy(this);
        }
    }
}