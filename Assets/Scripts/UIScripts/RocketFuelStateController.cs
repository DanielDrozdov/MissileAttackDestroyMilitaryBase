using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketFuelStateController : MonoBehaviour
{
    private static RocketFuelStateController Instance;
    private Image fuelImage;
    private float fuelReserv = 100f;
    private float fuelCost = 10f;
    private float currentFuel;
    private bool HasFuel = true;

    private RocketFuelStateController() { }

    private void Awake() {
        Instance = this;
        fuelImage = GetComponent<Image>();
        currentFuel = fuelReserv;
    }

    public static RocketFuelStateController GetInstance() {
        return Instance;
    }

    public bool HasRocketFuel() {
        return HasFuel;
    }

    public void BurnFuel() {
        currentFuel -= fuelCost * Time.deltaTime;
        if(currentFuel <= 0) {
            HasFuel = false;
        }
        UpdateFuelImage();
    }

    public void ResetFuel() {
        currentFuel = fuelReserv;
        HasFuel = true;
        UpdateFuelImage();
    }

    private void UpdateFuelImage() {
        fuelImage.fillAmount = currentFuel / fuelReserv;
    }
}
