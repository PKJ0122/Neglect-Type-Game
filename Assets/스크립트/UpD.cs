using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 1f * Time.deltaTime, 0);
    }
}
