using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerWarning : ControllerManager
{
    public Texture2D _controllerWarningBackground;              //Creates slot in inspector to assign the controller warning background
    public Texture2D _controllerWarningText;                    //Creates slot in inspector to assign the controller warning text message
    public Texture2D _controllerDetectedText;                   //Creates slot in inspector to assign the controller detected text message

    public float _controllerWarningFadeValue;                   //Defines the fade value of the warning text
    private float _controllerWarningFadeSpeed = 0.25f;         //Defines the fade speed
    private bool _controllerConditionsMet;                      //Defines if the controller conditions are met for the game to continue

    // Start is called before the first frame update
    void Start()
    {
        _controllerWarningFadeValue = 1;                        //Fade Value equals one on start up
        _controllerConditionsMet = false;                       //Controller conditions met is false on start up

    }

    // Update is called once per frame
    void Update()
    {
        if(_controllerDetectedText == true)                     //If controller detected equals true
        {
            StartCoroutine("WaitToLoadMainMenu");               //Start WaitForMainMenu function
        }

        if(_controllerConditionsMet == false)                   //If controller conditions met equals false
        {
            return;                                             //then do nothing and return
        }

        if(_controllerConditionsMet == true)                    //if controller conditions met equals true
        {
            _controllerWarningFadeValue -= _controllerWarningFadeSpeed * Time.deltaTime;        //decrease fade value by fade speed times delta time

            if(_controllerWarningFadeValue < 0)                 //if fade value less than zero
            {
                _controllerWarningFadeValue = 0;                //then set fade value to equal zero
            }

            if(_controllerWarningFadeValue == 0)                //if fade value equals zero
            {
                _startUpFinished = true;                        //Set startup finished to true
                SceneManager.LoadScene("MainMenu");             //Load main menu
            }
        }
    }

    private IEnumerable WaitToLoadMainMenu()
    {
        yield return new WaitForSeconds(2);                     //Wait for this (s) many seconds

        _controllerConditionsMet = true;                        //set controller conditions met to true
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _controllerWarningBackground);         //Draw texture starting at 0,0 by the screen width and height, draw the message background

        GUI.color = new Color(1, 1, 1, _controllerWarningFadeValue);                                      //GUI color is equal to 1 1 1 (rgb default), plus the fade value

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _controllerWarningText);         //Draw texture starting at 0,0 by the screen width and height, draw the message text

        if(_controllerDetectedText == true)                     //if controller detected equals true
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _controllerDetectedText);         //Draw texture starting at 0,0 by the screen width and height, draw the message text
        }
    }
}
