using System.Collections;
using UnityEngine;

public class CameraMainController : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float Speed;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public IEnumerator FixScrean(Vector3 left, Vector3 right)
    {
        float speed = Speed;
        while (cam.WorldToViewportPoint(left).x < 0)
        {
            //cam.orthographicSize = cam.orthographicSize + 0.1f;
            if (cam.orthographic)
            {
                cam.orthographicSize = cam.orthographicSize + speed;
            }
            else
            {
                cam.fieldOfView = cam.fieldOfView + speed;
            }
            speed += 0.001f;
            yield return null;
        }

        while (cam.WorldToViewportPoint(right).x > 1)
        {
            if (cam.orthographic)
            {
                cam.orthographicSize = cam.orthographicSize + speed;
            }
            else
            {
                cam.fieldOfView = cam.fieldOfView + speed;
            }
            speed += 0.005f;
            yield return null;
        }
        Debug.Log("DONE");

    }

    public IEnumerator FixScreanReverse(Vector3 left, Vector3 right)
    {
        float speed = Speed * 3;
        if (cam.orthographic)
        {
            cam.orthographicSize = 50;
        }
        else
        {
            cam.fieldOfView = 50;
        }
        while (cam.WorldToViewportPoint(left).x > 0)
        {
            //cam.orthographicSize = cam.orthographicSize + 0.1f;
            if (cam.orthographic)
            {
                cam.orthographicSize = cam.orthographicSize - speed;
            }
            else
            {
                cam.fieldOfView = cam.fieldOfView - speed;
            }
            speed += 0.0005f;
            yield return null;
        }

        while (cam.WorldToViewportPoint(right).x < 1)
        {
            if (cam.orthographic)
            {
                cam.orthographicSize = cam.orthographicSize - speed;
            }
            else
            {
                cam.fieldOfView = cam.fieldOfView - speed;
            }
            speed += 0.0005f;
            yield return null;
        }
    }

}
