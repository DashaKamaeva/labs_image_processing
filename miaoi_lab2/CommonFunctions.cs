using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;
using System.Windows.Forms;

namespace miaoi_lab2
{
    public class CommonFunctions
    {
        public static List<int> fillListrgb(int[,] mas)
        {
            List<int> rgb = new List<int>();
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    rgb.Add(mas[i, j]);
                    //Console.WriteLine(mas[i, j]);
                }
            }

            List<int> rgbUniq = rgb.Distinct().ToList();
            rgbUniq.Sort();
            return rgbUniq;
        }

        public static int[,] fromPicToMasRGB(PictureBox pic)
        {
            Bitmap picture = new Bitmap(pic.Image);
            int[,] mas = new int[picture.Height, picture.Width];

            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    mas[i, j] = picture.GetPixel(j, i).R;
                }
            }

            return mas;

        }
        public static Bitmap fromMas01ToPic(int[,] mas)
        {

            Bitmap picture = new Bitmap(mas.GetLength(1), mas.GetLength(0));
            //int[,] mas = new int[picture.Height, picture.Width];

            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (mas[i, j] == 1)
                        picture.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                    else if (mas[i, j] == 0)
                        picture.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                }
            }

            return picture;

        }

        //убрать 0 вокруг
        public static int[,] reduce(int[,] mas)
        {
            int[,] masNew = new int[mas.GetLength(0) - 2, mas.GetLength(1) - 2];
            for (int i = 0; i < masNew.GetLength(0); i++)
            {
                for (int j = 0; j < masNew.GetLength(1); j++)
                {
                    masNew[i, j] = mas[i + 1, j + 1];
                }
            }

            return masNew;
        }

        public static int[,] makeMas01(PictureBox pic)
        {
            Bitmap picture = new Bitmap(pic.Image);
            int[,] mas = new int[picture.Height, picture.Width];

            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    mas[i, j] = picture.GetPixel(j, i).R > 0 ? 0 : 1;
                }
            }

            return mas;

        }

        //расширение нулями
        public static int[,] extend(int[,] mas)
        {
            int[,] masNew = new int[mas.GetLength(0) + 2, mas.GetLength(1) + 2];
            for (int i = 0; i < masNew.GetLength(0); i++)
            {
                for (int j = 0; j < masNew.GetLength(1); j++)
                {
                    masNew[i, j] = 0;
                }
            }
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    masNew[i + 1, j + 1] = mas[i, j];
                }
            }
            return masNew;
        }

        //гистограмма двоичного разбиения
        public static Dictionary<int, int> makeGistogramLocal2(int[,] mas)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            dict.Add(0, 0);
            dict.Add(1, 0);
            dict.Add(2, 0);
            dict.Add(3, 0);
            dict.Add(4, 0);
            dict.Add(5, 0);
            dict.Add(6, 0);
            dict.Add(7, 0);
            dict.Add(8, 0);

            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    dict[mas[i, j]] += 1;
                }
            }

            return dict;
        }

        public static string changePicture(PictureBox pictureBox)
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap b = new Bitmap(openFileDialog.FileName);
                pictureBox.Width = b.Width;
                pictureBox.Height = b.Height;
                pictureBox.Image = b;
                path = openFileDialog.FileName;
                Console.WriteLine(path);
                
            }
            return path;
        }
        public static void savePicture(PictureBox pictureBox)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox.Image.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                    MessageBox.Show("Сохранено", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else pictureBox.Image.Save("poluton.bmp", ImageFormat.Bmp);
        }

        //вывод значений матрицы RGB
        public static void writeMatrixToGrid(DataGridView dataGrid, int[,] mas, List<int> l = null)
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
            dataGrid.RowCount = mas.GetLength(0);
            dataGrid.ColumnCount = mas.GetLength(1);

            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    dataGrid.Rows[i].Cells[j].Value = mas[i, j];
                    //if (mas[i, j] == 1)
                    //{
                    //    dataGrid[j, i].Style.BackColor = Color.BlueViolet;
                    //}
                    if (l == null)
                    {
                        dataGrid.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        dataGrid.Columns[j].HeaderCell.Value = (j + 1).ToString();
                    }
                    else
                    {
                        dataGrid.Rows[i].HeaderCell.Value = l[i].ToString();
                        dataGrid.Columns[j].HeaderCell.Value = l[j].ToString();
                    }
                }

            }
        }

        public static void colorNCuzli(DataGridView dataGrid)
        {
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                for (int j = 0; j < dataGrid.Columns.Count; j++)
                {
                    if (dataGrid[j, i].Value.ToString() == "1")
                    {
                        dataGrid[j, i].Style.BackColor = Color.BlueViolet;
                    }
                    if (dataGrid[j, i].Value.ToString() == "3" || dataGrid[j, i].Value.ToString() == "4")
                    {
                        dataGrid[j, i].Style.BackColor = Color.Brown;
                    }

                }
            }
        }public static void colorAll(DataGridView dataGrid)
        {
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                for (int j = 0; j < dataGrid.Columns.Count; j++)
                {
                    if (dataGrid[j, i].Value.ToString() != "0")
                    {
                        dataGrid[j, i].Style.BackColor = Color.BlueViolet;
                    }

                }
            }
        }

                public static int[,] copyMas(int[,] mas)
                {
                    int[,] newMas = new int[mas.GetLength(0), mas.GetLength(1)];
                    for (int i = 0; i < mas.GetLength(0); i++)
                    {
                        for (int j = 0; j < mas.GetLength(1); j++)
                        {
                            newMas[i, j] = mas[i, j];
                        }
                    }
                    return newMas;
                }

                public static bool isEqualToMas(int[,] mas, int[,] mas2)
                {
                    bool flag = true;
                    for (int i = 0; i < mas.GetLength(0); i++)
                    {
                        for (int j = 0; j < mas.GetLength(1); j++)
                        {
                            if (mas[i, j] != mas2[i, j])
                            {
                                flag = false;
                            }
                        }
                    }
                    return flag;
                }



                public static void writeMatrixToGrid(DataGridView dataGrid, double[,] mas, List<int> l)
                {
                    dataGrid.Rows.Clear();
                    dataGrid.Columns.Clear();
                    dataGrid.RowCount = mas.GetLength(0);
                    dataGrid.ColumnCount = mas.GetLength(1);

                    for (int i = 0; i < mas.GetLength(0); i++)
                    {
                        for (int j = 0; j < mas.GetLength(1); j++)
                        {
                            dataGrid.Rows[i].Cells[j].Value = mas[i, j];
                            dataGrid.Rows[i].HeaderCell.Value = l[i].ToString();
                            dataGrid.Columns[j].HeaderCell.Value = l[j].ToString();
                        }

                    }
                }

                //рисование гистограммы
                public static void DrawGistogram(Chart chart, Dictionary<int, int> d)
                {
                    chart.Series[0].Points.DataBindXY(d.Keys, d.Values);
                }

            }
        } 
