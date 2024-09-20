using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool oneWay;
    RoadGenerator rg;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        rg = target.GetComponent<RoadGenerator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!oneWay)
        {
            transform.position = target.position + target.rotation * offset;
            transform.rotation = target.rotation;
        }
        else if (rg != null && rg.directionOfMovement.z > 0.1f)
        {
            transform.position = target.position + offset;
        }
    }
}
