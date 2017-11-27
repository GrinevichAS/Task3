using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task3.Interfaces;
using Task3.ATESystem;
using Task3.Billing;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            IATE ate1 = new ATE();

            ITariff tariffFirst = new Tariff("First", 0.1);
            ITariff tariffSecond = new Tariff("Second", 0.2);

            Terminal term1 = new Terminal(1);
            Terminal term2 = new Terminal(2);
            Terminal term3 = new Terminal(3);
            Terminal term4 = new Terminal(4);

            
            term1.ConnectToPort(ate1.MakeContract(tariffFirst, term1.Id));
            term1.ConnectToPort(ate1.MakeContract(tariffFirst, term1.Id));
            term2.ConnectToPort(ate1.MakeContract(tariffSecond, term2.Id));
            term3.ConnectToPort(ate1.MakeContract(tariffSecond, term3.Id));
            term4.ConnectToPort(ate1.MakeContract(tariffFirst, term4.Id));

            Console.WriteLine("------");
            Console.WriteLine(term1.GetNumber());
            Console.WriteLine(term2.GetNumber());
            Console.WriteLine(term3.GetNumber());
            Console.WriteLine(term4.GetNumber());
            Console.WriteLine("------");

            term1.Call(term1.GetNumber());
            term1.Call(term2.GetNumber());
            term3.Call(term4.GetNumber());
            Thread.Sleep(4000);
            term1.EndCall();
            term4.EndCall();

            Console.WriteLine("------");
            term1.GetHistory();
            Console.WriteLine("------");
            term2.GetHistory();
            Console.WriteLine("------");
            term3.GetHistory();

            Console.ReadKey();

        }
    }
}
