using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public float minSize;
    public float maxSize;
    private void AdjustCameraSize()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        mainCamera.orthographicSize = Mathf.Lerp(minSize, maxSize, aspectRatio - 0.2f);
    }
    void Update()
    {
        AdjustCameraSize();
    }
}
