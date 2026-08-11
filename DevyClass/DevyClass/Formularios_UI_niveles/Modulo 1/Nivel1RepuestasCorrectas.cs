using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevyClass.Formularios_UI_niveles.Modulo_1
{
    // Clase que guarda el estado del nivel 1 mientras el usuario lo juega.
    // Se crea una sola vez en Nivel1 y se comparte entre todas las preguntas
    // (pregunta1, Pregunta2, Pregunta3) para que sepan cual opcion eligio
    // el usuario y cuantas respuestas correctas lleva.
    public class Nivel1RepuestasCorrectas
    {
        public int RespuestasCorrectas { get; set; } // Total de respuestas correctas (se incrementa al acertar).
        public int Pregunta1Res { get; set; }        // Opcion que eligio el usuario en la pregunta 1 (1, 2 o 3).
        public int Pregunta2Res { get; set; }        // Opcion elegida en la pregunta 2.
        public int Pregunta3Res { get; set; }        // Opcion elegida en la pregunta 3.
        public int Pregunta4Res { get; set; }        // Opcion elegida en la pregunta 4 (por ahora no se usa).
        public int progreso { get; set; } = 0;       // Porcentaje de progreso del nivel (avanza de 25 en 25).
    }
}
