using System.Reflection;
using System.Reflection.Metadata;
using TIMP_Lab5;

namespace WhiteBox_POLIZ;

[TestClass]
public class WhiteBox_POLIZ_PrivateMethod_GetStringNumber
{

    [TestMethod]
    public void GetStringNumber_SingleOperandsWithOneDigits_ReturnsCorrectlyPosition()
    {
        // Arrange
        var target = new POLIZ("1");
        var method = typeof(POLIZ).GetMethod("GetStringNumber", BindingFlags.NonPublic | BindingFlags.Instance);

        string expr = "123+";
        int pos = 0;
        object[] parameters = new object[] { expr, pos };

        // Act
        string result = (string)method.Invoke(target, parameters);

        // Assert
        Assert.AreEqual("123", result);
        Assert.AreEqual(2, (int)parameters[1]);

    }
    [TestMethod]
    public void GetStringNumber_ValidDigits_ParsesCorrectly()
    {
        // Arrange
        var target = new POLIZ("1");
        var method = typeof(POLIZ).GetMethod("GetStringNumber", BindingFlags.NonPublic | BindingFlags.Instance);

        string expr = "123+456";
        int pos = 0;
        object[] parameters = new object[] { expr, pos };

        // Act
        string result = (string)method.Invoke(target, parameters);

        // Assert
        Assert.AreEqual("123", result);
        Assert.AreEqual(2, (int)parameters[1]);
    }
    [TestMethod]
    public void GetStringNumber_SingleOperatorWithoutDigits_ReturnsZeroPosition()
    {
        var target = new POLIZ("1");
        var method = typeof(POLIZ).GetMethod("GetStringNumber", BindingFlags.NonPublic | BindingFlags.Instance);

        string expr = "*";
        int pos = 0;
        object[] parameters = new object[] { expr, pos };

        string result = (string)method.Invoke(target, parameters);

        Assert.AreEqual("", result);
        Assert.AreEqual(-1, (int)parameters[1]);
    }
    [TestMethod]
    public void GetStringNumber_ExpressionWithLongNumbers_ReturnCorrectlyPosition()
    {
        var target = new POLIZ("1");
        var method = typeof(POLIZ).GetMethod("GetStringNumber", BindingFlags.NonPublic | BindingFlags.Instance);

        string expr = "129382624974846387476239*34";
        int pos = 0; // 19
        object[] parameters = new object[] { expr, pos};

        string result = (string)method.Invoke(target, parameters);

        Assert.AreEqual("129382624974846387476239", result);
        Assert.AreEqual(23, (int)parameters[1]);
    }

}
