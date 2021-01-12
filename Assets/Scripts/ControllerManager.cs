using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ControllerManager : MonoBehaviour
{
    public Texture2D _controllerNotDetected;        //Create slot in inspector not detected warning text

    public bool _pS4Controller;                     //Creates bool for when a PS4 controller is connected
    public bool _xBOXController;                    //Creates bool for when a Xboc controller is connected
    public bool _controllerDetected;                //Creates bool for when a controller is connected

    public static bool _startUpFinished;            //Creates bool for when start up is finished

    private AudioSource _cmAudio;                   //defines naming convention for controller manager audio source
    public AudioClip _controlerDectedAudioClip;     //cretes slot in inspector to assign controlller detected audio clip

    void Awake()
    {
        _pS4Controller = false;                     //PS4 controller is false on awake
        _xBOXController = false;                    //Xbos controller is false on awake
        _controllerDetected = false;                //Controller detected is false on awake

        _startUpFinished = false;                   //Start up finished is false on awake
    }

    void Start()
    {
        DontDestroyOnLoad(this);                    //Don't destroy this gameobject when loading a new scene
    }

    void Update()
    {
        if(_controllerDetected == true)
        {
            return;
        }
        if(_startUpFinished == true)
        {
            Time.timeScale = 0;
        }
    }

    void LateUpdate()
    {
        if(_startUpFinished == true)
        {
            _cmAudio = GetComponent<AudioSource>();
        }

        string[] _joyStickNames = Input.GetJoystickNames();         //_joyStickNames equals get joystick names from inbuilt input

        for (int _js = 0; _js < _joyStickNames.Length; _js++)        //increase counter _js based oon joystick names length
        {
            if (_joyStickNames[_js].Length == 19)                   //If joystick name equals code 19
            {
                _pS4Controller = true;                              //Set PS4 controller to true

                if (_controllerDetected == true)
                {
                    return;
                }

                if (_startUpFinished == true)
                {
                    _cmAudio.PlayOneShot(_controlerDectedAudioClip);
                }

                Time.timeScale = 1;

                _controllerDetected = true;
            }

            if (_joyStickNames[_js].Length == 33)                 //If joystick name equals code 33
            {
                _xBOXController = true;                 //Set Xbox controller to true

                if (_controllerDetected == true)
                {
                    return;
                }

                if (_startUpFinished == true)
                {
                    _cmAudio.PlayOneShot(_controlerDectedAudioClip);
                }

                _controllerDetected = true;
            }

            if(_joyStickNames[_js].Length != 0)                    //if jopystick names does not equal zero
            {
                return;                                            //then do nothing and return
            }

            if(string.IsNullOrEmpty(_joyStickNames[_js]))          //if string is null/empty ie no controller detected
            {
                _controllerDetected = false;                       //then set controller detected to false
            }
        }
    }

    private void OnGUI()
    {
        if(_startUpFinished == false)               //If start up finished equals false
        {
            return;                                 //then do nothing and return
        }

        if(_controllerDetected == true)             //if controller detected equals true
        {
            return;                                 //then do nothing and return
        }

        if(_controllerDetected == false)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _controllerNotDetected);       //Draw a texture at this position by these dimensions, draw the controller not detected texture
        }
    }
}
