using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI; 

public class SaveAndLoad : MonoBehaviour
{

    //these vars are all to save dataa at a certain filepath
    private const string PATH_LOGS = "/Logs";
    private const string FILE_LOGS = PATH_LOGS + "/writing.txt";
    private string FILE_PATH_WRITING_LOGS;

    //This is the text field to be printed to on the screen
    public Text writingPrinted;
    
    //This is the text that the player/user is inputting into the system
    public InputField inputedText;
    
    //current text is meant to hold the text for the save file, as well as handle saving and updating the game view
    private string currentText;
    public string CurrentText
    {
        //Get only needs to get the current text - we don't need to pull it from the save until the program is re-openned
        get
        {
            return currentText;
        }
        //Set changes the value of current text
        //then it updates the on screen text to whatever the current text is
        //lastly, it saves the current text to the file
        set
        {
            currentText = value;
            writingPrinted.text = currentText;
            File.WriteAllText(FILE_PATH_WRITING_LOGS, currentText + "");
        }
    }
    
    //this is to keep track of the new line of text to be added
    private string newLine;
    public string NewLine
    {
        //you never need to 'get' the line so there's only a set function here
        set
        {
            newLine = value;
            CurrentText = CurrentText + "\n" + newLine;
            //we append the new line to the end of the current text!
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //this sets what the filepath should be
        FILE_PATH_WRITING_LOGS = Application.dataPath + FILE_LOGS;
        
        //if the file doesn't exist
        if (!File.Exists(FILE_PATH_WRITING_LOGS))
        {
            //create the directory
            Directory.CreateDirectory(Application.dataPath + PATH_LOGS);
            //then create the file
            File.Create(FILE_PATH_WRITING_LOGS);
        }
        else
        {
            //otherwise, the current text should be set to the contents of that file
            CurrentText = File.ReadAllText(FILE_PATH_WRITING_LOGS);
        }
        
        //lastly, set the input text field to 'active' 
        //this auto focuses the text field
        inputedText.ActivateInputField();

    }

    private void Update()
    { 
        //if the person clicks enter, run the new line function
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddNewLine();   
        }
    }

    //new line function
    //this works on the enter key
    //it also works using the submit button in the UI
    public void AddNewLine()
    {
        //the new line is set to the text of the inputed text field
        NewLine = inputedText.text;
        //then the field is set to blank in prep for more writing
        inputedText.text = "";
        //lastly, it is refocused so the player does not need to re-click on the field to write more
        inputedText.ActivateInputField();

    }

}
