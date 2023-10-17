using UnityEngine;

public class PT30Scene : MonoBehaviour
{
    public RectTransform uiElement; // Kéo và thả đối tượng UI element vào đây trong Inspector.
    public float widthPercentage = 30.0f; // Tỷ lệ độ rộng mong muốn.

    private void Start()
    {
        ResizeElement();
    }

    private void ResizeElement()
    {
        if (uiElement == null)
        {
            Debug.LogWarning("UI Element is not assigned!");
            return;
        }
        // Lấy kích thước màn hình hiện tại
        float screenWidth = Screen.width;

        // Tính toán độ rộng mới dựa trên tỷ lệ
        float newWidth = screenWidth * (widthPercentage / 100.0f);

        // Đặt độ rộng mới cho UI element
        uiElement.sizeDelta = new Vector2(newWidth, uiElement.sizeDelta.y);

        uiElement.position = new  Vector3(newWidth / 2 + 2, uiElement.position.y, uiElement.position.z);

    }
}
