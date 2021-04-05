using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiePanelController : MonoBehaviour
{
    public delegate void DiePanelActions();
    public static event DiePanelActions OnPlayerDie;

    private void Awake() {
        OnPlayerDie();
    }

    public void OnClick_RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
