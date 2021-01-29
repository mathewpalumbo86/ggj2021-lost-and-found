using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    int numberOfObjects;

    [SerializeField]
    float buryDistance;

    [SerializeField]
    Vector3 startSquare, endSquare;

    [SerializeField]
    float yPosition, rayDistance;

    [SerializeField]
    LayerMask sand;

    [SerializeField]
    GameObject[] scrapPrefabs;

    [SerializeField]
    string seed;

    System.Random rand;

    private void Start()
    {
        rand = new System.Random(seed.GetHashCode());
        
        for(int i = 0; i < numberOfObjects; i++)
        {
            Vector3 point = RayCastDown(RandomVector(startSquare, endSquare), buryDistance);
            Quaternion angle = Quaternion.Euler(RandomVector(Vector3.zero, Vector3.one * 360));
            int scrap = rand.Next(0, scrapPrefabs.Length - 1);
            GeneratePrefabs(point, angle, scrap);
        }
    }

    Vector3 RayCastDown(Vector3 startPoint, float buryDistance)
    {
        startPoint.y = yPosition;
        RaycastHit hit;
        Physics.Raycast(startPoint, Vector3.down, out hit, rayDistance, sand);

        Vector3 point = hit.point;
        point.y -= buryDistance;
        return point;
    }

    

    void GeneratePrefabs(Vector3 position, Quaternion angle, int prefabNumber)
    {
        var go = Instantiate(scrapPrefabs[prefabNumber]);
        go.transform.position = position;
        go.transform.rotation = angle;
    }

    Vector3 RandomVector(Vector3 min, Vector3 max)
    {
        float xVar = max.x - min.x;
        float yVar = max.y - min.y;
        float zVar = max.z - min.z;
        float x = ((float)rand.NextDouble() * xVar) + min.x;
        float y = ((float)rand.NextDouble() * yVar) + min.y;
        float z = ((float)rand.NextDouble() * zVar) + min.z;
        return new Vector3(x, y, z);
    }
}
