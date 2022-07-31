using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandHelper : MonoBehaviour
{
      public Material material;
      public float speed = 0.25f;

      private float time = 1.0f;

      void Start()
      {
            material = GetComponent<Renderer>().material;
      }
      void Update()
      {
            time -= speed * Time.deltaTime * transform.up.y;
            time = Mathf.Clamp01(time);
            material.SetFloat("_Amount", time);
            material.SetFloat("_Width", speed * 100.0f);
      }
}
