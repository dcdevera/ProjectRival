using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]                 //Attach audio source when attaching script

public class MainMenu : MonoBehaviour
{
    public int _selectedButton = 0;                     //Defines selected GUI button
    public float _timeBetweenButtonPress = 0.1f;        //Defines delay time between button presses
    public float _timeDelay;                            //Defines delay variable value

    public float _mainMenuVerticalInputTimer;           //Defines vertical input timer
    public float _mainMenuVerticalInputDelay = 1f;    //Defines vertical input delay

    public Texture2D _mainMenuBackground;               //Creates slot in inspector to assign main menu background
    public Texture2D _mainMenuTitle;               //Creates slot in inspector to assign main menu background

    private AudioSource _mainMenuAudio;                 //Defines naming for main menu audio component
    public AudioClip _mainMenuMusic;                    //Creates slot in inspector to assign main menu music
    public AudioClip _mainMenuStartButtonAudio;         //Creates slot in inspector to assign main menu start button audio
    public AudioClip _mainMenuQuitButtonAudio;          //Creates slot in inspector to assign main menu quit button audio

    public float _mainMenuFadeValue;                   //Defines fade value
    public float _mainMenuFadeSpeed = 0.30f;            //Defines fade speed

    public float _mainMenuButtonWidth = 100f;           //Defines main menu button width size
    public float _mainMenuButtonHeight = 25f;           //Defines main menu button height size
    public float _mainMenuGUIOffset = 10f;              //Defines main menu GUI offset

    private bool _startingOnePlayerGame;                //Defines if starting one player game
    private bool _startingTwoPlayerGame;                //Defines if starting two player game
    private bool _quittingGame;                         //Defines if quiting game

    private bool _pS4Controller;                        //when a ps4 controller is connected
    private bool _xBOXController;                       //when a xbox controller is connected

    private string[] _mainMenuButtons = new string[]    //Creates array of GUI buttons for the main menu scene
    {
        "_onePlayer",
        "_twoPlayer",
        "_quit"
    };

    private MainMenuController _mainMenuController;     //Defines naming convention for main menu controller

    private enum MainMenuController                     //Defines states main menu can exist in
    {
        MainMenuFadeIn = 0,
        MainMenuAtIdle = 1,
        MainMenuFadeOut = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        _startingOnePlayerGame = false;                 //starting one player game is false on start up
        _startingTwoPlayerGame = false;                 //starting two player game is false on start up
        _quittingGame = false;                          //quiting game is false on start up

        _pS4Controller = false;                         //ps4 controller is false on startup
        _xBOXController = false;                         //xbox controller is false on startup

        _mainMenuFadeValue = 0;                         //Fade value equals zero on start up

        _mainMenuAudio = GetComponent<AudioSource>();   //_mainMenuAudio equals audio source component

        _mainMenuAudio.volume = 0;                      //Audio volume equals on start up
        _mainMenuAudio.clip = _mainMenuMusic;           //Audio clip equals main menu music
        _mainMenuAudio.loop = true;                     //set audio to loop
        _mainMenuAudio.Play();                          //play audio

        _mainMenuController = MainMenu.MainMenuController.MainMenuFadeIn;       //State equals fade in on start up

        StartCoroutine("MainMenuManager");              //Start MainMenuManager on start up
    }

