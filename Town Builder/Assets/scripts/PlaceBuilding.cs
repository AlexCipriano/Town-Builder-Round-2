using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuilding : MonoBehaviour
{
	[SerializeField]
	private GameObject placeableObjectPrefab;
	[SerializeField]
	private GameObject[] prefabArray;
	[SerializeField]
	private KeyCode newObjectHotkey = KeyCode.A;
	[SerializeField]
	private Grid grid;
	[SerializeField]
	public Camera mainCamera;
	public Camera buildingCam;
	public Vector3 offset;
	public Vector3 offsetBack;
	public Vector3 offsetForward;
	public Vector3 offsetLeft;
	public Vector3 offsetRight;
	public Vector3 tentoffest;

	private GameObject currentPlaceableObject;

	private float mouseWheelRotation;
	public int arrayCount;

	public bool isBuildable = false;



	private void start()
	{
		arrayCount = 0;
		placeableObjectPrefab = prefabArray [arrayCount];
	}

	private void Update()
	{
		HandleNewObjectHotkey();

		if (currentPlaceableObject != null)
		{


			if (Input.GetKeyDown(KeyCode.A)) {
				currentPlaceableObject.transform.position = currentPlaceableObject.transform.position + new Vector3 (-5f, 0f, 0f);
				UpdateCamera ();
			}
			if (Input.GetKeyDown(KeyCode.D)) {
				currentPlaceableObject.transform.position = currentPlaceableObject.transform.position + new Vector3 (5f, 0f, 0f);
				UpdateCamera ();
			}
			if (Input.GetKeyDown(KeyCode.W)) {
				currentPlaceableObject.transform.position = currentPlaceableObject.transform.position + new Vector3 (0f, 0f, 5f);
				UpdateCamera ();
			}
			if (Input.GetKeyDown(KeyCode.S)) {
				currentPlaceableObject.transform.position = currentPlaceableObject.transform.position + new Vector3 (0f, 0f, -5f);
				UpdateCamera ();
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				offset = offsetLeft;
				buildingCam.transform.rotation = Quaternion.Euler (30f, 90f, 0f);
				UpdateCamera ();
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)) {
				offset = offsetRight;
				buildingCam.transform.rotation = Quaternion.Euler (30f, -90f, 0f);
				UpdateCamera ();
			}
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				offset = offsetForward;
				buildingCam.transform.rotation = Quaternion.Euler (30f, 180f, 0f);
				UpdateCamera ();
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				offset = offsetBack;
				buildingCam.transform.rotation = Quaternion.Euler (30f, 0f, 0f);
				UpdateCamera ();
			}

			ReleaseIfClicked();
		}
	}

	private void HandleNewObjectHotkey()
	{
		
		if (Input.GetKeyDown (newObjectHotkey)) {
			currentPlaceableObject = Instantiate (placeableObjectPrefab);
			if (arrayCount == 0) {
				currentPlaceableObject.transform.position = tentoffest;
			} else {
				currentPlaceableObject.transform.position = new Vector3 (0f, 2.5f, 0f);
			}
			buildingCam.enabled = true;
			mainCamera.enabled = false;
			buildingCam.gameObject.SetActive (true);
			UpdateCamera ();
		} 

		if (Input.GetKeyDown (KeyCode.Q)) {
			arrayCount--;
			if (arrayCount < 0){
				arrayCount = prefabArray.Length - 1;
			}
			placeableObjectPrefab = prefabArray [arrayCount];
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			arrayCount++;
			if (arrayCount > prefabArray.Length - 1)
				arrayCount = 0;
			placeableObjectPrefab = prefabArray [arrayCount];
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Destroy (currentPlaceableObject);
			buildingCam.enabled = false;
			mainCamera.enabled = true;
		}

			
	}


	private void MoveCurrentObjectToMouse()
	{
		



	}
		

	private void ReleaseIfClicked()
	{
		if (Input.GetMouseButtonDown(0))
		{
			currentPlaceableObject = null;
			buildingCam.enabled = false;
			mainCamera.enabled = true;
		}
	}

	private Vector3 PlaceBuildingNear(Vector3 near){
		var finalPosition = grid.GetGridPoint(near);
		finalPosition += new Vector3 (0f, 2.5f, 0f);
		return finalPosition;
	}

	private void UpdateCamera() { 
		buildingCam.transform.position = Vector3.Lerp(buildingCam.transform.position,currentPlaceableObject.transform.position + offset, 10f );
	}

}

	