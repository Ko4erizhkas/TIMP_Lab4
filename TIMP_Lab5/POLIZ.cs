namespace TIMP_Lab5;
public enum Operators
{
    Add = '+',
    Divide = '/',
    Multiply = '*',
    Subtracting = '-',

}
public class POLIZ
{
    public string infixExpr { get; private set; }
    public string postfixExpr { get; private set; }

    private readonly Dictionary<char, int> operPriority = new Dictionary<char, int>
    {
        { '(', 0},
        { '+', 1 },
        { '-', 1},
        { '*', 2},
        { '/', 2},
        { '^', 3},
        { '~', 4}, // Унарный минус
    };

    public POLIZ (string expression)
    {
        if (expression.Count() != 0)
        {
            infixExpr = expression;
            postfixExpr = ToPostfix(expression + '\r');
        }
        else throw new ArgumentException("Void expression");
        
    }
    private string GetStringNumber(string expr, ref int pos)
    {
        string strNumber = "";
        for (; pos < expr.Length; pos++)
        {
            char num = expr[pos];
            if (Char.IsDigit(num))
            {
                strNumber += num;
            }
            else
            {
                pos--;
                break;
            }
        }
        return strNumber;
    }
    private string ToPostfix(string infixExpr)
    {
        string postfixExpr = "";
        Stack<char> stack = new();

        for (int i = 0; i < infixExpr.Length; i++)
        {
            char c = infixExpr[i];

            if (Char.IsDigit(c))
            {
                postfixExpr += GetStringNumber(infixExpr, ref i) + " ";
            }
            else if (c == '(')
            {
                stack.Push(c);
            }
            else if (c == ')')
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                    postfixExpr += stack.Pop();
                stack.Pop();
            }
            else if (operPriority.ContainsKey(c))
            {
                char op = c;
                if (op == '-' && (i == 0 || (i > 1 && operPriority.ContainsKey(infixExpr[i - 1]))))
                    op = '~';

                while (stack.Count > 0 && (operPriority[stack.Peek()] >= operPriority[op]))
                    postfixExpr += stack.Pop();
                stack.Push(op);
            }
        }
        foreach (char op in stack)
            postfixExpr += op;

        return postfixExpr;
    }
    private double Execute(char op, double first, double second) => op switch
    {
        '+' => first + second,
        '-' => first - second,
        '*' => first * second,
        '/' => (first == 0) || (second == 0) ? throw new DivideByZeroException("Деление на ноль") : first / second,
        '^' => Math.Pow(first, second),
        _ => throw new ArgumentException("Неизвестный оператор")
    };

    public double Calc()
    {
        //	Стек для хранения чисел
        Stack<double> locals = new();
        //	Счётчик действий
        int counter = 0;

        //	Проходим по строке
        for (int i = 0; i < postfixExpr.Length; i++)
        {
            //	Текущий символ
            char c = postfixExpr[i];

            //	Если символ число
            if (Char.IsDigit(c))
            {
                //	Парсим
                string number = GetStringNumber(postfixExpr, ref i);
                //	Заносим в стек, преобразовав из String в Double-тип
                locals.Push(Convert.ToDouble(number));
            }
            //	Если символ есть в списке операторов
            else if (operPriority.ContainsKey(c))
            {
                //	Прибавляем значение счётчику
                counter += 1;
                //	Проверяем, является ли данный оператор унарным
                if (c == '~')
                {
                    //	Проверяем, пуст ли стек: если да - задаём нулевое значение,
                    //	еси нет - выталкиваем из стека значение
                    double last = locals.Count > 0 ? locals.Pop() : 0;

                    //	Получаем результат операции и заносим в стек
                    locals.Push(Execute('-', 0, last));
                    //	Отчитываемся пользователю о проделанной работе
                    Console.WriteLine($"{counter}) {c}{last} = {locals.Peek()}");
                    //	Указываем, что нужно перейти к следующей итерации цикла
                    //	 для того, чтобы пропустить остальной код
                    continue;
                }

                //	Получаем значения из стека в обратном порядке
                double second = locals.Count > 0 ? locals.Pop() : 0,
                first = locals.Count > 0 ? locals.Pop() : 0;

                //	Получаем результат операции и заносим в стек
                locals.Push(Execute(c, first, second));
                //	Отчитываемся пользователю о проделанной работе
                Console.WriteLine($"{counter}) {first} {c} {second} = {locals.Peek()}");
            }
        }
        return locals.Pop();
    }
}
