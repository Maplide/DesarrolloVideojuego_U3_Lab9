using NUnit.Framework;

public class GameLogicEdgeCaseTests
{
    // ===============================
    //   PRUEBAS DE INICIALIZACIÓN
    // ===============================

    [Test]
    public void Constructor_SetsMinimumObjectives_WhenInitializedWithZero()
    {
        // ARRANGE & ACT
        var gameLogic = new GameLogic(0);

        // ASSERT
        Assert.AreEqual(1, gameLogic.ObjectivesToWin, 
            "Si se inicializa con 0, el valor debe forzarse a 1.");
        Assert.AreEqual(0, gameLogic.ObjectivesCompleted);
        Assert.IsFalse(gameLogic.IsVictoryConditionMet);
    }

    [Test]
    public void Constructor_SetsMinimumObjectives_WhenInitializedWithNegativeValue()
    {
        // ARRANGE & ACT
        var gameLogic = new GameLogic(-10);

        // ASSERT
        Assert.AreEqual(1, gameLogic.ObjectivesToWin,
            "Los valores negativos deben convertirse en 1.");
        Assert.AreEqual(0, gameLogic.ObjectivesCompleted);
        Assert.IsFalse(gameLogic.IsVictoryConditionMet);
    }

    // ===============================
    //       PRUEBAS DE PROGRESO
    // ===============================

    [Test]
    public void CompleteObjective_DoesNotExceed_ObjectivesToWin()
    {
        // ARRANGE
        var gameLogic = new GameLogic(1);

        // ACT
        gameLogic.CompleteObjective(); // 1/1
        gameLogic.CompleteObjective(); // intento extra
        gameLogic.CompleteObjective(); // intento extra

        // ASSERT
        Assert.AreEqual(1, gameLogic.ObjectivesCompleted,
            "Nunca debe superar la cantidad de objetivos requeridos.");
        Assert.IsTrue(gameLogic.IsVictoryConditionMet);
    }

    [Test]
    public void CompletingObjectives_ReachesVictory_WhenCountMatches()
    {
        // ARRANGE
        var gameLogic = new GameLogic(3);

        // ACT
        gameLogic.CompleteObjective(); // 1
        gameLogic.CompleteObjective(); // 2
        gameLogic.CompleteObjective(); // 3

        // ASSERT
        Assert.IsTrue(gameLogic.IsVictoryConditionMet,
            "Debe activar victoria cuando los objetivos completados alcanzan el total.");
    }

    [Test]
    public void CompleteObjective_DoesNothing_AfterVictoryConditionMet()
    {
        // ARRANGE
        var gameLogic = new GameLogic(2);

        // ACT
        gameLogic.CompleteObjective(); // 1
        gameLogic.CompleteObjective(); // 2 → victoria
        gameLogic.CompleteObjective(); // intento extra

        // ASSERT
        Assert.AreEqual(2, gameLogic.ObjectivesCompleted,
            "No debe incrementar después de cumplirse la victoria.");
    }

    // ===============================
    //      PRUEBAS DE CONSISTENCIA
    // ===============================

    [Test]
    public void IsVictoryConditionMet_IsFalse_WhenNotEnoughObjectives()
    {
        var gameLogic = new GameLogic(5);

        gameLogic.CompleteObjective(); // 1

        Assert.IsFalse(gameLogic.IsVictoryConditionMet);
    }
}
