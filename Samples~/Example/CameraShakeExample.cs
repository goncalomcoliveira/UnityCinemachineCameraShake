using GoncaloMCOliveira.CinemachineCameraShake;
using UnityEngine;

public class CameraShakeExample : MonoBehaviour {
    
    public void TestUITap() {
        CameraShakePresets.UITap().Play();
    }
    
    public void TestTensionBuild() {
        CameraShakePresets.TensionBuild().Play();
    }
    
    public void TestLandHeavy() {
        CameraShakePresets.LandHeavy().Play();
    }
    
}