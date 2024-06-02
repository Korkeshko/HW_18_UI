using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
    }

    void UpdateText(float value)
    {
        text.SetText(value.ToString());
    }
}
