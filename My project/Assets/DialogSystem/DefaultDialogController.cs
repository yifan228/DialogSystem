using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDialogController : ADialogController
{
    private string dialogTopic;
    protected override void LoadTxTSettings()
    {
        switch (dialogTopic)
        {
            default:
                break;
        }
    }
    /// <summary>
    /// 不會有選項callback 的對話 、一系列對話，主要用於日常對話
    /// </summary>
    public DefaultDialogController(string dialogTopic)
    {
        this.dialogTopic = dialogTopic;
    }
}
