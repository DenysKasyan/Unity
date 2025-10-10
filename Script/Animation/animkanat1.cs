using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NumberPopup : MonoBehaviour, IPointerClickHandler
{
    public Image numberImage;
    public float animationDuration = 1f;
    public Canvas canvas;

    private void Start()
    {
        numberImage.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, canvas.worldCamera, out localPosition);

        Image newNumberImage = Instantiate(numberImage, canvas.transform);
        newNumberImage.rectTransform.anchoredPosition = localPosition;
        newNumberImage.gameObject.SetActive(true);

        newNumberImage.raycastTarget = false;

        // �������� ��������
        LeanTween.moveLocalY(newNumberImage.gameObject, localPosition.y + 100, animationDuration)
            .setEase(LeanTweenType.easeOutCirc)
            .setOnComplete(() =>
            {
                Destroy(newNumberImage.gameObject);
            });
    }
}
