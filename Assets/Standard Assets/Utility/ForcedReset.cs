using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // Si se presiona el botón asignado para resetear
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            // Recargar la escena actual de forma asíncrona
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}
