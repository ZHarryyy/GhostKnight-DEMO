using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;
    public string Id;
    public bool activationStatus;
    private bool canActivate;
    private Light2D light2D;
    private CanvasGroup canvasGroup;

    [SerializeField] private GameObject lightSmall;
    [SerializeField] private GameObject lightBig;

    private void Start()
    {
        anim = GetComponent<Animator>();
        light2D = GetComponentInChildren<Light2D>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    private void Update()
    {
        if (canActivate && Input.GetKeyDown(KeyCode.E) && !activationStatus) ActivateCheckpoint();

        if (activationStatus)
        {
            light2D.intensity = 4f;
            light2D.pointLightOuterRadius = 4f;
        }
    }

    [ContextMenu("Generate checkpoint id")]
    private void GenerateId()
    {
        Id = Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            canActivate = true;

            if (!activationStatus)
            {
                StartCoroutine(FadeInKeyPrompt());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            canActivate = false;

            StartCoroutine(FadeOutKeyPrompt());
        }
    }

    public void ActivateCheckpoint()
    {
        AudioManager.instance.PlaySFX(4, transform);
        activationStatus = true;
        lightSmall.SetActive(false);
        anim.SetBool("active", true);
        lightBig.SetActive(true);
        StartCoroutine(FadeOutKeyPrompt());
    }

    private IEnumerator FadeInKeyPrompt()
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
            yield return null;
        }
    }

    private IEnumerator FadeOutKeyPrompt()
    {
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = 0f;
        float duration = 1f;

        float elapsedTime = 0f;
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
