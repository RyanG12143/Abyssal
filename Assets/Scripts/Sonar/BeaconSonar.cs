using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconSonar : MonoBehaviour
{

    private bool isPointing = false;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPoint(GameObject Beacon, GameObject Nightingale){
        StartCoroutine(Point());

        while(isPointing){
            Vector3 target = new Vector3(Beacon.transform.position.x, Beacon.transform.position.y , 0f);
        
            Vector3 objectPos = transform.position;
            target.x = target.x - objectPos.x;
            target.y = target.y - objectPos.y;

            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 45));

            transform.position = new Vector3(Nightingale.transform.position.x + 1f, Nightingale.transform.position.y, 0f);
        }

    }

    IEnumerator Point(){

        isPointing = true;

        Color current = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0f);
        Color end = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1f);

        for (int i = 0; i < 100; i++)
        {

            GetComponent<SpriteRenderer>().color = Color.Lerp(current, end, i / 100.0f);

            yield return new WaitForSeconds(.015f);
        }
            
            yield return new WaitForSeconds(3f);

        
        for (int i = 0; i < 100; i++)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(end, current, i / 100.0f);

            yield return new WaitForSeconds(.015f);
        }

        GetComponent<SpriteRenderer>().color = current;

        isPointing = false;
    }

}
