using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowScript : MonoBehaviour
{
    public Renderer r;
    public int m = 20;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        r.material.SetTextureOffset("_MainTex", new Vector2(Time.time*m, 0));
    }
}
