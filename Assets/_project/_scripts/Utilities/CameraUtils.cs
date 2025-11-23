using UnityEngine;

public static class CameraUtils
{
    public static float ScreenWidthWorld => Camera.main.orthographicSize * 2f * Camera.main.aspect;
    public static float ScreenHeightWorld => Camera.main.orthographicSize * 2f;
}
