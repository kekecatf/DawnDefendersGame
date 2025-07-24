// CraftingSystem.cs (KARAVAN S�STEM�NE UYGUN HAL�)

using UnityEngine;
using System.Collections.Generic;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; private set; }

    [Header("Crafting Data")]
    public List<WeaponBlueprint> availableBlueprints;

    [Header("UI References")]
    public GameObject craftingPanel; // Ana panel
    public Transform blueprintsContainer; // Tariflerin g�sterilece�i UI container
    public GameObject blueprintUIPrefab; // Bir tarifi temsil eden UI prefab'�

    private PlayerStats playerStats;
    private List<BlueprintUI> blueprintUIElements = new List<BlueprintUI>();

    void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);
    }

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        InitializeCraftingPanel();
        craftingPanel.SetActive(false); // Ba�lang��ta paneli gizle
    }

    // --- Panel Kontrol Fonksiyonlar� (CraftingStation taraf�ndan �a�r�lacak) ---
    public void OpenCraftingPanel()
    {
        // 4. KONTROL: Bu fonksiyon �a�r�l�yor mu?
        Debug.Log("OpenCraftingPanel() fonksiyonu �A�RILDI.");

        if (craftingPanel != null)
        {
            craftingPanel.SetActive(true);
            // 5. KONTROL: Panel aktif edildi mi?
            Debug.Log("craftingPanel.SetActive(true) komutu �al��t�r�ld�. Panelin sahnede g�r�nmesi laz�m.");
            UpdateAllBlueprintUI();
        }
        else
        {
            // E�ER BU HATA G�R�N�RSE, SORUN BUDUR!
            Debug.LogError("HATA: CraftingSystem �zerindeki 'craftingPanel' referans� atanmam�� (null)!");
        }
    }

    public void CloseCraftingPanel()
    {
        craftingPanel.SetActive(false);
    }

    // --- Kurulum ve UI G�ncelleme ---
    private void InitializeCraftingPanel()
    {
        foreach (Transform child in blueprintsContainer) Destroy(child.gameObject);
        blueprintUIElements.Clear();

        foreach (var blueprint in availableBlueprints)
        {
            GameObject uiObject = Instantiate(blueprintUIPrefab, blueprintsContainer);
            BlueprintUI blueprintUI = uiObject.GetComponent<BlueprintUI>();

            // UI eleman�na gerekli bilgileri ve fonksiyonu ata
            blueprintUI.Setup(blueprint, () => TryCraftWeapon(blueprint));

            blueprintUIElements.Add(blueprintUI);
        }
    }

    public void UpdateAllBlueprintUI()
    {
        foreach (var uiElement in blueprintUIElements)
        {
            bool canBeCrafted = CanCraft(uiElement.GetBlueprint());
            uiElement.SetCraftableStatus(canBeCrafted);
        }
    }

    // --- Craft Mant��� ---
    private bool CanCraft(WeaponBlueprint blueprint)
    {
        // E�er bu silah�n kilidi zaten a��ksa, tekrar craftlanamaz.
        if (WeaponSlotManager.Instance.IsWeaponUnlocked(blueprint.weaponSlotIndexToUnlock))
        {
            return false;
        }

        foreach (var requirement in blueprint.requiredParts)
        {
            if (playerStats.GetWeaponPartCount(requirement.partType) < requirement.amount)
            {
                return false;
            }
        }
        return true;
    }

    public void TryCraftWeapon(WeaponBlueprint blueprint)
    {
        if (CanCraft(blueprint))
        {
            playerStats.ConsumeWeaponParts(blueprint.requiredParts);
            WeaponSlotManager.Instance.UnlockWeapon(blueprint.weaponSlotIndexToUnlock);
            UpdateAllBlueprintUI(); // Craft sonras� UI'� tekrar g�ncelle

            Debug.Log($"Craft BA�ARILI: {blueprint.weaponName} �retildi!");
        }
        else
        {
            Debug.LogWarning("Craft BA�ARISIZ: Yeterli par�a yok veya silah zaten a��k.");
        }
    }
}