    // Update is called once per frame
    void Update()
    {
        string[] _joyStickNames = Input.GetJoystickNames();     //_joyStickNames equals joystick names from inbuilt import

        for(int _js = 0;_js < _joyStickNames.Length;_js++)      //_js equals the joysticknames length
        {
            if(_joyStickNames[_js].Length == 0)                 //if joystick names equals zero (if no controller attached)
            {
                return;                                         //do nothing and return
            }

            if(_joyStickNames[_js].Length == 19)                //if joystick names equals code 19 (ps4 controller)
            {
                _pS4Controller = true;                          //then set ps4 controller to true
            }

            if (_joyStickNames[_js].Length == 33)                //if joystick names equals code 33 (xbox controller)
            {
                _xBOXController = true;                          //then set xbox controller to true
            }
        }

        if(_mainMenuVerticalInputTimer > 0)                      //if vertical input timer is greater than zero
        {
            _mainMenuVerticalInputTimer -= 1f * Time.deltaTime;  //then reduce vertical input timer
        }

        if(Input.GetAxis("Vertical") > 0f && _selectedButton == 0)      //if input equals vertical (positive) and selected button equals zero
        {
            return;                                                     //then do nothing and return
        }

        if(Input.GetAxis("Vertical") > 0f && _selectedButton == 1)      //If input equals vertical (positive) and selected button equals 1
        {
            if(_mainMenuVerticalInputTimer > 0)                         //if vertical input timer is greater than zero
            {
                return;                                                 //then do nothing and return
            }

            _mainMenuVerticalInputTimer = _mainMenuVerticalInputDelay;  //make vertical input timer equal to input delay
            _selectedButton = 0;                                        //and make selected button equal to zero
        }

        if (Input.GetAxis("Vertical") > 0f && _selectedButton == 2)      //If input equals vertical (positive) and selected button equals 2
        {
            if (_mainMenuVerticalInputTimer > 0)                         //if vertical input timer is greater than zero
            {
                return;                                                 //then do nothing and return
            }

            _mainMenuVerticalInputTimer = _mainMenuVerticalInputDelay;  //make vertical input timer equal to input delay
            _selectedButton = 1;                                        //and make selected button equal to one
        }

        if (Input.GetAxis("Vertical") < 0f && _selectedButton == 2)      //if input equals vertical (negative) and selected button equals two
        {
            return;                                                     //then do nothing and return
        }

        if (Input.GetAxis("Vertical") < 0f && _selectedButton == 0)      //If input equals vertical (negative) and selected button equals 2
        {
            if (_mainMenuVerticalInputTimer > 0)                         //if vertical input timer is greater than zero
            {
                return;                                                 //then do nothing and return
            }

            _mainMenuVerticalInputTimer = _mainMenuVerticalInputDelay;  //make vertical input timer equal to input delay
            _selectedButton = 1;                                        //and make selected button equal to one
        }

        if (Input.GetAxis("Vertical") < 0f && _selectedButton == 1)      //If input equals vertical (negative) and selected button equals 1
        {
            if (_mainMenuVerticalInputTimer > 0)                         //if vertical input timer is greater than zero
            {
                return;                                                 //then do nothing and return
            }

            _mainMenuVerticalInputTimer = _mainMenuVerticalInputDelay;  //make vertical input timer equal to input delay
            _selectedButton = 2;                                        //and make selected button equal to two
        }
    }

    private IEnumerator MainMenuManager()
    {
        while(true)
        {
            switch(_mainMenuController)
            {
                case MainMenuController.MainMenuFadeIn:
                    MainMenuFadeIn();
                    break;

                case MainMenuController.MainMenuAtIdle:
                    MainMenuAtIdle();
                    break;

                case MainMenuController.MainMenuFadeOut:
                    MainMenuFadeOut();
                    break;
            }
            yield return null;
        }
    }

    private void MainMenuFadeIn()
    {
        Debug.Log("MainMenuFadeIn");

        _mainMenuAudio.volume += _mainMenuFadeSpeed * Time.deltaTime;           //Increase volume by the fade speed

        _mainMenuFadeValue += _mainMenuFadeSpeed * Time.deltaTime;              //Increase fade value by fade speed
        
        if(_mainMenuFadeValue > 1)                                              //If fade value is greater than one
        {
            _mainMenuFadeValue = 1;                                             //then make fade value equal to one
        }
        
        if(_mainMenuFadeValue == 1)                                             //If fade value equals one
        {
            _mainMenuController = MainMenu.MainMenuController.MainMenuAtIdle;   //then make state equal to main menu at idle
        } 
    }

    private void MainMenuAtIdle()
    {
        Debug.Log("MainMenuAtIdle");

        if(_startingOnePlayerGame || _quittingGame == true)                     //if starting one player game OR quitting game is true
        {
            _mainMenuController = MainMenu.MainMenuController.MainMenuFadeOut;  //then make state equal to main menu fade out
        }
    }

