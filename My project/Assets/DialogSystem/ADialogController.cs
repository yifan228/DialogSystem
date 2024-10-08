﻿using System;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
/// <summary>
/// 以
/// </summary>
public class ADialogController 
{
    protected int currentProgress;//從零開始
    DialogModel model;
    protected DialogueView currentView;
    protected string txtPath;
    public string DialogTopic;
    public void StartDialogAtProgress(TextAsset textsData,DialogueView view)
    {
        currentView = view;
        model = new DialogModel(textsData);
        currentView.Initialize(ButtonsCallback, PlayNextById);
        PlaySentence(0);
    }

    // private void LoadTextByLabel(int progress)
    // {
    //     Addressables.LoadAssetsAsync<TextAsset>(txtLabel, (txt) => textsData.Add(txt)).Completed += (operationHandler) =>
    //     {
    //         textsData =textsData.OrderBy(t =>t.name[0]).ToList();
    //         for (int i = 0; i < textsData.Count; i++)
    //         {
    //              Debug.Log($"data{i} name : "+textsData[i].name);
    //         }
    //         if (operationHandler.Status == AsyncOperationStatus.Succeeded)
    //         {
    //             currentProgress = progress;
    //             if (textsData.Count <= currentProgress)
    //             {
    //                 Debug.LogWarning("對話progress 超過此主題的文本數量");
    //             }
    //             model = new DialogModel(textsData[currentProgress]);
    //             currentView.Initialize(ButtonsCallback, PlayNextById);
    //             PlaySentence(0);
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Failed to load");
    //         }
    //     };
    // }

    // private void LoadTextByName(string dialogName)
    // {
    //     Addressables.LoadAssetAsync<TextAsset>(dialogName).Completed += (operationHandler) =>
    //     {
    //         if (operationHandler.Status == AsyncOperationStatus.Succeeded)
    //         {
    //             model = new DialogModel(operationHandler.Result);
    //             currentView.Initialize(ButtonsCallback, PlayNextById);
    //             PlaySentence(0);
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Failed to load");
    //         }
    //     };
    // }
    protected virtual void LoadTxTSettings()
    {
    }
    private void PlayNextById(DialogContentData currentContent)
    {
        Debug.Log("play next"+ currentContent.nexttype);
        if (currentContent.nexttype == "Sentence")
        {
            PlaySentence(currentContent.nextid);
        }
        else if(currentContent.nexttype == "Button")
        {
            PlayButtons(currentContent.nextid);
        }else if (currentContent.nexttype == "End")
        {
            DialogEnd();
        }
    }
    protected void PlaySentence(int id)
    {
        Debug.Log("btn next id : "+id);
        if (id == 100)
        {
            DialogEnd();
            return;
        }
        currentView.PlaySentence(model.GetSentence(id));
    }

    private void PlayButtons(int id)
    {
        currentView.PlayButtons(model.GetButtons(id));
    }

    protected virtual void ButtonsCallback(DialogContentData clickBtn)
    {
        currentView.SetAllBtnsActiveToFalse();
        //Debug.Log("on click : " + clickBtn.content);
        PlaySentence(clickBtn.nextid);
    }
    protected virtual void DialogEnd()
    {
        currentView.Close();
    }
}

[Serializable]
public class DialogContentData
{ 
    public int id;
    public string type;
    public int nextid;
    /// <summary>
    /// Sentence / Button
    /// </summary>
    public string nexttype;
    public string avatarimagename;
    /// <summary>
    /// 立繪
    /// </summary>
    public string imagename;
    public string avatarname;
    public string content;
    /// <summary>
    /// 若type 是 按鈕
    /// </summary>
    public string callbackname;
}

