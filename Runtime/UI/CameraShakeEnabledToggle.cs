using UnityEngine;
using UnityEngine.UI;

namespace GoncaloMCOliveira.CinemachineCameraShake {
    
    /// <summary>
    /// UI component that synchronizes a Unity Toggle with the enabled state
    /// of the camera shake system.
    /// Sends and receives enable toggle events via CameraShakeEvents.
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class CameraShakeEnabledToggle : MonoBehaviour {
    
        [Tooltip("The UI toggle component to link with.")]
        private Toggle _toggle;
    
        private void Awake() {
            if (_toggle == null)
                _toggle = GetComponent<Toggle>();
        }
    
        private void OnEnable() {
            CameraShakeEvents.ShakesEnabledToggledBySystem += UpdateToggle;
            CameraShakeEvents.GlobalIntensityChangedBySystem += UpdateToggleOnIntensityChange;
            _toggle.onValueChanged.AddListener(OnToggleChanged);
        }
    
        private void OnDisable() {
            CameraShakeEvents.ShakesEnabledToggledBySystem -= UpdateToggle;
            CameraShakeEvents.GlobalIntensityChangedBySystem -= UpdateToggleOnIntensityChange;
            _toggle.onValueChanged.RemoveListener(OnToggleChanged);
        }
    
        /// <summary>
        /// Called when the user changes the toggle.
        /// True means "disabled" (muted), false means enabled.
        /// </summary>
        private void OnToggleChanged(bool isOn) {
            CameraShakeEvents.RaiseUIShakesEnabledToggled(isOn);
        }
    
        /// <summary>
        /// Updates the toggle without notifying listeners.
        /// </summary>
        private void UpdateToggle(bool isDisabled) {
            _toggle.SetIsOnWithoutNotify(isDisabled);
        }
    
        /// <summary>
        /// Auto-disable toggle when intensity is effectively zero.
        /// </summary>
        private void UpdateToggleOnIntensityChange(float intensity) {
            _toggle.isOn = intensity <= 0.0001f;
        }
    }
}