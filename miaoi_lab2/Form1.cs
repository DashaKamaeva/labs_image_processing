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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int[,] zongaSunyaMas;
        private void button1_Click(object sender, EventArgs e)
        {

            CommonFunctions.changePicture(pictureBox1);
            pictureBox2.Size = pictureBox1.Size;
        }

        //рассчитать/вывести матрицу 0 и 1
        private void button3_Click(object sender, EventArgs e)
        {
            //int[,] rgb = CommonFunctions.fromPicToMasRGB(pictureBox1);
            int[,] mas01 = CommonFunctions.makeMas01(pictureBox1);
            CommonFunctions.writeMatrixToGrid(dataGridView1, mas01);
        }


        //произвести утоньшение
        private void button2_Click(object sender, EventArgs e)
        {
            int[,] mas01 = CommonFunctions.makeMas01(pictureBox1);
            int[,] mas01wide = CommonFunctions.extend(mas01);

            int[,] resultMas = CommonFunctions.copyMas(mas01wide); ;

            bool flag = true;

            while (flag)
            {
                bool end1 = false;
                bool end2 = false;
                int[,] t = makeDeleting1(resultMas);
                if (CommonFunctions.isEqualToMas(t, resultMas))
                {
                    end1 = true;
                }
                resultMas = makeDeleting2(t);
                if (CommonFunctions.isEqualToMas(t, resultMas))
                {
                    end2 = true;
                }

                if (end1 && end2) { flag = false; }

            }

            zongaSunyaMas = CommonFunctions.reduce(resultMas);
            CommonFunctions.writeMatrixToGrid(dataGridView2, zongaSunyaMas);
            CommonFunctions.colorAll(dataGridView2);
            pictureBox2.Image = CommonFunctions.fromMas01ToPic(zongaSunyaMas);
        }

        public int[,] makeDeleting1(int [,] mas01)
        {
            int[,] deletedMas = CommonFunctions.copyMas(mas01);
            for(int i = 1; i < mas01.GetLength(0)-1; i++)
            {
                for(int j = 1; j < mas01.GetLength(1)-1; j++)
                {
                    if (mas01[i, j] == 1)
                    {
                        if (ifShouldDelete1(i, j, mas01))
                        {
                            deletedMas[i, j] = 0;
                        }
                    }
                }
            }
            return deletedMas;
        }
        
        public int[,] makeDeleting2(int [,] mas01)
        {
            int[,] deletedMas = CommonFunctions.copyMas(mas01);
            for(int i = 1; i < mas01.GetLength(0)-1; i++)
            {
                for(int j = 1; j < mas01.GetLength(1)-1; j++)
                {
                    if (mas01[i, j] == 1)
                    {
                        if (ifShouldDelete2(i, j, mas01))
                        {
                            deletedMas[i, j] = 0;
                        }
                    }
                }
            }
            return deletedMas;
        }

        public bool ifShouldDelete1(int i,int j, int [,] mas01)
        {
            int p1 = mas01[i, j];
            int p2 = mas01[i-1, j];
            int p3 = mas01[i-1, j+1];
            int p4 = mas01[i, j+1];
            int p5 = mas01[i+1, j+1];
            int p6 = mas01[i+1, j];
            int p7 = mas01[i+1, j-1];
            int p8 = mas01[i, j-1];
            int p9 = mas01[i-1, j-1];
            bool answer = false;
            //1
            if((p2+p3+p4+p5+p6+p7+p8+p9)<=6  && (p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9) >= 2)
            {
                string s = p2.ToString() + p3.ToString() + p4.ToString() + p5.ToString() + 
                    p6.ToString() + p7.ToString() + p8.ToString() + p9.ToString() + p2.ToString();
                string subS = "01";
                int c = (s.Length - s.Replace(subS, "").Length) / subS.Length;
                if (c==1)
                {
                    if(p2*p4*p6==0 && p4 * p6 * p8 == 0)
                    {
                        answer = true;
                    }
                }
            }

            return answer;
        }


        public bool ifShouldDelete2(int i, int j, int[,] mas01)
        {
            int p1 = mas01[i, j];
            int p2 = mas01[i - 1, j];
            int p3 = mas01[i - 1, j + 1];
            int p4 = mas01[i, j + 1];
            int p5 = mas01[i + 1, j + 1];
            int p6 = mas01[i + 1, j];
            int p7 = mas01[i + 1, j - 1];
            int p8 = mas01[i, j - 1];
            int p9 = mas01[i - 1, j - 1];
            bool answer = false;
            //1
            if (((p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9) <= 6 && (p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9) >= 2))
            {
                string s = p2.ToString() + p3.ToString() + p4.ToString() + p5.ToString() +
                    p6.ToString() + p7.ToString() + p8.ToString() + p9.ToString() + p2.ToString();
                string subS = "01";
                int c = (s.Length - s.Replace(subS, "").Length) / subS.Length;
                if (c == 1)
                {
                    if (p2 * p4 * p8 == 0 && p2 * p6 * p8 == 0)
                    {
                        answer = true;
                    }

                }
            }
            return answer;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[,] mas01wide;
            if (pictureBox2.Image!=null)
            {
                mas01wide = CommonFunctions.extend( zongaSunyaMas);
            }
            else
            {
                mas01wide = CommonFunctions.extend(CommonFunctions.makeMas01(pictureBox1));
            }

            
            Form2Grids f2 = new Form2Grids();
            f2.find_CharacteristicNumbers(mas01wide);
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CommonFunctions.savePicture(pictureBox2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3Raspoznavanie f3 = new Form3Raspoznavanie();
            f3.Show();
        }
    }
}
