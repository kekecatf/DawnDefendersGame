// BlueprintUI.cs

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class BlueprintUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Image weaponIcon;
    public TextMeshProUGUI weaponNameText;
    public Button craftButton;

    private WeaponBlueprint currentBlueprint;

    public void Setup(WeaponBlueprint blueprint, UnityAction craftAction)
    {
        currentBlueprint = blueprint;

        weaponNameText.text = blueprint.weaponName;
        weaponIcon.sprite = blueprint.weaponIcon;

        // Butonun eski t�m listener'lar�n� temizle ve yenisini ekle.
        craftButton.onClick.RemoveAllListeners();
        craftButton.onClick.AddListener(craftAction);
    }

    public void SetCraftableStatus(bool canCraft)
    {
        // E�er craftlanabilirse, ikonu renkli ve butonu t�klanabilir yap.
        if (canCraft)
        {
            weaponIcon.color = Color.white;
            craftButton.interactable = true;
        }
        // De�ilse, ikonu karart (siyah yap) ve butonu devre d��� b�rak.
        else
        {
            weaponIcon.color = Color.black;
            craftButton.interactable = false;
        }
    }

    public WeaponBlueprint GetBlueprint()
    {
        return currentBlueprint;
    }
}