using MelonLoader;
using UnityEngine;
using System;
using System.Collections.Generic;

[assembly: MelonInfo(typeof(NpcYFix.NpcYFixMod), "NPC Y Fix", "11.1.0", "Anonymous")]
[assembly: MelonGame(null, null)]

namespace NpcYFix
{
    public class NpcYFixMod : MelonMod
    {
        static int ticks = 0;
        static HashSet<string> animated = new HashSet<string>();

        const float FIXED_Y = -10.75f;
        const float SPACING = 2.5f;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("=== NPC Y Fix v11.1.0 ===");
        }

        public override void OnUpdate()
        {
            if (++ticks % 15 != 0) return;

            try
            {
                var all = UnityEngine.Object.FindObjectsOfType<Transform>();

                var npcs = new List<Transform>();
                foreach (var t in all)
                {
                    if (t == null) continue;
                    var go = t.gameObject;
                    if (go == null || !go.activeInHierarchy) continue;
                    string n = go.name;
                    if (!n.StartsWith("Role") || !n.Contains("(Clone)")) continue;
                    npcs.Add(t);
                }

                if (npcs.Count == 0) return;

                npcs.Sort((a, b) => string.Compare(a.gameObject.name, b.gameObject.name));

                int count = npcs.Count;
                float startX = -(count - 1) * SPACING / 2f;

                for (int i = 0; i < count; i++)
                {
                    var t = npcs[i];
                    string name = t.gameObject.name;
                    var pos = t.position;

                    if (Mathf.Abs(pos.x) < 0.5f && Mathf.Abs(pos.y) < 0.5f)
                    {
                        float x = startX + i * SPACING;
                        t.position = new Vector3(x, FIXED_Y, 0f);
                        MelonLogger.Msg($"PLACED {name} at ({x:F2},{FIXED_Y})");
                    }

                    if (!animated.Contains(name))
                        TryAnimate(t, name);
                }
            }
            catch (Exception e) { MelonLogger.Error($"EX: {e}"); }
        }

        void TryAnimate(Transform npcRoot, string name)
        {
            try
            {
                Transform spineChild = null;
                for (int i = 0; i < npcRoot.childCount; i++)
                {
                    var c = npcRoot.GetChild(i);
                    if (c != null && c.gameObject.name.Contains("Spine")) { spineChild = c; break; }
                }
                if (spineChild == null) return;

                var sa = spineChild.gameObject.GetComponent<Il2CppSpine.Unity.SkeletonAnimation>();
                if (sa == null || sa.state == null) return;

                string[] prefs = { "idle", "moves_idle", "L_idle", "R_idle" };
                foreach (var anim in prefs)
                {
                    try { sa.state.SetAnimation(0, anim, true); animated.Add(name); return; }
                    catch { }
                }
            }
            catch { }
        }
    }
}
