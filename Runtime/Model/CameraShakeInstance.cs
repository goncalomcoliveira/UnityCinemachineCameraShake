using UnityEngine;

namespace GoncaloMCOliveira.CinemachineCameraShake {
    
    public class CameraShakeInstance {

        #region Lifetime

        public ShakeLifetime Lifetime { get; private set; } = ShakeLifetime.Timed;
        public float Duration { get; private set; } = 0.2f;

        #endregion

        #region Shake Parameters

        public float Amplitude { get; private set; } = 1f;

        public ShakeDirectionMode DirectionMode { get; private set; }
            = ShakeDirectionMode.Random;

        public Vector3 FixedDirection { get; private set; } = Vector3.zero;

        #endregion

        #region Fade

        public float FadeInDuration { get; private set; }
        public float FadeOutDuration { get; private set; }

        #endregion

        #region Builder

        public CameraShakeInstance Timed(float duration) {
            Lifetime = ShakeLifetime.Timed;
            Duration = Mathf.Max(0f, duration);
            return this;
        }

        public CameraShakeInstance Looping() {
            Lifetime = ShakeLifetime.Looping;
            Duration = 0f;
            return this;
        }

        public CameraShakeInstance WithAmplitude(float value) {
            Amplitude = Mathf.Max(0f, value);
            return this;
        }

        public CameraShakeInstance RandomDirection() {
            DirectionMode = ShakeDirectionMode.Random;
            return this;
        }

        public CameraShakeInstance RandomHorizontal() {
            DirectionMode = ShakeDirectionMode.RandomHorizontal;
            return this;
        }

        public CameraShakeInstance RandomVertical() {
            DirectionMode = ShakeDirectionMode.RandomVertical;
            return this;
        }

        public CameraShakeInstance WithDirection(Vector2 direction) {
            DirectionMode = ShakeDirectionMode.Fixed;
            FixedDirection = new Vector3(direction.x, direction.y, 0f).normalized;
            return this;
        }

        public CameraShakeInstance WithFadeIn(float duration) {
            FadeInDuration = Mathf.Max(0f, duration);
            return this;
        }

        public CameraShakeInstance WithFadeOut(float duration) {
            FadeOutDuration = Mathf.Max(0f, duration);
            return this;
        }

        #endregion

        #region Lifecycle

        public void Play() {
            CameraShakeEvents.PlayShake(this);
        }

        public void Stop() {
            CameraShakeEvents.StopShake(this);
        }

        #endregion

        #region Intensity

        public float EvaluateIntensity(float playTime, float stopTime, bool stopping) {

            if (!stopping && FadeInDuration > 0f && playTime < FadeInDuration)
                return Mathf.Clamp01(playTime / FadeInDuration);

            if (stopping && FadeOutDuration > 0f)
                return Mathf.Clamp01(1f - stopTime / FadeOutDuration);

            return 1f;
        }

        #endregion
    }
}

