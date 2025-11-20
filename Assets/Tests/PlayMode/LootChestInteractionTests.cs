#if UNITY_EDITOR

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LootChestInteractionTests
{
    // Las pruebas de Play Mode usan corrutinas con [UnityTest]
    [UnityTest]
    public IEnumerator LootChest_Interact_OpensChestAndBecomesNonInteractable()
    {
        // ARRANGE: Creamos un cofre en la escena de prueba
        var chestPrefab = new GameObject();
        
        // Añadimos el componente LootChestController simulado
        var lootChest = chestPrefab.AddComponent<LootChestController>(); 
        
        // ACT: Primera interacción
        lootChest.Interact();
        
        // ASSERT: El cofre debe estar abierto
        Assert.IsTrue(lootChest.IsOpened, 
            "El cofre debería estar abierto después de la primera interacción.");

        // Esperar un frame (buena práctica en pruebas con coroutines)
        yield return null;

        // ACT 2: Segunda interacción
        lootChest.Interact();

        // ASSERT 2: El cofre debe permanecer abierto
        Assert.IsTrue(lootChest.IsOpened, 
            "El cofre debería permanecer abierto después de la segunda interacción.");
    }
}

#endif
