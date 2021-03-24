using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRocketLifeCreaterController : MonoBehaviour
{
    [SerializeField] private Transform rocketSpawnerPoint;
    [SerializeField] private Transform generalGuyPoint;
    [SerializeField] private GameObject rocketPrefab;
    private static NewRocketLifeCreaterController Instance;
    private CameraForwardController cameraForwardController;
    private Queue<GameObject> rocketsQueue;

    public int lifes;
    private float cameraMoveSpeed = 10f;
    private bool IsStartAnim = true;

    private NewRocketLifeCreaterController() { }

    private void Awake() {
        rocketsQueue = new Queue<GameObject>(lifes);
        Instance = this;
        CreateRockets();
    }

    private void Start() {
        cameraForwardController = CameraForwardController.GetInstance();
        cameraForwardController.transform.position = new Vector3(rocketSpawnerPoint.position.x,rocketSpawnerPoint.position.y,
            cameraForwardController.transform.position.z);
        StartCameraAnim();
    }

    public static NewRocketLifeCreaterController GetInstance() {
        return Instance;
    }

    public void StartCameraAnim() {
        Vector3 targetPoint;
        if(IsStartAnim) {
            targetPoint = new Vector3(generalGuyPoint.transform.position.x, cameraForwardController.transform.position.y,
                       cameraForwardController.transform.position.z);
        } else {
            targetPoint = new Vector3(rocketSpawnerPoint.position.x, rocketSpawnerPoint.position.y,
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
        if(!IsStartAnim) {
            SpawnRocket();
        } else {
            IsStartAnim = false;
            StartCameraAnim();
        }
    }

    private void SpawnRocket() {
        if(rocketsQueue.Count <= 0) {
            Debug.LogError("GAME OVER");
            return;
        }
        GameObject rocket = rocketsQueue.Dequeue();
        cameraForwardController.playerTransform = rocket.transform;
        RocketFuelStateController.GetInstance().ResetFuel();
        rocket.SetActive(true);
    }

    private void CreateRockets() {
        for(int i = 0;i < lifes; i++) {
            GameObject newRocket = Instantiate(rocketPrefab, rocketSpawnerPoint.position,rocketPrefab.transform.rotation, transform);
            rocketsQueue.Enqueue(newRocket);
        }
    }
}
