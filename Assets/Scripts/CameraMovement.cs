using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float Sensitivity {
		get { return sensitivity; }
		set { sensitivity = value; }
	}
	[Range(0.1f, 9f)] [SerializeField] float sensitivity = 2f;
	[Range(0f, 90f)] [SerializeField] float yRotationLimit = 88f;

	Vector2 rotation = Vector2.zero;
	float mouseX, mouseY;

    private void Start() {
		mouseX = 0;
		mouseY = 0;
    }

    void Update() {
		mouseX += Input.GetAxis("Mouse X") * sensitivity;
		mouseY += Input.GetAxis("Mouse Y") * sensitivity;
		mouseY = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

		Quaternion xQuat = Quaternion.AngleAxis(mouseX, Vector3.up);
		Quaternion yQuat = Quaternion.AngleAxis(mouseY, Vector3.left);

		transform.localRotation = xQuat * yQuat;
	}
}