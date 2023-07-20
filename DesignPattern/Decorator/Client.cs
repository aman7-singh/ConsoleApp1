using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.Decorator
{
    class Client
    {
        public void Execute()
        {
            var espresso = new Espresso();
            var espressoChocolate = new Chocolate(espresso);
            var espressoChocolateAmount = espressoChocolate.Cost();
            Console.WriteLine($" --------- {espresso} - {espressoChocolate} --------- ");
            Console.WriteLine(espressoChocolateAmount); //105

            var espressoCaremel = new Caramel(espresso);
            var espressoCaremelAmount = espressoCaremel.Cost();
            Console.WriteLine($" ---------  {espresso} - {espressoCaremel} --------- ");
            Console.WriteLine(espressoCaremelAmount); //103

            var cappuccino = new Cappuccino();
            var cappuccinoCaremel = new Caramel(cappuccino);
            var cappuccinoCaremelAmount = cappuccinoCaremel.Cost();
            Console.WriteLine($" ---------  {cappuccino} - {cappuccinoCaremel} --------- ");
            Console.WriteLine(cappuccinoCaremelAmount); //153

            var cappuccinoSpcl = new Cappuccino();
            var cappuccinoSpclCaremel = new Caramel(cappuccinoSpcl);
            var cappuccinoSpclChocolate = new Chocolate(cappuccinoSpclCaremel);
            var cappuccinoSpclCaremelAmount = cappuccinoSpclChocolate.Cost();
            Console.WriteLine($" --------- {cappuccino} - {cappuccinoSpclCaremel} - {cappuccinoSpclChocolate} --------- ");
            Console.WriteLine(cappuccinoSpclCaremelAmount); //158
        }
    }
}