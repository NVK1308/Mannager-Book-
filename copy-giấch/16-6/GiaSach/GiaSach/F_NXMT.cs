using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaSach
{
    public partial class F_NXMT : Form
    {
        public F_NXMT()
        {
            InitializeComponent();
        }

        private void F_NXMT_Load(object sender, EventArgs e)
        {
            lbTile.Text = Form1.tille_nx;
            if (Form1.tille_nx == "Nhập sách" || Form1.tille_nx == "Trả sách")
            {
                cbb_vt_6.Visible = false;
            }
            if (Form1.tille_nx == "Mượn sách")
            {
                button4.Text = "Hạn mượn";
            }





            // 
            tb_ds_dachon.Columns.Add("Mã sách");
            tb_ds_dachon.Columns.Add("Tên sách");
            tb_ds_dachon.Columns.Add("Số lượng");
            tb_ds_dachon.Columns.Add("Vị trí");
            tb_ds_dachon.Columns.Add("Giao dịch");

        }
        // tìm kiếm 

        private void pntk_Click(object sender, EventArgs e)
        {
            tb_search.Clear();
            tb_search.Focus();
        }
        private void tb_search_Click(object sender, EventArgs e)
        {
            tb_search.Clear();
            tb_search.Focus();
        }
        private void btsearch_Click(object sender, EventArgs e)
        {
            // timkiem(Form1.tb_hientai, Grid_ds); 

        }

        // show grid cho nhập xuất 
        public void show_nhap_xuat()
        {
            // nhập sách 
            if (Form1.tille_nx == "Nhập sách" || Form1.tille_nx == "Trả sách")
            {
                show_grid(Grid_ds, Form1.tb_danhmuc);
                addItem(lit_loc(Form1.tb_danhmuc, 0), cbb_msp_0);
                addItem(lit_loc(Form1.tb_danhmuc, 8), cbb_nxb_7);
                addItem(lit_loc(Form1.tb_danhmuc, 3), cbb_tl_3);
                addItem(lit_loc(Form1.tb_danhmuc, 1), cbb_ts_1);
                addItem(lit_loc(Form1.tb_danhmuc, 2), cbb_tg_2);

            }

            // xuất sách
            if (Form1.tille_nx == "Xuất sách" || Form1.tille_nx == "Mượn sách")
            {
                show_grid(Grid_ds, Form1.tb_hientai);

                // laod combobox
                addItem(lit_loc(Form1.tb_hientai, 0), cbb_msp_0);
                addItem(lit_loc(Form1.tb_hientai, 7), cbb_nxb_7);
                addItem(lit_loc(Form1.tb_hientai, 3), cbb_tl_3);
                addItem(lit_loc(Form1.tb_hientai, 1), cbb_ts_1);
                addItem(lit_loc(Form1.tb_hientai, 2), cbb_tg_2);
                addItem(lit_loc(Form1.tb_hientai, 6), cbb_vt_6);
            }
        }
        // hàm tất cả click 
        private void button6_Click(object sender, EventArgs e)
        {
            show_nhap_xuat(); 




        }



        // hàm tìm kiếm  
        public void timkiem(DataTable tb, DataGridView dg)
        {
            dg.Rows.Clear();
            int dem = 0;

            string t = convertToUnSign(tb_search.Text);
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                dg.Rows.Add("");
                for (int j = 0; j < tb.Columns.Count; j++)
                {
                    string a = convertToUnSign(tb.Rows[i][j].ToString());
                    if (string.Compare(t.Trim(), a.Trim(), true) == 0)
                    {
                        dem++;
                        DataGridViewRow dr = (DataGridViewRow)dg.Rows[0].Clone();
                        for (int k = 0; k < dg.Columns.Count; k++)
                        {
                            dr.Cells[k].Value = tb.Rows[i][k];
                        }
                        //dg.Rows[i].Cells[j].Value = tb.Rows[i][j];
                        dg.Rows.Add(dr);

                    }
                }
            }

            if (dem == 0)
            {
                MessageBox.Show("Không có thông tin!", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tb_search.Text = "Tìm kiếm";
            }
        }

        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        // hàm show datagrid 

        public static void show_grid(DataGridView dg, DataTable tb)
        {
            dg.Columns.Clear();
            dg.Rows.Clear();
            dg.ColumnCount = tb.Columns.Count;
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                dg.Columns[i].HeaderText = tb.Columns[i].Caption; 

            }

            for (int i = 0; i < tb.Rows.Count; i++)
            {
                dg.Rows.Add("");
                for (int j = 0; j < tb.Columns.Count; j++)
                {
                    dg.Rows[i].Cells[j].Value = tb.Rows[i][j];
                }
            }
            Form1.editGridview(dg); 
        }



        // hàm add item cbb 
        void addItem(List<string> l, ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("Tất cả");
            for (int i = 0; i < l.Count; i++)
            {
                cb.Items.Add(l[i]);
            }
        }

        // lit danh sách item 
        public List<string> lit_loc(DataTable tb, int c)
        {
            List<string> l = new List<string>();
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                l.Add(tb.Rows[i][c].ToString());
            }
            l = l.Distinct().ToList();
            return l;
        }


        // tạo bảng danh sách 
        Panel add_ds()
        {
            Panel p = new Panel(); // hàng 

            p.Dock = System.Windows.Forms.DockStyle.Top;
            p.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            p.Size = new System.Drawing.Size(721, 40);
            p.Name = l_ds[0];
            // button Mã sách 
            Button bms = new Button();
            bms.Dock = DockStyle.Left;
            bms.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            bms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bms.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            bms.ForeColor = System.Drawing.Color.Black;
            bms.Location = new System.Drawing.Point(664, 0);
            bms.Margin = new System.Windows.Forms.Padding(0);
            bms.Text = "Mã sách";
            bms.TextAlign = ContentAlignment.MiddleLeft;
            bms.Size = new System.Drawing.Size(85, 40);
            bms.BackColor = System.Drawing.Color.White;
            if (l_ds.Count > 0)
            {
                bms.Text = l_ds[0];
            }


            // button tên sách 
            Button bts = new Button();
            bts.Dock = DockStyle.Left;
            bts.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            bts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bts.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            bts.ForeColor = System.Drawing.Color.Black;
            bts.BackColor = System.Drawing.Color.White;
            bts.Margin = new System.Windows.Forms.Padding(0);
            bts.Text = "Tên sách";
            bts.TextAlign = ContentAlignment.MiddleLeft;
            bts.Size = new System.Drawing.Size(279, 40);
            bts.Text = l_ds[1];


            // cbb vị trí 
            ComboBox cbbvt = new ComboBox();
            cbbvt.BackColor = System.Drawing.Color.White;
            cbbvt.Dock = System.Windows.Forms.DockStyle.Left;
            cbbvt.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cbbvt.FormattingEnabled = true;
            cbbvt.Size = new System.Drawing.Size(91, 37);
            cbbvt.Name = "vt_" + p.Name;
            cbbvt.SelectedValueChanged += new System.EventHandler(cbbvt_SelectedValueChanged);
            // chọn vị trí 
            if (Form1.tille_nx == "Nhập sách" || Form1.tille_nx == "Trả sách")
            {
                // cùng tên 
                for (int j = 0; j < Form1.tb_hientai.Rows.Count; j++)
                {
                    string ma = Form1.tb_hientai.Rows[j][0].ToString().Trim(); // mã 

                    // cùng tên 
                    if (ma == cbbvt.Name.Split('_')[1])
                    {
                        //MessageBox.Show(ma + "\n" + cbbvt.Name.Split('_')[1]);
                        cbbvt.Items.Clear();
                        cbbvt.Items.Add(Form1.tb_hientai.Rows[j][6].ToString().Trim());
                        cbbvt.Text = (Form1.tb_hientai.Rows[j][6].ToString().Trim());
                        break;
                    }
                    else
                    {
                        for (int i = 0; i < Form1.l_vitri.Count; i++)
                        {
                            cbbvt.Items.Add(Form1.l_vitri[i]);
                        }
                    }

                }

            }
            if (Form1.tille_nx == "Xuất sách" || Form1.tille_nx == "Mượn sách")
            {
                cbbvt.Text = l_ds[3];
            }



            // cbb dao dịch 
            //ComboBox cbgd = new ComboBox();
            //cbgd.BackColor = System.Drawing.Color.White;
            //cbgd.Dock = System.Windows.Forms.DockStyle.Left;
            //cbgd.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //cbgd.FormattingEnabled = true;
            //cbgd.Size = new System.Drawing.Size(129, 37);
            //cbgd.Items.Add("Nhập sách");
            //cbgd.Items.Add("Xuất sách");
            //cbgd.Items.Add("Mượn sách");
            //cbgd.Items.Add("Trả sách");
            //cbgd.Text = l_ds[4];


            // số lượng 
            NumericUpDown n_sl = new NumericUpDown();
            n_sl.BackColor = System.Drawing.Color.White;
            n_sl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            n_sl.Dock = System.Windows.Forms.DockStyle.Left;
            n_sl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            n_sl.Size = new System.Drawing.Size(80, 38);

            n_sl.ValueChanged += new System.EventHandler(n_sl_ValueChanged);
            n_sl.Name = "sl_" + p.Name;
            if (Form1.tille_nx == "Xuất sách")
            {
                n_sl.Maximum = Convert.ToInt32(l_ds[2]);
            }

            // button xóa
            Button b_xoa = new Button();
            b_xoa.BackColor = System.Drawing.Color.White;
            b_xoa.BackgroundImage = global::GiaSach.Properties.Resources.trash;
            b_xoa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            b_xoa.Dock = System.Windows.Forms.DockStyle.Left;
            b_xoa.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            b_xoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            b_xoa.ForeColor = System.Drawing.Color.Black;
            b_xoa.Margin = new System.Windows.Forms.Padding(0);
            b_xoa.Text = "";
            b_xoa.Size = new System.Drawing.Size(57, 40);
            b_xoa.UseVisualStyleBackColor = false;
            b_xoa.Click += new System.EventHandler(xoa_click);
            b_xoa.Name = "X_" + p.Name;

            //
            p.Controls.Add(b_xoa);
           // p.Controls.Add(cbgd);
            p.Controls.Add(cbbvt);
            p.Controls.Add(n_sl);
            p.Controls.Add(bts);
            p.Controls.Add(bms);
            // list danh sách panel 
            l_pds.Add(p);
            return p;
        }
        public void xoa_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (MessageBox.Show("Bạn có muốn xóa sách này ra khỏi giỏ hàng không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = l_pds.Count - 1; i >= 0; i--)
                {
                    if (l_pds[i].Name == b.Name.Split('_')[1])
                    {
                        pn_Giohang.Controls.RemoveAt(i);
                        l_ds.RemoveAt(i);
                        l_pds.RemoveAt(i);
                    }

                }

                // xóa bảng da cho n
                for (int i = tb_ds_dachon.Rows.Count - 1; i >= 0; i--)
                {
                    if (b.Name.Split('_')[1].Trim() == tb_ds_dachon.Rows[i][0].ToString().Trim())
                    {
                        tb_ds_dachon.Rows.RemoveAt(i);

                    }
                }
            }
            MessageBox.Show(tb_ds_dachon.Rows.Count.ToString()+"\n" +l_pds.Count.ToString() ); 

        }

        // chọn nhâp xuất 
        int index = -2;
        public static DataTable table_nx = new DataTable();
        public static List<string> l_ds = new List<string>();
        List<Panel> l_pds = new List<Panel>();
        DataTable tb_ds_dachon = new DataTable();
        //  int stt_ds = 0; 
        private void Grid_ds_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //stt_ds++; 
            l_ds.Clear();
            index = e.RowIndex;
            if (index != -1)
            {
                l_ds.Add(Grid_ds.Rows[index].Cells[0].Value.ToString());
                l_ds.Add(Grid_ds.Rows[index].Cells[1].Value.ToString());
                l_ds.Add(Grid_ds.Rows[index].Cells[5].Value.ToString());
                l_ds.Add(Grid_ds.Rows[index].Cells[6].Value.ToString());
                l_ds.Add(Form1.tille_nx);

            }
            // không cho chọn 1 sản phẩm 2 lần 

            // bảng danh sách đã chọn, 
            if (l_ds.Count > 0)
            {
                tb_ds_dachon.Rows.Add(l_ds[0], l_ds[1], l_ds[2], l_ds[3], l_ds[4]);
            }

            // kiểm tra các sản phẩm đã chọn 

            int temp_trung = 0;
            for (int i = 0; i < tb_ds_dachon.Rows.Count - 1; i++)
            {
                if (l_ds.Count > 0)
                {
                    if (l_ds[0] == tb_ds_dachon.Rows[i][0])
                    {
                        temp_trung++;
                        tb_ds_dachon.Rows.RemoveAt(i);
                    }
                }

            }
            // không cho trùng 
            if (temp_trung == 0 && l_ds.Count > 0)
            {
                pn_Giohang.Controls.Add(add_ds());
            }

            dataGridView1.DataSource = tb_ds_dachon;

        }




        // đồng ý nhập xuất 

        private void Bt_Save_ls_Click(object sender, EventArgs e)
        {
            int checkdung = 0;
            int checksvt = 0, checksl2 = 0;
            int indexsai = -1; 

            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                if (tb_ds_dachon.Rows[i][2].ToString().Trim() != "0" && tb_ds_dachon.Rows[i][3].ToString() != "")
                {
                    checkdung++;

                }
                else
                {
                    checkdung = 0;
                    if (tb_ds_dachon.Rows[i][3].ToString().Trim() == "")
                    {
                        checksvt++; //  chưa chọn vị trí 
                        indexsai = i; 
                    }
                    if (tb_ds_dachon.Rows[i][2].ToString().Trim() == "0")
                    {
                        checksl2++; //  chưa chọn số lượng
                        indexsai = i;
                    }
                    break; 
                }
            }

            if (checkdung != 0 )
            {
                save_danhmuc();
                save_hientai();
                save_ls();

                F_Cauhinh.tool_save_txt(Form1.tb_ls, "ls.txt"); // lưu lịch sử vào file txt 
                F_Cauhinh.tool_save_txt(Form1.tb_danhmuc, "danhmuc.txt"); // lưu lịch sử vào file txt 
                F_Cauhinh.tool_save_txt(Form1.tb_hientai, "hientai.txt"); // lưu lịch sử vào file txt 
                // xóa dữ liệu 
                for (int i = l_pds.Count-1; i >=0 ; i--)
                {
                    pn_Giohang.Controls.RemoveAt(i);
                  
                }
                l_pds.Clear();
                l_ds.Clear(); 
                tb_ds_dachon.Rows.Clear();
            }
            else
            {
                if (checksvt != 0 )
                {
                    MessageBox.Show("Vui lòng chọn vị trí để " + lbTile.Text +" ''"+ tb_ds_dachon.Rows[indexsai][1].ToString()+"''." , "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (checksl2 != 0 )
                {
                    MessageBox.Show("Vui lòng chọn số lượng để " + lbTile.Text+" ''" + tb_ds_dachon.Rows[indexsai][1].ToString() + "''.", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }


            show_nhap_xuat();
        }

        // ham luu lich su 
        public void save_ls()
        {
          
            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                for (int j = 0; j < Form1.tb_danhmuc.Rows.Count; j++)
                {
                    if (tb_ds_dachon.Rows[i][0].ToString().Trim() == Form1.tb_danhmuc.Rows[j][0].ToString().Trim())
                    {
                        MessageBox.Show(Form1.tb_ls.Columns.Count.ToString()); 
                        // tb_ls
                        Form1.tb_ls.Rows.Add(
                                               DateTime.Now.ToShortDateString(), // ngày  
                                               tb_ds_dachon.Rows[i][4], // giao dịch 
                                               tb_ds_dachon.Rows[i][0], // mã sách 
                                               tb_ds_dachon.Rows[i][1], // tên sách
                                               Form1.tb_danhmuc.Rows[j][2],  // tác giả 
                                               Form1.tb_danhmuc.Rows[j][3],  // thể loại
                                               Form1.tb_danhmuc.Rows[j][4],  // số trang
                                               tb_ds_dachon.Rows[i][2], // số lượng
                                               tb_ds_dachon.Rows[i][3], // vị trí 
                                               Form1.tb_danhmuc.Rows[j][8],  // NXB
                                               lb_Acount.Text, // PIC
                                               ""
                                             
                                            );
                    }
                }
            }

       
            
           
        }


        // ham luu vao bang nhap 
        public void save_hientai()
        {

            for (int i = tb_ds_dachon.Rows.Count - 1 ; i >=0 ; i--)
            {
                string ma_dachon = tb_ds_dachon.Rows[i][0].ToString().Trim();
                for (int k = Form1.tb_hientai.Rows.Count - 1 ; k >=0; k--)
                {
                    string ma_hientai = Form1.tb_hientai.Rows[k][0].ToString().Trim();
                    if (ma_dachon == ma_hientai) //trùng nhau 
                    {
                        MessageBox.Show(tb_ds_dachon.Rows.Count.ToString()); 
                        if (lbTile.Text.Trim() == "Nhập sách")
                        {
                            int sl = Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString()) + Convert.ToInt32(Form1.tb_hientai.Rows[k][5].ToString());
                            Form1.tb_hientai.Rows[k][5] = sl;
                           
                        }


                        if (lbTile.Text == "Xuất sách")
                        {
                            int sl = - Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString()) + Convert.ToInt32(Form1.tb_hientai.Rows[k][5].ToString());
                           
                            Form1.tb_hientai.Rows[k][5] = sl;
                            if (sl == 0)
                            {
                                Form1.tb_hientai.Rows.RemoveAt(k);
                            }
                           // tb_ds_dachon.Rows.RemoveAt(i);
                        }
                        tb_ds_dachon.Rows.RemoveAt(i);
                        break; 
                    }
                }
            }


            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                string ma_dachon = tb_ds_dachon.Rows[i][0].ToString().Trim();
                for (int k = 0; k < Form1.tb_hientai.Rows.Count; k++)
                {
                    string ma_hientai = Form1.tb_hientai.Rows[k][0].ToString().Trim();
                    if (ma_dachon != ma_hientai)
                    {
                        int sl = Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString());
                        string vt = tb_ds_dachon.Rows[i][3].ToString();
                        for (int j = 0; j < Form1.tb_danhmuc.Rows.Count; j++)
                        {
                            if (tb_ds_dachon.Rows[i][0].ToString().Trim() == Form1.tb_danhmuc.Rows[j][0].ToString().Trim())
                            {
                                if (tb_ds_dachon.Rows[i][3].ToString() != null&& tb_ds_dachon.Rows[i][3].ToString() != "")
                                {
                                    Form1.tb_hientai.Rows.Add(
                                                    tb_ds_dachon.Rows[i][0], // mã sách 
                                                    tb_ds_dachon.Rows[i][1], // tên sách
                                                    Form1.tb_danhmuc.Rows[j][2],  // tác giả 
                                                    Form1.tb_danhmuc.Rows[j][3],  // thể loại
                                                    Form1.tb_danhmuc.Rows[j][4],  // số trang
                                                    sl.ToString(), // số lượng
                                                    tb_ds_dachon.Rows[i][3],  // vị trí
                                                    Form1.tb_danhmuc.Rows[j][8], // NXB
                                                   "" // ghi chú 
                                                    );
                                }
                                //
                                else
                                {
                                    MessageBox.Show("Vui lòng chọn vị trí bạn muốn " + lbTile.Text + "!", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        break;
                    }


                }
            }
          
         
        }

        public void save_danhmuc()
        {
            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                string ma_hientai = tb_ds_dachon.Rows[i][0].ToString().Trim();

                for (int j = 0; j < Form1.tb_danhmuc.Rows.Count; j++)
                {
                   
                    string ma_danhmuc = Form1.tb_danhmuc.Rows[j][0].ToString().Trim();
                    if (string.Compare(ma_danhmuc, ma_hientai,true) == 0)
                    {
                        if (string.Compare(lbTile.Text , "Nhập sách", true) == 0 )
                        {
                            int sl  = Convert.ToInt32(Form1.tb_danhmuc.Rows[j][5].ToString()) + Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString());
                            Form1.tb_danhmuc.Rows[j][5] = sl;
                        }
                        if (string.Compare(lbTile.Text, "Xuất sách", true) ==0)
                        {
                            int sl = (-1)*Convert.ToInt32(Form1.tb_danhmuc.Rows[j][5].ToString()) + Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString());
                            Form1.tb_danhmuc.Rows[j][5] = sl;
                        }
                        if (string.Compare(lbTile.Text, "Mượn sách", true) == 0)
                        { 
                        
                        }
                        if (string.Compare(lbTile.Text, "Trả sách", true) ==0 )
                        {
                        
                        }

                       
                    }
                }
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Form1.tb_danhmuc;

        }

        private void n_sl_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown n = (NumericUpDown)sender;

            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {

                if (tb_ds_dachon.Rows[i][0].ToString().Trim() == n.Name.Split('_')[1].Trim())
                {
                    // MessageBox.Show(tb_ds_dachon.Rows[i][0].ToString().Trim() + "\n" + "n:" + n.Name.Split('_')[1].Trim());
                    tb_ds_dachon.Rows[i][2] = n.Value.ToString();
                    // break; 
                }
            }
        }

        private void cbbvt_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                if (tb_ds_dachon.Rows[i][0].ToString().Trim() == c.Name.Split('_')[1].Trim())
                {
                    tb_ds_dachon.Rows[i][3] = c.Text;
                }
            }
        }

        private void bt_Huy_Click(object sender, EventArgs e)
        {
            if (tb_ds_dachon.Rows.Count > 0 )
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn hủy thao tác " + lbTile.Text + "?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    tb_ds_dachon.Rows.Clear();
                    for (int i = 0; i < l_pds.Count; i++)
                    {
                        pn_Giohang.Controls.Remove(l_pds[i]);
                    }
                }
            }
          
        }
    }
}
