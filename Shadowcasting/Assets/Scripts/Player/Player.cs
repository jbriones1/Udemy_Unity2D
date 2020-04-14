using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private FieldOfView fov;
	private float aimDir;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		aimDir = Utilities.GetAngleFromVector(transform.rotation.eulerAngles);
		fov.SetAimDirection();
		fov.SetOrigin(transform.position);
	}



}
