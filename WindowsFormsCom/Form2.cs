using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using micro = Microsoft.Office.Interop.Word;
using excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace WindowsFormsCom
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //第1步：打开word并且可见
            micro.Application word = new micro.ApplicationClass { Visible = true };

            //第2步：添加一个新的文档
            object o = Missing.Value;
            micro.Document doc = word.Documents.Add(ref o, ref o, ref o, ref o);

            //来一段
            micro.Paragraph para = doc.Paragraphs.Add(ref o);
            para.Range.Font.Color = micro.WdColor.wdColorDarkRed;
            para.Range.Text = "这就是个测试";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            excel.Application excelapp = new excel.ApplicationClass { Visible = true };

            object o = Missing.Value;
            excel.Workbook wb = excelapp.Workbooks.Add(o);
            excel.Worksheet ws = wb.Worksheets.get_Item(1) as excel.Worksheet;
            ws.Cells[1, 1] = "测试1";
            ws.Cells[1, 2] = "测试2";
            ws.Cells[1, 3] = "测试3";
        }
    }
}
