using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] DialogueView dialogUI;
    [SerializeField] TextAsset dialogData;
    ADialogController currentDialogController;
    private void Start()
    {
        //currentDialogController = SimpleFactory("tutorial");
        //currentDialogController.StartDialogAtProgress(0,dialogUI);
    }

    [ContextMenu("test dialog")]
    public void TestDialog()
    {
        StartDialog();
    }

    public void StartDialog()
    {
        currentDialogController = SimpleFactory("");
        currentDialogController.StartDialogAtProgress(dialogData, dialogUI);
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


