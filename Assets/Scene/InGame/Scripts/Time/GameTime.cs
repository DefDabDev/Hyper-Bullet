using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameTime {

    private static float _timeScale = 1f;
    public static float timeScale { set { _timeScale = value; } get { return _timeScale; } }

    private static float _deltaTime = (1f / 60f);
    public static float deltaTime { set { _deltaTime = value; } get { return (_deltaTime * timeScale); } }
}
