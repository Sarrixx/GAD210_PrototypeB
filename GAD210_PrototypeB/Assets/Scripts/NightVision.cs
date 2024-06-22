using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class NightVision : MonoBehaviour
{
    [SerializeField] private PostProcessProfile filterAProfile;
    [SerializeField] private PostProcessProfile filterBProfile;
    [SerializeField] private PostProcessProfile filterCProfile;
    [SerializeField] private Color filterAColour = Color.green;
    [SerializeField] private Color filterBColour = Color.red;
    [SerializeField] private Color filterCColour = Color.grey;
    [SerializeField] private float fogStart;
    [SerializeField] private float fogEnd;
    [SerializeField] private AudioClip activationClip;
    [SerializeField] private AudioClip toggleClip;
    [SerializeField] private HUD hud;

    private bool filterAActive = false;
    private bool filterBActive = false;
    private bool filterCActive = false;
    private float defaultFogStart;
    private float defaultFogEnd;
    private Color defaultAmbientColor;
    private PostProcessVolume postProcessVolume;
    private AudioSource aSrc;

    private void Awake()
    {
        defaultAmbientColor = RenderSettings.ambientLight;
        defaultFogStart = RenderSettings.fogStartDistance;
        defaultFogEnd = RenderSettings.fogEndDistance;
        TryGetComponent(out postProcessVolume);
        TryGetComponent(out aSrc);
        if(aSrc != null)
        {
            aSrc.loop = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(postProcessVolume != null)
        {
            postProcessVolume.weight = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0) == true || (filterAActive == true && Input.GetKeyDown(KeyCode.Alpha1) == true) || 
            (filterBActive == true && Input.GetKeyDown(KeyCode.Alpha2) == true) || (filterCActive == true && Input.GetKeyDown(KeyCode.Alpha3) == true))
        {
            ToggleFilter();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            ToggleFilter(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
            ToggleFilter(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) == true)
        {
            ToggleFilter(3);
        }
    }

    private void ToggleFilter(int index = 0)
    {
        switch(index)
        {
            case 0:
            default:
                if(postProcessVolume != null) { postProcessVolume.weight = 0f; }
                if (RenderSettings.ambientLight != defaultAmbientColor) { RenderSettings.ambientLight = defaultAmbientColor; }
                if(RenderSettings.fogStartDistance != defaultFogStart) { RenderSettings.fogStartDistance = defaultFogStart; }
                if(RenderSettings.fogEndDistance != defaultFogEnd) { RenderSettings.fogEndDistance = defaultFogEnd; }
                filterAActive = false;
                filterBActive = false;
                filterCActive = false;
                if (aSrc.clip != null && aSrc.isPlaying == true)
                {
                    aSrc.Stop();
                }
                aSrc.PlayOneShot(activationClip);
                if (hud != null)
                {
                    hud.ToggleNightVision(0);
                }
                break;
            case 1:
                if (filterAActive == false && postProcessVolume != null && filterAProfile != null) 
                {
                    postProcessVolume.profile = filterAProfile;
                    postProcessVolume.weight = 1f;
                    if (RenderSettings.ambientLight != filterAColour) { RenderSettings.ambientLight = filterAColour; }
                    if (RenderSettings.fogStartDistance != fogStart) { RenderSettings.fogStartDistance = fogStart; }
                    if (RenderSettings.fogEndDistance != fogEnd) { RenderSettings.fogEndDistance = fogEnd; }
                    if (filterBActive == true || filterCActive == true)
                    {
                        aSrc.PlayOneShot(toggleClip);
                    }
                    else
                    {
                        if (aSrc.clip != null)
                        {
                            aSrc.Play();
                        }
                        aSrc.PlayOneShot(activationClip);
                    }
                    filterAActive = true;
                    filterBActive = false;
                    filterCActive = false;
                    if (hud != null)
                    {
                        hud.ToggleNightVision(1);
                    }
                }
                break;
            case 2:
                if (filterBActive == false && postProcessVolume != null && filterBProfile != null)
                {
                    postProcessVolume.profile = filterBProfile;
                    postProcessVolume.weight = 1f;
                    if (RenderSettings.ambientLight != filterBColour) { RenderSettings.ambientLight = filterBColour; }
                    if (RenderSettings.fogStartDistance != fogStart) { RenderSettings.fogStartDistance = fogStart; }
                    if (RenderSettings.fogEndDistance != fogEnd) { RenderSettings.fogEndDistance = fogEnd; }
                    if (filterAActive == true || filterCActive == true)
                    {
                        aSrc.PlayOneShot(toggleClip);
                    }
                    else
                    {
                        if (aSrc.clip != null)
                        {
                            aSrc.Play();
                        }
                        aSrc.PlayOneShot(activationClip);
                    }
                    filterBActive = true;
                    filterAActive = false;
                    filterCActive = false;
                    if (hud != null)
                    {
                        hud.ToggleNightVision(2);
                    }
                }
                break;
            case 3:
                if (filterCActive == false && postProcessVolume != null && filterCProfile != null)
                {
                    postProcessVolume.profile = filterCProfile;
                    postProcessVolume.weight = 1f;
                    if (RenderSettings.ambientLight != filterCColour) { RenderSettings.ambientLight = filterCColour; }
                    if (RenderSettings.fogStartDistance != fogStart) { RenderSettings.fogStartDistance = fogStart; }
                    if (RenderSettings.fogEndDistance != fogEnd) { RenderSettings.fogEndDistance = fogEnd; }
                    if (filterBActive == true || filterAActive == true)
                    {
                        aSrc.PlayOneShot(toggleClip);
                    }
                    else
                    {
                        if (aSrc.clip != null)
                        {
                            aSrc.Play();
                        }
                        aSrc.PlayOneShot(activationClip);
                    }
                    filterCActive = true;
                    filterAActive = false;
                    filterBActive = false;
                    if (hud != null)
                    {
                        hud.ToggleNightVision(3);
                    }
                }
                break;
        }
    }
}
