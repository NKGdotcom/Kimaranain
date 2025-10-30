using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class SinkWater : MonoBehaviour
{
    [Header("水の範囲")]
    [SerializeField] private BoxCollider waterZone;
    [Header("水の密度")]
    [SerializeField] private float waterDensity = 1000f;
    [Header("浮力計算用の分割数")]
    [SerializeField] private int resolutionBoxCollider = 10;
    //水に入ったオブジェクト
    private HashSet<FloatingObjects> inWaterFloatingObjects = new HashSet<FloatingObjects>();

    [Space(5)] //以下デバッグ
    private bool isBuoyancyRay = false;
    private UnityEngine.Color buoyancyRayColor = UnityEngine.Color.red;
    private bool waterGizmos = false;
    private UnityEngine.Color waterBoxGizmosColor = new UnityEngine.Color(0, 0.5f, 1f, 0.4f);
    private UnityEngine.Color waterLineGizmosColor = new UnityEngine.Color(0, 0.5f, 1f, 0.4f);

    private void FixedUpdate()
    {
        foreach (FloatingObjects floatingObject in inWaterFloatingObjects)
        {
            ApplyBuoyancy(floatingObject);
        }
    }
    /// <summary>
    /// 浮力をかける
    /// </summary>
    /// <param name="_floatingObject"></param>
    private void ApplyBuoyancy(FloatingObjects _floatingObject)
    {
        //BoxColliderを分割した頂点を取得
        Vector3[] _colliderPoints = GetBoxColliderPoints(_floatingObject.BoxCollider, resolutionBoxCollider);

        //水面下の点を抽出
        float _waterSurface = waterZone.bounds.max.y;
        Vector3[] _submergedPoints = _colliderPoints.Where(p => p.y <= _waterSurface).ToArray();

        if (_submergedPoints.Length == 0) return;

        //水面下の点の平均一を取得
        Vector3 _averageSubmergedPoint = _submergedPoints.Aggregate(Vector3.zero, (sum, point) => sum + point) / _submergedPoints.Length;
       
        //沈んでいる体積の割合を計算
        float _submergedVolumeRatio = (float)_submergedPoints.Length / _colliderPoints.Length;
        
        //浮力の計算
        float _buoyancyForce = waterDensity * _floatingObject.Volume * _submergedVolumeRatio * Physics.gravity.magnitude;
       
        //平均位置に浮力を適用
        _floatingObject.Rigidbody.AddForceAtPosition(Vector3.up * _buoyancyForce, _averageSubmergedPoint, ForceMode.Force);

        if (!isBuoyancyRay) return;

        Debug.DrawRay(_averageSubmergedPoint, Vector3.up * 2f, buoyancyRayColor, Time.fixedDeltaTime);
    }

    private Vector3[] GetBoxColliderPoints(BoxCollider _boxCollider, int _resolution)
    {
        List <Vector3> points = new List<Vector3>();

        Vector3 _center = _boxCollider.center;
        Vector3 _size = _boxCollider.size;
        Transform _transform = _boxCollider.transform;

        for (int i = 0; i < _resolution; i++)
        {
            for (int j =0;j<_resolution; j++)
            {
                for(int k = 0; k<_resolution; k++)
                {
                    float x = Mathf.Lerp(-_size.x / 2, _size.x / 2, i / (_resolution - 1f));
                    float y = Mathf.Lerp(-_size.y / 2, _size.y / 2, j / (_resolution - 1f));
                    float z = Mathf.Lerp(-_size.z / 2, _size.z / 2, k / (_resolution - 1f));

                    Vector3 worldPoint = transform.TransformPoint(_center + new Vector3(x, y, z));
                    points.Add(worldPoint);
                }                
            }
        }
        return points.ToArray();
    }
    private void OnDrawGizmos()
    {
        if (!waterGizmos) return;

        // Transformを考慮
        Gizmos.matrix = transform.localToWorldMatrix;

        // 水の範囲のBoxを描画
        Gizmos.color = waterBoxGizmosColor;
        Gizmos.DrawCube(waterZone.center, waterZone.size);

        // 水の範囲のLineを描画
        Gizmos.color = waterLineGizmosColor;
        Gizmos.DrawWireCube(waterZone.center, waterZone.size);

        // 考慮したTransformを元に戻す
        Gizmos.matrix = Matrix4x4.identity;
    }
    private void OnTriggerEnter(Collider other)
    {
        FloatingObjects _floatingObject = other.GetComponent<FloatingObjects>();

        if(_floatingObject != null)
        {
            _floatingObject.SetDamping(true);
            inWaterFloatingObjects.Add(_floatingObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        FloatingObjects _floatingObject = other.GetComponent<FloatingObjects>();

        if(_floatingObject != null)
        {
            _floatingObject.SetDamping(false); 
            inWaterFloatingObjects.Remove(_floatingObject);
        }
    }
}
