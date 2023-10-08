using System;

class Parent
{
    public string Pole1 { get; set; }
    public int Pole2 { get; set; }
    public double Pole3 { get; set; }
    public double Pole4 { get; set; }

    public Parent() { }

    public Parent(string name, int yearOfBirth, double income)
    {
        Pole1 = name;
        Pole2 = yearOfBirth;
        Pole3 = income;
    }

    public void Print()
    {
        Console.WriteLine($"Ім'я: {Pole1}");
        Console.WriteLine($"Рік народження: {Pole2}");
        Console.WriteLine($"Дохід: {Pole3}");
        if (Pole4 > 0)
        {
            Console.WriteLine($"Податок: {Pole4}");
        }
    }

    public void Metod1(int currentYear)
    {
        int age = currentYear - Pole2;
        if (age < 17 || Pole3 < 1000)
        {
            Pole4 = 0;
        }
        else if (age >= 17 && Pole3 >= 1000 && Pole3 <= 10000)
        {
            Pole4 = 0.2 * Pole3;
        }
        else
        {
            Pole4 = 0.25 * Pole3;
        }
    }

    public void CalculateTax()
    {
        Metod1(DateTime.Now.Year);
    }
}

class Child : Parent
{
    public double Pole5 { get; set; }

    public Child(string name, int yearOfBirth, double income, double discountPercentage)
        : base(name, yearOfBirth, income)
    {
        Pole5 = discountPercentage;
    }

    public new void Metod1(int currentYear)
    {
        base.Metod1(currentYear);
        double discountAmount = Pole5 / 100 * Pole4;
        Pole4 -= discountAmount;
    }

    public void CalculateDiscountedTax()
    {
        Metod1(DateTime.Now.Year);
    }
}

class Program
{
    static void Main()
    {
        string name;
        int yearOfBirth;
        double income;
        double discountPercentage = 0;

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Write("Введіть ім'я: ");
        name = Console.ReadLine();

        Console.Write("Введіть рік народження: ");
        while (!int.TryParse(Console.ReadLine(), out yearOfBirth))
        {
            Console.WriteLine("Некоректний ввід. Введіть рік народження знову: ");
        }

        Console.Write("Введіть дохід: ");
        while (!double.TryParse(Console.ReadLine(), out income))
        {
            Console.WriteLine("Некоректний ввід. Введіть дохід знову: ");
        }

        Console.Write("Чи є знижка? (так/ні): ");
        string discountInput = Console.ReadLine().ToLower();

        Parent user = new Parent(name, yearOfBirth, income);

        if (discountInput == "так")
        {
            Console.Write("Введіть відсоток знижки: ");
            while (!double.TryParse(Console.ReadLine(), out discountPercentage))
            {
                Console.WriteLine("Некоректний ввід. Введіть відсоток знижки знову: ");
            }
            Child child = new Child(name, yearOfBirth, income, discountPercentage);
            child.CalculateDiscountedTax();
            Console.WriteLine($"Податок для користувача зі знижкою: {child.Pole4}");
        }
        else
        {
            user.CalculateTax();
            Console.WriteLine($"Податок для користувача: {user.Pole4}");
        }
    }
}
