using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AddFade : MonoBehaviour
{
    private const float Z_OFFSET = 0.001f;

    void Start()
    {
        GameObject shadow = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Material shadowMaterial = new Material(Shader.Find(StandardShaderUtils.STANDARD_SHADER));
        shadow.transform.SetParent(transform);
        shadow.transform.position = transform.position;
        shadow.transform.localPosition = new Vector3(
            shadow.transform.localPosition.x,
            shadow.transform.localPosition.y,
            shadow.transform.localPosition.z + Z_OFFSET
        );
        shadow.transform.rotation = transform.rotation;
        shadow.transform.localScale = Vector3.one;
        shadowMaterial.CopyPropertiesFromMaterial(GetComponent<Renderer>().material);
        StandardShaderUtils.ChangeRenderMode(shadowMaterial, StandardShaderUtils.BlendMode.Fade);
        shadow.GetComponent<Renderer>().material = shadowMaterial;
        shadow.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
        shadow.GetComponent<MeshCollider>().enabled = false;
    }
}
