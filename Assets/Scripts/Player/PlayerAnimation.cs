﻿using UnityEngine;
using System.Collections;

public enum PlayStatus
{
    start,idle,cast,spellcast
}

[System.Serializable]
public class PlayerAnimationData
{
    public Vector3[] headPosition;
    public Vector3[] bodyPosition;
    public string namePrefix;
    public int framerate;
}

public class PlayerAnimation : MonoBehaviour {

    public PlayStatus playStatus = PlayStatus.start;
    public PlayerAnimationData startData, idleData, castData, spellcastData;
    private PlayerAnimationData currentData;

    private UISprite bodySprite;//身体
    private UISprite headSprite;//头部

    private UIAtlas atlas;//图集

    private float timerFrame;
    private float timer;//计时器

    private BetterList<string> allSpriteList;
    private BetterList<string> spriteList = new BetterList<string>();

    private int frameCount = 0;

    void Awake()
    {
        bodySprite = this.transform.FindChild("body").GetComponent<UISprite>();
        headSprite = this.transform.FindChild("head").GetComponent<UISprite>();

        atlas = bodySprite.atlas;
        allSpriteList = atlas.GetListOfSprites();

        currentData = GetCurrentAnimation();
        timerFrame = 1f / currentData.framerate;
     
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timerFrame)
        {
            if (frameCount < spriteList.size)
            {
                string spriteName = spriteList[frameCount];
                bodySprite.spriteName = spriteName;
                UISpriteData uisd = atlas.GetSprite(spriteName);
                bodySprite.width = uisd.width;
                bodySprite.height = uisd.height;
                bodySprite.transform.localPosition = currentData.bodyPosition[frameCount];
                headSprite.transform.localPosition = currentData.headPosition[frameCount];
                frameCount++;
            }

            if (playStatus.Equals(PlayStatus.start) && frameCount == spriteList.size)
            {
                SetPlayerStatus(PlayStatus.idle);
            }

            if (playStatus.Equals(PlayStatus.idle) && frameCount == spriteList.size)
            {
                frameCount = 0;
            }

            timer = 0;
        }
    }


    PlayerAnimationData GetCurrentAnimation()
    {
        switch(playStatus)
        {
            case PlayStatus.start:
                return startData;
            case PlayStatus.idle:
                return idleData;
            case PlayStatus.cast:
                return castData;
            case PlayStatus.spellcast:
                return spellcastData;
        }
        return null;
    }

    public void SetPlayerStatus(PlayStatus status)
    {
        this.playStatus = status;
        frameCount = 0;
        currentData = GetCurrentAnimation();
        SetSpriteList();
    }

    private void SetSpriteList()
    {
        spriteList = new BetterList<string>();
        int count = allSpriteList.size;
        for (int i = 0; i < count; i++)
        {
            string spriteName = allSpriteList[i];
            if(spriteName.StartsWith(currentData.namePrefix))
            {
                spriteList.Add(spriteName);
            }
        }
    }
}
