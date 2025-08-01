// CraftingStation.cs (SON VE DOĞRU HALİ)

using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingStation : MonoBehaviour
{
    public GameObject interactionPrompt;
    public static bool IsPlayerInRange { get; private set; }

    private void Awake()
    {
        IsPlayerInRange = false;
    }

    private void Start()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    // Bu script'in Update fonksiyonuna ihtiyacı yok.
    // Tuş dinleme işini CraftingSystem yapacak.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInRange = true;
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInRange = false;
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }
            
            // HATA BURADAYDI: CloseCraftingPanel diye bir fonksiyon yok.
            // Bunun yerine, panelleri doğrudan kapatıyoruz.
            if (WeaponCraftingSystem.Instance != null)
            {
                WeaponCraftingSystem.Instance.craftingPanel.SetActive(false);
            }
        }
    }
}