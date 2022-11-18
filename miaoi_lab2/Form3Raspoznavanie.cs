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
    public partial class Form3Raspoznavanie : Form
    {
        string path = "D:\\4 курс\\МиАОИ\\картинки\\утоньшенные\\a.bmp";
        Class cl;
        Example ex;
        public Form3Raspoznavanie()
        {
            InitializeComponent();
            //doTable();
            cl = new Class();
            
        }

        public void doTable()
        {
            //var imageColumn = new DataGridViewImageColumn();
            //imageColumn.HeaderText = "Изображение";
            //dataGridView1.Columns.Add(imageColumn);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            path = CommonFunctions.changePicture(pictureBox1);
            //path = pictureBox1.ImageLocation;
            
        }

        public void CreateExampleWithNoClass()
        {
            int[,] rgb01 = CommonFunctions.makeMas01(pictureBox1);
            int[,] rgbw = CommonFunctions.extend(rgb01);
            Form2Grids f2 = new Form2Grids();
            f2.find_CharacteristicNumbers(rgbw);

            ex = new Example(cl, "", f2.Nuzl, f2.Nkonc, path);
            ex.addZonds(f2.z1, f2.z2);
            writeExampleToGrid(ex);
            cl.addExample(ex);

            chart1.Series[0].Points.AddXY(ex.zond1, ex.zond2);
            chart1.Series[0].Points[cl.examples.IndexOf(ex)].Color =
                Color.FromArgb(0,255,0);

            dataGridView2.Rows.Add("?", "");
            dataGridView2[1, dataGridView2.Rows.Count-2].Style.BackColor = Color.FromArgb(0, 255, 0);
        }
      

        //сохранить все данные+имена классов
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Example ex in cl.examples)
            {
                ex.changeClassName(dataGridView1[3, ex.id - 1].Value.ToString());
            }
            Serializer.doSerializing(cl);
        }

        //вывести все данные
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            cl = Serializer.doDeserializing();
            foreach(Example ex in cl.examples)
            {
                writeExampleToGrid(ex);
            }
            drawPoints();
        }


        public void writeExampleToGrid(Example ex)
        { var pic = new Bitmap(ex.image);
            dataGridView1.Rows.Add(ex.id, ex.zond1, ex.zond2,ex.className,"", new Bitmap(ex.image));
            dataGridView1.Rows[dataGridView1.Rows.Count-2].Height = pic.Height ;
        }

        //добавить последнюю строку в файл
        private void button5_Click(object sender, EventArgs e)
        {
            ex.changeClassName(dataGridView1[3, dataGridView1.Rows.Count-2].Value.ToString());
            Serializer.doSerializingAdd1(ex);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            CreateExampleWithNoClass();
            
           
            
        }

        public double findEvklidovo(Example ex1, Example ex2)
        {
            double r = Math.Round(Math.Sqrt(Math.Pow(ex1.zond2 - ex2.zond2, 2) + Math.Pow(ex1.zond1 - ex2.zond1, 2)),4);
            return r;
        }

        public void drawPoints()
        {
            chart1.Series[0].Points.Clear();
            List<string> ls= cl.findClasses();
            int koef = 255 / ls.Count;
            foreach (Example ex in cl.examples)
            {
                chart1.Series[0].Points.AddXY(ex.zond1, ex.zond2);
                chart1.Series[0].Points[cl.examples.IndexOf(ex)].Color = 
                    Color.FromArgb(koef*ls.IndexOf(ex.className), koef/(ls.IndexOf(ex.className)*5+1), 
                    255- koef * ls.IndexOf(ex.className));
            }
            dataGridView2.Rows.Clear();
            foreach(string cl in ls)
            {
                dataGridView2.Rows.Add(cl, "");
                dataGridView2[1,ls.IndexOf(cl)].Style.BackColor= Color.FromArgb(koef * ls.IndexOf(cl), koef / (ls.IndexOf(cl) * 5 + 1),
                    255 - koef * ls.IndexOf(cl));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double min_r = 50;
            string clasNew = "";

            foreach (Example example in cl.examples)
            {
                if (example.id != cl.examples.Count)
                {
                    double r = findEvklidovo(example, ex);
                    if (r < min_r) { min_r = r; clasNew = example.className; }
                    dataGridView1[4, cl.examples.IndexOf(example)].Value = r;
                }
            }

            dataGridView1[3, dataGridView1.Rows.Count-2].Value = clasNew;
            ex.className = clasNew;
            drawPoints();
        }


    }
}
