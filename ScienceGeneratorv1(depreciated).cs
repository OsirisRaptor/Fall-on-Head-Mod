using UnityEngine;

[KSPAddon(KSPAddon.Startup.Flight, false)]
public class ScienceGenerator : MonoBehaviour {
    private bool ragdolling = false;

    private void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.magnitude > 5f && !ragdolling) {
            ragdolling = true;
            StartCoroutine(RagdollCoroutine());
        }
    }

    private IEnumerator RagdollCoroutine() {
        KerbalEVA kerbal = FlightGlobals.ActiveVessel.FindPartModuleImplementing<KerbalEVA>();
        if (kerbal != null) {
            kerbal.Ragdoll();
            yield return new WaitForSeconds(2f); // Wait for the Kerbal to fully ragdoll
            ScienceUtil.GetExperimentSubject().GetScience(1.0f, ExperimentSituations.SrfLanded, null);
            ragdolling = false;
        }
    }
}
