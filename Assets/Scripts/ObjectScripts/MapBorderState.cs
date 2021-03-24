using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBorderState : MonoBehaviour
{
    private static MapBorderState Instance;

    private MapBorderState() { }

    private void Awake() {
        Instance = this;
    }

    public static MapBorderState GetInstance() {
        return Instance;
    }
}
