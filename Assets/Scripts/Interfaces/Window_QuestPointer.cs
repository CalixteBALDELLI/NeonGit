using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class QuestPointer : MonoBehaviour
{
    [Header("Cible à suivre")]
    [SerializeField] bool isAZoneBoss;
    public Transform target;

    [Header("Marge du bord d'écran (en pixels)")]
    public float edgePadding = 50f;

    float                            currentDistance;
    private          RectTransform   pointerRectTransform;
    private          Camera          mainCamera;
    private          Canvas          canvas;
    [SerializeField] Image           image;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] bool            isArrow;
    [SerializeField] bool            isTeleporterArrow;
    [SerializeField] Sprite          teleporterIcon;
    [SerializeField] Sprite          bossIcon;
    void Awake()
    {
        if (isAZoneBoss)
        {
            target = GameObject.FindGameObjectWithTag("ZoneBoss").transform;
            if (isArrow == false)
            {
                image.sprite = bossIcon;
            }
        }

        if (isTeleporterArrow)
        {
            target = GameObject.FindGameObjectWithTag("Teleporter").transform;
            if (isArrow == false)
            {
                image.sprite  = teleporterIcon;
            }
        }
        pointerRectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
        canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
        currentDistance   = (target.position - PlayerStats.SINGLETON.playerTransform.transform.position).magnitude;
        distanceText.text = currentDistance.ToString("f0") + "m";
        bool isOffScreen =
            screenPos.x < 0 || screenPos.x > Screen.width ||
            screenPos.y < 0 || screenPos.y > Screen.height;
            
        // Détection apparition dans l'écran
        if (!isOffScreen)
        {
            isOffScreen = true;
            //Debug.Log("La cible est visible");
            image.enabled        = false;
            distanceText.enabled = false;

            // Fait disparaitre la fleche tant que la target est dans l'ecran
        }
        else if (isOffScreen)
        {
            //Debug.Log("La cible n'est pas visible");
            image.enabled = true;
            distanceText.enabled = true;
            // Fait aparaitre la fleche tant que la target n'est pas dans l'ecran
        }
        // Met une marge a de x au bord de l'ecran
        screenPos.x = Mathf.Clamp(screenPos.x, edgePadding, Screen.width - edgePadding);
        screenPos.y = Mathf.Clamp(screenPos.y, edgePadding, Screen.height - edgePadding);
        screenPos.z = 0f;

        pointerRectTransform.position = screenPos;

        // Calcul direction (vers la cible dans le monde)
        Vector3 toTarget = (target.position - mainCamera.transform.position).normalized;
        
        if (isArrow)
        {
            float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
            pointerRectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}