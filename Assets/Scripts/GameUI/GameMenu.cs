using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

    public static GameMenu instance;

    public GameObject optionMenu;
    private UIWidget widget;

    void Start()
    {
        instance = this;
        optionMenu.gameObject.SetActive(false);
        widget = this.GetComponent<UIWidget>();
        Close();
    }

    public void OnClickOptionBtn()
    {
        optionMenu.gameObject.SetActive(true);
    }

    public void OnClickStartBtn()
    {

    }

    public void Show()
    {
        widget.alpha = 1;
    }

    public void Close()
    {
        widget.alpha = 0;
    }

}
