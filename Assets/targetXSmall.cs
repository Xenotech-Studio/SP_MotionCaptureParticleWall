using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetXSmall : MonoBehaviour
{
    public List<GameObject> targetPt;
    public GameObject selfObject;
    public Vector3 initialScale;
    public float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = selfObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var VARIABLE in targetPt)
        {
            if (Mathf.Abs(VARIABLE.transform.position.x-selfObject.transform.position.x) < maxDistance)
            {
                initialScale = selfObject.transform.localScale;
                selfObject.transform.localScale =Mathf.Abs(VARIABLE.transform.position.x- selfObject.transform.position.x)/maxDistance*new Vector3(1,1f,1);
                selfObject.transform.localScale+=new Vector3(0,initialScale.y-selfObject.transform.localScale.y,initialScale.z-selfObject.transform.localScale.z);
                return;
            }
            else
            {
                selfObject.transform.localScale = new Vector3(1,1f,1);
            }
        }
        
    }
}
