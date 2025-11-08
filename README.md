# Estudio de Desarrollo: "Russ Estudio"

## Miembros del Equipo

| Apellidos y Nombres | Código | Rol | Responsabilidades |
|----------------------|--------|------|-------------------|
| **BELITO RAMIREZ MORI OCTAVIO** | 74902137 | Diseñador/a de Comportamiento (Behavior Designer) | Configuró el comportamiento de la IA en el Editor, asignó los waypoints, ajustó velocidades, radios de detección y pruebas de equilibrio de movimiento. |
| **CORONEL BURGOS JAVIER DANIEL** | 71997263 | Integrador/a y QA (Integration & QA) | Preparó el entorno de pruebas, horneó el NavMesh, validó los prefabs de enemigo y jugador, ejecutó las pruebas finales de patrulla, detección y persecución. Documentó el flujo y resultados en el informe de QA. |
| **QUISPE UBALDO ALFREDO** | 71438344 | Arquitecto/a de IA (AI Architect) | Diseñó y programó la arquitectura del sistema: AIController, AIState, PatrolState y ChaseState, aplicando los principios SOLID y el patrón State para un código limpio y mantenible. |

---

## Descripción del Hito
**Guía Práctica #12 – “La Chispa de Vida”**

En esta práctica implementamos el sistema de **Inteligencia Artificial con Patrón State**, mejorando las transiciones entre los estados de **Patrulla** y **Persecución**.  
Ajustamos parámetros clave como velocidad, radios de detección y persecución, y configuramos el **NavMesh** para un comportamiento realista del enemigo.  
El objetivo fue dar "vida" a la IA, logrando que perciba al jugador y reaccione dinámicamente dentro del entorno.

---

## Reflexión del Estudio

### Sinergia y Fricción
El mayor beneficio de trabajar en equipo fue la **complementariedad de roles**: mientras uno programaba la arquitectura, otro integraba y otro equilibraba los parámetros visuales y de comportamiento.  
El desafío principal fue la **coordinación en los commits y las escenas**, ya que cualquier cambio mal sincronizado podía romper el proyecto.  
Lo solucionamos **dividiendo tareas claramente** y usando **comunicación constante** por mensajes para evitar conflictos y mantener la versión limpia.

### El Alma de la Máquina
El parámetro que más influyó para hacer que la IA se sintiera "viva" fue el **`detectionRadius`**.  
Ajustar este valor definió el momento exacto en que el enemigo percibía al jugador, generando una sensación de atención y reacción natural.  
También fue clave equilibrarlo con **`loseSightRadius`** y la **velocidad de persecución (`chaseSpeed`)** para evitar comportamientos robóticos o impredecibles.

---

## Enlace al Repositorio
[https://github.com/Maplide/DesarrolloVideojuego_U3_Lab9](https://github.com/Maplide/DesarrolloVideojuego_U3_Lab9)

---

## Ingeniero a cargo
**Diego Alejandro Fernández Rivera**

---

## Versión Final
**Unidad 3 – Guía Práctica #12 “La Chispa de Vida”**
