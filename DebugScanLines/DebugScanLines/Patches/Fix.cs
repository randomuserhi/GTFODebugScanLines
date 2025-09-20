using API;
using ChainedPuzzles;
using HarmonyLib;
using UnityEngine;

namespace BioScannerFix {
    [HarmonyPatch]
    internal static class Fix {
        [HarmonyPatch(typeof(CP_Holopath_Spline), nameof(CP_Holopath_Spline.GeneratePath))]
        [HarmonyPostfix]
        private static void Postfix_CP_Holopath_GeneratePath(CP_Holopath_Spline __instance) {
            DebugHolopath path = __instance.gameObject.AddComponent<DebugHolopath>();
            path.spline = __instance;


            path.totalDistance = 0.0f;
            var points = __instance.CurvySpline.controlPoints;
            if (points.Count == 0) return;

            for (int i = 1; i < points.Count; ++i) {
                path.totalDistance += (points[i].transform.position - points[i - 1].transform.position).magnitude;
            }
        }

        public class DebugHolopath : MonoBehaviour {
            public CP_Holopath_Spline? spline = null;
            public float totalDistance = 0.0f;

            public void Update() {
                if (spline == null) return;
                if (!spline.m_isVisible) return;

                var points = spline.CurvySpline.controlPoints;
                if (points.Count == 0) return;

                float d = 0.0f;
                float distance = spline.CurvyExtrusion.To * totalDistance;
                for (int i = 1; i < points.Count && d <= distance; ++i) {
                    Vector3 diff = points[i].transform.position - points[i - 1].transform.position;
                    float dist = diff.magnitude;

                    float lerp = 1.0f;
                    float diffDist = distance - d;
                    if (diffDist < dist) {
                        lerp = diffDist / dist;
                    }
                    APILogger.Warn($"lerp: {diffDist}");

                    Fig.DrawLine(points[i - 1].transform.position, points[i - 1].transform.position + diff * lerp, Color.white, 4);

                    d += dist;
                }
            }
        }
    }
}
