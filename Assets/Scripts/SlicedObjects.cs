using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedObjects : MonoBehaviour
{
   private DamageController damageController;
    /// <summary>
    ///검 시작부분
    /// </summary>
    public Transform startSlicePoint;
    /// <summary>
    /// 검 끝부분
    /// </summary>
    public Transform endSlicePoint;
    /// <summary>
    /// 
    /// </summary>
    public VelocityEstimator velocityEstimator;
    /// <summary>
    /// 자를수 있는 레이어 빨강 파랑
    /// </summary>
    public LayerMask sliceableLayer;
    /// <summary>
    /// 자른 단면 머테리얼
    /// </summary>
    [SerializeField] private Material crossSectionMaterial;
    /// <summary>
    /// 잘렸을 때 단면이 튕겨져 나가는 힘
    /// </summary>
    [SerializeField] private float cutForce = 2000f;
    public void SetDamageController(DamageController controller)
    {
        damageController = controller;
    }

    private void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }
    /// <summary>
    /// 타겟을 자르는 함수
    /// </summary>
    /// <param name="target"></param>
    public void Slice(GameObject target)
    {
        
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();
        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
             
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull, target.transform.position);
            Destroy(upperHull,2f);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull, target.transform.position);
            Destroy(lowerHull, 2f);

            Destroy(target);
            damageController.EnemyDamaged();
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject, Vector3 originalPosition)
    {
        slicedObject.transform.position = originalPosition; // Ensure the sliced object's position matches the original
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
