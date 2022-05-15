using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnBaiToan8So
{

    // Khai báo  NODE
    public class Node
    {
        public int[,] MaTran8So;
        public int SoViTriSai;
        public int ChiSoNode;
        public int ChaNode;
        public int ChiPhiDenNode;
    }
    public class ThuatToanAStar
    {
        private int ChiSo = 0;// chỉ số của Node sẽ tăng sau mỗi lần sinh ra 1 node
        private int fn = 0;// Sau mỗi lần sinh ra Node con thì chi phí các node tăng 1 



        //
        public Stack<int[,]> TimPhuongAn(int[,] MaTran, int n)
        {
            Stack<int[,]> SepKetQua = new Stack<int[,]>();
            List<Node> Close = new List<Node>();
            List<Node> Open = new List<Node>();

            //khai báo và khởi tạo cho node đầu tiên
            Node TamSo = new Node();
            TamSo.MaTran8So = MaTran;
            TamSo.SoViTriSai = ViTriSai(MaTran);
            TamSo.ChiSoNode = 0;
            TamSo.ChaNode = -1;
            TamSo.ChiPhiDenNode = 0;
            //cho trạng thái đầu tiên vào Open;
            Open.Add(TamSo);

            int t = 0;
            int m = Open.Count; // Số lượng Node trong Open
            while (m != 0)
            {
                // chọn node tốt nhất trong tập Open và chuyển nó sang Close
                TamSo = new Node();
                TamSo = Open[t];
                Open.Remove(TamSo);
                Close.Add(TamSo);

                // Nếu node có số vị trí sai là 0, tức là đích thì thoát
                if (TamSo.SoViTriSai == 0)
                {
                    break;
                }
                else
                {
                    // Sinh ra hướng đi có thể có ở vị trí node hiện tại
                    List<Node> ListHuongDi = new List<Node>();
                    ListHuongDi = HuongDi(TamSo);

                    int hd = ListHuongDi.Count;
                    for (int i = 0; i < hd; i++)
                    {
                        // hướng đi không thuộc Open và Close
                        if (!NodeTrungNhau(ListHuongDi[i], Open) && !NodeTrungNhau(ListHuongDi[i], Close))
                        {
                            Open.Add(ListHuongDi[i]);
                        }
                        else
                        {   // nếu hướng đi thuộc Open
                            if (NodeTrungNhau(ListHuongDi[i], Open))
                            {
                                /*nếu hướng đi đó tốt hơn thì sẽ được cập nhật lại, 
                                ngược lại thì sẽ không cập nhật*/
                                ChiPhiTotHon(ListHuongDi[i], Open);
                            }
                            else
                            {
                                //nếu hướng đi thuộc Close
                                if (NodeTrungNhau(ListHuongDi[i], Close))
                                {
                                    /*nếu hướng đi đó tốt hơn thì sẽ được cập nhật lại, 
                                    ngược lại thì sẽ không cập nhật và chuyển từ Close sang Open*/
                                    if (ChiPhiTotHon(ListHuongDi[i], Close))
                                    {
                                        Node temp = new Node();
                                        temp = LayNodeTrungTrongListMT(ListHuongDi[i], Close);
                                        Close.Remove(temp);
                                        Open.Add(temp);
                                    }
                                }
                            }
                        }
                    }
                    //chọn vị trí có phí tốt nhất trong Open
                    t = ViTriTotNhat(Open);
                }
            }

            //truy vét kết quả tỏng tập Close
            SepKetQua = TruyVet(Close);
            return SepKetQua;
        }



        //truy vét phương án đường đi trong tập ListNODE
        Stack<int[,]> TruyVet(List<Node> ListNODE)
        {
            Stack<int[,]> PhuongAn = new Stack<int[,]>();
            int n = ListNODE.Count - 1;
            int t = ListNODE[n].ChaNode;
            Node temp = new Node();
            PhuongAn.Push(ListNODE[n].MaTran8So);

            // Tìm phương án thông qua dò chan ode cuối cùng và chỉ số node đầu tiên
            while (t != -1)
            {
                for (int i = 0; i < n; i++)
                {
                    if (t == ListNODE[i].ChiSoNode)
                    {
                        temp = ListNODE[i];
                        break;
                    }
                }
                PhuongAn.Push(temp.MaTran8So);
                t = temp.ChaNode;
            }
            return PhuongAn;
        }



        // Hàm sinh ra các hướng đi từ một node sinh ra các node con
        // Trả về danh sách các hướng đi
        List<Node> HuongDi(Node TamSo)
        {
            int n = 3;
            List<Node> ListHuongDi = new List<Node>();


            // Xác định vị trí 0
            int h = 0;
            int c = 0;
            bool CheckViTri0 = false;
            for (h = 0; h < n; h++)
            {
                for (c = 0; c < n; c++)
                    if (TamSo.MaTran8So[h, c] == 0)
                    {
                        CheckViTri0 = true;
                        break;
                    }

                if (CheckViTri0) break;
            }

            Node Temp = new Node();
            Temp.MaTran8So = new int[n, n];
            //Copy mảng Ma trận sang mảng ma trận tạm
            Array.Copy(TamSo.MaTran8So, Temp.MaTran8So, TamSo.MaTran8So.Length);
            fn++;// tăng chi phí của node con lên 1 đơn vị

            // A Star
            // Xét các hướng đi theo 4 hướng: trên, dưới, phải, trái             
            // Đi lên
            if (h > 0 && h < n)
            {
                // thay đổi hướng đi của ma trận
                Temp.MaTran8So[h, c] = Temp.MaTran8So[h - 1, c];
                Temp.MaTran8So[h - 1, c] = 0;

                // cập nhật lại thông số của node
                Temp.SoViTriSai = ViTriSai(Temp.MaTran8So);
                ChiSo++;
                Temp.ChiSoNode = ChiSo;
                Temp.ChaNode = TamSo.ChiSoNode;
                // F() = G + H  trong đó G là số bước, H là số vị trí sai
                Temp.ChiPhiDenNode = fn + Temp.SoViTriSai;
                ListHuongDi.Add(Temp);

                //sau khi thay đổi ma trận thì copy lại ma trận cha cho MaTran để xét trường hợp tiếp theo
                Temp = new Node();
                Temp.MaTran8So = new int[n, n];
                Array.Copy(TamSo.MaTran8So, Temp.MaTran8So, TamSo.MaTran8So.Length);
            }
            
            // Đi xuống
            if (h < n - 1 && h >= 0)
            {
                // thay đổi hướng đi của ma trận
                Temp.MaTran8So[h, c] = Temp.MaTran8So[h + 1, c];
                Temp.MaTran8So[h + 1, c] = 0;
                // cập nhật lại thông số của node
                Temp.SoViTriSai = ViTriSai(Temp.MaTran8So);
                ChiSo++;
                Temp.ChiSoNode = ChiSo;
                Temp.ChaNode = TamSo.ChiSoNode;
                Temp.ChiPhiDenNode = fn + Temp.SoViTriSai;
                ListHuongDi.Add(Temp);

                //sau khi thay đổi ma trận thì copy lại ma trận cha cho MaTran để xét trường hợp tiếp theo
                Temp = new Node();
                Temp.MaTran8So = new int[n, n];
                Array.Copy(TamSo.MaTran8So, Temp.MaTran8So, TamSo.MaTran8So.Length);
            }

            // Xét cột dọc bắt đầu từ cột thứ 2 trở đi
            // Qua trái
            if (c > 0 && c <= n )
            {
                Temp.MaTran8So[h, c] = Temp.MaTran8So[h, c - 1];
                Temp.MaTran8So[h, c - 1] = 0;
                Temp.SoViTriSai = ViTriSai(Temp.MaTran8So);
                ChiSo++;
                Temp.ChiSoNode = ChiSo;
                Temp.ChaNode = TamSo.ChiSoNode;
                Temp.ChiPhiDenNode = fn + Temp.SoViTriSai;
                ListHuongDi.Add(Temp);
                Temp = new Node();
                Temp.MaTran8So = new int[n, n];
                Array.Copy(TamSo.MaTran8So, Temp.MaTran8So, TamSo.MaTran8So.Length);
            }

            // Xét cột dọc bắt đầu từ cột cuối cùng -1 trở xuống
            // Qua phải
            if (c < n - 1 && c >= 0)
            {
                Temp.MaTran8So[h, c] = Temp.MaTran8So[h, c + 1];
                Temp.MaTran8So[h, c + 1] = 0;
                Temp.SoViTriSai = ViTriSai(Temp.MaTran8So);
                ChiSo++;
                Temp.ChiSoNode = ChiSo;
                Temp.ChaNode = TamSo.ChiSoNode;
                Temp.ChiPhiDenNode = fn + Temp.SoViTriSai;
                ListHuongDi.Add(Temp);
            }

            return ListHuongDi;
        }



        // Chọn vị trí có chi phí tốt nhất trong ListNODE
        // Trả về vị trí tốt nhất (ít vị trí sai nhất)
        int ViTriTotNhat(List<Node> ListNODE)
        {
            if (ListNODE.Count != 0)
            {
                Node min = new Node();
                min = ListNODE[0];
                int vitri = 0;
                int n = ListNODE.Count; // Tổng số Node có trong List

                for (int i = 1; i < n; i++)
                    if (min.SoViTriSai > ListNODE[i].SoViTriSai)
                    {
                        min = ListNODE[i];
                        vitri = i;
                    }
                    else
                    {
                        if (min.SoViTriSai  == ListNODE[i].SoViTriSai)
                        {
                            if (min.ChiPhiDenNode > ListNODE[i].ChiPhiDenNode)
                            {
                                min = ListNODE[i];
                                vitri = i;
                            }
                        }
                    }
                return vitri;
            }
            return 0;
        }



        // So sánh chi phí của hai node
        bool ChiPhiTotHon(Node TamSo, List<Node> List8So)
        {
            for (int i = 0; i < 3; i++)
                if (MaTranBangNhau(List8So[i].MaTran8So, TamSo.MaTran8So))
                {
                    if (TamSo.ChiPhiDenNode < List8So[i].ChiPhiDenNode)
                    {
                        List8So[i].ChaNode = TamSo.ChaNode;// cập nhật lại cha của hướng đi
                        List8So[i].ChiPhiDenNode = TamSo.ChiPhiDenNode;// cập nhật lại chi phí đường đi
                        return true;
                    }
                    else 
                        return false;
                }
            return false;
        }



        // Lấy ra node bị trùng trong tập Close
        Node LayNodeTrungTrongListMT(Node TamSo, List<Node> List8So)
        {
            Node Trung = new Node();
            for (int i = 0; i < List8So.Count; i++)
                if (MaTranBangNhau(List8So[i].MaTran8So, TamSo.MaTran8So))
                {
                    Trung = List8So[i];
                    break;
                }
            return Trung;
        }



        // So sánh node này có trùng với 1 node trong danh sách các node khác không
        bool NodeTrungNhau(Node TamSo, List<Node> List8So)
        {
            for (int i = 0; i < List8So.Count; i++)
                if (MaTranBangNhau(List8So[i].MaTran8So, TamSo.MaTran8So))
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



        // Trả về số mảnh nằm sai vị trí 
        // Hàm Heuristic
        int ViTriSai(int[,] MaTran)
        {
            int dung = 0;
            int t = 0;

            // Dò xem có bao nhiêu vị trí đúng trong mảng
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                    for (int j = 0; j < 3; j++)
                    {
                        t++;
                        if (MaTran[i, j] == t)
                            dung++;
                    }
                else
                {
                    if (MaTran[1, 2] == 4)
                        dung++;
                    if (MaTran[2, 2] == 5)
                        dung++;
                    if (MaTran[2, 1] == 6)
                        dung++;
                    if (MaTran[2, 0] == 7)
                        dung++;
                    if (MaTran[1, 0] == 8)
                        dung++;

                    break;
                }
            }
            return 8 - dung;
        }

        
    }
}
