using TIMP_Lab5;

namespace BlackBoxTests;

[TestClass]
public class POLIZTests
{
    [TestMethod]
    public void Constructor_ValidExpression_SetsInfixAndPostfixCorrectly()
    {
        // Arrange
        string infix = "3 + 4";
        string expectedInfix = "3 + 4";
        string expectedPostfix = "3 4 +";

        // Act
        var poliz = new POLIZ(infix);

        // Assert
        Assert.AreEqual(expectedInfix, poliz.infixExpr, "infixExpr должен быть равен входному выражению");
        Assert.AreEqual(expectedPostfix, poliz.postfixExpr, "postfixExpr должен быть корректной ОПЗ");
    }

    [TestMethod]
    public void Calc_SimpleAddition_ReturnsCorrectResult()
    {
        // Arrange
        string infix = "3 + 4";
        double expected = 7.0;

        // Act
        var poliz = new POLIZ(infix);
        double result = poliz.Calc();

        // Assert
        Assert.AreEqual(expected, result, 0.0001, "Ожидался результат 7 для выражения 3 + 4");
    }

    [TestMethod]
    public void Calc_ComplexExpression_ReturnsCorrectResult()
    {
        // Arrange
        string infix = "123 / 3 + 16 * 9 / 3";
        double expected = 89.0;

        // Act
        var poliz = new POLIZ(infix);
        double result = poliz.Calc();

        // Assert
        Assert.AreEqual(expected, result, 0.0001, "Ожидался результат 89 для выражения 123 / 3 + 16 * 9 / 3");
    }

    [TestMethod]
    public void Calc_SpecificExpression_ReturnsCorrectResult()
    {
        // Arrange
        string infix = "123 / 3 + 16 * 9 / 3 - 8897";
        double expected = -8808.0;
        
        // Act
        var poliz = new POLIZ(infix);
        double result = poliz.Calc();

        // Assert
        Assert.AreEqual(expected, result, 0.0001, "Ожидался результат -8808 для выражения 123 / 3 + 16 * 9 / 3 - 8897");
    }

    [TestMethod]
    [ExpectedException(typeof(DivideByZeroException))]
    public void Calc_DivisionByZero_ThrowsException()
    {
        // Arrange
        string infix = "10 / 0";

        // Act
        var poliz = new POLIZ(infix);
        poliz.Calc();

        // Assert: Ожидается исключение DivideByZeroException
    }

    [TestMethod]
    public void Calc_UnaryMinus_ReturnsCorrectResult()
    {
        // Arrange
        string infix = "-5 + 3";
        double expected = -2.0; // ~-5 + 3 = -5 + 3 = -2

        // Act
        var poliz = new POLIZ(infix);
        double result = poliz.Calc();

        // Assert
        Assert.AreEqual(expected, result, 0.0001, "Ожидался результат -2 для выражения -5 + 3");
    }

    [TestMethod]
    public void Calc_ExpressionWithParentheses_ReturnsCorrectResult()
    {
        // Arrange
        string infix = "( 2 + 3 ) * 4";
        double expected = 20.0; // (2 + 3) = 5, 5 * 4 = 20

        // Act
        var poliz = new POLIZ(infix);
        double result = poliz.Calc();

        // Assert
        Assert.AreEqual(expected, result, 0.0001, "Ожидался результат 20 для выражения (2 + 3) * 4");
    }

    [TestMethod]
    public void ToPostfix_ComplexExpression_ReturnsCorrectPostfix()
    {
        // Arrange
        string infix = "123 / 3 + 16 * 9 / 3";
        string expectedPostfix = "123 3 /16 9 *3 /+";

        // Act
        var poliz = new POLIZ(infix);

        // Assert
        Assert.AreEqual(expectedPostfix, poliz.postfixExpr, "Ожидалась корректная ОПЗ для выражения 123 / 3 + 16 * 9 / 3");
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("5*2", "5 2 *", 10.0)]
    [DataRow("10-4", "10 4 -", 6.0)]
    [DataRow("2^3", "2 3 ^", 8.0)]
    public void Calc_VariousOperations_ReturnsCorrectResult(string infix, string expectedPostfix, double expectedResult)
    {
        // Arrange
        double tolerance = 0.0001;

        // Act
        var poliz = new POLIZ(infix);
        double result = poliz.Calc();

        // Assert
        Assert.AreEqual(expectedPostfix, poliz.postfixExpr, $"Ожидалась ОПЗ {expectedPostfix} для выражения {infix}");
        Assert.AreEqual(expectedResult, result, tolerance, $"Ожидался результат {expectedResult} для выражения {infix}");
    }
}