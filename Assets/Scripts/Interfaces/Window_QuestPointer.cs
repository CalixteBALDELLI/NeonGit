using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class QuestPointer : MonoBehaviour
{
    [Header("Cible à suivre")]
    public Transform target;

    [Header("Marge du bord d'écran (en pixels)")]
    public float edgePadding = 50f;

    private RectTransform pointerRectTransform;
    private Camera mainCamera;
    private Canvas canvas;

    private bool hasTargetBeenSeen = false;

    void Awake()
    {
        pointerRectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null || canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            Debug.LogWarning("[QuestPointer] Le Canvas parent doit être en 'Screen Space - Overlay'.");
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
        bool isBehind = screenPos.z < 0;

        bool isOffScreen =
            screenPos.x < 0 || screenPos.x > Screen.width ||
            screenPos.y < 0 || screenPos.y > Screen.height ||
            isBehind;

        // Détection apparition dans l'écran
        if (!isOffScreen && !hasTargetBeenSeen)
        {
            hasTargetBeenSeen = true;
            Debug.Log("[QuestPointer] 🎯 La cible est maintenant visible à l'écran !");
        }

        // Afficher la flèche si la cible est hors écran ou si elle a été vue une fois
        bool shouldShowPointer = isOffScreen || hasTargetBeenSeen;

        pointerRectTransform.gameObject.SetActive(shouldShowPointer);

        if (!shouldShowPointer)
            return;

        // Si la cible est visible à l'écran, positionne la flèche au centre (ou autre position) sans la faire sortir de l'écran
        if (!isOffScreen)
        {
            // Position flèche au centre bas de l'écran (ou autre position fixe)
            pointerRectTransform.position = new Vector3(Screen.width / 2f, edgePadding, 0f);
            pointerRectTransform.rotation = Quaternion.identity; // pas de rotation
            return;
        }

        // Si hors écran, positionne la flèche clamped au bord avec la bonne rotation

        // Clamp position à l'intérieur de l'écran (avec marges)
        screenPos.x = Mathf.Clamp(screenPos.x, edgePadding, Screen.width - edgePadding);
        screenPos.y = Mathf.Clamp(screenPos.y, edgePadding, Screen.height - edgePadding);
        screenPos.z = 0f;

        pointerRectTransform.position = screenPos;

        // Calcul direction (vers la cible dans le monde)
        Vector3 toTarget = (target.position - mainCamera.transform.position).normalized;
        float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
        pointerRectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}