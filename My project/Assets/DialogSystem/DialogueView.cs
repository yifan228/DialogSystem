using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.U2D;
public class DialogueView : MonoBehaviour
{
    Action<DialogContentData> buttonCallback;
    Action<DialogContentData> moveNext;
    DialogContentData currentSentence = null;
    List<DialogContentData> currentBtnsdata;
    [SerializeField] SpriteAtlas avatarAtalas;
    [SerializeField] Image avatarImamge;
    [SerializeField] TMP_Text avatarName;
    [SerializeField] TMP_Text sentenceContent;
    [SerializeField] Button[] choiceBtns;
    bool isNeedToChoose;
    bool onPlayingSentenc;
    Tween currentDialogTween;
    public void Initialize(Action<DialogContentData> btnCallback, Action<DialogContentData> moveToNext)
    {
        Open();
        buttonCallback = btnCallback;
        moveNext = moveToNext;
    }

    public void SetAtlasInRunTime(SpriteAtlas atlas)
    {
        avatarAtalas = atlas;
    }

    public void PlaySentence(DialogContentData data)
    {
        onPlayingSentenc = true;
        isNeedToChoose = false;
        currentSentence = data;
        avatarName.text = currentSentence.avatarname;
        if (avatarAtalas != null)
        {
            Sprite avatarSp = avatarAtalas.GetSprite(currentSentence.avatarimagename);
            if (avatarSp is null)
            {
                avatarImamge.enabled = false;
            }
            else
            {
                avatarImamge.enabled = true;
                avatarImamge.sprite = avatarSp;
            }
        }
        sentenceContent.text = string.Empty;
        currentDialogTween = DOTween.To(() => string.Empty, value => sentenceContent.text = value, data.content, data.content.Length * 0.1f).OnComplete(()=>onPlayingSentenc = false);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))
        {
            if (!onPlayingSentenc)
            {
                sentenceContent.text = currentSentence.content;
                MoveNext();
            }
            else
            {
                sentenceContent.text = currentSentence.content;
                currentDialogTween.Kill();
                onPlayingSentenc = false;
            }
        }
    }
    private void MoveNext()
    {
        if (!isNeedToChoose && currentSentence !=null)
        {
            moveNext(currentSentence);
        }
    }

    public void PlayButtons(List<DialogContentData> data)
    {
        currentBtnsdata = data;
        isNeedToChoose = true;
        for (int i = 0; i < data.Count; i++)
        {
            choiceBtns[i].gameObject.SetActive(true);
            choiceBtns[i].GetComponentInChildren<TMP_Text>().text = currentBtnsdata[i].content;
        }
    }

    /// <summary>
    /// 用拖曳的方式掛在場景上
    /// </summary>
    /// <param name="index"></param>
    public void ButtonsEvent(int index)
    {
        buttonCallback(currentBtnsdata[index]);
    }

    public void SetAllBtnsActiveToFalse()
    {
        for (int i = 0; i < choiceBtns.Length; i++)
        {
            if (choiceBtns[i].gameObject.activeSelf)
            {
                choiceBtns[i].gameObject.SetActive(false);
            }
        }
    }

    private void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
