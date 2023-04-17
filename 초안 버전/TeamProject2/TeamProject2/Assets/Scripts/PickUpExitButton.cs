using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpExitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
