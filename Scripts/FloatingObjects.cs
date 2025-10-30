using UnityEngine;

public class FloatingObjects : MonoBehaviour
{
    [Header("材質の種類")]
    [SerializeField] private MaterialType materialType;

    private Rigidbody rigidbody;
    private BoxCollider boxCollider;
    //材質のプロパティ
    private MaterialProperties materialProperties;
    //体積
    private float volume;

    public Rigidbody Rigidbody { get => rigidbody; }
    public BoxCollider BoxCollider { get => boxCollider; }
    public float Volume { get => volume; }
    private void Start()
    {
        InitSetting();
    }
    /// <summary>
    /// 初期設定
    /// </summary>
    private void InitSetting()
    {
        //情報の取得
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

        materialProperties = MaterialProperties.GetProperties(materialType);
        volume = boxCollider.size.x * boxCollider.size.y * boxCollider.size.z;
        rigidbody.mass = volume * materialProperties.Density;
        rigidbody.linearDamping = materialProperties.LinearDampingInAir;
        rigidbody.angularDamping = materialProperties.AngularDampingInAir;
    }
    /// <summary>
    /// 抵抗の設定
    /// </summary>
    /// <param name="_isInWater"></param>
    public void SetDamping(bool _isInWater)
    {
        if (_isInWater)
        {
            rigidbody.linearDamping = materialProperties.LinearDampingInWater;
            rigidbody.angularDamping = materialProperties.AngularDampingInWater;
        }
        else
        {
            rigidbody.linearDamping = materialProperties.LinearDampingInAir;
            rigidbody.angularDamping = materialProperties.AngularDampingInAir;
        }
    }
    /// <summary>
    /// 重さを追加
    /// </summary>
    private void AddWeight()
    {
        materialType = MaterialType.Add;
        InitSetting();
    }
    /// <summary>
    /// 重さがなくなる
    /// </summary>
    public void RemoveWeight()
    {
        materialType = MaterialType.Normal;
        InitSetting();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("weight"))
        {
            Debug.Log("OK");
            AddWeight();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("weight"))
        {
            Debug.Log("Remove");
            RemoveWeight();
        }
    }
}
public enum MaterialType
{
    Add, Normal
}
