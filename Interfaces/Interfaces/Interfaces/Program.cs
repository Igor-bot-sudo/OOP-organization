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
        public void M1() { Console.WriteLine("A.M1()"); }
        public void M2() { Console.WriteLine("A.M2()"); }
    }

    internal class B : A, I2
    {
        public new void M1() { Console.WriteLine("B.M1()"); }
        public new void M2() { Console.WriteLine("B.M2()"); }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            I1 i1 = new A();
            I2 i2 = new B();

            i1.M1();
            i2.M2();

            Console.ReadLine();
        }
    }
}
