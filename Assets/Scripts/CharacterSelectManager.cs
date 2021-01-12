using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{

    public static bool _xBot;                                        //defines if xbot is selected
    public static bool _unityChan;                                   //defines if xbot is selected
    public static bool _male;                                   //defines if xbot is selected
    public static bool _female;                                   //defines if xbot is selected
    public static bool _heavy;                                   //defines if xbot is selected
    public static bool _berserker;                                   //defines if xbot is selected

    void Awake()
    {
        _xBot = false;                                               //xbot is false on start up
        _unityChan = false;                                               //xbot is false on start up
        _male = false;                                               //xbot is false on start up
        _female = false;                                               //xbot is false on start up
        _heavy = false;                                               //xbot is false on start up
        _berserker = false;                                               //xbot is false on start up
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);                                     //dont destroy this gameobject when loading scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
