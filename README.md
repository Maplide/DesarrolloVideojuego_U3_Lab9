# 🧠 Práctica 12 – La Chispa de Vida  
### IA con Máquinas de Estado en un Entorno de Estudio

## 🎯 Descripción del Proyecto
Este proyecto implementa un sistema de **Inteligencia Artificial (IA)** en Unity, basado en el **Patrón de Diseño State**, para dotar de comportamiento dinámico a un enemigo dentro de un entorno 3D.  
La IA utiliza **NavMesh** para desplazarse de forma autónoma, alternando entre estados de **Patrulla** y **Persecución**, reaccionando de manera coherente a la posición del jugador.  

El resultado es una IA modular, escalable y fácil de mantener, capaz de representar decisiones simples de vigilancia y respuesta ante estímulos dentro del mundo del juego.


---

## 👥 Equipo de Desarrollo

| Apellidos y Nombres | Código | Rol | Responsabilidades |
|----------------------|---------|-----|--------------------|
| **BELITO RAMIREZ MORI OCTAVIO** | 74902137 | 🎨 *Diseñador/a de Comportamiento (Behavior Designer)* | Configuró el comportamiento de la IA en el Editor, asignó los waypoints, ajustó velocidades, radios de detección y pruebas de equilibrio de movimiento. |
| **CORONEL BURGOS JAVIER DANIEL** | 71997263 | 🧩 *Integrador/a y QA (Integration & QA)* | Preparó el entorno de pruebas, horneó el NavMesh, validó los prefabs de enemigo y jugador, ejecutó las pruebas finales de patrulla, detección y persecución. Documentó el flujo y resultados en el informe de QA. |
| **QUISPE UBALDO ALFREDO** | 71438344 | 💻 *Arquitecto/a de IA (AI Architect)* | Diseñó y programó la arquitectura del sistema: `AIController`, `AIState`, `PatrolState` y `ChaseState`, aplicando los principios SOLID y el patrón State para un código limpio y mantenible. |

---

## 🧩 Características Principales
- Implementación del **Patrón State** aplicado a Inteligencia Artificial.
- Uso del **sistema de navegación (NavMeshAgent)** para el movimiento autónomo del enemigo.
- Estados principales:
  - `PatrolState` → El enemigo patrulla entre waypoints.
  - `ChaseState` → Persigue al jugador cuando entra en su radio de detección.
  - Retorno automático al estado de patrulla al perder de vista al jugador.
- Arquitectura desacoplada, extensible a nuevos estados (`AttackState`, `StunState`, etc.).
- Configuración editable desde el **Inspector**: velocidades, radios y puntos de patrulla.
- Flujo de colaboración con roles definidos en un entorno de “Estudio de Juego”.

---

## 🧠 Reflexión Final del Equipo
El desarrollo permitió integrar conocimientos de **arquitectura de software**, **navegación autónoma** y **trabajo colaborativo** bajo una estructura profesional.  
El **Patrón State** demostró su potencia al mantener la IA modular, simple y extensible.  
Como equipo, asumimos roles específicos que simulan el flujo real de un estudio de videojuegos, fortaleciendo la comunicación, la planificación y la validación de resultados.  

> “Una IA bien diseñada no solo reacciona, **da vida al mundo del juego**.”

---
