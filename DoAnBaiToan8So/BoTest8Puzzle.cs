using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnBaiToan8So
{
    public class BoTest8Puzzle
    {


        // Tạo bộ Test cho bài 8 Puzzle
        public int[,] BoTest8So(int kt)
        {
            int[,] MaTran = new int[kt, kt];
            MaTran[0, 0] = 1;
            MaTran[0, 1] = 2;
            MaTran[0, 2] = 3;
            MaTran[1, 0] = 8;
            MaTran[1, 1] = 0;
            MaTran[1, 2] = 4;
            MaTran[2, 0] = 7;
            MaTran[2, 1] = 6;
            MaTran[2, 2] = 5;

            // tập ListMT lưu lại các hướng đã đi để đảm bảo sinh ra hướng đi mới không trùng lặp
            List<int[,]> ListMT = new List<int[,]>();
            int n = 3;
            int[,] Temp = new int[n, n];
            Array.Copy(MaTran, Temp, MaTran.Length);
            ListMT.Add(Temp);
            int h = 1, c = 1;  // Vị trí của 0
            Random rd = new Random();
            int m = rd.Next(10, 100);// Số lần đảo lộn
            int t = rd.Next(1, 5);// Hướng di chuyển

            // số lần lặp được lấy random từ đó số lượng hướng đi sẽ thay đổi theo
            for (int r = 0; r < m; r++)
            {
                // đi lên trên với t =1
                if (h > 0 && h <= n - 1 && t == 1)
                {
                    MaTran[h, c] = MaTran[h - 1, c];
                    MaTran[h - 1, c] = 0;
                    //  Check xem MaTran đã có trong ListMT hay chưa, nếu chưa thì lưu nó vào ListMT và tiếp tục
                    if (!MaTranDaSinh(MaTran, ListMT))
                    {
                        h--;
                        Temp = new int[n, n];
                        Array.Copy(MaTran, Temp, MaTran.Length);
                        ListMT.Add(Temp);
                    }
                    else
                    {
                        MaTran[h - 1, c] = MaTran[h, c];
                        MaTran[h, c] = 0;
                    }
                }
                t = rd.Next(1, 5);

                //đi sang trái với t=2
                if (c > 0 && c <= n - 1 && t == 2)
                {
                    MaTran[h, c] = MaTran[h, c - 1];
                    MaTran[h, c - 1] = 0;
                    if (!MaTranDaSinh(MaTran, ListMT))
                    {
                        c--;
                        Temp = new int[n, n];
                        Array.Copy(MaTran, Temp, MaTran.Length);
                        ListMT.Add(Temp);
                    }
                    else
                    {
                        MaTran[h, c - 1] = MaTran[h, c];
                        MaTran[h, c] = 0;
                    }
                }

                t = rd.Next(1, 5);
                //đi xuống giưới với t=3
                if (h < n - 1 && h >= 0 && t == 3)
                {
                    MaTran[h, c] = MaTran[h + 1, c];
                    MaTran[h + 1, c] = 0;
                    if (!MaTranDaSinh(MaTran, ListMT))
                    {
                        h++;
                        Temp = new int[n, n];
                        Array.Copy(MaTran, Temp, MaTran.Length);
                        ListMT.Add(Temp);
                    }
                    else
                    {
                        MaTran[h + 1, c] = MaTran[h, c];
                        MaTran[h, c] = 0;
                    }
                }

                t = rd.Next(1, 5);
                //đi sang phải với t = 4
                if (c < n - 1 && c >= 0 && t == 4)
                {
                    MaTran[h, c] = MaTran[h, c + 1];
                    MaTran[h, c + 1] = 0;
                    if (!MaTranDaSinh(MaTran, ListMT))
                    {
                        c++;
                        Temp = new int[n, n];
                        Array.Copy(MaTran, Temp, MaTran.Length);
                        ListMT.Add(Temp);
                    }
                    else
                    {
                        MaTran[h, c + 1] = MaTran[h, c];
                        MaTran[h, c] = 0;
                    }
                }
            }

            // Trả về MaTra đảo lộn cuối cùng trong dánh sách ListMT
            int MT = ListMT.Count - 1;
            return ListMT[MT];
        }



        //So sánh nếu ma trận A đã có trang danh sách ListMT thì trả về true
        bool MaTranDaSinh(int[,] A, List<int[,]> ListMT)
        {
            for (int i = 0; i < ListMT.Count; i++)
                if (MaTranBangNhau(A, ListMT[i]))
                    return true;
            return false;
        }



        // So sánh hai ma trận có bằng nhau hay không
        bool MaTranBangNhau(int[,] A, int[,] B)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (A[i, j] != B[i, j])
                        return false;
            return true;
        }
    }
}
