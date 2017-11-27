using Task3.Interfaces;

namespace Task3.Billing
{
    public class Tariff : ITariff
    {
        public string Name { get; private set; }
        public double Cost { get; private set; }

        public Tariff(string name, double cost)
        {
            Name = name;
            Cost = cost;
        }
    }
}