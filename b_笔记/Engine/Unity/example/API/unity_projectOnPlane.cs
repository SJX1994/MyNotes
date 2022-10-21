using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// Vector3.ProjectOnPlane - example

// Generate a random plane in xy. Show the position of a random
// vector and a connection to the plane. The example shows nothing
// in the Game view but uses Update(). The script reference example
// uses Gizmos to show the positions and axes in the Scene.

public class unity_projectOnPlane : MonoBehaviour
{
      void Update()
      {
            project();
      }

      //方向向量目标点 看向点
      public Transform target, looker;
      //两点确定法线方向
      public Transform n1, n2;
      //跟随点
      public Transform sign;

      void project()
      {
            looker.transform.LookAt(target);

            //指向目标的向量
            var dir = target.position - transform.position;
            //平行于X轴平面的法向量 Y轴正方向向量 （1,0,0）
            var normal = n1.position - n2.position;
            //投影向量
            var pj = Vector3.ProjectOnPlane(dir, normal);
            //相对本对象位置进行变化
            sign.position = transform.position + pj;

            //绘制方向
            Debug.DrawLine(transform.position, target.position);
            //绘制投影
            Debug.DrawLine(transform.position, sign.position, Color.blue);
            //绘制法线
            Debug.DrawLine(target.position, sign.position, Color.red);
      }

      //绘制平面
      private void OnDrawGizmos()
      {
            //平行于X轴平面的法向量 Y轴正方向向量 （1,0,0）
            var dir = n1.position - n2.position;

            Handles.color = Color.yellow;
            for (int i = 0; i < 10; i++)
            {
                  var dis = i + 1;
                  Handles.DrawWireDisc(transform.position, dir, dis * 0.1f);
            }
      }

}