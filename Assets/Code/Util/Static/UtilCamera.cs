using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilCamera
{
#nullable enable
    public static GameObject CreateCamera(GameObject? parentObject = null)
    {
#nullable disable
        GameObject cameraObject = new GameObject("Camera");
        cameraObject.AddComponent<AudioListener>();
        Camera camera = AddCamera(cameraObject);
        if (!parentObject) return cameraObject;
        camera.transform.position = parentObject.transform.position;
        camera.transform.SetParent(parentObject.transform);
        return cameraObject;
    }
    public static Camera AddCamera(GameObject cameraObject)
    {
        Camera camera = cameraObject.AddComponent<Camera>();
        camera.backgroundColor = Color.black;
        camera.depth = -1;
        camera.orthographic = true;
        camera.nearClipPlane = 0;
        camera.orthographicSize = 10;
        int layerMask = 1 << 6;
        camera.cullingMask &= ~layerMask;
        return camera;
    }
}
