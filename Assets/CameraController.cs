using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public bool tilt = false;
    public float speed = 100f;
    public GameObject pitch;

    public void ToggleTilt()
    {
        tilt = !tilt;
        ResetRotation();
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
    }

    public Vector2 Center()
    {
        Camera main = Camera.main;
        return new Vector2(main.pixelWidth / 2f, main.pixelHeight / 2f);
    }

    public void Rotate(Vector2 offset)
    {
        Vector3 axis = new Vector3(-offset.y, offset.x, 0);
        transform.Rotate(axis, Time.deltaTime * speed * (offset.magnitude / Center().magnitude));
    }

    public void RotateNoTilt(Vector2 offset)
    {
        transform.Rotate(Vector3.up, Time.deltaTime * speed * (offset.x / Center().magnitude));
        pitch.transform.Rotate(Vector3.right, Time.deltaTime * speed * (-offset.y / Center().magnitude));
    }
}