using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Public;

public static class Quata
{
    public static int GetCurrentQuata(int dependet, int FoodCount)
    {
        int res = 0;
        if (FoodCount == 0)
            return 0;
        double div = (double)FoodCount / dependet;


        if (div > 0 && div <= 1)
        {
            res = 1;
        }
        else if (div > 1 && div <= 2)
        {
            res = 2;
        }
        else if (div > 2 && div <= 3)
        {
            res = 3;
        }
        else if (div > 3 && div <= 4)
        {
            res = 4;
        }
        else if (div > 4 && div <= 5)
        {
            res = 5;
        }
        else if (div > 5 && div <= 6)
        {
            res = 6;
        }
        else if (div > 6 && div <= 7)
        {
            res = 7;
        }
        else if (div > 7 && div <= 8)
        {
            res = 8;
        }
        else if (div > 8 && div <= 9)
        {
            res = 9;
        }
        else if (div > 9 && div <= 10)
        {
            res = 10;
        }
        else
            res = 11;
        return res;
    }
}
