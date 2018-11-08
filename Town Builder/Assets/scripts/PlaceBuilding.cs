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
	private GameObject mainCamera;

	private GameObject currentPlaceableObject;

	private float mouseWheelRotation;
	private int arrayCount;

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
			MoveCurrentObjectToMouse();
			ReleaseIfClicked();
		}
	}

	private void HandleNewObjectHotkey()
	{
		if (Input.GetKeyDown (newObjectHotkey)) {
			currentPlaceableObject = Instantiate (placeableObjectPrefab);
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
		}

			
	}


	private void MoveCurrentObjectToMouse()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo))
		{
			currentPlaceableObject.transform.position = PlaceBuildingNear(hitInfo.point);
			currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
		}
	}
		

	private void ReleaseIfClicked()
	{
		if (Input.GetMouseButtonDown(0))
		{
			currentPlaceableObject = null;
		}
	}

	private Vector3 PlaceBuildingNear(Vector3 near){
		var finalPosition = grid.GetGridPoint(near);
		finalPosition += new Vector3 (0f, 2.5f, 0f);
		return finalPosition;
	}

}

	