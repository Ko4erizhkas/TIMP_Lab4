using System.Reflection;
using TIMP_Lab5;

namespace WhiteBox_POLIZ;

[TestClass]
public class WhiteBox_POLIZ_PrivateMethod_ToPostFix
{
    [TestMethod]
    public void ToPostfix_Addition_ReturnCorrectPostfix()
    {
        var target = new POLIZ("1");
        var method = typeof(POLIZ).GetMethod("ToPostfix", BindingFlags.NonPublic | BindingFlags.Instance);

        string expr = "123+32";
        string expected = "123 32 +";

        string result = (string)method.Invoke(target, new object[] { expr });

        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void ToPostfix_AdditionAndMultiplication_ReturnCorrectPostfix()
    {
        var target = new POLIZ("1");
        var method = typeof(POLIZ).GetMethod("ToPostfix", BindingFlags.NonPublic | BindingFlags.Instance);

        string expr = "(123*3) + 23";
        string expected = "123 3 *23 +";

        string result = (string)method.Invoke(target, new object[] { expr });

        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void ToPostfix_AdditionAndMultiplicationAndDivide_ReturnCorrectPostfix()
    {
        var target = new POLIZ("1");
        var method = typeof(POLIZ).GetMethod("ToPostfix", BindingFlags.NonPublic | BindingFlags.Instance);

        string expr = "(123 * 3) + 23 / (123 - 3)";
        string expected = "123 3 *23 123 3 -/+";

        string result = (string)method.Invoke(target, new object[] { expr });

        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void ToPostfix_DivideByZero_ReturnCorrectPostfix()
    {
        var target = new POLIZ("1");
        var method = typeof(POLIZ).GetMethod("ToPostfix", BindingFlags.NonPublic | BindingFlags.Instance);

        string expr = "123/0";
        string expected = "123 0 /";

        string result = (string)method.Invoke(target, new object[] { expr });

        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void ToPostfix_MultiplicationByZero_ReturnCorrectPostfix()
    {
        var target = new POLIZ("1");
        var method = typeof(POLIZ).GetMethod("ToPostfix", BindingFlags.NonPublic | BindingFlags.Instance);

        string expr = "123*0";
        string expected = "123 0 *";

        string result = (string)method.Invoke(target, new object[] { expr });

        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void ToPostfix_DivideAndMultiplicationByZero_ReturnCorrectPostfix()
    {
        var target = new POLIZ("1");
        var method = typeof(POLIZ).GetMethod("ToPostfix", BindingFlags.NonPublic | BindingFlags.Instance);

        string expr = "(123*0) / 0";
        string expected = "123 0 *0 /";

        string result = (string)method.Invoke(target, new object[] { expr });

        Assert.AreEqual(expected, result);
    }
}
