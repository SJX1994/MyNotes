using System.Collections;
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
// reference: 
// https://github.com/lchaumartin/SpiderProceduralAnimation.git
public class new_ProceduralAnimation : MonoBehaviour
{

      public List<Transform> legTargets;
      public in_ProceduralAnimation in_PA;
      private Vector3 Test;
      void Start()
      {

            in_PA.legTargets = legTargets;
            in_PA.lastBodyUp = transform.up;
            in_PA.defaultLegPositions = new Vector3[in_PA.legTargets.Count];
            in_PA.lastLegPositions = new Vector3[in_PA.legTargets.Count];
            in_PA.smoothness = 8;
            in_PA.bodyOrientation = true;
            in_PA.raycastRange = 1f;
            in_PA.stepHeight = 0.15f;
            in_PA.stepSize = 0.15f;
            in_PA.velocityMultiplier = 15f;
            in_PA.init = false;
            sjx_ProceduraAnimation PA = new sjx_ProceduraAnimation(in_PA);
            PA.ProceduraAnimation_Init(out in_PA);
            in_PA.lastBodyPos = transform.position;
      }

      void FixedUpdate() // 不受游戏帧率影响
      {
            in_PA.velocity = transform.position - in_PA.lastBodyPos;
            in_PA.velocity = (in_PA.velocity + in_PA.smoothness * in_PA.lastVelocity) / (in_PA.smoothness + 1f);
            // 最大范围之间的距离
            if (in_PA.velocity.magnitude < float.MinValue)//0.000_025f)
                  in_PA.velocity = in_PA.lastVelocity;
            else
                  in_PA.lastVelocity = in_PA.velocity;

            // 移动方向
            Vector3[] desiredPositions = new Vector3[in_PA.legTargets.Count];
            int indexToMove = -1;
            float maxDistance = in_PA.stepSize;
            for (int i = 0; i < in_PA.legTargets.Count; ++i)
            {
                  desiredPositions[i] = transform.TransformPoint(in_PA.defaultLegPositions[i]);

                  var pj = Vector3.ProjectOnPlane(desiredPositions[i] + in_PA.velocity * in_PA.velocityMultiplier - in_PA.lastLegPositions[i], transform.up);

                  float distance = Vector3.ProjectOnPlane(desiredPositions[i] + in_PA.velocity * in_PA.velocityMultiplier - in_PA.lastLegPositions[i], transform.up).magnitude;
                  if (distance > maxDistance)
                  {
                        maxDistance = distance;
                        indexToMove = i;
                  }
                  // 绘制法线
                  Debug.DrawLine(desiredPositions[i], desiredPositions[i] + pj, Color.red, 0.1f);
            }
            for (int i = 0; i < in_PA.legTargets.Count; ++i)
                  if (i != indexToMove)
                        legTargets[i].position = in_PA.lastLegPositions[i];

            // 最后一个移动脚
            if (indexToMove != -1 && !in_PA.legMoving[0])
            {
                  Vector3 targetPoint = desiredPositions[indexToMove] + Mathf.Clamp(in_PA.velocity.magnitude * in_PA.velocityMultiplier, 0.0f, 1.5f) * (desiredPositions[indexToMove] - legTargets[indexToMove].position) + in_PA.velocity * in_PA.velocityMultiplier;
                  Vector3[] positionAndNormal = MatchToSurfaceFromAbove(targetPoint, in_PA.raycastRange, transform.up);
                  in_PA.legMoving[0] = true;
                  StartCoroutine(PerformStep(indexToMove, positionAndNormal[0]));
                  Test = positionAndNormal[0];
            }
            // 身体摇晃
            in_PA.lastBodyPos = transform.position;
            if (in_PA.legTargets.Count > 3 && in_PA.bodyOrientation)
            {
                  Vector3 v1 = legTargets[0].position - legTargets[1].position;
                  Vector3 v2 = legTargets[2].position - legTargets[3].position;
                  Vector3 normal = Vector3.Cross(v1, v2).normalized;
                  Vector3 up = Vector3.Lerp(in_PA.lastBodyUp, normal, 1f / (float)(in_PA.smoothness + 1));
                  transform.up = up;
                  in_PA.lastBodyUp = up;
                  Debug.DrawLine(transform.position, transform.position + up, Color.white, 0.1f);
            }

            sjx_ProceduraAnimation PA = new sjx_ProceduraAnimation(in_PA);
            PA.ProceduralAnimation_Update(out in_PA);
      }
      IEnumerator PerformStep(int index, Vector3 targetPoint)
      {
            Vector3 startPos = in_PA.lastLegPositions[index];
            for (int i = 1; i <= in_PA.smoothness; ++i)
            {
                  legTargets[index].position = Vector3.Lerp(startPos, targetPoint, i / (float)(in_PA.smoothness + 1f));
                  legTargets[index].position += transform.up * Mathf.Sin(i / (float)(in_PA.smoothness + 1f) * Mathf.PI) * in_PA.stepHeight;
                  yield return new WaitForFixedUpdate();
            }
            legTargets[index].position = targetPoint;
            in_PA.lastLegPositions[index] = legTargets[index].position;
            in_PA.legMoving[0] = false;
      }
      static Vector3[] MatchToSurfaceFromAbove(Vector3 point, float halfRange, Vector3 up)
      {
            Vector3[] res = new Vector3[2];
            RaycastHit hit;
            Ray ray = new Ray(point + halfRange * up, -up);

            if (Physics.Raycast(ray, out hit, 2f * halfRange))
            {
                  res[0] = hit.point;
                  res[1] = hit.normal;
            }
            else
            {
                  res[0] = point;
            }
            return res;
      }
      [ExecuteInEditMode]
      private void OnDrawGizmosSelected()
      {
            if (!Application.isPlaying)
            {
                  in_PA.legTargets = legTargets;
                  in_PA.lastBodyUp = transform.up;
                  in_PA.defaultLegPositions = new Vector3[in_PA.legTargets.Count];
                  in_PA.lastLegPositions = new Vector3[in_PA.legTargets.Count];
                  in_PA.smoothness = 8;
                  in_PA.bodyOrientation = true;
                  in_PA.raycastRange = 1f;
                  in_PA.stepHeight = 0.15f;
                  in_PA.stepSize = 0.15f;
                  in_PA.velocityMultiplier = 15f;
                  in_PA.init = false;
            }


            if (in_PA.legTargets.Count > 0)
            {
                  for (int i = 0; i < in_PA.legTargets.Count; ++i)
                  {
                        Gizmos.color = Color.red;
                        Gizmos.DrawWireSphere(legTargets[i].position, 0.05f);
                        Gizmos.color = Color.green;
                        Gizmos.DrawWireSphere(transform.TransformPoint(in_PA.defaultLegPositions[i]), in_PA.stepSize);
                  }
            }
            // 最后一个移动的脚
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Test, 0.3f);

      }
}
public struct in_ProceduralAnimation
{
      public List<UnityEngine.Transform> legTargets;
      public float stepSize;
      public int smoothness;
      public float stepHeight;
      public bool bodyOrientation;
      public Vector3[] defaultLegPositions;
      public Vector3 lastBodyUp;
      public Vector3[] lastLegPositions;

