//原版playerInput

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerInput:MonoBehaviour
//{

//    //public bool attack;
//    //public bool defense;
//    //public bool dodgy;

//    public MyButton Att = new MyButton();
//    public MyButton Def = new MyButton();
//    public MyButton Dod = new MyButton();

//    public bool lightAttack=false;
//    public bool hardAttack=false;
//    public bool Defense=false;
//    public bool Dodgy=false;

//    public Button att;
//    public Button def;
//    public Button dod;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        att.onClick.AddListener(onclick);
        
//        //Att.Tick(att.enabled);
//        //Def.Tick(def.enabled);
//        //Dod.Tick(dod.enabled);
//        lightAttack = Att.OnPressed;
//        hardAttack = (Att.IsPressing && !Att.IsDelaying);
//        Defense = Def.IsPressing;
//        Dodgy = Dod.OnPressed;
//    }
//    void onclick()
//    {
//        Att.Tick(att.enabled);
//        lightAttack = Att.OnPressed;
//    }
//}
