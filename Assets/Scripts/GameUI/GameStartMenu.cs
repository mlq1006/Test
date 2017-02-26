using UnityEngine;
using System.Collections;

public class GameStartMenu : MonoBehaviour {

    void Start()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowMenu());
    }

    IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(3f);
        Close();
    }

    public void OnClickBgBtn()
    {
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        GameMenu.instance.Show();
    }
}
