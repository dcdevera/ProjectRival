using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]                             //add audio source when attaching the script

public class CharacterSelect : CharacterSelectManager
{

    public float _characterSelectInputTimer;                        //character select timer
    public float _characterSelectInputDelay = 1f;                   //character select input delay

    private GameObject _characterDemo;                              //selected character game object

    public int _characterSelectState;                               //selected character state

    private enum CharacterSelectModels                              //Defines which character to load
    {
        UnityChan = 0,
        xBot = 1,
        Male = 2,
        Female = 3,
        Heavy = 4,
        Berserker = 5
    }

    // Start is called before the first frame update
    void Start()
    {
        CharacterSelectManager();                                   //call CharacterSelectManager on start up
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CharacterSelectManager()
    {
        switch(_characterSelectState)
        {
            default:
            case 0:
                xBot();
                break;
            case 1:
                UnityChan();
                break;
            case 2:
                UnityChan();
                break;
            case 3:
                UnityChan();
                break;
            case 4:
                UnityChan();
                break;
            case 5:
                UnityChan();
                break;

        }
    }

    private void xBot()
    {
        Debug.Log("xBot");

        Destroy(_characterDemo);                    //destroy character demo object

        _characterDemo = Instantiate(Resources.Load("xbot")) as GameObject;                 //sets character demo game object to selected character

        _characterDemo.transform.position = new Vector3(-0.5f,0,-7f);

        _xBot = true;
        _unityChan = false;
        _male = false;
        _female = false;
        _heavy = false;
        _berserker = false;


    }

    private void UnityChan()
    {
        Debug.Log("UnityChan");

        Destroy(_characterDemo);                    //destroy character demo object

        _characterDemo = Instantiate(Resources.Load("unitychan")) as GameObject;                 //sets character demo game object to selected character

        _characterDemo.transform.position = new Vector3(-0.5f, 0, -7f);

        _xBot = false;
        _unityChan = true;
        _male = false;
        _female = false;
        _heavy = false;
        _berserker = false;

    }

    private void Male()
    {
        Debug.Log("Male");

        Destroy(_characterDemo);                    //destroy character demo object

        _characterDemo = Instantiate(Resources.Load("Male")) as GameObject;                 //sets character demo game object to selected character

        _characterDemo.transform.position = new Vector3(-0.5f, 0, -7f);

        _xBot = false;
        _unityChan = false;
        _male = true;
        _female = false;
        _heavy = false;
        _berserker = false;

    }

    private void Female()
    {
        Debug.Log("Female");

        Destroy(_characterDemo);                    //destroy character demo object

        _characterDemo = Instantiate(Resources.Load("Female")) as GameObject;                 //sets character demo game object to selected character

        _characterDemo.transform.position = new Vector3(-0.5f, 0, -7f);

        _xBot = false;
        _unityChan = false;
        _male = false;
        _female = true;
        _heavy = false;
        _berserker = false;

    }

    private void Heavy()
    {
        Debug.Log("Heavy");

        Destroy(_characterDemo);                    //destroy character demo object

        _characterDemo = Instantiate(Resources.Load("Heavy")) as GameObject;                 //sets character demo game object to selected character

        _characterDemo.transform.position = new Vector3(-0.5f, 0, -7f);

        _xBot = false;
        _unityChan = false;
        _male = false;
        _female = false;
        _heavy = true;
        _berserker = false;

    }

    private void Berserker()
    {
        Debug.Log("Berserker");

        Destroy(_characterDemo);                    //destroy character demo object

        _characterDemo = Instantiate(Resources.Load("Berserker")) as GameObject;                 //sets character demo game object to selected character

        _characterDemo.transform.position = new Vector3(-0.5f, 0, -7f);

        _xBot = false;
        _unityChan = false;
        _male = false;
        _female = false;
        _heavy = false;
        _berserker = true;

    }


    private void OnGUI()
    {
        
    }
}
