using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    private delegate void TargetFoundDelegate(EventTrigger trigger);
    private enum TriggerType { target = 1, end = 0 }

    [SerializeField] private TriggerType type;
    [SerializeField] private EventTrigger[] targets;
    [SerializeField] private EventTrigger nextSection;

    private bool engaged = false;
    private int totalTargets = 0;
    private int foundTargets = 0;

    private static HUD hud;

    private event TargetFoundDelegate TargetFound;
    public int TypeIndex { get { return (int)type; } }

    private void Awake()
    {
        if (hud == null)
        {
            hud = FindObjectOfType<HUD>();
        }
        if (type == TriggerType.end)
        {
            foreach(EventTrigger trigger in targets)
            {
                totalTargets++;
                trigger.TargetFound += TargetFoundResponse;
            }
            if (nextSection != null)
            {
                nextSection.gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        if (hud != null && type == TriggerType.end)
        {
            hud.UpdateFoundTargets(0, totalTargets);
        }
    }

    private void Update()
    {
        if(engaged == true)
        {
            if (Input.GetButtonDown("Interaction") == true && type == TriggerType.target)
            {
                TargetFound.Invoke(this);
                if (hud != null)
                {
                    hud.ToggleInteractionAlert(false);
                }
                engaged = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == true)
        {
            if (type == TriggerType.target)
            {
                if (hud != null)
                {
                    hud.ToggleInteractionAlert(true);
                    engaged = true;
                }
            }
            else if (type == TriggerType.end)
            {
                foreach(EventTrigger trigger in targets)
                {
                    if(trigger.gameObject.activeSelf == true)
                    {
                        trigger.gameObject.SetActive(false);
                    }
                }
                hud.DisplaySectionOutcome(foundTargets, totalTargets);
                if(nextSection != null && nextSection.targets.Length > 0)
                {
                    nextSection.gameObject.SetActive(true);
                    hud.UpdateFoundTargets(0, nextSection.totalTargets);
                }
                else
                {
                    //game end
                }
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") == true && type == TriggerType.target)
        {
            if (hud != null)
            {
                hud.ToggleInteractionAlert(false);
                engaged = false;
            }
        }
    }

    private void TargetFoundResponse(EventTrigger signallingTrigger)
    {
        if (type == TriggerType.end)
        {
            foundTargets++;
            if (hud != null)
            {
                hud.UpdateFoundTargets(foundTargets, totalTargets);
            }
            signallingTrigger.TargetFound -= TargetFoundResponse;
        }
    }
}
