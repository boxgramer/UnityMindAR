using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ARManager : MonoBehaviour
{
    public Transform markerObject;
    public Transform rotateableObject;
    public float positionScale = 0.01f;
    public float rotationMultiplier = 2f;

    public float rotateSpeed = 0.2f;
    private float currentXRotation = 0f;


    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
    private void Start()
    {
        HideModel();
    }
    public void SetMarkerPosition(string value)
    {
        string[] data = value.Split(',');

        float x = float.Parse(data[0], CultureInfo.InvariantCulture);
        float y = float.Parse(data[1], CultureInfo.InvariantCulture);
        float z = float.Parse(data[2], CultureInfo.InvariantCulture);

        // Three.js ? Unity conversion
        Vector3 converted = new Vector3(
             x,
             y,
             -z
         ) * positionScale;

        markerObject.localPosition = converted;
    }

    public void SetMarkerRotation(string value)
    {
        string[] data = value.Split(',');

        float x = float.Parse(data[0], CultureInfo.InvariantCulture);
        float y = float.Parse(data[1], CultureInfo.InvariantCulture);
        float z = float.Parse(data[2], CultureInfo.InvariantCulture);

        // rotation conversion usually needs adjustment
        markerObject.rotation = Quaternion.Euler(
            -x * Mathf.Rad2Deg * rotationMultiplier - 90,
            -y * Mathf.Rad2Deg * rotationMultiplier,
             z * Mathf.Rad2Deg * rotationMultiplier
        );
    }
    public void HideModel()
    {
        markerObject.gameObject.SetActive(false);
    }
    public void ShowModel()
    {
        markerObject.gameObject.SetActive(true);
    }

    private void Update()
    {
        HandleTouchRotation();
        HandleMouseRotation(); // Editor testing
    }

    private void HandleTouchRotation()
    {
        if (Touch.activeTouches.Count == 0)
            return;

        var touch = Touch.activeTouches[0];

        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
        {
            // vertical drag -> rotate X
            currentXRotation -=
                touch.delta.x * rotateSpeed;

            currentXRotation = Mathf.Clamp(
                currentXRotation,
                -180f,
                180f
            );

            ApplyRotation();
        }
    }

    /* --------------------------------
       Mouse for Editor Test
    -------------------------------- */

    private void HandleMouseRotation()
    {
        if (Mouse.current == null)
            return;


        if (Mouse.current.leftButton.isPressed)
        {

            Debug.Log(" mosue click");
            float mouseX =
                Mouse.current.delta.ReadValue().x;

            currentXRotation -=
                mouseX * rotateSpeed;

            currentXRotation = Mathf.Clamp(
                currentXRotation,
                -180f,
                180f
            );
            Debug.Log("rotation:" + currentXRotation);

            ApplyRotation();
        }
    }

    void ApplyRotation()
    {
        // Child rotates only on X
        rotateableObject.localRotation =
            Quaternion.Euler(
                0f,
                currentXRotation,
                0f
            );
    }
}
