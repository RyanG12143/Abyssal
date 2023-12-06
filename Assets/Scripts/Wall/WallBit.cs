using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x + Random.Range(-0.8f, 0.8f), transform.position.y + Random.Range(-0.8f, 0.8f), transform.position.z);
        float scaleRandom = Random.Range(0.8f, 1.2f);
        transform.localScale = new Vector3(scaleRandom, scaleRandom, scaleRandom);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + Random.Range(-100f, 100f));
        StartCoroutine(Delete());
    }

    // Update is called once per frame
    IEnumerator Delete()
    {
        for (int i = 0; i < 10; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.1f);

            yield return new WaitForSeconds(0.08f);
        }
        Destroy(gameObject);
    }
}
