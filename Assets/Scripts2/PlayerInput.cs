using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.EventSystems;
public class PlayerInput : MonoBehaviourPun
{
    public bool attack=false;
    public bool defense=false;
    public bool dodgy=false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (photonView.IsMine)
        {
            
        }
    }
    void Attack()
    {
        attack = true;
    }
    void Defense()
    {
        defense = true;
    }
    void Dodgy()
    {
        dodgy = true;
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    ((IPointerClickHandler)att).OnPointerClick(eventData);
    //    Debug.Log("att");
    //}
}
