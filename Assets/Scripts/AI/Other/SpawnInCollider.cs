using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SpawnInCollider : MonoBehaviour
{
    public PolygonCollider2D polyCollider;
    public int numSpawn = 1;
    public GameObject[] toSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if (polyCollider == null) GetComponent<PolygonCollider2D>();
        if (polyCollider == null) Debug.Log("Please assign PlygonCollider2D component.");

        int i = 0;
        while (i < numSpawn)
        {
            Vector3 rndPoint3D = RandomPointInBounds(polyCollider.bounds, 1f);
            Vector2 rndPoint2D = new Vector2(rndPoint3D.x, rndPoint3D.y);
            Vector2 rndPointInside = polyCollider.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));


            if (rndPointInside.x == rndPoint2D.x && rndPointInside.y == rndPoint2D.y)
            {
                GameObject spawn = toSpawn[Random.Range(0, toSpawn.Length - 1)];
                Instantiate(spawn);
                spawn.transform.localScale = new Vector3(1f,1f,1f);
                spawn.transform.position = rndPoint2D;
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 RandomPointInBounds(Bounds bounds, float scale)
    {
        return new Vector3(
            Random.Range(bounds.min.x * scale, bounds.max.x * scale),
            Random.Range(bounds.min.y * scale, bounds.max.y * scale),
            Random.Range(bounds.min.z * scale, bounds.max.z * scale)
        );
    }
}
