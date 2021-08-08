using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    internal interface I1
    {
        void M1();
    }

    internal interface I2
    {
        void M2();
    }

    internal class A : I1, I2
    {
        void I1.M1() { Console.WriteLine("A.M1()"); }
        void I2.M2() { Console.WriteLine("A.M2()"); }
    }

    internal class B : A, I1, I2
    {
        void I1.M1() { Console.WriteLine("B.M1()"); }
        void I2.M2() { Console.WriteLine("B.M2()"); }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            I1 i1 = new A();
            I2 i2 = new A();

            i1.M1();
            i2.M2();

            i1 = new B();
            i2 = new B();

            i1.M1();
            i2.M2();

            Console.ReadLine();
        }
    }
}
