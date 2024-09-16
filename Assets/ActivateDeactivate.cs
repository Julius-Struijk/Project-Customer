using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDeactivate : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject objectOnAndOff;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            objectOnAndOff.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            objectOnAndOff.SetActive(true);
        }
        
    }
}
