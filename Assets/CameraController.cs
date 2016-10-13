using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float speed = 100f;

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 pos = Input.mousePosition;
            Vector2 offset = pos - Center();
            Rotate(offset);
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
        gameObject.transform.Rotate(axis, Time.deltaTime * speed * (offset.magnitude / Center().magnitude));
    }
}