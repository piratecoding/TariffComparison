using ConsoleTables;

class Tariff
{
    public string? TariffName { get; set; }
    public double AnnualCost { get; set; }
}

class TariffCalculation
{    public static double CalculateBasicTariff(double consumption)
    {
        var calc = 5 * 12 + 0.22 * consumption;
        return calc;
    }

    public static double CalculatePackageTariff(double consumption)
    {
        if (consumption > 4000)
        {
            var calc = 800 + 0.30 * (consumption - 4000);
            return calc;
        } 
        else
        {
            return 800;
        }
    }
}

class Helpers
{
    public static List<Tariff> SortTariffs(double basic, double package)
    {
        var list = new List<Tariff>();
        list.Add(new Tariff { TariffName = "Basic", AnnualCost = basic });
        list.Add(new Tariff { TariffName = "Package", AnnualCost = package });
        return list.OrderBy(d => d.AnnualCost).ToList();
    }

    public static ConsoleTable GenerateTable(List<Tariff> list)
    {
        ConsoleTable table = new ConsoleTable("Tariff name", "Price in euros/year");
        foreach (var item in list)
        {
            table.AddRow(item.TariffName, item.AnnualCost.ToString());
        }

        return table;
    }

    public static bool ValidateInput(string? input)
    {
        double result;
        bool isNumber = double.TryParse(input, out result);
        return isNumber;
    }
}

class TariffCalculator
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to simple electricity calculator.");

        while (true)
        {
            Console.WriteLine("Please enter your yearly consumption (kWh/year)...");

            var input = Console.ReadLine();
            var isValidInput = Helpers.ValidateInput(input);

            if (isValidInput)
            {
                double consumption = Double.Parse(input);
                double basicTariff = TariffCalculation.CalculateBasicTariff(consumption);
                double packageTariff = TariffCalculation.CalculatePackageTariff(consumption);
                var list = Helpers.SortTariffs(basicTariff, packageTariff);
                var table = Helpers.GenerateTable(list);
                Console.WriteLine(table);
                Console.WriteLine("To try again, please press enter first.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Please enter a number!");
            }
        }        
    }
}