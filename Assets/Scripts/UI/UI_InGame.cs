using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    // [SerializeField] private Image dashImage;
    // [SerializeField] private Image parryImage;
    // [SerializeField] private Image crystalImage;
    // [SerializeField] private Image swordImage;
    // [SerializeField] private Image blackholeImage;
    // [SerializeField] private Image flaskImage;

    [Header("Souls info")]
    private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI currentSouls;
    [SerializeField] private float soulsAmount;
    [SerializeField] private float increaseRate = 100;

    private SkillManager skills;

    private void Start()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();

        if (playerStats != null) playerStats.onHealthChanged += UpdateHealthUI;

        skills = SkillManager.instance;
    }

    private void Update()
    {
        UpdateSoulsUI();
        
        if (PlayerManager.instance.player.isRed) return;

        // if (Input.GetKeyDown(KeyCode.LeftShift) && skills.dash.dashUnlocked) SetCooldownOf(dashImage);
        // if (Input.GetKeyDown(KeyCode.Q) && skills.parry.parryUnlocked) SetCooldownOf(parryImage);
        // if (Input.GetKeyDown(KeyCode.F) && skills.crystal.crystalUnlocked) SetCooldownOf(crystalImage);
        // if (Input.GetKeyDown(KeyCode.Mouse1) && skills.sword.swordUnlocked) SetCooldownOf(swordImage);
        // if (Input.GetKeyDown(KeyCode.R) && skills.blackhole.blackholeUnlocked) SetCooldownOf(blackholeImage);
        // if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.instance.GetEquipment(EquipmentType.Flask) != null) SetCooldownOf(flaskImage);

        // CheckCooldownOf(dashImage, skills.dash.cooldown);
        // CheckCooldownOf(parryImage, skills.parry.cooldown);
        // CheckCooldownOf(crystalImage, skills.crystal.cooldown);
        // CheckCooldownOf(swordImage, skills.sword.cooldown);
        // CheckCooldownOf(blackholeImage, skills.blackhole.cooldown);
        // CheckCooldownOf(flaskImage, Inventory.instance.flaskCooldown);
    }

    private void UpdateSoulsUI()
    {
        if (soulsAmount < PlayerManager.instance.GetCurrency())
        {
            soulsAmount += Time.deltaTime * increaseRate;

            StartCoroutine(fadeInAndOutSoul());
        }
        else
        {
            soulsAmount = PlayerManager.instance.GetCurrency();
        }

        currentSouls.text = ((int)soulsAmount).ToString();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;
    }

    private void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount <= 0) _image.fillAmount = 1;
    }

    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0) _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
    }

    public IEnumerator fadeInAndOutSoul()
    {
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = 1f;
        float duration = 1f;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            canvasGroup.alpha = currentAlpha;
            elapsedTime += Time.deltaTime;
        }

        yield return new WaitForSeconds(5f);

        startAlpha = canvasGroup.alpha;
        targetAlpha = 0f;
        
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            canvasGroup.alpha = currentAlpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