    private void MainMenuFadeOut()
    {
        Debug.Log("MainMenuFadeOut");

        _mainMenuAudio.volume -= _mainMenuFadeSpeed * Time.deltaTime;           //Decrease volume by the fade speed

        _mainMenuFadeValue -= _mainMenuFadeSpeed * Time.deltaTime;              //Decrease fade value by fade speed

        if (_mainMenuFadeValue < 0)                                              //If fade value is less than zero
        {
            _mainMenuFadeValue = 0;                                             //then make fade value equal to one
        }

        if(_mainMenuFadeValue == 0 && _startingOnePlayerGame == true)           //if fade value equals zero AND starting one player game is true
        {
            SceneManager.LoadScene("CharacterSelect");                          //load character select scene
        }
    }

    private void MainMenuButtonPress()
    {
        Debug.Log("MainMenuButtonPress");

        GUI.FocusControl(_mainMenuButtons[_selectedButton]);

        switch(_selectedButton)
        {
            case 0:
                _mainMenuAudio.PlayOneShot(_mainMenuStartButtonAudio);          //play start button audio clip
                _startingOnePlayerGame = true;                                  //Set starting one player game to true
                break;

            case 1:
                _mainMenuAudio.PlayOneShot(_mainMenuStartButtonAudio);          //play start button audio clip
                _startingTwoPlayerGame = true;                                  //Set starting two player game to true
                break;

            case 2:
                _mainMenuAudio.PlayOneShot(_mainMenuStartButtonAudio);          //play start button audio clip
                _quittingGame = true;                                           //Set quitting game to true
                break;
        }
    }

    void OnGUI()
    {
        if(Time.deltaTime >= _timeDelay && (Input.GetButton("Fire1")))          //if time is greater than or equals our time delay AND input equals "Fire1"
        {
            StartCoroutine("MainMenuButtonPress");                              //then start MainMenuButtonPress function
            _timeDelay = Time.deltaTime + _timeBetweenButtonPress;              //then make time delat equal current time plus timebetweenbuttonpress
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _mainMenuBackground);      //Draw main menu background at position 0,0 by screen width and height

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _mainMenuTitle);      //Draw main menu background at position 0,0 by screen width and height

        GUI.color = new Color(1, 1, 1, _mainMenuFadeValue);                     //GUI color equal to 1 1 1 rgb plus fade value (alpha)

        GUI.BeginGroup(new Rect(Screen.width / 2 - _mainMenuButtonWidth / 2, Screen.height / 1.5f, _mainMenuButtonWidth, _mainMenuButtonHeight * 3 + _mainMenuGUIOffset * 2));      //Begin GUI group at position X and Y by dimension x and dimension y

        GUI.SetNextControlName("_onePlayer");                                   //Set name to one player
        if(GUI.Button(new Rect(0,0,_mainMenuButtonWidth, _mainMenuButtonHeight),"One Player"))      //Create button at position with GUI group by these dimensions, and print "One Player"
        {
            _selectedButton = 0;                                                //set selected button to zero
            MainMenuButtonPress();                                              //call MainMenuButtonPress function
        }

        GUI.SetNextControlName("_twoPlayer");                                   //Set name to two player
        if (GUI.Button(new Rect(0, _mainMenuButtonHeight + _mainMenuGUIOffset, _mainMenuButtonWidth, _mainMenuButtonHeight), "Two Player"))      //Create button at position with GUI group by these dimensions, and print "Two Player"
        {
            _selectedButton = 1;                                                //set selected button to one
            MainMenuButtonPress();                                              //call MainMenuButtonPress function
        }

        GUI.SetNextControlName("_quit");                                   //Set name to quit
        if (GUI.Button(new Rect(0, _mainMenuButtonHeight*2 + _mainMenuGUIOffset*2, _mainMenuButtonWidth, _mainMenuButtonHeight), "Quit"))      //Create button at position with GUI group by these dimensions, and print "Quit"
        {
            _selectedButton = 2;                                                //set selected button to two
            MainMenuButtonPress();                                              //call MainMenuButtonPress function
        }


        GUI.EndGroup();                                                         //End GUI Group

        if(_pS4Controller == true || _xBOXController == true)                   //if ps4 OR xbox controller equals true
        {
            GUI.FocusControl(_mainMenuButtons[_selectedButton]);                //then focus equals main menu selected button
        }
    }

    
}
