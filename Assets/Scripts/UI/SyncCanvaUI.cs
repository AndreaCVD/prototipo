using UnityEngine;
using UnityEngine.UIElements;

public class SyncCanvaUI : MonoBehaviour
{
    [SerializeField] UIDocument uiDocument;
    [SerializeField] RectTransform prota;
    [SerializeField] RectTransform enemigo;

    VisualElement playerSprite;
    VisualElement enemySprite;

    void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        playerSprite = root.Q<VisualElement>("player-sprite");
        enemySprite = root.Q<VisualElement>("enemy-sprite");
    }

    void LateUpdate()
    {
        MoveToElement(prota, playerSprite);
        MoveToElement(enemigo, enemySprite);
    }

    void MoveToElement(RectTransform target, VisualElement ve)
    {
        // Obtener bounds del VisualElement en coordenadas de pantalla
        var bounds = ve.worldBound;
        Vector2 center = new Vector2(
            bounds.x + bounds.width * 0.5f,
            bounds.y + bounds.height * 0.5f
        );

        // UI Toolkit tiene Y invertida respecto a Screen
        center.y = Screen.height - center.y;

        target.position = center;
    }

}
