using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image nightVisionOverlay;
    [SerializeField] private Text filterAText;
    [SerializeField] private Text filterBText;
    [SerializeField] private Text filterCText;
    [SerializeField] private Image filterAImage;
    [SerializeField] private Image filterBImage;
    [SerializeField] private Image filterCImage;
    [SerializeField] private Text targetsText;
    [SerializeField] private Text endGameText;
    [SerializeField] private Text interactionText;

    private Coroutine sectionTextRoutine;

    private void Start()
    {
        nightVisionOverlay.gameObject.SetActive(false);
        endGameText.gameObject.SetActive(false);
        ToggleInteractionAlert(false);
    }

    public void ToggleNightVision(int toggleIndex)
    {
        switch (toggleIndex)
        {
            default:
            case 0:
                nightVisionOverlay.gameObject.SetActive(false);
                break;
            case 1:
                nightVisionOverlay.color = Color.green;
                filterAText.color = Color.green;
                filterBText.color = Color.green;
                filterCText.color = Color.green;
                filterAImage.enabled = true;
                filterBImage.enabled = false;
                filterCImage.enabled = false;
                nightVisionOverlay.gameObject.SetActive(true);
                break;
            case 2:
                nightVisionOverlay.color = Color.red;
                filterAText.color = Color.red;
                filterBText.color = Color.red;
                filterCText.color = Color.red;
                filterAImage.enabled = false;
                filterBImage.enabled = true;
                filterCImage.enabled = false;
                nightVisionOverlay.gameObject.SetActive(true);
                break;
            case 3:
                nightVisionOverlay.color = Color.grey;
                filterAText.color = Color.grey;
                filterBText.color = Color.grey;
                filterCText.color = Color.grey;
                filterAImage.enabled = false;
                filterBImage.enabled = false;
                filterCImage.enabled = true;
                nightVisionOverlay.gameObject.SetActive(true);
                break;
        }
    }

    private IEnumerator DisplayOutcomeForTime(float time)
    {
        targetsText.gameObject.SetActive(false);
        endGameText.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        targetsText.gameObject.SetActive(true);
        endGameText.gameObject.SetActive(false);
    }

    public void UpdateFoundTargets(int foundTargets, int totalTargets)
    {
        targetsText.text = $"Found targets: {foundTargets}/{totalTargets}";
    }

    public void DisplaySectionOutcome(int foundTargets, int totalTargets)
    {
        endGameText.text = $"Found targets: {foundTargets} of {totalTargets} ({(float)foundTargets / totalTargets * 100f}%)";
        if (sectionTextRoutine != null)
        {
            StopCoroutine(sectionTextRoutine);
        }
        sectionTextRoutine = StartCoroutine(DisplayOutcomeForTime(5));
    }

    public void ToggleInteractionAlert(bool enabled)
    {
        interactionText.gameObject.SetActive(enabled);
    }
}
