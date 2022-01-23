using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie_Calc_Wpf.Models
{
    static class Calculations
    {
        public static double Add(double a, double b) => a + b;

        public static double Sub(double a, double b) => a - b;

        public static double Sqrt(double a) => Math.Round(Math.Sqrt(a), 10);

        public static double Sqr(double a) => Math.Round(a * a, 10);

        public static double DivOne(double a) => 1 / a;

        public static double Mult(double a, double b) => a * b;

        public static double Div(double a, double b) => Math.Round(a / b, 10);

        public static double Qube(double a) => Math.Round(a * a * a, 10);
    }
}
