using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;

    public Vector3 GetNearestPointOnGrid(float x, float y, float z)
    {
        Vector3 pos = new Vector3(x, y, z) - transform.localPosition;

        int xCount = Mathf.RoundToInt(pos.x / size);
        int yCount = Mathf.RoundToInt(pos.y / size);
        int zCount = Mathf.RoundToInt(pos.z / size);

        Vector3 result = new Vector3(xCount * size, yCount * size, zCount * size);
        result += transform.localPosition;
        return result;
    }


    public Vector3 GetNearestPointOnGrid(Vector3 pos)
    {
        pos -= transform.localPosition;

        int xCount = Mathf.RoundToInt(pos.x / size);
        int yCount = Mathf.RoundToInt(pos.y / size);
        int zCount = Mathf.RoundToInt(pos.z / size);

        Vector3 result = new Vector3(xCount * size, yCount * size, zCount * size);
        result += transform.localPosition;
        return result;
    }

    public Vector3 GetNearestDownPointOnGrid(Vector3 pos)
    {
        pos -= transform.localPosition;

        int xCount = Mathf.FloorToInt(pos.x / size);
        int yCount = Mathf.FloorToInt(pos.y / size);
        int zCount = Mathf.FloorToInt(pos.z / size);

        Vector3 result = new Vector3(xCount * size, yCount * size, zCount * size);
        result += transform.localPosition;
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = transform.localPosition.x - 50; x < transform.localPosition.x + 50; x += size)
        {
            for (float y = transform.localPosition.y - 50; y < transform.localPosition.y + 50; y += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, y, 0));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
