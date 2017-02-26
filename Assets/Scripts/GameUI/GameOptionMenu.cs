using UnityEngine;
using System.Collections;


[System.Serializable]
public class ButtonSprite
{
    public string normalSprite;
    public string hoverSprite;
    public string pressSprite;
}

public class GameOptionMenu : MonoBehaviour {

    private UIButton audioBtn;
    private UIButton musicBtn;

    public ButtonSprite audioOnSprite;
    public ButtonSprite audioOffSprite;

    public ButtonSprite musicOnSprite;
    public ButtonSprite musicOffSprite;

    void Start()
    {
        Init();
    }

    void Init()
    {
        audioBtn = transform.FindChild("audiobtn").GetComponent<UIButton>();
        musicBtn = transform.FindChild("musicbtn").GetComponent<UIButton>();

        SetAudioButton();
        SetMusicButton();

    }

    void OnEnable()
    {
        TweenScale ts = this.GetComponent<TweenScale>();
        if(!ts.enabled)
        {
            ts.ResetToBeginning();
            ts.PlayForward();
        }
    }

    void SetAudioButton()
    {
        int audioValue = PlayerPrefManager.GetAudio();
        ButtonSprite btnSprite = null;
        if(0 == audioValue)
        {
            btnSprite = audioOffSprite;
        }else if(1 == audioValue)
        {
            btnSprite = audioOnSprite;
        }
        audioBtn.normalSprite = btnSprite.normalSprite;
        audioBtn.hoverSprite = btnSprite.hoverSprite;
        audioBtn.pressedSprite = btnSprite.pressSprite;
    }

    void SetMusicButton()
    {
        int musicValue = PlayerPrefManager.GetMusic();
        ButtonSprite btnSprite = null;
        if (0 == musicValue)
        {
            btnSprite = musicOffSprite;
        }
        else if (1 == musicValue)
        {
            btnSprite = musicOnSprite;
        }
        musicBtn.normalSprite = btnSprite.normalSprite;
        musicBtn.hoverSprite = btnSprite.hoverSprite;
        musicBtn.pressedSprite = btnSprite.pressSprite;
    }

    public void OnClickAudioBtn()
    {
        int audioValue = PlayerPrefManager.GetAudio();
        PlayerPrefManager.SetAudio(audioValue == 0 ?1:0);
        SetAudioButton();
    }

    public void OnClickMusicBtn()
    {
        int musicValue = PlayerPrefManager.GetMusic();
        PlayerPrefManager.SetMusic(musicValue == 0 ? 1 : 0);
        SetMusicButton();
    }

    public void OnClickCloseBtn()
    {
        gameObject.SetActive(false);
    }

}
