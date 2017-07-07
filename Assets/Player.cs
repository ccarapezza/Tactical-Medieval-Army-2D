using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private static Player instance;
    public static Player Instance { get { return instance; } }

    public List<Unit> units;
    private int selectedUnitIndex;
    private Camera mainCamera;
    private float cameraTransitionToUnit;
    public float cameraTransitionDurationToUnit;

    private float cameraTransitionToProjectile;
    public float cameraTransitionDurationToProjectile;

    public Projectile followProjectile;

    private bool returnCorroutineExecute;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        foreach (var unit in units)
            unit.active = false;

        selectedUnitIndex = 0;
        units[selectedUnitIndex].active = true;

        mainCamera = GetComponent<Camera>();
        cameraTransitionToProjectile = cameraTransitionDurationToProjectile;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
            NextUnit();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            
        }

        CameraMoveToSelectedUnit();
        CameraMoveToProjectile();
    }

    public void followPorjectile() {
        followProjectile = units[selectedUnitIndex].currentProjectile;
        cameraTransitionToProjectile = 0;
    }

    private void CameraMoveToSelectedUnit() {
        if (followProjectile != null) return;
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
        if (followProjectile==null) return;
        cameraTransitionToProjectile += Time.deltaTime;
        float interpolant = cameraTransitionToProjectile / cameraTransitionDurationToProjectile;
        Vector3 newCamPosition = new Vector3(followProjectile.transform.position.x, followProjectile.transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCamPosition, Time.deltaTime * 15f); //interpolant);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 5f, interpolant);
        //print(followProjectile.GetComponent<Rigidbody2D>().velocity.magnitude);
        if (followProjectile.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f && !returnCorroutineExecute) {
            print(followProjectile.GetComponent<Rigidbody2D>().velocity.magnitude);
            StartCoroutine(ReturnLookToUnit(2));
        }
    }

    IEnumerator ReturnLookToUnit(float time) {
        print("Return");
        returnCorroutineExecute = true;
        yield return new WaitForSeconds(time);
        followProjectile = null;
        returnCorroutineExecute = false;
        cameraTransitionToUnit = 0;
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
