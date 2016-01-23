﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubbleDirector : Singletone<SpeechBubbleDirector>
{
    #region Variables
    private List<SpeechBubbleControl> mControlList = new List<SpeechBubbleControl>();
    private Dictionary<string, ConversationFileControl> mConvDic = new Dictionary<string, ConversationFileControl>();
    private List<SpeechBubbleControl> mTouchedBubbles = new List<SpeechBubbleControl>();
    [SerializeField]
    private GameObject mSpeechBubble = null;
    #endregion

    public List<SpeechBubbleControl> ControlList
    {
        get
        {
            return mControlList;
        }
    }

    #region VirtualFunctions
    void Start()
    {
        var cons = FindObjectsOfType<SpeechBubbleControl>();

        for(int i = 0; i < cons.Length; i++)
        {
            mControlList.Add(cons[i]);
        }

        var convs = FindObjectsOfType<ConversationFileControl>();

        for(int i = 0; i < convs.Length; i++)
        {
            mConvDic.Add(new string(convs[i].FileName.ToCharArray(), 0, convs[i].FileName.Length - 4), convs[i]);
        }
    }

    #endregion

    #region CustomFunctions
    public int MakeSpeechBubble(Transform fObj, Vector2 fOffset)
    {
        GameObject bubble = Instantiate(mSpeechBubble) as GameObject;
        bubble.transform.parent = UIDirector.Instance.GetUIAnchor(UIAnchor.Side.Center).transform;

        Vector3 screen = SmooothCamera.Instance.Camera3D.WorldToScreenPoint(fObj.position);
        bubble.transform.localPosition = screen;
        bubble.transform.localScale = Vector3.one;

        SpeechBubbleControl con = bubble.GetComponent<SpeechBubbleControl>();
        con.mSubject = fObj.gameObject;
        con.mOffset = fOffset;

        mControlList.Add(con);

        return mControlList.Count - 1;
    }

    public bool RemoveSpeechBubble(SpeechBubbleControl fCon)
    {
        return mControlList.Remove(fCon);
    }

    public void SpeechBubbleTouched(SpeechBubbleControl fCon)
    {
        mTouchedBubbles.Add(fCon);
    }

    /// <summary>
    /// 지정하는 말풍선에 텍스트를 표시하는 스크립트
    /// </summary>
    /// <param name="fSpeechIdx">말풍선 인덱스</param>
    /// <param name="fConvName">ConverstaionControl 파일 이름</param>
    /// <param name="fConvIdx">ConverstionControl 인덱스</param>
    /// <param name="fDuration">표시할 시간</param>
    public void ShowText(int fSpeechIdx, string fConvName, int fConvIdx, float fDuration)
    {
        if(fSpeechIdx < 0 || fSpeechIdx >= mControlList.Count)
        {
            Debug.LogWarning(name + ".SpeechBubbleDirector.ShowText() " + "fSpeechIdx is out of Range");
            return;
        }

        ConversationFileControl convCon = null;
        if(mConvDic.TryGetValue(fConvName, out convCon))
        {
            if(fConvIdx < 0 || fConvIdx >= convCon.ConvsList.Count)
            {
                Debug.LogWarning(name + ".SpeechBubbleDirector.ShowText() " + "fConvIdx is out of Range");
                return;
            }

            mControlList[fSpeechIdx].ShowText(convCon.ConvsList[fConvIdx], fDuration);
        }
    }
    #endregion
}