using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RocketLifesPanelController : MonoBehaviour
{
    private static RocketLifesPanelController Instance;
    private TextMeshProUGUI text;

    private RocketLifesPanelController() { }

    private void Awake() {
        Instance = this;
        text = GetComponent<TextMeshProUGUI>();
    }

    public static RocketLifesPanelController GetInstance() {
        return Instance;
    }

    public void UpdateLifesPanel(int lifes) {
        text.text = lifes.ToString();
    }
}
