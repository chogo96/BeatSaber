using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedObjects : MonoBehaviour
{
   private DamageController damageController;
    /// <summary>
    ///�� ���ۺκ�
    /// </summary>
    public Transform startSlicePoint;
    /// <summary>
    /// �� ���κ�
    /// </summary>
    public Transform endSlicePoint;
    /// <summary>
    /// 
    /// </summary>
    public VelocityEstimator velocityEstimator;
    /// <summary>
    /// �ڸ��� �ִ� ���̾� ���� �Ķ�
    /// </summary>
    public LayerMask sliceableLayer;
    /// <summary>
    /// �ڸ� �ܸ� ���׸���
    /// </summary>
    [SerializeField] private Material crossSectionMaterial;
    /// <summary>
    /// �߷��� �� �ܸ��� ƨ���� ������ ��
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
    /// Ÿ���� �ڸ��� �Լ�
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
