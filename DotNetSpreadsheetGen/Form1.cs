using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;

namespace DotNetSpreadsheetGen
{
    public partial class Form1 : Form
    {
        SpreadsheetUtils oEngine;
        Excel.Application oExcel;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;

        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01);
            dateTimePicker2.Value = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month));
            oEngine = new SpreadsheetUtils();
            
        }

        private void btGen_Click(object sender, EventArgs e)
        {
            // textBox2.Text = oEngine.IsWeekDay(new DateTime(2019, 10, 7)).ToString();
            String sFileName;
            oExcel = oExcel is null ? new Excel.Application() : oExcel;
            sFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resource\time sheet.xls");

            xlWorkBook = oExcel.Workbooks.Open(sFileName, System.Reflection.Missing.Value, true /*Read-only*/ );
            xlWorkSheet = oExcel.Worksheets["Sheet1"];
            
            xlWorkSheet.Cells[6, 3] = EmployeeTB.Text; //C6
            xlWorkSheet.Cells[6, 15] = ManagerTB.Text; //O6
            xlWorkSheet.Cells[8, 3] = dateTimePicker1.Value.ToString("MM/dd/yyyy"); // C8
            xlWorkSheet.Cells[8, 15] = dateTimePicker2.Value.ToString("MM/dd/yyyy"); // O8
            xlWorkSheet.Columns["O:O"].AutoFit();
            
            xlWorkSheet.Cells[1, 17] = ClientTB.Text;

            for (DateTime date = dateTimePicker1.Value; date <= dateTimePicker2.Value; date = date.AddDays(1.0))
            {
                int iRow = date.Day < 16 ? 12 : 13;
                int iCol = date.Day == 16 ? 2 : (date.Day > 16 ? date.Day - 15 : date.Day) + 1;

                if (oEngine.IsWeekDay(date))
                    xlWorkSheet.Cells[iRow, iCol] = 8;
            }

            oExcel.Visible = true;
        }
        
    }
}
