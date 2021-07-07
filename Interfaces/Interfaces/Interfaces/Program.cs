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

    internal class B : A
    {
        public new void M1() { Console.WriteLine("B.M1()"); }
        public new void M2() { Console.WriteLine("B.M2()"); }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            A a = new A();
            B b = new B();

            a.M1();
            a.M2();
            b.M1();
            b.M2();
            Console.ReadLine();
        }
    }
}
