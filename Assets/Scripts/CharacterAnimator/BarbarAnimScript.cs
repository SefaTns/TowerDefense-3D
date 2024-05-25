using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarAnimScript : MonoBehaviour
{
    private Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        anim.Play();
    }
}
