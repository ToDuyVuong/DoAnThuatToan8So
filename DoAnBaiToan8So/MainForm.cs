using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnBaiToan8So
{
    public partial class MainForm : Form
    {
        // Khai báo các biến 
        int[,] MaTranCT8So;
        CT8SO EightPuzzle;
        BoTest8Puzzle Test8;       
        Stack<int[,]> Sep;        
        Button[,] Mangbutton;
        //int n = 3;
        int SoLanDiChuyen = 0;


        public MainForm()
        {
            InitializeComponent();
            MaTranCT8So = new int[3, 3];
            EightPuzzle = new CT8SO();
            Test8 = new BoTest8Puzzle();
            Sep = new Stack<int[,]>();
            Mangbutton = new Button[3, 3];
        }



        // Đổi màu các button báo hiệu chương trình sẳn sàng chạy
        void load8So_DoiMauCacButton(int[,] a, Button[,] b)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(0); j++)
                {
                    if (a[i, j] == 0)
                    {
                        b[i, j].Text = "";
                        b[i, j].BackColor = Color.Black;
                    }
                    else
                    {
                        b[i, j].Text = a[i, j].ToString();
                        b[i, j].BackColor = Color.Blue;
                    }
                }
        }



        // Khởi tạo
        void KhoiTao8So()
        {
            // Tạo trò chơi 8 số bằng cách chọn các bộ test
            MaTranCT8So = Test8.BoTest8So(3); 
            
            // Đổi màu đánh dấu chương trình đã sẳn sàng
            load8So_DoiMauCacButton(MaTranCT8So, Mangbutton);

            Sep = EightPuzzle.TimPhuongAn(MaTranCT8So, 3);
            Sep.Pop(); // Xóa phần tử khỏi đỉnh

            // Chọn tốc độ chạy ban đầu
            comboboxTocDo.Text = comboboxTocDo.Items[1].ToString();
            // Tính số bước đi
            labelSoLanDiChuyenAStar.Text = "0";
            SoLanDiChuyen = 0;
            // Đóng các button
            buttonBatDau.Enabled = false;
            buttonDung.Enabled = false;
            timer1.Enabled = false;
        }


        
        // Chơi mới
        private void bottonChoiMoi_Click(object sender, EventArgs e)
        {
            KhoiTao8So();
            buttonBatDau.Enabled = true;
        }

        

        // Tạm dừng
        private void bottonDung_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            buttonDung.Enabled = false;
            buttonBatDau.Enabled = true;
        }



        // Load Form
        private void MainForm_Load(object sender, EventArgs e)
        {

            {

                Mangbutton[0, 0] = ButtonAStartSo1;
                Mangbutton[0, 1] = ButtonAStartSo2;
                Mangbutton[0, 2] = ButtonAStartSo3;
                Mangbutton[1, 0] = ButtonAStartSo8;
                Mangbutton[1, 1] = ButtonAStartSo0;
                Mangbutton[1, 2] = ButtonAStartSo4;
                Mangbutton[2, 0] = ButtonAStartSo7;
                Mangbutton[2, 1] = ButtonAStartSo6;
                Mangbutton[2, 2] = ButtonAStartSo5;

                buttonBatDau.Enabled = false;
                buttonDung.Enabled = false;

            }
        }



        // Chạy CT giải
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (comboboxTocDo.Text)
            {
                case "1": timer1.Interval = 1750; break;
                case "2": timer1.Interval = 225; break;
            }

            int[,] Temp = new int[3, 3];

            if (Sep.Count != 0)
            {
                Temp = Sep.Pop(); // Xóa phần tử khỏi đỉnh
                load8So_DoiMauCacButton(Temp, Mangbutton);

                SoLanDiChuyen++;
                labelSoLanDiChuyenAStar.Text = SoLanDiChuyen.ToString();
            }
            else
                timer1.Enabled = false;
        }



        // Bắt đầu CT
        private void bottonBatDau_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            buttonBatDau.Enabled = false;
            buttonDung.Enabled = true;
        }


        // Đóng CT
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
