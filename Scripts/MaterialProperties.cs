using UnityEngine;

public class MaterialProperties : MonoBehaviour
{
    //–§“x
    public float Density { get; private set; }
    //‹ó‹C’ïR(…’†)
    public float LinearDampingInWater { get; private set; }
    //‰ñ“]’ïR(…’†)
    public float AngularDampingInWater { get; private set; }
    //‹ó‹C’ïR(‹ó’†)
    public float LinearDampingInAir { get; private set; }
    //‰ñ“]’ïR(‹ó’†)
    public float AngularDampingInAir { get; private set; }

    private MaterialProperties(float _density, float _linearDampingInWater, float _angularDampingInWater, float _linearDampingInAir, float _angularDampingInAir)
    {
        Density = _density;
        LinearDampingInWater = _linearDampingInWater;
        AngularDampingInWater = _angularDampingInWater;
        LinearDampingInAir = _linearDampingInAir;
        AngularDampingInAir = _angularDampingInAir;
    }

    public static MaterialProperties GetProperties(MaterialType _materialType)
    {
        switch(_materialType)
        {
            case MaterialType.Add:
                return new MaterialProperties(1200f, 3.0f, 2.5f, 0.3f, 0.3f);
            case MaterialType.Normal:
                return new MaterialProperties(500f, 4.0f, 2.0f, 0.5f, 0.2f);
            default:
                return new MaterialProperties(1000f, 3.0f, 2.5f, 0.3f, 0.3f);
        }
    }
}
