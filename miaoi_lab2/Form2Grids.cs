using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace miaoi_lab2
{
    public partial class Form2Grids : Form
    {
        public Form2Grids()
        {
            InitializeComponent();
        }
        int[,] masA8;
        int[,] masB8;
        int[,] masC8;
        int[,] masCN;

        int[,] masIsx;

        public int Nkonc;
        public int Nuzl;
        public int z1;
        public int z2;
        public void find_CharacteristicNumbers(int[,] mas)
        {
            masIsx = mas;
            masA8 = new int[mas.GetLength(0) - 2, mas.GetLength(1) - 2];
            masC8 = new int[mas.GetLength(0) - 2, mas.GetLength(1) - 2];
            masB8 = new int[mas.GetLength(0) - 2, mas.GetLength(1) - 2];
            masCN = new int[mas.GetLength(0) - 2, mas.GetLength(1) - 2];
            for (int i = 1; i < mas.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < mas.GetLength(1) - 1; j++)
                {
                    if (mas[i, j] == 1)
                    {
                        int p1 = mas[i, j + 1];
                        int p2 = mas[i - 1, j + 1];
                        int p3 = mas[i - 1, j];
                        int p4 = mas[i - 1, j - 1];
                        int p5 = mas[i, j - 1];
                        int p6 = mas[i + 1, j - 1];
                        int p7 = mas[i + 1, j];
                        int p8 = mas[i + 1, j + 1];
                        masA8[i - 1, j - 1] = p1 + p2 + p3 + p4 + p5 + p6 + p7 + p8;
                        masC8[i - 1, j - 1] = p1 * p2 * p3 + p2 * p3 * p4 + p3 * p4 * p5 +
                            p4 * p5 * p6 + p5 * p6 * p7 + p6 * p7 * p8 + p7 * p8 * p1;
                        masB8[i - 1, j - 1] = p1 * p2 + p2 * p3 + p3 * p4 + p4 * p5 + p5 * p6 + p6 * p7 + p7 * p8 + p8 * p1;
                        masCN[i - 1, j - 1] = masA8[i - 1, j - 1] - masB8[i - 1, j - 1];
                    }
                    else
                    {
                        masA8[i - 1, j - 1] = 0;
                        masC8[i - 1, j - 1] = 0;
                        masB8[i - 1, j - 1] = 0;
                        masCN[i - 1, j - 1] = 0;

                    }
                }
            }
            findNuNk();

            findZonds();
            //CommonFunctions.writeMatrixToGrid(dataGridView1, masA8);
            //CommonFunctions.writeMatrixToGrid(dataGridView2, masC8);
            //CommonFunctions.writeMatrixToGrid(dataGridView3, masCN);
            //CommonFunctions.colorNCuzli(dataGridView3);
        }

        public void findZonds()
        {
            z1 = 0;
            z2 = 0;
            string perehod = "01";
            string str1 = "";
            string str2 = "";
            for (int i = 0; i < masIsx.GetLength(0); i++)
            {
                str2 += masIsx[i, 35];
            }
            for (int j = 0; j < masIsx.GetLength(1); j++)
            {
                str1 += masIsx[35, j];
            }
            z1 = (str1.Length - str1.Replace(perehod, "").Length) / perehod.Length;
            z2 = (str2.Length - str2.Replace(perehod, "").Length) / perehod.Length;

        }


        public void findNuNk()
        {
            Nkonc = 0;
            Nuzl = 0;
            for (int i = 0; i < masCN.GetLength(0); i++)
            {
                for (int j = 0; j < masCN.GetLength(1); j++)
                {
                    if (masCN[i, j] == 1) Nkonc += 1;
                    if (masCN[i, j] == 3 || masCN[i, j] == 4) Nuzl += 1;
                }
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rezult = comboBox1.SelectedItem.ToString();
            switch (rezult) {
                case "A8":
                    CommonFunctions.writeMatrixToGrid(dataGridView3, masA8);
                    CommonFunctions.colorAll(dataGridView3);
                    break;
                case "C8":
                    CommonFunctions.writeMatrixToGrid(dataGridView3, masC8);
                    CommonFunctions.colorAll(dataGridView3);
                    break;
                case "Nc":
                    CommonFunctions.writeMatrixToGrid(dataGridView3, masCN);
                    CommonFunctions.colorNCuzli(dataGridView3);
                    break;
                case "B8":
                    CommonFunctions.writeMatrixToGrid(dataGridView3, masB8);
                    CommonFunctions.colorAll(dataGridView3);
                    break;
                case "Z1":
                    CommonFunctions.writeMatrixToGrid(dataGridView3,CommonFunctions.reduce( masIsx));
                    CommonFunctions.colorAll(dataGridView3);
                    drawZond1();
                    break;
                case "Z2":
                    CommonFunctions.writeMatrixToGrid(dataGridView3,CommonFunctions.reduce( masIsx));
                    CommonFunctions.colorAll(dataGridView3);
                    drawZond2();
                    break;
                default: break;
            }
            label3.Text = rezult;
        }


        public void drawZond1()
        {
            for(int i = 0; i < masIsx.GetLength(1); i++)
            {
                dataGridView3[i, 35].Style.BackColor = Color.Red;
            }
        }
        
        public void drawZond2()
        {
            for(int i = 0; i < masIsx.GetLength(0); i++)
            {
                dataGridView3[35,i].Style.BackColor = Color.Blue;
            }
        }
    }
}
