using UnityEngine;
using UnityEngine.UI;

namespace GoncaloMCOliveira.CinemachineCameraShake {
    
    /// <summary>
    /// UI component that synchronizes a Unity Slider with the global
    /// intensity of the camera shake system.
    /// Sends and receives intensity change events via CameraShakeEvents.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class CameraShakeIntensitySlider : MonoBehaviour {

        [Tooltip("The UI slider component to link with.")]
        private Slider _slider;

        private void Awake() {
            if (_slider == null)
                _slider = GetComponent<Slider>();

            _slider.minValue = 0.0001f;
            _slider.maxValue = 1f;
        }

        private void OnEnable() {
            CameraShakeEvents.GlobalIntensityChangedBySystem += UpdateSlider;
            _slider.onValueChanged.AddListener(OnSliderChanged);
        }

        private void OnDisable() {
            CameraShakeEvents.GlobalIntensityChangedBySystem -= UpdateSlider;
            _slider.onValueChanged.RemoveListener(OnSliderChanged);
        }

        /// <summary>
        /// Called when the user changes the slider value.
        /// </summary>
        private void OnSliderChanged(float value) {
            CameraShakeEvents.RaiseUIGlobalIntensityChanged(value);
        }

        /// <summary>
        /// Updates the slider without triggering listeners.
        /// </summary>
        private void UpdateSlider(float value) {
            _slider.SetValueWithoutNotify(value);
        }
    }
}
