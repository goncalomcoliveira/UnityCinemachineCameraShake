using System.Collections;
using System.Collections.Generic;
using GoncaloMCOliveira.Singleton;
using Unity.Cinemachine;
using UnityEngine;

namespace GoncaloMCOliveira.CinemachineCameraShake {
    
    /// <summary>
    /// Central manager responsible for playing, blending,
    /// and stopping Cinemachine camera shakes using impulses.
    /// </summary>
    public class CameraShakeManager : PersistentSingleton<CameraShakeManager> {

        #region Global Settings

        [Range(0f, 2f)]
        public float globalIntensity = 1f;

        public bool shakesEnabled = true;

        #endregion

        #region Runtime State

        private readonly List<ActiveCameraShake> activeShakes = new();
        private readonly List<ActiveCameraShake> removalBuffer = new();

        private Coroutine shakeRoutine;

        [Header("Impulse Source")]
        [SerializeField] private CinemachineImpulseSource impulseSource;

        #endregion

        #region Event Wiring

        private void OnEnable() {
            CameraShakeEvents.ShakePlayed += PlayShake;
            CameraShakeEvents.ShakeStopped += StopShake;
            CameraShakeEvents.AllShakesStopped += StopAll;
            CameraShakeEvents.GlobalIntensityChangedByUI += SetGlobalIntensity;
            CameraShakeEvents.ShakesEnabledToggledByUI += ToggleShakesEnabled;
        }

        private void OnDisable() {
            CameraShakeEvents.ShakePlayed -= PlayShake;
            CameraShakeEvents.ShakeStopped -= StopShake;
            CameraShakeEvents.AllShakesStopped -= StopAll;
            CameraShakeEvents.GlobalIntensityChangedByUI -= SetGlobalIntensity;
            CameraShakeEvents.ShakesEnabledToggledByUI -= ToggleShakesEnabled;
        }

        #endregion

        #region Event Handlers

        private void PlayShake(CameraShakeInstance instance) {
            if (!shakesEnabled || impulseSource == null)
                return;

            activeShakes.Add(new ActiveCameraShake {
                Instance = instance
            });

            shakeRoutine ??= StartCoroutine(ShakeLoop());
        }

        private void StopShake(CameraShakeInstance instance) {
            foreach (var s in activeShakes) {
                if (s.Instance == instance && !s.IsStopping) {
                    s.IsStopping = true;
                    s.StopTime = 0f;
                    break;
                }
            }
        }

        private void StopAll() {
            foreach (var s in activeShakes)
                s.IsStopping = true;
        }

        #endregion

        #region Shake Processing

        private IEnumerator ShakeLoop() {

            while (activeShakes.Count > 0) {

                var impulse = Vector3.zero;
                removalBuffer.Clear();

                foreach (var s in activeShakes) {

                    s.PlayTime += Time.deltaTime;

                    if (s.IsStopping)
                        s.StopTime += Time.deltaTime;

                    var inst = s.Instance;

                    if (!s.IsStopping &&
                        inst.Lifetime == ShakeLifetime.Timed &&
                        s.PlayTime >= inst.Duration) {

                        s.IsStopping = true;
                        s.StopTime = 0f;
                    }

                    var intensity = inst.EvaluateIntensity(
                        s.PlayTime,
                        s.StopTime,
                        s.IsStopping
                    );

                    Vector3 direction = ResolveDirection(inst);

                    impulse += direction * (inst.Amplitude * intensity);

                    if (s.IsStopping && s.StopTime >= inst.FadeOutDuration)
                        removalBuffer.Add(s);
                }

                foreach (var s in removalBuffer)
                    activeShakes.Remove(s);

                if (impulse != Vector3.zero) {
                    impulseSource.GenerateImpulse(
                        impulse * globalIntensity
                    );
                }

                yield return null;
            }

            shakeRoutine = null;
        }
        
        private static Vector3 ResolveDirection(CameraShakeInstance inst) {
            return inst.DirectionMode switch {

                ShakeDirectionMode.Fixed =>
                    inst.FixedDirection,

                ShakeDirectionMode.RandomHorizontal =>
                    new Vector3(
                        Random.Range(-1f, 1f),
                        0f,
                        0f
                    ),

                ShakeDirectionMode.RandomVertical =>
                    new Vector3(
                        0f,
                        Random.Range(-1f, 1f),
                        0f
                    ),

                ShakeDirectionMode.Random =>
                    new Vector3(
                        Random.Range(-1f, 1f),
                        Random.Range(-1f, 1f),
                        0f
                    ),

                _ => Vector3.zero
            };
        }

        #endregion

        #region Settings

        public void SetGlobalIntensity(float value) {
            globalIntensity = value;
            CameraShakeEvents.RaiseSystemGlobalIntensityChanged(value);
        }

        public void ToggleShakesEnabled(bool disable) {
            shakesEnabled = !disable;
            CameraShakeEvents.RaiseSystemShakesEnabledToggled(disable);
        }

        #endregion
    }
}



