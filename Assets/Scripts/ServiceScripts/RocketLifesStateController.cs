using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLifesStateController : MonoBehaviour
{
    [SerializeField] private Transform rocketSpawnerPoint;
    [SerializeField] private Transform generalGuyPoint;
    [SerializeField] private GameObject rocketPrefab;
    private static RocketLifesStateController Instance;
    private CameraForwardController cameraForwardController;
    private Queue<GameObject> rocketsQueue;

    [SerializeField] private int lifes;
    private float cameraMoveSpeed = 10f;
    private bool IsStartAnim = true;
    private bool IsGameOver;
    private bool IsPlayerWin;

    private RocketLifesStateController() { }

    private void Awake() {
        rocketsQueue = new Queue<GameObject>(lifes);
        Instance = this;
        CreateRocketsInPool();
    }

    private void Start() {
        cameraForwardController = CameraForwardController.GetInstance();
        cameraForwardController.transform.position = new Vector3(rocketSpawnerPoint.position.x,rocketSpawnerPoint.position.y,
            cameraForwardController.transform.position.z);
        RocketLifesPanelController.GetInstance().UpdateLifesPanel(rocketsQueue.Count);
        StartPanelController.OnGameStarted += StartCameraAnim;
    }

    private void OnDestroy() {
        StartPanelController.OnGameStarted -= StartCameraAnim;
    }

    public static RocketLifesStateController GetInstance() {
        return Instance;
    }

    public void OnPlayerWin() {
        IsPlayerWin = true;
    }

    public bool GetPlayerWinState() {
        return IsPlayerWin;
    }

    public void StartCameraAnim() {
        Vector3 targetPoint;
        if(rocketsQueue.Count <= 0) {
            PanelsStateController.GetInstance().ActivateDiePanel();
            IsGameOver = true;
        }

        if(!IsStartAnim && !IsGameOver) {
            targetPoint = new Vector3(rocketSpawnerPoint.position.x, rocketSpawnerPoint.position.y,
                       cameraForwardController.transform.position.z);
        } else {
            targetPoint = new Vector3(generalGuyPoint.transform.position.x, generalGuyPoint.transform.position.y,
                       cameraForwardController.transform.position.z);
        }

        StartCoroutine(CameraAnimCoroutine(targetPoint));
    }

    private IEnumerator CameraAnimCoroutine(Vector3 targetPoint) {
        bool IsCoroutineEnded = false;
        while(!IsCoroutineEnded) {
            cameraForwardController.transform.position = Vector3.MoveTowards(cameraForwardController.transform.position, targetPoint,
                cameraMoveSpeed * Time.deltaTime);
            if(cameraForwardController.transform.position.x == targetPoint.x) {
                IsCoroutineEnded = true;
            }
            yield return null;
        }

        StopCoroutine(CameraAnimCoroutine(targetPoint));
        if(!IsGameOver) {
            if(!IsStartAnim) {
                ActivateRocket();
            } else {
                IsStartAnim = false;
                StartCameraAnim();
            }
        }
    }

    private void ActivateRocket() {
        GameObject rocket = rocketsQueue.Dequeue();
        cameraForwardController.playerTransform = rocket.transform;
        RocketFuelStateController.GetInstance().ResetFuel();
        RocketLifesPanelController.GetInstance().UpdateLifesPanel(rocketsQueue.Count);
        rocket.SetActive(true);
    }

    private void CreateRocketsInPool() {
        for(int i = 0;i < lifes; i++) {
            GameObject newRocket = Instantiate(rocketPrefab, rocketSpawnerPoint.position,rocketPrefab.transform.rotation, transform);
            rocketsQueue.Enqueue(newRocket);
        }
    }

}
