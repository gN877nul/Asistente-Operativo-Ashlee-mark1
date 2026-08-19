# Asistente-Operativo-Ashlee-mark1
A.S.H.L.E.E.

Asistente de inteligencia artificial en consola, desarrollado en C# / .NET, pensado como el "sistema operativo" de un traje/armadura tipo Iron Man construido en la vida real. Inspirado en J.A.R.V.I.S.

Proyecto en desarrollo activo. Este README documenta el estado actual del prototipo en consola.

-Características actuales
Arquitectura modular: núcleo, comandos, protocolos y voz separados en capas independientes.
Interpretación de comandos por teclado y por voz, usando la misma lógica compartida.
Reconocimiento de voz offline (sin internet, sin API keys) con Vosk, usando un modelo en español.
Síntesis de voz (texto a voz) con System.Speech.Synthesis: ASHLEE responde en voz alta después de ejecutar comandos, y narra pasos importantes mientras un protocolo se ejecuta.
Prevención de retroalimentación de audio: el micrófono se "silencia" automáticamente mientras ASHLEE está hablando, para no reconocerse a sí misma como un comando.
Sistema de protocolos extensible: cada protocolo (encendido, diagnóstico, seguridad...) es una clase independiente que implementa una interfaz común, fácil de ampliar.
-Comandos disponibles
Comando	Qué hace
hola	Saludo simple
estado	Reporta el estado del sistema y cuántos protocolos hay cargados
iniciar	Ejecuta el protocolo ARMOR_STARTUP (energía, sensores, actuadores)
diagnostico	Ejecuta el protocolo DIAGNOSTIC (chequeo completo de sistemas)
seguridad	Ejecuta el protocolo SECURITY (verificación de identidad y accesos)
protocolos	Lista todos los protocolos registrados, con su descripción
ayuda	Muestra la lista de comandos disponibles
apagar	Apaga el sistema

Los mismos 8 comandos funcionan igual por teclado (escribiéndolos) o por voz (diciéndolos en voz alta).

 Arquitectura del proyecto
A.S.H.L.E.E.
│
├── Program.cs                  → punto de entrada de la aplicación
│
├── Core/
│   └── AshleeSystem.cs         → orquesta el bucle principal (teclado + voz)
│
├── Commands/
│   └── CommandProcessor.cs     → interpreta y ejecuta cada uno de los 8 comandos
│
├── Protocolos/
│   ├── IProtocol.cs            → contrato común de todo protocolo
│   ├── ProtocolManager.cs      → registra y ejecuta protocolos por nombre
│   ├── StartupProtocol.cs      → protocolo ARMOR_STARTUP
│   ├── DiagnosticProtocol.cs   → protocolo DIAGNOSTIC
│   └── SecurityProtocol.cs     → protocolo SECURITY
│
└── Voice/
    ├── VoiceController.cs      → reconocimiento de voz offline (Vosk)
    └── SpeechService.cs        → síntesis de voz / respuestas habladas

Flujo de un comando:

Teclado ─┐
         ├──► CommandProcessor ──► ProtocolManager ──► Protocolo específico
Voz ─────┘                                                     │
                                                                 ▼
                                                          SpeechService
                                                        (respuesta hablada)
Tecnologías
.NET 10 (C#)
Vosk — reconocimiento de voz offline
NAudio — captura de audio del micrófono
System.Speech.Synthesis — síntesis de voz (texto a voz), usando las voces instaladas en Windows

-Cómo correrlo
Requisitos
Windows (el proyecto usa APIs de voz específicas de Windows)
.NET 10 SDK
Un micrófono configurado como dispositivo de entrada predeterminado
Configuración
Clona el repositorio y abre la solución en Visual Studio.

Descarga el modelo de voz en español vosk-model-small-es-0.42 desde alphacephei.com/vosk/models.
Descomprímelo dentro de una carpeta Model/ en la raíz del proyecto, de forma que quede:
   Model/vosk-model-small-es-0.42/am/
   Model/vosk-model-small-es-0.42/conf/
   Model/vosk-model-small-es-0.42/graph/
   
Restaura los paquetes NuGet (Vosk, NAudio, System.Speech) — Visual Studio lo hace automáticamente al compilar.
Ejecuta el proyecto. Deberías ver:
   [VOZ] Reconocedores instalados en este equipo:
   ...
   
   ASHLEE ONLINE.

   
Escribe o di cualquiera de los 8 comandos.
-Roadmap

 Migrar la interfaz de consola a una interfaz gráfica (WPF) tipo HUD
 Añadir más protocolos (mantenimiento, emergencia, vuelo, etc.)
 Integrar hardware real (Arduino / ESP32) para sensores y actuadores
 Explorar síntesis de voz más natural (Piper TTS) para hardware embebido
 Portar el reconocimiento de voz a Raspberry Pi / Linux
