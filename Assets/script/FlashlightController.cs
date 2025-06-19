using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [Header("Componentes")]
    public Light flashlight;
    public Slider flashlightSlider;
    public Image sliderFill;

    [Header("Controles")]
    public KeyCode toggleKey = KeyCode.R;

    [Header("Tiempos")]
    public float maxUsageTime = 120f;
    public float cooldownTime = 60f;

    private float remainingUsageTime;
    private float remainingCooldownTime;
    private bool isOn = false;
    private bool isCoolingDown = false;

    private Color batteryColor = Color.green;
    private Color cooldownColor = Color.blue;

    void Start()
    {
        Debug.Log("FlashlightController en: " + gameObject.name);

          if (flashlight == null)
            Debug.LogError("❌ Falta asignar: flashlight");
    if (flashlightSlider == null)
        Debug.LogError("❌ Falta asignar: flashlightSlider");
    if (sliderFill == null)
        Debug.LogError("❌ Falta asignar: sliderFill");

    if (flashlight == null || flashlightSlider == null || sliderFill == null)
    {
        Debug.LogError("❌ FlashlightController: Faltan referencias en el Inspector.");
        enabled = false;
        return;
    }

    flashlight.enabled = false;
    remainingUsageTime = maxUsageTime;

    flashlightSlider.gameObject.SetActive(true);
    sliderFill.gameObject.SetActive(true); // <- Fuerza que el Fill esté activo
    flashlightSlider.maxValue = maxUsageTime;
    flashlightSlider.value = maxUsageTime;
    flashlightSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!flashlight || !flashlightSlider || !sliderFill)
            return;

        if (Input.GetKeyDown(toggleKey))
        {
            if (isOn)
                TurnOff();
            else if (!isCoolingDown && remainingUsageTime > 0f)
                TurnOn();
        }

        if (isOn)
        {
            remainingUsageTime -= Time.deltaTime;
            flashlightSlider.value = remainingUsageTime;

            if (remainingUsageTime <= 0f)
            {
                remainingUsageTime = 0f;
                StartCooldown();
            }
        }

        if (isCoolingDown)
        {
            remainingCooldownTime -= Time.deltaTime;
            flashlightSlider.maxValue = cooldownTime;
            flashlightSlider.value = cooldownTime - remainingCooldownTime;

            if (remainingCooldownTime <= 0f)
            {
                isCoolingDown = false;
                remainingUsageTime = maxUsageTime;
                flashlightSlider.gameObject.SetActive(false);
            }
        }

        if (!isOn && !isCoolingDown)
        {
            flashlightSlider.gameObject.SetActive(false);
        }
    }

    void TurnOn()
    {
        flashlight.enabled = true;
        isOn = true;

        flashlightSlider.maxValue = maxUsageTime;
        flashlightSlider.value = remainingUsageTime;
        flashlightSlider.gameObject.SetActive(true);
        sliderFill.color = batteryColor;
    }

    void TurnOff()
    {
        flashlight.enabled = false;
        isOn = false;
        flashlightSlider.gameObject.SetActive(false);
    }

    void StartCooldown()
    {
        TurnOff();
        isCoolingDown = true;
        remainingCooldownTime = cooldownTime;

        flashlightSlider.maxValue = cooldownTime;
        flashlightSlider.value = 0f;
        flashlightSlider.gameObject.SetActive(true);
        sliderFill.color = cooldownColor;
    }
}
