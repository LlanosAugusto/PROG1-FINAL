using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [Header("HUD Settings")]
    [SerializeField] private Slider playerHealthSlider = null;
    [SerializeField] private Slider runeHealthSlider = null;
    [SerializeField] private TMP_Text waveText = null;

    [Header("Fireball cooldown Settings")] 
    [SerializeField] private GameObject fireballIcon = null; 
    [SerializeField] private GameObject fireballCooldownIcon = null;
    [SerializeField] private TMP_Text cooldownText = null;

    [Header("Dash cooldown Settings")]
    [SerializeField] private GameObject dashReadyImage = null;
    [SerializeField] private GameObject dashCooldownImage = null;
    [SerializeField] private TMP_Text dashCooldownText = null;

    [Header("Item cooldown Settings")]
    [SerializeField] private GameObject meatReadyImage = null;
    [SerializeField] private GameObject meatCooldownImage = null;
    [SerializeField] private TMP_Text meatCooldownText = null;

    [SerializeField] private GameObject potionReadyImage = null;
    [SerializeField] private GameObject potionCooldownImage = null;
    [SerializeField] private TMP_Text potionCooldownText = null;

    [Header("Pause Settings")]
    [SerializeField] private GameObject pausePanel = null;
    [SerializeField] private Button resumeBtn = null;
    [SerializeField] private Button backToMenuBtn = null;

    [Header("Lose Settings")]
    [SerializeField] private GameObject losePanel = null;
    [SerializeField] private Button retryBtn = null;
    [SerializeField] private Button loseBackToMenuBtn = null;

    [Header("Win Settings")]
    [SerializeField] private GameObject winPanel = null;
    [SerializeField] private Button winBackToMenuBtn = null;

    private Action onEnablePlayerInput = null;

    private const string allWaveText = "Wave {0}/{1}";

    private void Start()
    {
        resumeBtn.onClick.AddListener(() => TogglePause(false));
        backToMenuBtn.onClick.AddListener(BackToMenu);

        retryBtn.onClick.AddListener(Retry);
        loseBackToMenuBtn.onClick.AddListener(BackToMenu);

        winBackToMenuBtn.onClick.AddListener(BackToMenu);

        ResetCooldownUI();
        ResetDashUI();
    }

    public void Init(Action onEnablePlayerInput)
    {
        this.onEnablePlayerInput = onEnablePlayerInput;
    }

    //Fireball cooldown
    public void StartCooldown(float cooldownTime)
    {
        StartCoroutine(CooldownCoroutine(cooldownTime));
    }

    private System.Collections.IEnumerator CooldownCoroutine(float cooldownTime)
    {
        ResetCooldownUI();

        if (cooldownText != null && fireballIcon != null && fireballCooldownIcon != null)
        {
            Debug.Log("cooldown imagen");
            fireballIcon.SetActive(false);
            fireballCooldownIcon.SetActive(true);
            cooldownText.gameObject.SetActive(true);

            float remainingTime = cooldownTime;

            while (remainingTime > 0)
            {
                cooldownText.text = Mathf.Ceil(remainingTime).ToString();
                remainingTime -= Time.deltaTime;
                yield return null;
            }

            ResetCooldownUI();
        }
    }

    private void ResetCooldownUI()
    {
        if (fireballIcon != null) fireballIcon.SetActive(true);
        if (fireballCooldownIcon != null) fireballCooldownIcon.SetActive(false);
        if (cooldownText != null) cooldownText.gameObject.SetActive(false);
    }
    //Fin fireball cooldown

    //Dash cooldown
    public void StartDashCooldownUI(float cooldownTime)
    {
        StartCoroutine(DashCooldownCoroutine(cooldownTime));
    }

    private System.Collections.IEnumerator DashCooldownCoroutine(float cooldownTime)
    {
        ResetDashUI();

        if (dashReadyImage != null && dashCooldownImage != null && dashCooldownText != null)
        {

            dashReadyImage.gameObject.SetActive(false);
            dashCooldownImage.gameObject.SetActive(true);
            dashCooldownText.gameObject.SetActive(true);

            float remainingTime = cooldownTime;

            while (remainingTime > 0)
            {
                dashCooldownText.text = Mathf.Ceil(remainingTime).ToString();
                remainingTime -= Time.deltaTime;
                yield return null;
            }

            ResetDashUI();
        }
    }

    private void ResetDashUI()
    {
        if (dashReadyImage != null) dashReadyImage.gameObject.SetActive(true);
        if (dashCooldownImage != null) dashCooldownImage.gameObject.SetActive(false);
        if (dashCooldownText != null) dashCooldownText.gameObject.SetActive(false);
    }
    //Fin dash cooldown

    //Inicio meat cooldown
    public void StartMeatCooldownUI(float cooldownTime)
    {
        StartCoroutine(MeatCooldownCoroutine(cooldownTime, meatReadyImage, meatCooldownImage, meatCooldownText));
    }

    private System.Collections.IEnumerator MeatCooldownCoroutine(float cooldownTime, GameObject readyImage, GameObject cooldownImage, TMP_Text cooldownText)
    {
        if (readyImage != null) readyImage.SetActive(false);
        if (cooldownImage != null) cooldownImage.SetActive(true);
        if (cooldownText != null) cooldownText.gameObject.SetActive(true);

        float remainingTime = cooldownTime;

        while (remainingTime > 0)
        {
            if (cooldownText != null)
                cooldownText.text = Mathf.Ceil(remainingTime).ToString();
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        if (readyImage != null) readyImage.SetActive(true);
        if (cooldownImage != null) cooldownImage.SetActive(false);
        if (cooldownText != null) cooldownText.gameObject.SetActive(false);
    }

    //Fin meat cooldown

    //Inicio potion cooldown
    public void StartPotionCooldownUI(float cooldownTime)
    {
        StartCoroutine(PotionCooldownCoroutine(cooldownTime, potionReadyImage, potionCooldownImage, potionCooldownText));
    }

    private System.Collections.IEnumerator PotionCooldownCoroutine(float cooldownTime, GameObject readyImage, GameObject cooldownImage, TMP_Text cooldownText)
    {
        if (readyImage != null) readyImage.SetActive(false);
        if (cooldownImage != null) cooldownImage.SetActive(true);
        if (cooldownText != null) cooldownText.gameObject.SetActive(true);

        float remainingTime = cooldownTime;

        while (remainingTime > 0)
        {
            if (cooldownText != null)
                cooldownText.text = Mathf.Ceil(remainingTime).ToString();
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        if (readyImage != null) readyImage.SetActive(true);
        if (cooldownImage != null) cooldownImage.SetActive(false);
        if (cooldownText != null) cooldownText.gameObject.SetActive(false);
    }

    //Fin potion cooldown

    public void TogglePause(bool status)
    {
        pausePanel.SetActive(status);
        ToggleTimeScale(!status);

        if (!status)
        {
            onEnablePlayerInput?.Invoke();
        }
    }

    public void OpenLosePanel()
    {
        losePanel.SetActive(true);
    }

    public void OpenWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void UpdatePlayerHealth(int currentLives, int maxLives)
    {
        if (playerHealthSlider != null)
            playerHealthSlider.value = (float)currentLives / maxLives;
    }

    public void UpdateRuneHealth(int currentLives, int maxLives)
    {
        if (runeHealthSlider != null)
            runeHealthSlider.value = (float)currentLives / maxLives;
    }

    public void UpdateWave(int currentWave, int maxWave)
    {
        if (waveText != null)
            waveText.text = string.Format(allWaveText, currentWave, maxWave);
    }

    private void Retry()
    {
        retryBtn.interactable = false;
        loseBackToMenuBtn.interactable = false;

        GameManager.Instance.ChangeScene(SceneGame.Shooter);
        GameManager.Instance.AudioManager.ToggleMusic(true);
    }

    private void BackToMenu()
    {
        resumeBtn.interactable = false;
        backToMenuBtn.interactable = false;

        retryBtn.interactable = false;
        loseBackToMenuBtn.interactable = false;

        winBackToMenuBtn.interactable = false;

        GameManager.Instance.ChangeScene(SceneGame.Menu);
        GameManager.Instance.AudioManager.ToggleMusic(true);
        ToggleTimeScale(true);
    }

    private void ToggleTimeScale(bool status)
    {
        Time.timeScale = status ? 1f : 0f;
    }
}
