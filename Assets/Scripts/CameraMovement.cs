using UnityEngine;

/// <summary>
/// A simple FPP (First Person Perspective) camera rotation script.
/// Like those found in most FPS (First Person Shooter) games.
/// </summary>
public class CameraMovement : MonoBehaviour
{

	public float Sensitivity
	{
		get { return sensitivity; }
		set { sensitivity = value; }
	}
	[Range(0.1f, 9f)] [SerializeField] float sensitivity = 2f;
	[Range(0f, 90f)] [SerializeField] float yRotationLimit = 88f;

	Vector2 rotation = Vector2.zero;

	void Update()
	{
		rotation.x += Input.GetAxis("Mouse X") * sensitivity;
		rotation.y += Input.GetAxis("Mouse Y") * sensitivity;
		rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
		var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
		var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

		transform.localRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
	}
}