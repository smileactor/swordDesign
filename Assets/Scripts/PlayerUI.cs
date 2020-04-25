using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Com.MyCompany.MyGame
{
    public class PlayerUI : MonoBehaviour
    {
        #region Private Fields


        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private Text playerNameText;


        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;
        [SerializeField]
        private ActorManager am;



        #endregion


        #region MonoBehaviour Callbacks
        void Start()
        {
            if (am != null && playerNameText != null)
            {
                playerNameText.text = am.photonView.Owner.NickName;
            }
        }
        void Update()
        {
            // Reflect the Player Health
            if (am != null && playerHealthSlider != null)
            {
                playerHealthSlider.value = am.sm.Health;
            }
        }

        #endregion


        #region Public Methods


        #endregion


    }
}