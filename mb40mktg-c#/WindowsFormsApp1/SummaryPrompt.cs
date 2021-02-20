using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SummaryPrompt : Form
    {
        public string tag = string.Empty;

        public SummaryPrompt()
        {
            InitializeComponent();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            string response = string.Empty;
            string filename = string.Empty;
            bool go = false;
            Cursor.Current = Cursors.WaitCursor;
            RestClient restClient = new RestClient();
            switch(tag)
            {
                case "DAILY":
                    restClient.endPoint = Settings.baseUrl
                        + "/api/reports/day";
                    restClient.tag = "daily";
                    restClient.header = dayPicker.Value.ToString("MM/dd/yyyy");
                    response = restClient.GetRequest();
                    Console.WriteLine(response);
                    filename = "Daily Collection";
                    go = true;
                    break;
                case "WEEKLY":
                    if (Convert.ToInt32(week_tb.Text) >= 1 && Convert.ToInt32(week_tb.Text) <= 52)
                    {
                        restClient.endPoint = Settings.baseUrl
                        + "/api/reports/week";
                        restClient.tag = "weekly";
                        restClient.header = week_tb.Text.ToString();
                        response = restClient.GetRequest();
                        Console.WriteLine(response);
                        filename = "Weekly Collection";
                        go = true;
                    } else
                    {
                        MessageBox.Show("Input value is out of range.", "Report",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        go = false;
                    }
                        
                    break;
                case "MONTHLY":
                    if (Convert.ToInt32(week_tb.Text) >= 1 && Convert.ToInt32(week_tb.Text) <= 12)
                    {
                        restClient.endPoint = Settings.baseUrl
                            + "/api/reports/month";
                        restClient.tag = "monthly";
                        restClient.header = week_tb.Text.ToString();
                        response = restClient.GetRequest();
                        Console.WriteLine(response);
                        filename = "Monthly Collection";
                        go = true;
                    }
                    else
                    {
                        MessageBox.Show("Input value is out of range.", "Report",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        go = false;
                    }
                    break;
                case "CUSTOMER":
                    restClient.endPoint = Settings.baseUrl
                        + "/api/transaction";
                    response = restClient.GetRequest();
                    Console.WriteLine(response);
                    go = false;

                    var transactions = JsonConvert.DeserializeObject<List<Payment>>(response);
                    if (transactions.Count != 0)
                    {
                        summary_data.DataSource = transactions;
                        List<Payment> payment = new List<Payment>();
                        foreach (Payment p in transactions)
                        {
                            if (p.last_name.Equals(week_tb.Text.ToString()))
                            {
                                payment.Add(p);
                            }
                        }
                        if (payment.Count != 0)
                        {
                            summary_data.DataSource = payment;
                            ImportDataGridViewDataToExcelSheet(summary_data, "Customer Payment - " + week_tb.Text.ToString(), Payment.headers);
                            this.Close();
                        } else
                        {
                            MessageBox.Show("No data.", "Report",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        }
                    }
                    break;
            }
            
            if (go)
            {
                var summary = JsonConvert.DeserializeObject<List<Summary>>(response);
                if (summary != null && summary.Count != 0)
                {
                    summary_data.DataSource = summary;
                    ImportDataGridViewDataToExcelSheet(summary_data, filename, Summary.headers);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No data.", "Report",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void SummaryPrompt_Load(object sender, EventArgs e)
        {
            switch(tag)
            {
                case "DAILY":
                    select_lbl.Text = "Select Day:";
                    dayPicker.Visible = true;
                    week_lbl.Visible = false;
                    week_tb.Visible = false;
                    break;
                case "WEEKLY":
                    select_lbl.Text = "Select Week:";
                    dayPicker.Visible = false;
                    week_lbl.Visible = true;
                    week_tb.Visible = true;
                    week_lbl.Text = "Week (1-52):";
                    break;
                case "MONTHLY":
                    select_lbl.Text = "Select Month:";
                    dayPicker.Visible = false;
                    week_lbl.Visible = true;
                    week_tb.Visible = true;
                    week_lbl.Text = "Month (1-12):";
                    break;
                case "CUSTOMER":
                    select_lbl.Text = "Enter Client Last Name:";
                    dayPicker.Visible = false;
                    week_lbl.Visible = true;
                    week_tb.Visible = true;
                    week_lbl.Text = "Last Name:";
                    break;
            }
        }

        private void ImportDataGridViewDataToExcelSheet(DataGridView datagrid, string filename, string[] headers)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            for (int x = 1; x < datagrid.Columns.Count + 1; x++)
            {
                xlWorkSheet.Cells[1, x] = headers[x];
            }

            for (int i = 0; i < datagrid.Rows.Count; i++)
            {
                for (int j = 0; j < datagrid.Columns.Count; j++)
                {
                    xlWorkSheet.Cells[i + 2, j + 1] = datagrid.Rows[i].Cells[j].Value.ToString();
                }
            }

            var saveFileDialoge = new SaveFileDialog();
            saveFileDialoge.FileName = filename;
            saveFileDialoge.DefaultExt = ".xlsx";
            if (saveFileDialoge.ShowDialog() == DialogResult.OK)
            {
                xlWorkBook.SaveAs(saveFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
