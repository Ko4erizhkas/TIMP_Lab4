
namespace TIMP_Lab5;
internal class Program
{
    static void Main(string[] args)
    {
        string expression = Convert.ToString(Console.ReadLine());
        POLIZ poliz = new POLIZ(expression);

        Console.WriteLine(poliz.Calc());
    }
}
