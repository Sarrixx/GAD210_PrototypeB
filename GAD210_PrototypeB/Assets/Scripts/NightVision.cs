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

    private bool filterAActive = false;
    private bool filterBActive = false;
    private bool filterCActive = false;
    private Color defaultAmbientColor;
    private PostProcessVolume postProcessVolume;

    private void Awake()
    {
        defaultAmbientColor = RenderSettings.ambientLight;
        TryGetComponent(out postProcessVolume);
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
                if (RenderSettings.ambientLight != defaultAmbientColor) { RenderSettings.ambientLight = defaultAmbientColor; };
                filterAActive = false;
                filterBActive = false;
                filterCActive = false;
                break;
            case 1:
                if (filterAActive == false && postProcessVolume != null && filterAProfile != null) 
                {
                    postProcessVolume.profile = filterAProfile;
                    postProcessVolume.weight = 1f;
                    if (RenderSettings.ambientLight != filterAColour) { RenderSettings.ambientLight = filterAColour; };
                    filterAActive = true;
                    filterBActive = false;
                    filterCActive = false;
                }
                break;
            case 2:
                if (filterBActive == false && postProcessVolume != null && filterBProfile != null)
                {
                    postProcessVolume.profile = filterBProfile;
                    postProcessVolume.weight = 1f;
                    if (RenderSettings.ambientLight != filterBColour) { RenderSettings.ambientLight = filterBColour; };
                    filterBActive = true;
                    filterAActive = false;
                    filterCActive = false;
                }
                break;
            case 3:
                if (filterCActive == false && postProcessVolume != null && filterCProfile != null)
                {
                    postProcessVolume.profile = filterCProfile;
                    postProcessVolume.weight = 1f;
                    if (RenderSettings.ambientLight != filterCColour) { RenderSettings.ambientLight = filterCColour; };
                    filterCActive = true;
                    filterAActive = false;
                    filterBActive = false;
                }
                break;
        }
    }
}
