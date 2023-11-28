using UnityEngine;
using UnityEngine.UI;

public class CarCameraScript : MonoBehaviour
{
    public Transform car;
    public float distance = 6.4f;
    public float height = 1.4f;
    public float rotationDamping = 3.0f;
    public float heightDamping = 2.0f;
    public float zoomRatio = 0.5f;
    public float defaultFOV = 60f;
    public float rotationSpeed = 2f;
    public Text speedText; // Reference to a UI Text component for displaying speed

    private float rotationX = 0f;
    private float rotationY = 0f;

    void LateUpdate()
    {
        float wantedHeight = car.position.y + height;
        float myHeight = transform.position.y;

        myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.position = car.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        Vector3 temp = transform.position;
        temp.y = myHeight;
        transform.position = temp;
        transform.LookAt(car);

        // Display car speed
        float speed = car.GetComponent<Rigidbody>().velocity.magnitude * 2.23694f; // Convert m/s to mph
        speedText.text = Mathf.Round(speed) + " mph";
    }

    void Update()
    {
        // Get mouse input for free-look rotation
        rotationY += Input.GetAxis("Mouse X") * rotationSpeed;
        rotationX -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Limit vertical rotation to a certain range (-60 to 60 degrees)
        rotationX = Mathf.Clamp(rotationX, -60f, 60f);
    }

    void FixedUpdate()
    {
        float acc = car.GetComponent<Rigidbody>().velocity.magnitude;
        GetComponent<Camera>().fieldOfView = defaultFOV + acc * zoomRatio * Time.deltaTime;
    }
}
