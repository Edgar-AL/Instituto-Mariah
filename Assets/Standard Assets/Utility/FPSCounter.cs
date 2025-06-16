using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Utility
{
    public class FPSCounter : MonoBehaviour
    {
        const float fpsMeasurePeriod = 0.5f;
        private int m_FpsAccumulator = 0;
        private float m_FpsNextPeriod = 0;
        private int m_CurrentFps;
        const string display = "{0} FPS";
        private Text m_Text;

        public Text fpsDisplayText; // Asignar desde el Inspector (opcional)

        private void Start()
        {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;

            // Usa el asignado desde el inspector, o busca uno automáticamente en el mismo GameObject
            if (fpsDisplayText != null)
            {
                m_Text = fpsDisplayText;
            }
            else
            {
                m_Text = GetComponent<Text>();
            }
        }

        private void Update()
        {
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod)
            {
                m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
                m_FpsAccumulator = 0;
                m_FpsNextPeriod += fpsMeasurePeriod;

                if (m_Text != null)
                {
                    m_Text.text = string.Format(display, m_CurrentFps);
                }
            }
        }
    }
}
