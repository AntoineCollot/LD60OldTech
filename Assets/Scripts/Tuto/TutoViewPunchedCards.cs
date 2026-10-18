using UnityEngine;

public class TutoViewPunchedCards : MonoBehaviour
{
    void Update()
    {
        if(CardManager.Instance.IsInViewMode)
            gameObject.SetActive(false);
    }
}
