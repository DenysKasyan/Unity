using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class NumberPopup0 : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI numberTextPrefab;
    public float animationDuration = 1f;
    public Canvas canvas;

    private GRNupgrade grnUpgrade;

    private void Start()
    {
        grnUpgrade = FindObjectOfType<GRNupgrade>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (grnUpgrade == null) return;

        int coinsPerClick = GRNupgrade.rate + GRNupgrade.skinBonus; 

        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, canvas.worldCamera, out localPosition);

        TextMeshProUGUI newNumberText = Instantiate(numberTextPrefab, canvas.transform);
        newNumberText.rectTransform.anchoredPosition = localPosition;
        newNumberText.gameObject.SetActive(true);

        newNumberText.text = $"+{coinsPerClick}";

        newNumberText.raycastTarget = false;

        LeanTween.moveLocalY(newNumberText.gameObject, localPosition.y + 100, animationDuration)
            .setEase(LeanTweenType.easeOutCirc)
            .setOnComplete(() =>
            {
                Destroy(newNumberText.gameObject);
            });
    }
}
