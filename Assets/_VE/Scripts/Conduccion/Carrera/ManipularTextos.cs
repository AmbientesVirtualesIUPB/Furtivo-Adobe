using TMPro;
using UnityEngine;

public class ManipularTextos : MonoBehaviour
{
    public TextMeshProUGUI[] textoFinal; // Referencia al componente TextMeshProUGUI
    public TextMeshProUGUI[] textoFinalSombra; // Referencia al componente TextMeshProUGUI

    public void CambiarTextos()
    {
        for (int i = 0; i < textoFinal.Length; i++)
        {
            textoFinal[i].text = "GANASTE";
            textoFinalSombra[i].text = "GANASTE";

            // Convertir colores hexadecimales a Color
            Color color1, color2, color3, color4;
            ColorUtility.TryParseHtmlString("#00F8FF", out color1); // Inferior izquierda
            ColorUtility.TryParseHtmlString("#00FFE3", out color2); // Inferior derecha
            ColorUtility.TryParseHtmlString("#00FF35", out color3); // Superior izquierda
            ColorUtility.TryParseHtmlString("#00FF2A", out color4); // Superior derecha

            // Crear un nuevo gradiente
            VertexGradient newGradient = new VertexGradient(
                color1,   // Color de esquina inferior izquierda
                color2,   // Color de esquina inferior derecha
                color3,   // Color de esquina superior izquierda
                color4    // Color de esquina superior derecha
            );

            // Asignar el gradiente al texto
            textoFinal[i].colorGradient = newGradient;
        }
    }
}
