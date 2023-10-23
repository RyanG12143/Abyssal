using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconSonar : MonoBehaviour
{

    private bool isPointing = false;
    
    private GameObject _Beacon;

    public GameObject _Nightingale;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPointing){
            Vector3 target = new Vector3(_Beacon.transform.position.x, _Beacon.transform.position.y , 0f);
        
            Vector3 objectPos = _Nightingale.transform.position;
            target.x = target.x - objectPos.x;
            target.y = target.y - objectPos.y;

            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

           // float newPosx = (_Nightingale.transform.position.x + (_Beacon.transform.position.x - _Nightingale.transform.position.x) / 1000) + 1;
           // float newPosy = (_Nightingale.transform.position.y + (_Beacon.transform.position.y - _Nightingale.transform.position.y) / 1000) + 1;

           // Vector2 newPos = new Vector2(newPosx, newPosy);

        Vector2 directionAtoB = _Nightingale.transform.position - _Beacon.transform.position;
        Vector2 midpointAtoB = new Vector3(((_Nightingale.transform.position.x+_Beacon.transform.position.x)/2f), ((_Nightingale.transform.position.y+_Beacon.transform.position.y)/2f)); // midpoint between A B
        transform.position = Vector2.Lerp(transform.position, midpointAtoB, 10f * Time.deltaTime);

            
        }

    }

    public void StartPoint(GameObject Beacon){
        _Beacon = Beacon;
        StartCoroutine(Point());

    }

    IEnumerator Point(){

        isPointing = true;

        Color current = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0f);
        Color end = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1f);

        for (int i = 0; i < 26; i++)
        {

            GetComponent<SpriteRenderer>().color = Color.Lerp(current, end, i / 26.0f);

            yield return new WaitForSeconds(.02f);
        }
            
            yield return new WaitForSeconds(3f);

        
        for (int i = 0; i < 26; i++)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(end, current, i / 26.0f);

            yield return new WaitForSeconds(.02f);
        }

        GetComponent<SpriteRenderer>().color = current;

        isPointing = false;
    }

}
