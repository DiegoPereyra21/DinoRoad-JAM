using UnityEngine;
public class HideControls : MonoBehaviour
{
    [SerializeField] private float visibleTime = 5f;
    void Start()
    {
        Invoke(nameof(Hide), visibleTime);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}