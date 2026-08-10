using System;

namespace GoncaloMCOliveira.CinemachineCameraShake {
    
    public static class CameraShakeEvents {

        public static event Action<CameraShakeInstance> ShakePlayed;
        public static event Action<CameraShakeInstance> ShakeStopped;
        public static event Action AllShakesStopped;

        public static Action<float> GlobalIntensityChangedByUI;
        public static Action<float> GlobalIntensityChangedBySystem;

        public static Action<bool> ShakesEnabledToggledByUI;
        public static Action<bool> ShakesEnabledToggledBySystem;

        public static void PlayShake(CameraShakeInstance instance)
            => ShakePlayed?.Invoke(instance);

        public static void StopShake(CameraShakeInstance instance)
            => ShakeStopped?.Invoke(instance);

        public static void StopAllShakes()
            => AllShakesStopped?.Invoke();

        public static void RaiseUIGlobalIntensityChanged(float value)
            => GlobalIntensityChangedByUI?.Invoke(value);

        public static void RaiseSystemGlobalIntensityChanged(float value)
            => GlobalIntensityChangedBySystem?.Invoke(value);

        public static void RaiseUIShakesEnabledToggled(bool disabled)
            => ShakesEnabledToggledByUI?.Invoke(disabled);

        public static void RaiseSystemShakesEnabledToggled(bool disabled)
            => ShakesEnabledToggledBySystem?.Invoke(disabled);
    }
}