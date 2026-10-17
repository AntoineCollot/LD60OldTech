using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] Renderer holesRend;
    Transform targetPosition;

    Vector3 refPos;
    Quaternion refRot;
    const float SMOOTH = 0.1f;

    static readonly int HOLE_TEX_PROPERTY = Shader.PropertyToID("_HoleTex");

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition.position, ref refPos, SMOOTH);
        transform.rotation = QuaternionUtils.SmoothDamp(transform.rotation, targetPosition.rotation, ref refRot, SMOOTH);
    }

    public void SetData(CardData data)
    {
        text.text = data.description;
        holesRend.material.SetTexture(HOLE_TEX_PROPERTY, data.texture);
    }

    public void SetPosition(Transform target)
    {
        targetPosition = target;
    }
}
