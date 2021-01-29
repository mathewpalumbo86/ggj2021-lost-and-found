using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingGun : PlayerTool
{

    [SerializeField]
    float range, forceOffset, force;

    [SerializeField]
    LayerMask sand;

    [SerializeField]
    bool shoot;

    private void Update()
    {
        if (shoot)
        {
            GetDigPoint();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            shoot = !shoot;
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
    
}
