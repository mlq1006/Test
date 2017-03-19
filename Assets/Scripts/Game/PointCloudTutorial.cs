using UnityEngine;
using System.Collections;

public class PointCloudTutorial : MonoBehaviour {

    private void OnCustomGesture(PointCloudGesture gesture)
    {
        Debug.Log(gesture.RecognizedTemplate.name);
        if (null != GameController.Instance.MagicGesture)
        {
            GameController.Instance.MagicGesture(gesture.RecognizedTemplate.name);
        }


    }

}
