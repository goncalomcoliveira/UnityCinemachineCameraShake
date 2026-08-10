using UnityEngine;

namespace GoncaloMCOliveira.CinemachineCameraShake {
    
    public static class CameraShakePresets {

        // =========================
        // UI / Feedback
        // =========================

        /// <summary>
        /// Tiny confirmation shake.
        /// Buttons, pickups, menu actions.
        /// </summary>
        public static CameraShakeInstance UITap() {
            return new CameraShakeInstance()
                .Timed(0.05f)
                .WithAmplitude(0.12f)
                .RandomDirection()
                .WithFadeIn(0.01f)
                .WithFadeOut(0.03f);
        }

        /// <summary>
        /// Invalid action or error feedback.
        /// </summary>
        public static CameraShakeInstance UIError() {
            return new CameraShakeInstance()
                .Timed(0.08f)
                .WithAmplitude(0.25f)
                .RandomHorizontal()
                .WithFadeIn(0.01f)
                .WithFadeOut(0.05f);
        }

        // =========================
        // Player Movement
        // =========================

        /// <summary>
        /// Soft player landing.
        /// </summary>
        public static CameraShakeInstance LandLight() {
            return new CameraShakeInstance()
                .Timed(0.08f)
                .WithAmplitude(0.25f)
                .RandomVertical()
                .WithFadeIn(0.01f)
                .WithFadeOut(0.05f);
        }

        /// <summary>
        /// Heavy landing or ground pound.
        /// </summary>
        public static CameraShakeInstance LandHeavy() {
            return new CameraShakeInstance()
                .Timed(0.2f)
                .WithAmplitude(0.9f)
                .RandomVertical()
                .WithFadeIn(0.02f)
                .WithFadeOut(0.15f);
        }

        /// <summary>
        /// Fast dash or burst movement.
        /// </summary>
        public static CameraShakeInstance Dash(Vector2 direction) {
            return new CameraShakeInstance()
                .Timed(0.1f)
                .WithAmplitude(0.35f)
                .WithDirection(direction)
                .WithFadeIn(0.01f)
                .WithFadeOut(0.08f);
        }

        // =========================
        // Combat
        // =========================

        /// <summary>
        /// Light hit confirmation.
        /// </summary>
        public static CameraShakeInstance HitLight(Vector2 hitDirection) {
            return new CameraShakeInstance()
                .Timed(0.08f)
                .WithAmplitude(0.3f)
                .WithDirection(hitDirection)
                .WithFadeIn(0.01f)
                .WithFadeOut(0.06f);
        }

        /// <summary>
        /// Heavy melee or strong projectile hit.
        /// </summary>
        public static CameraShakeInstance HitHeavy(Vector2 hitDirection) {
            return new CameraShakeInstance()
                .Timed(0.18f)
                .WithAmplitude(0.8f)
                .WithDirection(hitDirection)
                .WithFadeIn(0.02f)
                .WithFadeOut(0.14f);
        }

        /// <summary>
        /// Critical hit, finisher, or powerful ability.
        /// </summary>
        public static CameraShakeInstance CriticalHit() {
            return new CameraShakeInstance()
                .Timed(0.25f)
                .WithAmplitude(1.1f)
                .RandomDirection()
                .WithFadeIn(0.02f)
                .WithFadeOut(0.2f);
        }

        // =========================
        // Environment
        // =========================

        /// <summary>
        /// Subtle background motion.
        /// Machinery, tension, large structures.
        /// </summary>
        public static CameraShakeInstance AmbientLow() {
            return new CameraShakeInstance()
                .Looping()
                .WithAmplitude(0.12f)
                .RandomVertical()
                .WithFadeIn(0.5f)
                .WithFadeOut(0.5f);
        }

        /// <summary>
        /// Sustained environmental shaking.
        /// </summary>
        public static CameraShakeInstance Earthquake() {
            return new CameraShakeInstance()
                .Looping()
                .WithAmplitude(0.6f)
                .RandomHorizontal()
                .WithFadeIn(0.4f)
                .WithFadeOut(0.6f);
        }

        /// <summary>
        /// Sudden explosion in the world.
        /// </summary>
        public static CameraShakeInstance Explosion(Vector2 worldPosition, Transform camera) {
            Vector2 dir = ((Vector2)camera.position - worldPosition).normalized;

            return new CameraShakeInstance()
                .Timed(0.35f)
                .WithAmplitude(1.3f)
                .WithDirection(dir)
                .WithFadeIn(0.03f)
                .WithFadeOut(0.25f);
        }

        // =========================
        // Drama / Transitions
        // =========================

        /// <summary>
        /// Gradual tension build-up.
        /// Boss intros, warnings.
        /// </summary>
        public static CameraShakeInstance TensionBuild() {
            return new CameraShakeInstance()
                .Timed(0.6f)
                .WithAmplitude(0.5f)
                .RandomVertical()
                .WithFadeIn(0.45f)
                .WithFadeOut(0.15f);
        }

        /// <summary>
        /// Big cinematic moment.
        /// </summary>
        public static CameraShakeInstance CinematicImpact() {
            return new CameraShakeInstance()
                .Timed(0.4f)
                .WithAmplitude(1.6f)
                .RandomDirection()
                .WithFadeIn(0.05f)
                .WithFadeOut(0.3f);
        }
    }
}
