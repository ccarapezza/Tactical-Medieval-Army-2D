using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<Unit> units;
    private int selectedUnitIndex;
    private Camera mainCamera;
    private float cameraTransitionToUnit;
    public float cameraTransitionDurationToUnit;

    private float cameraTransitionToProjectile;
    public float cameraTransitionDurationToProjectile;

    private bool followProjectile;


    // Use this for initialization
    void Start () {
        foreach (var unit in units)
            unit.active = false;

        selectedUnitIndex = 0;
        units[selectedUnitIndex].active = true;

        mainCamera = GetComponent<Camera>();
        followProjectile = false;
        cameraTransitionToProjectile = cameraTransitionDurationToProjectile;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
            NextUnit();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            followProjectile = true;
            cameraTransitionToProjectile = 0;
        }

        CameraMoveToSelectedUnit();
        CameraMoveToProjectile();
    }

    private void CameraMoveToSelectedUnit() {
        if (followProjectile) return;
        if (cameraTransitionToUnit <= cameraTransitionDurationToUnit) {
            cameraTransitionToUnit += Time.deltaTime;
            float interpolant = cameraTransitionToUnit / cameraTransitionDurationToUnit;
            Unit selectedUnit = units[selectedUnitIndex];
            Vector3 newCamPosition = new Vector3(selectedUnit.transform.position.x, selectedUnit.transform.position.y + selectedUnit.cameraPositionYOffset, mainCamera.transform.position.z);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCamPosition, interpolant);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, selectedUnit.cameraZoom, interpolant);
        }
    }

    private void CameraMoveToProjectile()
    {
        if (!followProjectile) return;
        cameraTransitionToProjectile += Time.deltaTime;
        float interpolant = cameraTransitionToProjectile / cameraTransitionDurationToProjectile;
        Unit selectedUnit = units[selectedUnitIndex];
        Projectile projPosition = selectedUnit.currentProjectile;
        Vector3 newCamPosition = new Vector3(projPosition.transform.position.x, projPosition.transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCamPosition, interpolant);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, selectedUnit.cameraZoom, interpolant);
    }

    void NextUnit() {
        cameraTransitionToUnit = 0;
        units[selectedUnitIndex].active = false;
        if (units.Count == ++selectedUnitIndex){
            selectedUnitIndex = 0;
        }
        units[selectedUnitIndex].active = true;
    }
}