      public bool[] legMoving;
      public bool init;
      public Vector3 velocity;
      public Vector3 lastVelocity;
      public Vector3 lastBodyPos;
      public float velocityMultiplier;
      public float raycastRange;

};
public class sjx_ProceduraAnimation
{
      public in_ProceduralAnimation proceduralAnimation;
      public sjx_ProceduraAnimation(in_ProceduralAnimation proceduralAnimation) => ProceduralAnimation = proceduralAnimation;

      public in_ProceduralAnimation ProceduralAnimation
      {
            get => proceduralAnimation;
            set => proceduralAnimation = value;

      }
      public void ProceduraAnimation_Init(out in_ProceduralAnimation in_PA)
      {
            in_PA = proceduralAnimation;
            in_PA.legMoving = new bool[proceduralAnimation.legTargets.Count];
            for (int i = 0; i < proceduralAnimation.legTargets.Count; ++i)
            {
                  in_PA.defaultLegPositions[i] = in_PA.legTargets[i].localPosition;
                  in_PA.lastLegPositions[i] = in_PA.legTargets[i].position;
                  in_PA.legMoving[i] = false;
            }
            in_PA.init = true;
      }
      public void ProceduralAnimation_Update(out in_ProceduralAnimation in_PA)
      {
            in_PA = proceduralAnimation;
      }
      ~sjx_ProceduraAnimation()
      {
            if (!proceduralAnimation.init)
            {
                  Debug.LogWarning("sjx_ProceduraAnimation_Init");
            }
            else
            {
                  Debug.Log("sjx_ProceduraAnimation_Updateing");
            }
      } // 析构函数
}