using GiaSach;
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

namespace Stepp2
{
    public partial class FLogin : Form
    {
        public FLogin()
        {
            InitializeComponent();
            tPass.UseSystemPasswordChar = true;
            LoadFileAcount();

        }
        // load File acount(txt)
        public static string file = Application.StartupPath + @"\" + "acount.txt";
        public static DataTable Table_Acount = new DataTable();
      

        void LoadFileAcount()
        {

            //  Table_Acount Table_Acount.

            Table_Acount.Rows.Clear();
            Table_Acount.Columns.Clear();
            Table_Acount.Columns.Add("Mã nhân viên");
            Table_Acount.Columns.Add("Tên nhân viên");
            Table_Acount.Columns.Add("Năm Sinh");
            Table_Acount.Columns.Add("Giới Tính");
            Table_Acount.Columns.Add("CMND");
            Table_Acount.Columns.Add("SĐT");
            Table_Acount.Columns.Add("Địa Chỉ");
            Table_Acount.Columns.Add("ID");
            Table_Acount.Columns.Add("Mật khẩu");
            Table_Acount.Columns.Add("Phân quyền");


            //
            string[] lines = File.ReadAllLines(Application.StartupPath + @"\" + "acount.txt"); // đọc tất cả các dòng lưu vào một mảng string
            string[] values;                         // giá trị của từng dòng


            for (int i = 0; i < lines.Length; i++)
            {

                values = lines[i].ToString().Split('|');
                string[] row = new string[values.Length];
                for (int j = 0; j < values.Length; j++)
                {
                    if (values[j] != null && values[j] != "")
                    {
                        row[j] = values[j].Trim();
                    }
                    
                }
                Table_Acount.Rows.Add(row);

            }
            for (int i = Table_Acount.Rows.Count - 1; i >= 0; i--)
            {
                string a = Table_Acount.Rows[i][0].ToString() +
                    Table_Acount.Rows[i][1].ToString() +
                    Table_Acount.Rows[i][2].ToString() +
                    Table_Acount.Rows[i][3].ToString() +
                    Table_Acount.Rows[i][4].ToString() +
                    Table_Acount.Rows[i][5].ToString() +
                    Table_Acount.Rows[i][6].ToString() +
                    Table_Acount.Rows[i][7].ToString() +
                    Table_Acount.Rows[i][8].ToString() +
                    Table_Acount.Rows[i][9].ToString();
                if (a.Trim() == null && a.Trim() == "")
                {
                    MessageBox.Show("ok");
                    FLogin.Table_Acount.Rows.RemoveAt(i);
                }
            }
            dataGridView1.DataSource = Table_Acount; 
        }

        // DANG NHAP 
        public static string acount;
        public void Acount()
        {
            int demt = 0;
            int demf = 0;
            int demf2 = 0;
            Form1 f = new Form1();
            for (int i = 0; i < Table_Acount.Rows.Count; i++)
            {
               // acount = "";
               
                if (tUser.Text == Table_Acount.Rows[i][7].ToString().Trim() && tPass.Text == Table_Acount.Rows[i][8].ToString())
                {   
                    acount = Table_Acount.Rows[i][1].ToString(); // đổi thàng một cột phân quyền mới có tên quản lý/ nhân viên
                    demt++;
                    break;
                }
                // 
                if (tUser.Text != Table_Acount.Rows[i][7].ToString() && tPass.Text == Table_Acount.Rows[i][8].ToString())
                {

                    demf++;
                }
                if (tUser.Text == Table_Acount.Rows[i][7].ToString() && tPass.Text != Table_Acount.Rows[i][8].ToString())
                {

                    demf2++;
                }
              
            }
            if (demf > 0)
            {
                lbNam.Visible = true;
                lbPass.Visible = false;
                tUser.Text = "";
                tUser.Focus();
            }
            if (demf2 != 0)
            {
                lbPass.Visible = true;
                lbNam.Visible = false;
                tPass.Text = "";
                tPass.Focus();
            }
            if (demt != 0)
            {
                
                if (f.IsDisposed == true)
                {
                    f = new Form1();
                   
                }
               
                this.Hide();
                f.ShowDialog();
                this.Close();

            }
        }



        // setup pass
   
        private void button1_Click_1(object sender, EventArgs e)
        {
          
            Acount();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void tPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {

                tPass.UseSystemPasswordChar = true;
            }
            else
            {
                tPass.UseSystemPasswordChar = false;
            }
        }
    }
}

