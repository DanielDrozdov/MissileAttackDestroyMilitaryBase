using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    private static LoadSceneManager Instance;
    private string currentLevelNumber = "CurrentLevelNumber";
    [SerializeField] private int passiveLevelsCount;
    private int levelsCountInBuild;
    private int levelNumber;

    private LoadSceneManager() { }

    private void Awake() {
        Instance = this;
        levelsCountInBuild = SceneManager.sceneCountInBuildSettings - passiveLevelsCount;
        LoadLevel();
        DontDestroyOnLoad(gameObject);
    }

    public static LoadSceneManager GetInstance() {
        return Instance;
    }

    public void LoadNextLevel() {
        PlayerPrefs.SetInt(currentLevelNumber, ++levelNumber);
        LoadLevel();
    }

    private void LoadLevel() {
        SetScenesData();
        SceneManager.LoadScene(levelNumber);
    }

    private void SetScenesData() {
        if(PlayerPrefs.HasKey(currentLevelNumber)) {
            levelNumber = PlayerPrefs.GetInt(currentLevelNumber);
        }
        if(levelNumber > levelsCountInBuild) {
            levelNumber = levelsCountInBuild;
        }
    }
}
