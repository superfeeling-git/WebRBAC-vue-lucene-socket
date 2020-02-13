using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel格式解析
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 私有变量

        private string _path;  //winForm路径

        private ExcelHelper _helper;  //工具对象

        #endregion

        #region 初始化
        private void Form1_Load(object sender, EventArgs e)
        {
            _path = System.Environment.CurrentDirectory + "\\测试目录\\"; //获取winForm应用程序下
            _helper = new ExcelHelper();
        }
        #endregion

        #region 私有方法

        private void ShowOK()
        {
            MessageBox.Show("成功！");
        }

        private void OpenExcel(string filepath)
        {
            MessageBox.Show("已生成文件：" + filepath);
            ExcelHelper.OpenExcel(filepath);
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            _helper = new ExcelHelper();
            string filepath = _path + "空的Excel.xls";
            _helper.OutputFilePath = filepath;
            _helper.SaveAsFile();
            OpenExcel(filepath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filepath = _path + "另存为的Excel.xls";
            _helper = new ExcelHelper(_path + "..\\template.xls", filepath);
            _helper.SetCellsBackColor(1, 1, 2, 7, ColorIndex.粉红);
            _helper.SaveAsFile();
            OpenExcel(filepath);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string filepath = _path + "初始化数据.xls";
            _helper = new ExcelHelper(_path + "..\\template.xls", filepath);
            object[,] oSArray = new object[2, 7];
            oSArray[0, 0] = "'001";
            oSArray[0, 1] = "'001";
            oSArray[0, 2] = "'001";
            oSArray[0, 3] = "3";
            oSArray[0, 4] = "1";
            oSArray[0, 5] = "0.6";
            oSArray[0, 6] = "1.8";
            oSArray[1, 0] = "'001";
            oSArray[1, 1] = "'002";
            oSArray[1, 2] = "'002";
            oSArray[1, 3] = "3";
            oSArray[1, 4] = "2";
            oSArray[1, 5] = "0.6";
            oSArray[1, 6] = "3.6";

            _helper.ArrayToExcel(oSArray, 3, 1);
            _helper.SaveAsFile();
            OpenExcel(filepath);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filepath = _path + "删除行.xls";
            _helper = new ExcelHelper(_path + "初始化数据.xls", filepath);
            _helper.InsertRows(3, 2);
            _helper.SaveAsFile();
            OpenExcel(filepath);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string filepath = _path + "删除行.xls";
            _helper = new ExcelHelper(_path + "初始化数据.xls", filepath);
            _helper.DeleteRows(3, 1);
            _helper.SaveAsFile();
            OpenExcel(filepath);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string filepath = _path + "合并行.xls";
            _helper = new ExcelHelper(_path + "初始化数据.xls", filepath);
            _helper.MergeCells(3, 1, 3, 3, "001");
            _helper.SaveAsFile();
            OpenExcel(filepath);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string filepath = _path + "复制行.xls";
            _helper = new ExcelHelper(_path + "初始化数据.xls", filepath);
            _helper.CopyRows(3, 3); //将第三行复制三次
            _helper.SaveAsFile();
            OpenExcel(filepath);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string filepath = _path + "插入列.xls";
            _helper = new ExcelHelper(_path + "初始化数据.xls", filepath);
            _helper.InsertColumns(8, 2);
            _helper.SetCells(2, 8, "备注1");
            _helper.SetCells(2, 9, "备注2");
            _helper.SaveAsFile();
            OpenExcel(filepath);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string filepath = _path + "删除列.xls";
            _helper = new ExcelHelper(_path + "初始化数据.xls", filepath);
            _helper.DeleteColumns(1, 3); //删除前三列
            _helper.SaveAsFile();
            OpenExcel(filepath);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filepath = _path + "写入批注.xls";
            _helper = new ExcelHelper(_path + "初始化数据.xls", filepath);
            _helper.SetCellComment(2, 1, "goodsid");
            _helper.SetCellComment(2, 2, "sku1id");
            _helper.SetCellComment(2, 3, "sku2id");
            _helper.SetCellComment(2, 4, "qty");
            _helper.SaveAsFile();
            OpenExcel(filepath);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ExcelHelper.KillAllExcelProcess();
            ShowOK();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string filetype = comboBox1.Text;
            string extensions = string.Empty;
            SaveAsFileFormat format = SaveAsFileFormat.EXCEL;
            if (filetype == "")
            {
                MessageBox.Show("请选择一项！");
                return;
            }

            switch (filetype)
            {
                case "EXCEL":
                    extensions = "xls";
                    format = SaveAsFileFormat.EXCEL;
                    break;
                case "CSV":
                    extensions = "csv";
                    format = SaveAsFileFormat.CSV;
                    break;
                case "TEXT":
                    extensions = "txt";
                    format = SaveAsFileFormat.TEXT;
                    break;
                case "XML":
                    extensions = "xml";
                    format = SaveAsFileFormat.XML;
                    break;
                case "HTML":
                    extensions = "html";
                    format = SaveAsFileFormat.HTML;
                    break;
            }
            string filepath = _path + "导出文件." + extensions;

            _helper = new ExcelHelper(_path + "初始化数据.xls", filepath);
            _helper.SaveAsFile(format);
            MessageBox.Show("已导出文件！");
        }
    }
}
