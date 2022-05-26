using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace DoAnBaiToan8So
{
    public partial class MainForm : Form
    {
        // Khai báo các biến 
        int[,] MaTranCT8So;
        int[,] MaTranCT8SoBFS;

        ThuatToanAStar EightPuzzleAStar;
        ThuatToanBFS EightPuzzleBFS;

        BoTest8Puzzle Test8;     
        
        Stack<int[,]> StackPhuongAnAStar;
        Stack<int[,]> StackPhuongAnBFS;

        Button[,] Mangbutton;
        Button[,] MangbuttonBFS;

        //int n = 3;
        int SoLanDiChuyenAStar = 0;
        int SoLanDiChuyenBFS = 0;

        int AStarNhanhHon = 0;
        int BFSNhanhHon = 0;

        int NhoAStarNhanhHon = 0;
        int NhoBFSNhanhHon = 0;

        int ThoiGianChayAStar = 0;
        int ThoiGianChayBFS = 0;

        int ToiUuThoiGianChayAStar = 0;
        int ToiUuThoiGianChayBFS = 0;

        Stopwatch TimeAStar = new Stopwatch();
        Stopwatch TimeBFS = new Stopwatch();

        public MainForm()
        {
            InitializeComponent();
            MaTranCT8So = new int[3, 3];
            MaTranCT8SoBFS = new int[3, 3];
            EightPuzzleAStar = new ThuatToanAStar();
            EightPuzzleBFS = new ThuatToanBFS();
            Test8 = new BoTest8Puzzle();
            StackPhuongAnAStar = new Stack<int[,]>();
            StackPhuongAnBFS = new Stack<int[,]>();
            Mangbutton = new Button[3, 3];
            MangbuttonBFS = new Button[3, 3];

        }



        // Đổi màu các button báo hiệu chương trình sẳn sàng chạy cho thuật toán A Star
        void Load_DoiMauCacButtonAStar(int[,] A, Button[,] B)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (A[i, j] == 0)
                    {
                        B[i, j].Text = "";
                        B[i, j].BackColor = Color.Black;
                    }
                    else
                    {
                        B[i, j].Text = A[i, j].ToString();
                        B[i, j].BackColor = Color.Blue;
                    }
                }
        }




        // Đổi màu các button báo hiệu chương trình sẳn sàng chạy cho thuật toán BFS
        void Load_DoiMauCacButtonBFS(int[,] A, Button[,] B)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (A[i, j] == 0)
                    {
                        B[i, j].Text = "";
                        B[i, j].BackColor = Color.Black;
                    }
                    else
                    {
                        B[i, j].Text = A[i, j].ToString();
                        B[i, j].BackColor = Color.GreenYellow;
                    }
                }
        }



        // Khởi tạo
        void KhoiTao8So()
        {
            // Tạo trò chơi 8 số bằng cách chọn các bộ test
            int[,] ex = new int[3, 3];
            ex = Test8.BoTest8So(3);

            int h = 0;
            int[] H = new int[9];
            for (int i = 0; i<3; i++)
                for (int j = 0; j<3; j++)
                {
                    H[h] = ex[i, j];
                    h++;
                }    


            MaTranCT8So = ex;
            MaTranCT8SoBFS = ex;

            // Đổi màu đánh dấu chương trình đã sẳn sàng
            Load_DoiMauCacButtonAStar(MaTranCT8So, Mangbutton);
            Load_DoiMauCacButtonBFS(MaTranCT8SoBFS, MangbuttonBFS);

            StackPhuongAnAStar = EightPuzzleAStar.TimPhuongAn(MaTranCT8So, 3);
            StackPhuongAnAStar.Pop(); // Xóa phần tử khỏi đỉnh
            StackPhuongAnBFS = EightPuzzleBFS.TimPhuongAn(MaTranCT8SoBFS, 3);
            StackPhuongAnAStar.Pop(); //////////////////////////////////////////////////////// Chạy Test DEMO Chương trình
            //StackPhuongAnBFS.Pop();

            // Chọn tốc độ chạy ban đầu
            comboboxTocDo.Text = comboboxTocDo.Items[2].ToString();

            // Tính số bước đi
            labelSoLanDiChuyenAStar.Text = "0";
            SoLanDiChuyenAStar = 0;
            labelSoLanDiChuyenBFS.Text = "0";
            SoLanDiChuyenBFS = 0;

            // Đóng các button
            buttonBatDau.Enabled = false;
            buttonDung.Enabled = false;
            timer1.Enabled = false;
            timer2.Enabled = false;
        }


        
        // Chơi mới
        private void bottonChoiMoi_Click(object sender, EventArgs e)
        {           
            // Hàm tính toán thời gian chạy
            Invoke(new Action(() =>
            {
                TimeAStar.Stop();
                TimeBFS.Stop();
                TimeAStar.Reset();
                TimeBFS.Reset();

                labelThoiGianHoanThanhAStar.Text = TimeAStar.ElapsedTicks.ToString();
                labelThoiGianHoanThanhBFS.Text = TimeBFS.ElapsedTicks.ToString();
            }));

            KhoiTao8So();
            buttonBatDau.Enabled = true;
        }



        // Bắt đầu CT
        private void bottonBatDau_Click(object sender, EventArgs e)
        {
            // timer1.Enabled = true;
            // timer2.Enabled = true;

            // 2 hàm chạy cùng lúc
            Invoke(new Action(() =>
            {
                timer1.Enabled = true;
                TimeAStar.Start();
            }));
            Invoke(new Action(() =>
            {
                timer2.Enabled = true;
                TimeBFS.Start();
            }));

            buttonBatDau.Enabled = false;
            buttonDung.Enabled = true;
        }



        // Tạm dừng
        private void bottonDung_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                TimeAStar.Stop();
                TimeBFS.Stop();
                timer1.Enabled = false;
                timer2.Enabled = false;
            }));
            
            buttonDung.Enabled = false;
            buttonBatDau.Enabled = true;
        }

                

        // Chạy chương trình theo phương án giải A Star
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (comboboxTocDo.Text)
            {
                case "1": timer1.Interval = 1750; break;
                case "2": timer1.Interval = 225; break;
                case "3": timer1.Interval = 1; break;
            }

            int[,] Temp = new int[3, 3];

            if (StackPhuongAnAStar.Count != 0)
            {
                Temp = StackPhuongAnAStar.Pop(); // Xóa phần tử khỏi đỉnh
                Load_DoiMauCacButtonAStar(Temp, Mangbutton);

                SoLanDiChuyenAStar++;
                AStarNhanhHon = SoLanDiChuyenAStar;
                labelSoLanDiChuyenAStar.Text = SoLanDiChuyenAStar.ToString();
            }
            else
            {
                Invoke(new Action(() =>
                {
                    timer1.Enabled = false;
                    TimeAStar.Stop();
                  //  TimeAStar.Restart();                    
                }));

                ThoiGianChayAStar = Convert.ToInt32(TimeAStar.ElapsedTicks.ToString());

                // Đo thời gian chạy và so sánh
                if(timer2.Enabled == false)
                {
                    labelThoiGianHoanThanhAStar.Text = TimeAStar.ElapsedTicks.ToString();
                    labelThoiGianHoanThanhBFS.Text = TimeBFS.ElapsedTicks.ToString();

                    if (ThoiGianChayAStar > ThoiGianChayBFS)
                    {
                        ToiUuThoiGianChayBFS++;
                        labelSoLanThoiGianBFSNhanhHon.Text = ToiUuThoiGianChayBFS.ToString();
                    }
                    if (ThoiGianChayAStar < ThoiGianChayBFS)
                    {
                        ToiUuThoiGianChayAStar++;
                        labelSoLanThoiGianAStarNhanhHon.Text = ToiUuThoiGianChayAStar.ToString();
                    }
                }//timer1.Enabled = false;

                // So sánh chi phi đến đích
                if (timer2.Enabled == false)
                {
                    if (BFSNhanhHon > AStarNhanhHon)
                    {
                        NhoAStarNhanhHon++;
                        labelAStarNhanhHon.Text = NhoAStarNhanhHon.ToString();
                    }
                    if (BFSNhanhHon < AStarNhanhHon)
                    {
                        NhoBFSNhanhHon++;
                        labelBFSNhanhHon.Text = NhoBFSNhanhHon.ToString();
                    }
                }
            }
        }



        // Chạy chương trình theo phương án giải BFS
        private void timer2_Tick(object sender, EventArgs e)
        {
            switch (comboboxTocDo.Text)
            {
                case "1": timer2.Interval = 1750; break;
                case "2": timer2.Interval = 225; break;
                case "3": timer2.Interval = 1; break;
            }
            int[,] Temp = new int[3, 3];
            if (StackPhuongAnBFS.Count != 0)
            {
                Temp = StackPhuongAnBFS.Pop();
                Load_DoiMauCacButtonBFS(Temp, MangbuttonBFS);
                SoLanDiChuyenBFS++;
                BFSNhanhHon = SoLanDiChuyenBFS;
                labelSoLanDiChuyenBFS.Text = SoLanDiChuyenBFS.ToString();
            }
            else
            {
                Invoke(new Action(() =>
                {
                    timer2.Enabled = false;
                    TimeBFS.Stop();
                   // TimeBFS.Restart();
                }));
                ThoiGianChayBFS = Convert.ToInt32(TimeBFS.ElapsedTicks.ToString());
                if (timer1.Enabled == false)
                {
                    labelThoiGianHoanThanhAStar.Text = TimeAStar.ElapsedTicks.ToString();
                    labelThoiGianHoanThanhBFS.Text = TimeBFS.ElapsedTicks.ToString();
                    if (ThoiGianChayAStar > ThoiGianChayBFS)
                    {
                        ToiUuThoiGianChayBFS++;
                        labelSoLanThoiGianBFSNhanhHon.Text = ToiUuThoiGianChayBFS.ToString();
                    }
                    if (ThoiGianChayAStar < ThoiGianChayBFS)
                    {
                        ToiUuThoiGianChayAStar++;
                        labelSoLanThoiGianAStarNhanhHon.Text = ToiUuThoiGianChayAStar.ToString();
                    }
                }
                // timer2.Enabled = false;
                if (timer1.Enabled == false)
                {
                    if (BFSNhanhHon > AStarNhanhHon)
                    {
                        NhoAStarNhanhHon++;
                        labelAStarNhanhHon.Text = NhoAStarNhanhHon.ToString();
                    }
                    if (BFSNhanhHon < AStarNhanhHon)
                    {
                        NhoBFSNhanhHon++;
                        labelBFSNhanhHon.Text = NhoBFSNhanhHon.ToString();
                    }
                }  
            }
        }



        // Load Form
        private void MainForm_Load(object sender, EventArgs e)
        {            
                // Button cho A Star
                Mangbutton[0, 0] = ButtonAStartSo1;
                Mangbutton[0, 1] = ButtonAStartSo2;
                Mangbutton[0, 2] = ButtonAStartSo3;
                Mangbutton[1, 0] = ButtonAStartSo8;
                Mangbutton[1, 1] = ButtonAStartSo0;
                Mangbutton[1, 2] = ButtonAStartSo4;
                Mangbutton[2, 0] = ButtonAStartSo7;
                Mangbutton[2, 1] = ButtonAStartSo6;
                Mangbutton[2, 2] = ButtonAStartSo5;

                // Button cho BFS
                MangbuttonBFS[0, 0] = buttonSo1;
                MangbuttonBFS[0, 1] = buttonSo2;
                MangbuttonBFS[0, 2] = buttonSo3;
                MangbuttonBFS[1, 0] = buttonSo8;
                MangbuttonBFS[1, 1] = buttonSo0;
                MangbuttonBFS[1, 2] = buttonSo4;
                MangbuttonBFS[2, 0] = buttonSo7;
                MangbuttonBFS[2, 1] = buttonSo6;
                MangbuttonBFS[2, 2] = buttonSo5;

                buttonBatDau.Enabled = false;
                buttonDung.Enabled = false;            
        }



        // Đóng CT
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        


    }
}
