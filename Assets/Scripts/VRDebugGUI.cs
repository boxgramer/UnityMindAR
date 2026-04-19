using UnityEngine;
using UnityEngine.InputSystem;

using Gyroscope = UnityEngine.InputSystem.Gyroscope;
public class VRDebugGUI : MonoBehaviour
{
    private string debugText = "";
    private Vector2 scrollPos;

    void Start()
    {
        // Enable legacy gyro + compass
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            Input.compass.enabled = true;
        }

#if ENABLE_INPUT_SYSTEM
        // Enable available sensors manually (New Input System)
        if (Gyroscope.current != null)
            InputSystem.EnableDevice(Gyroscope.current);

        if (Accelerometer.current != null)
            InputSystem.EnableDevice(Accelerometer.current);

        if (GravitySensor.current != null)
            InputSystem.EnableDevice(GravitySensor.current);

        if (LinearAccelerationSensor.current != null)
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
#endif
    }

    void Update()
    {
        BuildDebugText();
    }

    void BuildDebugText()
    {
        debugText = "";

        //-----------------------------------
        // DEVICE INFO
        //-----------------------------------
        debugText += "========== DEVICE INFO ==========\n";
        debugText += $"Platform: {Application.platform}\n";
        debugText += $"Device Model: {SystemInfo.deviceModel}\n";
        debugText += $"Device Name: {SystemInfo.deviceName}\n";
        debugText += $"Operating System: {SystemInfo.operatingSystem}\n";
        debugText += $"Supports Gyroscope: {SystemInfo.supportsGyroscope}\n";
        debugText += $"Battery Level: {SystemInfo.batteryLevel}\n";
        debugText += $"Battery Status: {SystemInfo.batteryStatus}\n\n";

        //-----------------------------------
        // SCREEN INFO
        //-----------------------------------
        debugText += "========== SCREEN ==========\n";
        debugText += $"Resolution: {Screen.width} x {Screen.height}\n";
        debugText += $"Screen Orientation: {Screen.orientation}\n";
        debugText += $"Device Orientation: {Input.deviceOrientation}\n\n";

        //-----------------------------------
        // LEGACY SENSOR DATA
        //-----------------------------------
        debugText += "========== LEGACY SENSOR DATA ==========\n";

        if (SystemInfo.supportsGyroscope)
        {
            Quaternion q = Input.gyro.attitude;

            debugText += $"Gyro Quaternion: {q}\n";
            debugText += $"Gyro Euler: {q.eulerAngles}\n";
            debugText += $"Rotation Rate: {Input.gyro.rotationRate}\n";
            debugText += $"Rotation Rate Unbiased: {Input.gyro.rotationRateUnbiased}\n";
            debugText += $"Gravity: {Input.gyro.gravity}\n";
            debugText += $"User Acceleration: {Input.gyro.userAcceleration}\n";
        }
        else
        {
            debugText += "Gyroscope NOT supported\n";
        }

        debugText += $"Acceleration: {Input.acceleration}\n";

        debugText += $"Compass True Heading: {Input.compass.trueHeading}\n";
        debugText += $"Compass Magnetic Heading: {Input.compass.magneticHeading}\n\n";

#if ENABLE_INPUT_SYSTEM
        //-----------------------------------
        // NEW INPUT SYSTEM
        //-----------------------------------
        debugText += "========== NEW INPUT SYSTEM ==========\n";

        if (Gyroscope.current != null)
        {
            debugText += $"Gyroscope Angular Velocity: " +
                         $"{Gyroscope.current.angularVelocity.ReadValue()}\n";
        }
        else
        {
            debugText += "Gyroscope.current = NULL\n";
        }

        if (Accelerometer.current != null)
        {
            debugText += $"Accelerometer: " +
                         $"{Accelerometer.current.acceleration.ReadValue()}\n";
        }
        else
        {
            debugText += "Accelerometer.current = NULL\n";
        }

        if (GravitySensor.current != null)
        {
            debugText += $"Gravity Sensor: " +
                         $"{GravitySensor.current.gravity.ReadValue()}\n";
        }
        else
        {
            debugText += "GravitySensor.current = NULL\n";
        }

        if (LinearAccelerationSensor.current != null)
        {
            debugText += $"Linear Acceleration: " +
                         $"{LinearAccelerationSensor.current.acceleration.ReadValue()}\n";
        }
        else
        {
            debugText += "LinearAccelerationSensor.current = NULL\n";
        }

        debugText += "\n";
#endif

        //-----------------------------------
        // TOUCH INFO
        //-----------------------------------
        debugText += "========== TOUCH ==========\n";
        debugText += $"Touch Count: {Input.touchCount}\n";

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            debugText +=
                $"Touch {i} | " +
                $"Position: {t.position} | " +
                $"Phase: {t.phase} | " +
                $"FingerId: {t.fingerId}\n";
        }

        debugText += "\n";

        //-----------------------------------
        // WEBGL NOTE
        //-----------------------------------
        debugText += "========== WEBGL NOTE ==========\n";
        debugText += "If gyro values are zero:\n";
        debugText += "- Use HTTPS\n";
        debugText += "- Allow motion permission\n";
        debugText += "- Tap screen first\n";
        debugText += "- Browser may block sensors\n";
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 22;
        GUI.skin.button.fontSize = 22;

        GUILayout.BeginArea(
            new Rect(
                10,
                10,
                Screen.width - 20,
                Screen.height - 20
            )
        );

        GUILayout.BeginVertical("box");

        scrollPos = GUILayout.BeginScrollView(
            scrollPos,
            GUILayout.Width(Screen.width - 40),
            GUILayout.Height(Screen.height - 120)
        );

        GUILayout.Label(debugText);

        GUILayout.EndScrollView();

        if (GUILayout.Button("Refresh Debug Data", GUILayout.Height(60)))
        {
            BuildDebugText();
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}