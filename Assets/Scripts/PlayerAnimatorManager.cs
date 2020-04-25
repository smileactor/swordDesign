using UnityEngine;
using System.Collections;
using Photon.Pun;
namespace Com.MyCompany.MyGame
{
    public class PlayerAnimatorManager : MonoBehaviourPun
    {
        #region Private Fields

        [SerializeField]
        private float directionDampTime = .25f;
        private Animator animator;

        #endregion

        #region MonoBehaviour CallBacks

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)//如果该地区角色不是自己本地实例化角色，则返回。（因为我们要在离线模式下测试，所以为了方便测试，还要检测下是否在连接状态下）
            {
                return;
            }
            if (!animator)
            {
                return;
            }
            // deal with Jumping
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);//获得第一层base layer
            // only allow jumping if we are running.
            if (stateInfo.IsName("Base Layer.Run"))//当前状态名字？
            {
                // When using trigger parameter
                if (Input.GetButtonDown("Fire2"))
                {
                    animator.SetTrigger("Jump");
                }
            }
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (v < 0)
            {
                v = 0;
            }
            animator.SetFloat("Speed", h * h + v * v);
            animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);//平缓进行0到1的变换
        }

        #endregion
    }
}