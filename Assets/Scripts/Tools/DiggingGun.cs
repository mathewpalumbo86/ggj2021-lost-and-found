using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingGun : PlayerTool, IDig
{

    [SerializeField]
    float range, forceOffset, force;

    [SerializeField]
    LayerMask sand;

    [SerializeField]
    bool digging;

    private void Update()
    {
        if (digging)
        {
            GetDigPoint();
        }
        
    }

    void GetDigPoint()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, (transform.forward * range) + transform.position, Color.red, 0.1f);
        Physics.Raycast(transform.position, transform.forward, out hit, range, sand);

            MeshDeformer deformer = hit.collider?.GetComponent<MeshDeformer>();
        if (deformer)
        {
            Vector3 point = hit.point;
            point += Vector3.down * forceOffset;
            deformer.AddDeformingForce(point, force);
        }
    }

    public void Dig()
    {
        GetDigPoint();
    }

    public void DigStart()
    {
        digging = true;
    }

    public void DigEnd()
    {
        digging = false;
    }

    void DrawTexture()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, (transform.forward * range) + transform.position, Color.red, 0.1f);
        Physics.Raycast(transform.position, transform.forward, out hit, range, sand);

    }
}
