using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRocketLifeCreaterController : MonoBehaviour
{
    [SerializeField] private Transform rocketSpawnerPoint;
    [SerializeField] private GameObject rocketPrefab;
    private static NewRocketLifeCreaterController Instance;
    private CameraForwardController cameraForwardController;
    private Queue<GameObject> rocketsQueue;

    public int lifes;

    private NewRocketLifeCreaterController() { }

    private void Awake() {
        rocketsQueue = new Queue<GameObject>(lifes);
        Instance = this;
        CreateRockets();
    }

    private void Start() {
        cameraForwardController = CameraForwardController.GetInstance();
        SpawnRocket();
    }

    public static NewRocketLifeCreaterController GetInstance() {
        return Instance;
    }

    public void SpawnRocket() {
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
