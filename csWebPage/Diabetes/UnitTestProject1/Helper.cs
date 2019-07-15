using System;

namespace HelperNamespace
{
    public static class Helper
    {
        public static int Get_kcal(double w, double h,double g, double a,double d,int y) =>(int)(((10 * w) + (6.25 * h) - 5*y + g) * a * d);
    }
}