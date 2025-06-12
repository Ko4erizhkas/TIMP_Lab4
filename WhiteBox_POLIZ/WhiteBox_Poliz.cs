using System.Reflection;
using TIMP_Lab4;
using TIMP_Lab5;
namespace WhiteBox_POLIZ;

[TestClass]
public sealed class WhiteBox_Poliz
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Constructor_ReturnArgumentException()
    {
        var str = "";
        POLIZ poliz = new POLIZ(str);
    }
    [TestMethod]
    public void ReturnCorrectProperty_Infix()
    {
        var infixstr = "3+4";
        POLIZ poliz = new POLIZ(infixstr);

        Assert.AreEqual("3+4", infixstr);
    }
    [TestMethod]
    public void ReturnCorrectProperty_Postfix()
    {
        var infixstr = "3+4";
        POLIZ poliz = new POLIZ(infixstr);

        Assert.AreEqual("3 4 +", poliz.postfixExpr);
    }
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ReturnException_EnterLeftBracesWithoutRight()
    {
        string str = "342(";
        var poliz = new POLIZ(str);

        poliz.Calc();
        // Ожидается ArgumentException
    }
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ReturnException_EnterRightBracesWithoutLeft()
    {
        string str = "342)";
        var poliz = new POLIZ(str);

        poliz.Calc();
        // Ожидается ArgumentException
    }
    [TestMethod]
    [ExpectedException(typeof(DivideByZeroException))]
    public void ReturnException_DivideByZero_FirstNumber()
    {
        string expr = "0/3";
        var poliz = new POLIZ(expr);

        poliz.Calc();
        // Ожидается DivideByZeroException
    }
    [TestMethod]
    [ExpectedException(typeof(DivideByZeroException))]
    public void ReturnException_DivideByZero_SecondNumber()
    {
        string expr = "1/0";
        var poliz = new POLIZ(expr);

        poliz.Calc();
        // Ожидается DivideByZeroException
    }
}
