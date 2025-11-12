using System;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialAnimator_VFX_1 : MonoBehaviour
{
    public enum PropertyType
    {
        Float,
        Color
    }
    
    [Serializable]
    public struct MaterialProperty
    {
        public string name;
        public PropertyType type;
        public AnimationCurve floatValue;
        public Color colorValue;
    }

    public MaterialProperty[] properties;
    public Material mat;
    public float normalizedTime;
    
    private void Update()
    {
        foreach (MaterialProperty materialProperty in properties)
        {
            switch (materialProperty.type)
            {
                case PropertyType.Float:
                    mat.SetFloat(materialProperty.name, materialProperty.floatValue.Evaluate(normalizedTime));
                    break;
                case PropertyType.Color:
                    mat.SetColor(materialProperty.name, materialProperty.colorValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
