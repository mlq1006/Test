using UnityEngine;
using System.Collections;

public class PointCloudTutorial : MonoBehaviour {

    private void OnCustomGesture(PointCloudGesture gesture)
    {
        Debug.Log(gesture.RecognizedTemplate.name);
    }

}
