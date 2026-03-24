using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class AirPlane : MonoBehaviour
{
    public float enginePower = 20f;
    public float lifeBooster = 0.5f;
    public float drag = 0.01f;
    public float angularDrag = 0.01f;

    public float yawPower = 50f;
    public float pitpower = 50f;
    public float rollPower = 30f;

    private Rigidbody rb;

    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Thrust
        if (Keyboard.current.spaceKey.isPressed)
        {
            rb.AddForce(transform.forward * enginePower);
        }

        // Lift
        Vector3 lift = Vector3.Project(rb.linearVelocity, transform.forward);
        rb.AddForce(transform.up * lift.magnitude);

        //Drag
        rb.linearDamping = rb.linearVelocity.magnitude * drag;
        rb.angularDamping = rb.linearVelocity.magnitude * angularDrag;

        //Controls
        float yaw = (Keyboard.current.eKey.isPressed ? 1f : 0f) - (Keyboard.current.qKey.isPressed ? 1f : 0f);
        yaw *= yawPower;

        float pit = (Keyboard.current.sKey.isPressed ? 1f : 0f) - (Keyboard.current.wKey.isPressed ? 1f : 0f);
        pit *= pitpower;

        float roll = (Keyboard.current.aKey.isPressed ? 1f : 0f) - (Keyboard.current.dKey.isPressed ? 1f : 0f);
        roll *= rollPower;

        rb.AddTorque(transform.up * yaw);
        rb.AddTorque(transform.right * pit);
        rb.AddTorque(transform.forward * roll);
    }
}
