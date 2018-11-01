using UnityEngine;

public class Playermove : MonoBehaviour



{
	[SerializeField] private PlaceBuilding building;

	void Update()
	{
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Translate(x, 0, 0);
		transform.Translate(0, 0, z);

	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Buildable") == true){
			building.isBuildable = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Buildable") == true){
			building.isBuildable = false;
		}
	}

	
}