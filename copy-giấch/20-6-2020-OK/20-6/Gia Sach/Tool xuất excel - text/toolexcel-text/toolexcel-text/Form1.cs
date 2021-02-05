using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace toolexcel_text
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 
        DataTable tb = new DataTable(); 
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
        
            tb = TableTonKho("danh muc.xlsx");
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                DataGridViewRow dr = (DataGridViewRow)dataGridView3.Rows[0].Clone();
                for (int j = 0; j < dataGridView3.Columns.Count; j++)
                {
                    dr.Cells[j].Value = tb.Rows[i][j];
                }
                dataGridView3.Rows.Add(dr);
            }

        }

       
        private void save_Data( DataTable Table_LS)
        {
            string[] tr = new string[Table_LS.Rows.Count];
            for (int i = 0; i < Table_LS.Rows.Count; i++)
            {
                tr[i] = string.Format("{0,-10}", Table_LS.Rows[i][0].ToString()) + "|" +
                        string.Format("{0,-60}", Table_LS.Rows[i][1].ToString()) + "|" +
                        string.Format("{0,-60}", Table_LS.Rows[i][2].ToString()) + "|" +
                        string.Format("{0,-50}", Table_LS.Rows[i][3].ToString()) + "|" +
                        string.Format("{0,-15}", Table_LS.Rows[i][4].ToString()) + "|" +
                        string.Format("{0,-15}", Table_LS.Rows[i][5].ToString()) + "|" +
                        string.Format("{0,-40}", Table_LS.Rows[i][6].ToString()) + "|" +
                        string.Format("{0,-30}", Table_LS.Rows[i][7].ToString()) + "|" +
                        string.Format("{0,-30}", Table_LS.Rows[i][8].ToString());
            }
            File.WriteAllLines(Application.StartupPath + @"\" + "danhmuc.txt", tr);
        }


        // load ex 
        public DataTable TableTonKho( string filename)
        {

            DataTable tableLichSu = new DataTable();
            var stream = File.Open(Application.StartupPath + @"\" + filename, FileMode.Open, FileAccess.Read);
            IExcelDataReader reader;
            try
            {
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            catch
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            DataSet ds = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            tableLichSu = ds.Tables[0]; 

            return tableLichSu;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            save_Data(tb); 
        }


        //
//        xuat file text  
        public void xuat_txt()
        {
            
        }
    }
}
