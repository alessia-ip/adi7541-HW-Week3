using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndLoad : MonoBehaviour
{

    private const string PATH_LOGS = "/Logs";
    private const string FILE_LOGS = PATH_LOGS + "/writing.txt";
    private string FILE_PATH_WRITING_LOGS;

    public Text writingPrinted;
    public InputField inputedText;
    
    private string currentText;
    public string CurrentText
    {
        get
        {
            return currentText;
        }
        set
        {
            currentText = value;
            writingPrinted.text = currentText;
            File.WriteAllText(FILE_PATH_WRITING_LOGS, currentText + "");
        }
    }
    
    private string newLine;
    public string NewLine
    {
        set
        {
            newLine = value;
            CurrentText = CurrentText + "\n" + newLine;

        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        FILE_PATH_WRITING_LOGS = Application.dataPath + FILE_LOGS;
        if (!File.Exists(FILE_PATH_WRITING_LOGS))
        {
            Directory.CreateDirectory(Application.dataPath + PATH_LOGS);
            File.Create(FILE_PATH_WRITING_LOGS);
        }
        else
        {
            CurrentText = File.ReadAllText(FILE_PATH_WRITING_LOGS);
        }
        
        
        inputedText.ActivateInputField();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddNewLine();   
        }
    }

    public void AddNewLine()
    {
        NewLine = inputedText.text;
        inputedText.text = "";
        inputedText.ActivateInputField();

    }

}
