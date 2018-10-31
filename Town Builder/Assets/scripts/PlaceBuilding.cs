using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuilding : MonoBehaviour{

	public Grid grid;
	public GameObject prefab;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hitinfo;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hitinfo)) {
				PlaceBuildingNear (hitinfo.point);
			}
		}
	}


	private void PlaceBuildingNear(Vector3 near){
		var finalPosition = grid.GetGridPoint(near);
		finalPosition += new Vector3 (0f, 2.5f, 0f);
		Instantiate (prefab, finalPosition, Quaternion.identity);
	}

}
