using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRise : MonoBehaviour
{
    [Tooltip ("Game units per second")]
    [SerializeField] float _waterRiseSpeed = 0.2f;

    // Update is called once per frame
    void Update()
    {
        float yValue = _waterRiseSpeed * Time.deltaTime;
        transform.Translate(new Vector2(0f, yValue));

        //transform.Translate(new Vector2(0f, _waterRiseSpeed * Time.deltaTime));
    }
}
