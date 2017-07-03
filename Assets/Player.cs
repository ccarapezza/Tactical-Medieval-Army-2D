using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<Unit> units;
    private int selectedUnitIndex;
    private Camera mainCamera;
    private float changeUnitTime;

	// Use this for initialization
	void Start () {
        foreach (var unit in units)
            unit.active = false;

        selectedUnitIndex = 0;
        units[selectedUnitIndex].active = true;

        mainCamera = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
            NextUnit();

        Vector3 newCamPosition = new Vector3(units[selectedUnitIndex].transform.position.x, units[selectedUnitIndex].transform.position.y+units[selectedUnitIndex].cameraPositionYOffset, mainCamera.transform.position.z);

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCamPosition, Time.time - changeUnitTime);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, units[selectedUnitIndex].cameraZoom, Time.time - changeUnitTime);
    }

    void NextUnit() {
        changeUnitTime = Time.time+1f;
        units[selectedUnitIndex].active = false;
        if (units.Count == ++selectedUnitIndex){
            selectedUnitIndex = 0;
        }
        units[selectedUnitIndex].active = true;
    }
}
