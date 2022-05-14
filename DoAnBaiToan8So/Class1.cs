using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnBaiToan8So
{
    internal class Class1
    {
        public int[,] BoTestSo06(int kickThuoc)
        {
            int[,] MaTran = new int[kickThuoc, kickThuoc];
            MaTran[0, 0] = 8;
            MaTran[0, 1] = 7;
            MaTran[0, 2] = 6;
            MaTran[1, 0] = 5;
            MaTran[1, 1] = 4;
            MaTran[1, 2] = 3;
            MaTran[2, 0] = 2;
            MaTran[2, 1] = 1;
            MaTran[2, 2] = 0;
            return MaTran;
        }
    }
}
