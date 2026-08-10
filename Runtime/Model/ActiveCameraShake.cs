using UnityEngine;

namespace GoncaloMCOliveira.CinemachineCameraShake {
    internal sealed class ActiveCameraShake {
        public CameraShakeInstance Instance;
        public float PlayTime;
        public float StopTime;
        public bool IsStopping;

        public Vector3 RuntimeDirection;
    }
}