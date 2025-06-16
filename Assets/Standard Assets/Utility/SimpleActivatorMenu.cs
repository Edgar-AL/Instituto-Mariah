using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Utility
{
    public class SimpleActivatorMenu : MonoBehaviour
    {
        // Referencia al texto UI para mostrar el nombre de la cámara activa
        public Text camSwitchButton;
        public GameObject[] objects;

        private int m_CurrentActiveObject;

        private void OnEnable()
        {
            m_CurrentActiveObject = 0;

            // Activar solo el primer objeto
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(i == m_CurrentActiveObject);
            }

            UpdateButtonLabel();
        }

        public void NextCamera()
        {
            int nextActiveObject = m_CurrentActiveObject + 1 >= objects.Length ? 0 : m_CurrentActiveObject + 1;

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(i == nextActiveObject);
            }

            m_CurrentActiveObject = nextActiveObject;
            UpdateButtonLabel();
        }

        private void UpdateButtonLabel()
        {
            if (camSwitchButton != null && objects.Length > 0)
            {
                camSwitchButton.text = objects[m_CurrentActiveObject].name;
            }
        }
    }
}
