using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    int numberOfObjects;

    [SerializeField]
    int pool = 5;

    [SerializeField]
    float buryDistance;

    [SerializeField]
    Transform startSquare, endSquare;

    [SerializeField]
    float yPosition;

    [SerializeField]
    LayerMask sand;

    [SerializeField]
    GameObject[] scrapPrefabs;

    [SerializeField]
    string seed;

    System.Random rand;

    Coroutine spawner;

    int respawn;

    private void Start()
    {
        rand = string.IsNullOrEmpty(seed) ? new System.Random() : new System.Random(seed.GetHashCode());

        spawner = StartCoroutine(SpawnObjects(numberOfObjects));
    }

    IEnumerator SpawnObjects(int count)
    {
        respawn = 0;
        for (int i = 0; i < count; i++)
        {
            if (i % pool == 0)
                yield return new WaitForEndOfFrame();

            Vector3 point = RayCastDown(RandomVector(startSquare.position, endSquare.position), buryDistance);
            if (point == Vector3.zero)
                continue;
            Quaternion angle = Quaternion.Euler(RandomVector(Vector3.zero, Vector3.one * 360));
            int scrap = rand.Next(0, scrapPrefabs.Length - 1);
            GeneratePrefabs(point, angle, scrap);
        }

        if(respawn != 0)
            StartCoroutine(SpawnObjects(respawn));
    }

    Vector3 RayCastDown(Vector3 startRay, float buryDistance)
    {
        Vector3 point = new Vector3();
        float height = yPosition - startRay.y;
        startRay.y = yPosition;
        RaycastHit hit;
        if (Physics.Raycast(startRay, Vector3.down, out hit, height + 2, sand))
        {
            if (hit.collider.tag == "Water" || hit.collider.tag == "Rock")
            {
                point = Vector3.zero;
                respawn++;
            }
            else
            {
                point = hit.point;
                point.y -= buryDistance;
            }
        }

        return point;
    }

    

    void GeneratePrefabs(Vector3 position, Quaternion angle, int prefabNumber)
    {
        var go = Instantiate(scrapPrefabs[prefabNumber]);
        go.transform.position = position;
        go.transform.rotation = angle;
        go.transform.parent = transform;
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

    private void OnDrawGizmos()
    {
        Bounds bounds = new Bounds(startSquare.position, Vector3.zero);
        bounds.Encapsulate(endSquare.position);
        bounds.Encapsulate(new Vector3(startSquare.position.x, yPosition, startSquare.position.z));
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
