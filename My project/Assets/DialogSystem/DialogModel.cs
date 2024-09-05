using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DialogModel
{
    protected List<DialogContentData> contents;
    public DialogModel(TextAsset data)
    {
        DialogContents dialogContents = JsonUtility.FromJson<DialogContents>(data.text);
        contents = dialogContents.GetContents();
    }
    public DialogContentData GetSentence(int id)
    {
        return contents.Find(x=>x.id == id);
    }

    public List<DialogContentData> GetButtons(int id)
    {
        var d = contents.FindAll(x => x.id == id);
        if (d is null)
        {
            Debug.LogWarning($"{id} is null");
        }
        return contents.FindAll(x=>x.id == id);
    }
}

[Serializable]
public class DialogContents
{
    public DialogContentData[] Contents;
    public List<DialogContentData> GetContents()
    {
        return Contents.Select(x => x).ToList();
    }
}
