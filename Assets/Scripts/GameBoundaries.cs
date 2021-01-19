using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBoundaries
{
    public static float RightBoundary = Camera.main.orthographicSize * Screen.width / Screen.height;
    public static float LeftBoundary = RightBoundary * -1;
    public static float UpBoundary = Camera.main.orthographicSize;
    public static float DownBoundary = UpBoundary * -1;
}
