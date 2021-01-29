using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
    Mesh deformingMesh;
    Vector3[] originalVertices, displacedVertices, vertexVelocities;

    [SerializeField]
    float maxDeformation, dampAmount;

    float uniformScale = 1;
    [SerializeField]
    private float falloff;

    bool deformationOngoing, allFinished;

    private void Start()
    {
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        vertexVelocities = new Vector3[originalVertices.Length];
        originalVertices.CopyTo(displacedVertices, 0);
    }

    private void Update()
    {
        if (!deformationOngoing)
            return;
        uniformScale = transform.localScale.x;

        allFinished = true;
        for(int i = 0; i < displacedVertices.Length; i++)
        {
            UpdateVertex(i);
        }
        if (allFinished)
            deformationOngoing = false;

        deformingMesh.vertices = displacedVertices;
        deformingMesh.RecalculateNormals();
    }

    private void UpdateVertex(int i)
    {
        Vector3 velocity = vertexVelocities[i];
        Vector3 displacedVertice = displacedVertices[i] + velocity * Time.deltaTime;
        Vector3 maxDeformations = originalVertices[i] - Vector3.one * maxDeformation;
        displacedVertices[i] = VectorClamp(displacedVertice, maxDeformations, originalVertices[i]);
        velocity *= 1f - dampAmount * Time.deltaTime;
        vertexVelocities[i] = velocity;

        if (velocity != Vector3.zero)
            allFinished = false;
    }

    Vector3 VectorClamp(Vector3 value, Vector3 min, Vector3 max)
    {
        for(int i = 0; i < 3; i++)
        {
            value[i] = Mathf.Clamp(value[i], min[i], max[i]);
        }
        return value;
    }

    public void AddDeformingForce(Vector3 point, float force)
    {
        deformationOngoing = true;
        point = transform.InverseTransformPoint(point);
        for(int i = 0; i < displacedVertices.Length; i++)
        {
            AddForceToVertex(i, point, force);
        }
    }

    private void AddForceToVertex(int i, Vector3 point, float force)
    {
        Vector3 pointToVertex = displacedVertices[i] - point;
        pointToVertex *= uniformScale;
        float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude * falloff);
        if (attenuatedForce < 2)
            attenuatedForce = 0;
        float velocity = attenuatedForce * Time.deltaTime;
        vertexVelocities[i] += pointToVertex.normalized * velocity;
    }
}
