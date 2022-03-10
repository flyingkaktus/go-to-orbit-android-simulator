using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    [Range(0.1f, 20)]
    public float time_const = 1;
    [Range(-0.1f, -20)]
    public float gravity_const = -6.674f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = time_const;
    }
}
