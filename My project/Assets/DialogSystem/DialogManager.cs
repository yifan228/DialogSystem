using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] DialogueView dialogUI;
    ADialogController currentDialogController;
    private void Start()
    {
        //currentDialogController = SimpleFactory("tutorial");
        //currentDialogController.StartDialogAtProgress(0,dialogUI);
    }

    [ContextMenu("test dialog")]
    public void TestDialog()
    {
        StartDialog("test_dialog", 0);
    }

    public void StartDialog(string dialogName,int progress)
    {
        currentDialogController = SimpleFactory(dialogName);
        currentDialogController.StartDialogAtProgress(dialogName,progress, dialogUI);
    }

    private void CloseDialogUI()
    {
        dialogUI.Close();
    }

    private ADialogController SimpleFactory(string dialogTopic)
    {
        switch (dialogTopic)
        {
            default:
                return new DefaultDialogController(dialogTopic);
        }
    }
}


