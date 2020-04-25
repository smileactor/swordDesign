using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : IActorManagerInterface
{

    #region Public
    public float Health = 100f;

    #endregion

    #region private
    private Slider playerHealthSlider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (playerHealthSlider == null)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
