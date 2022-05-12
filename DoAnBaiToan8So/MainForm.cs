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

        int[,] MaTran;
        CT8SO TamSo;
        Stack<int[,]> stk;
        Button[,] Mangbt;
        int n = 3;
        int SoLanDiChuyen = 0;


        public MainForm()
        {
            InitializeComponent();
            MaTran = new int[n, n];
            TamSo = new CT8SO();

            stk = new Stack<int[,]>();
            Mangbt = new Button[n, n];
        }

        void load8So(int[,] a, Button[,] b)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(0); j++)
                {
                    if (a[i, j] == 0)
                    {
                        b[i, j].Text = "";
                        b[i, j].BackColor = Color.MediumSeaGreen;
                    }
                    else
                    {
                        b[i, j].Text = a[i, j].ToString();
                        b[i, j].BackColor = Color.White;
                    }
                }
        }

        void khoiTao8So()
        {
            MaTran = TamSo.randomMaTran(n);

            load8So(MaTran, Mangbt);

            stk = TamSo.timKetQua(MaTran, n);
            stk.Pop();
            comboboxTocDo.Text = comboboxTocDo.Items[0].ToString();
            labelSoLanDiChuyenAStar.Text = "0";
            SoLanDiChuyen = 0;
            buttonBatDau.Enabled = false;
            buttonDung.Enabled = false;
            timer1.Enabled = false;
        }


        

        private void bottonChoiMoi_Click(object sender, EventArgs e)
        {
            khoiTao8So();
            buttonBatDau.Enabled = true;
        }

        

        private void bottonDung_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            buttonDung.Enabled = false;
            buttonBatDau.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            {

                Mangbt[0, 0] = ButtonAStartSo1;
                Mangbt[0, 1] = ButtonAStartSo2;
                Mangbt[0, 2] = ButtonAStartSo3;
                Mangbt[1, 0] = ButtonAStartSo8;
                Mangbt[1, 1] = ButtonAStartSo0;
                Mangbt[1, 2] = ButtonAStartSo4;
                Mangbt[2, 0] = ButtonAStartSo7;
                Mangbt[2, 1] = ButtonAStartSo6;
                Mangbt[2, 2] = ButtonAStartSo5;

                buttonBatDau.Enabled = false;
                buttonDung.Enabled = false;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (comboboxTocDo.Text)
            {
                case "1": timer1.Interval = 2000; break;
                case "2": timer1.Interval = 1500; break;
                case "3": timer1.Interval = 1000; break;
                case "4": timer1.Interval = 500; break;
                case "5": timer1.Interval = 250; break;
            }

            int[,] Temp = new int[n, n];

            if (stk.Count != 0)
            {
                Temp = stk.Pop();
                load8So(Temp, Mangbt);

                SoLanDiChuyen++;
                labelSoLanDiChuyenAStar.Text = SoLanDiChuyen.ToString();
            }
            else
                timer1.Enabled = false;
        }

        private void bottonBatDau_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            buttonBatDau.Enabled = false;
            buttonDung.Enabled = true;
        }
    }
}
