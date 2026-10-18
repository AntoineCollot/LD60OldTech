using TMPro;
using UnityEngine;

public class Card : MonoBehaviour, ISelectable
{
    [SerializeField] protected TextMeshPro text;
    [SerializeField] protected Renderer cardRend;
    [SerializeField] protected Renderer holesRend;
    protected Transform targetPosition;

    protected Vector3 refPos;
    protected Quaternion refRot;
    protected Quaternion currentRot;
    protected Vector3 rotationOffset;
    protected const float SMOOTH = 0.1f;

    bool isSelected;
    public int id { get; private set; }
    CardData data;

    //Punch positions
    //Ref from krita's grid for a 1024 texture
    const float PUNCH_GRID_SIZEX = 82f / TEXTURE_SIZE;
    const float PUNCH_GRID_SIZEY = 156f / TEXTURE_SIZE;
    const float PUNCH_OFFSETX = 19f / TEXTURE_SIZE;
    const float PUNCH_OFFSETY = 83f / TEXTURE_SIZE;
    const int TEXTURE_SIZE = 1024;

    protected static readonly int HOLE_TEX_PROPERTY = Shader.PropertyToID("_HoleTex");
    protected static readonly int EMPTY_PROPERTY = Shader.PropertyToID("_EmptyAmount");
    protected static readonly int HOVER_PROPERTY = Shader.PropertyToID("_HoverAmount");

    protected void Update()
    {
        if (targetPosition == null)
            return;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition.position, ref refPos, SMOOTH);
        currentRot = QuaternionUtils.SmoothDamp(currentRot, targetPosition.rotation * Quaternion.Euler(rotationOffset), ref refRot, SMOOTH);
        transform.rotation = currentRot;
    }

    public void SetData(CardData data, int id)
    {
        this.data = data;
        this.id = id;
        text.text = data.description;
        holesRend.material.SetTexture(HOLE_TEX_PROPERTY, data.texture);
        UpdateEmptyState();
    }

    public void SetPosition(Transform target, bool isSelected, Vector3 rotationOffset = default(Vector3))
    {
        targetPosition = target;
        this.isSelected = isSelected;
        this.rotationOffset = rotationOffset;

        UpdateEmptyState();
    }

    public void UpdateEmptyState()
    {
        if (data.isEmpty && !isSelected)
        {
            cardRend.material.SetFloat(EMPTY_PROPERTY, 1);
            holesRend.material.SetFloat(EMPTY_PROPERTY, 1);
        }
        else
        {
            cardRend.material.SetFloat(EMPTY_PROPERTY, 0);
            holesRend.material.SetFloat(EMPTY_PROPERTY, 0);
        }
    }

    #region Punch Position
    bool CanPunchAtUV(Vector2 punchedUV, out Vector2Int punchedCoords)
    {
        Vector2 punchPos = punchedUV;
        punchPos.x += PUNCH_OFFSETX;
        punchPos.x /= PUNCH_GRID_SIZEX;
        punchPos.y += PUNCH_OFFSETY;
        punchPos.y /= PUNCH_GRID_SIZEY;

        punchedCoords = new Vector2Int(Mathf.RoundToInt(punchPos.x) - 1, Mathf.RoundToInt(punchPos.y) - 1);

        //Vector2 centerPunch = new Vector2(PUNCH_OFFSETX, PUNCH_OFFSETY);
        //centerPunch.x += punchedCoords.x * PUNCH_GRID_SIZEX + PUNCH_GRID_SIZEX * 0.5f;
        //centerPunch.y += punchedCoords.y * PUNCH_GRID_SIZEY + PUNCH_GRID_SIZEY * 0.5f;

        //Vector2 dist = Vector2.zero;
        //dist.x = Mathf.Abs(centerPunch.x - punchedUV.x) / PUNCH_GRID_SIZEX;
        //dist.y = Mathf.Abs(centerPunch.y - punchedUV.y) / PUNCH_GRID_SIZEY;

        return punchedCoords.x >= 0 && punchedCoords.y >= 0 && punchedCoords.x <= 11 && punchedCoords.y <= 4;

        //Vector2 candidatePos = new Vector2(PUNCH_OFFSETY, PUNCH_OFFSETY);
        //for (int d = 0; d < CardData.DIGIT_COUNT; d++)
        //{
        //    candidatePos += 
        //    for (int c = 0; c < CardData.COLUMN_COUNT; c++)
        //    {

        //    }
        //}
    }
    #endregion

    public void OnHoverEnter()
    {
        if (!isSelected)
        {
            cardRend.material.SetFloat(HOVER_PROPERTY, 1);
            SFXManager.PlaySound(GlobalSFX.CardHover);
        }
    }

    public virtual void OnHoverStay(Vector2 uv)
    {
        if (isSelected)
        {
            if (CanPunchAtUV(uv, out Vector2Int punchedCoords))
                CardManager.Instance.HoverHole(this, punchedCoords.y, punchedCoords.x);
            else
                CardManager.Instance.ClearCurrentHover();
        }
    }

    public void OnHoverExit()
    {
        if (isSelected)
            CardManager.Instance.ClearCurrentHover();

        cardRend.material.SetFloat(HOVER_PROPERTY, 0);
    }

    public virtual void OnSelect(Vector2 uv)
    {
        if (!CardManager.Instance.IsInViewMode)
            return;

        if (!isSelected)
            CardManager.Instance.SetSelected(this);
        else
        {
            if (CanPunchAtUV(uv, out Vector2Int punchedCoords))
            {
                CardManager.Instance.PunchCard(this, punchedCoords.y, punchedCoords.x);
                UpdateEmptyState();
            }
        }

        cardRend.material.SetFloat(HOVER_PROPERTY, 0);
        UpdateEmptyState();
    }

    public virtual void OnRightClick()
    {
        CardManager.Instance.ToggleCardOrder(this);
        SFXManager.PlaySound(GlobalSFX.CardChangeOrder);
    }
}
