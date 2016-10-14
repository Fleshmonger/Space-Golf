using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private bool grounded = true, overTheShoulder = true;
    private Vector3[] cameraPositions;

    public bool tilt = false;
    public float cameraRotateSpeed = 120f, launchSpeed = 20f;
    public Camera camera;
    public GameObject pitch;

    public void ToggleTilt()
    {
        tilt = !tilt;
        ResetRotation();
    }

    public void ToggleCameraPos()
    {
        overTheShoulder = !overTheShoulder;
        if (overTheShoulder)
        {
            camera.transform.localPosition = new Vector3(0f, 2.5f, -5f);
        }
        else
        {
            camera.transform.localPosition = Vector3.zero;
        }
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
        pitch.transform.rotation = Quaternion.identity;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleTilt();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleCameraPos();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 pos = Input.mousePosition;
            Vector2 offset = pos - Center();
            if (tilt)
            {
                Rotate(offset);
            }
            else
            {
                RotateNoTilt(offset);
            }
        }
        if (Input.GetMouseButtonDown(1) && grounded)
        {
            Vector3 direction = pitch.transform.forward;
            Rigidbody body = GetComponent<Rigidbody>();
            body.velocity = direction.normalized * launchSpeed;
            grounded = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        Rigidbody body = GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
    }

    public Vector2 Center()
    {
        Camera main = Camera.main;
        return new Vector2(main.pixelWidth / 2f, main.pixelHeight / 2f);
    }

    public void Rotate(Vector2 offset)
    {
        Vector3 axis = new Vector3(-offset.y, offset.x, 0);
        transform.Rotate(axis, Time.deltaTime * cameraRotateSpeed * (offset.magnitude / Center().magnitude));
    }

    public void RotateNoTilt(Vector2 offset)
    {
        transform.Rotate(Vector3.up, Time.deltaTime * cameraRotateSpeed * (offset.x / Center().magnitude));
        pitch.transform.Rotate(Vector3.right, Time.deltaTime * cameraRotateSpeed * (-offset.y / Center().magnitude));
    }
}