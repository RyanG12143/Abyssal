using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconSonar : MonoBehaviour
{

    // Is arrow effects running?
    private bool isPointing = false;
    
    // Beacon to be pointed at
    private GameObject _Beacon;

    // Nightingale
    private GameObject _Nightingale;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {

        if (_Nightingale == null)
        {
            _Nightingale = GameObject.Find("Nightingale");
        }

        // Runs as at the same time as Point to update the arrow direction
        if (isPointing && _Beacon.GetComponent<Beacon>().currState == BeaconState.Idle){
            Vector3 target = new Vector3(_Beacon.transform.position.x, _Beacon.transform.position.y , 0f);
        
            Vector3 objectPos = _Nightingale.transform.position;
            target.x = target.x - objectPos.x;
            target.y = target.y - objectPos.y;

            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Vector3 directionAtoB = _Nightingale.transform.position - _Beacon.transform.position;

            if (((Mathf.Abs(directionAtoB.x) + Mathf.Abs(directionAtoB.y)) < 5.0f))
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }

            directionAtoB.Normalize();
            transform.position = (_Nightingale.transform.position + (directionAtoB * -1.5f));


        }

    }

    /*
    Leo Dresang
    10/30/2023
    Begins the Fade effect
    */
    public void StartPoint(GameObject Beacon){
        _Beacon = Beacon;
        StartCoroutine(Point());

    }

    /*
    Leo Dresang
    10/30/2023
    Creates the Fade in/out effect as the arrow is pointing.
    */
    IEnumerator Point(){

        isPointing = true;

        Color current = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0f);
        Color end = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1f);

        for (int i = 0; i < 30; i++)
        {
            
            GetComponent<SpriteRenderer>().color = Color.Lerp(current, end, i / 30.0f);

            yield return new WaitForSeconds(.02f);
        }
            
            yield return new WaitForSeconds(3f);

        
        for (int i = 0; i < 30; i++)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(end, current, i / 30.0f);

            yield return new WaitForSeconds(.02f);
        }

        GetComponent<SpriteRenderer>().color = current;

        isPointing = false;
    }

}
