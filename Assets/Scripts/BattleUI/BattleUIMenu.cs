using UnityEngine;
using System.Collections;

public class BattleUIMenu : MonoBehaviour {

    public void Play()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

}
