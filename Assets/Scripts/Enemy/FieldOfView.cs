using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    private float fov;
    private float viewDistance;
    private Vector3 origin = Vector3.zero;
    private float startingAngle;

    private void Start()
    {
        //Create a new mesh for the raycast
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        //The smoothness of the FOV cone (the "quality")
        int rayCount = 50;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex; 
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                //No hit, vertex will be the length of the view distance
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //Hit object, vertex will stop at the point where it hit the object
                vertex = raycastHit2D.point;
            }
            
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            
            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        //Makes it so the fov doesn't dissapear when moving away form the origin
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        //angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    //Used by enemy script so the FOV cone follows the enemy
    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    //Used by enemy script so the FOV cone aims in the correct direction
    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }

        return n;
    }

    //Called in enemy start method
    public void SetFOV(float fov)
    {
        this.fov = fov;
    }

    //Called in enemy start method
    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }

    //Called when enemy dies, so the FOV cone also gets destroyed
    public void DestroyFOV()
    {
        Destroy(gameObject);
    }
}
