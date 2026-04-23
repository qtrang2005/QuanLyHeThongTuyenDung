using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hệ_thống_quản_lý_tuyển_dụng
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            SetupTabControl();
        }

        // ═══════════════════════════════════════════════
        // SETUP TAB CONTROL (giống Form6)
        // ═══════════════════════════════════════════════
        private void SetupTabControl()
        {
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new Size(210, 30); // rộng hơn vì text dài hơn Form6
            tabControl1.DrawItem += TabControl1_DrawItem;
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            foreach (TabPage page in tabControl1.TabPages)
            {
                page.BackColor = Color.White;
            }
        }

        // ═══════════════════════════════════════════════
        // SETUP LIST VIEWS (giống Form6)
        // ═══════════════════════════════════════════════
        private void SetupListViews()
        {
            // listView1 - Tab 2 (Cập nhật)
            listView1.OwnerDraw = true;
            listView1.DrawColumnHeader += ListView_DrawColumnHeader;
            listView1.DrawSubItem += ListView_DrawSubItem;
            listView1.DrawItem += ListView_DrawItem;

            // listView2 - Tab 3 (Tra cứu)
            listView2.OwnerDraw = true;
            listView2.DrawColumnHeader += ListView_DrawColumnHeader;
            listView2.DrawSubItem += ListView_DrawSubItem;
            listView2.DrawItem += ListView_DrawItem;

            // listView3 - Tab 4 (In danh sách)
            listView3.OwnerDraw = true;
            listView3.DrawColumnHeader += ListView_DrawColumnHeader;
            listView3.DrawSubItem += ListView_DrawSubItem;
            listView3.DrawItem += ListView_DrawItem;
        }

        // ───────────────────────────────────────────────
        // Vẽ HEADER cột (nền xanh đậm, chữ trắng)
        // ───────────────────────────────────────────────
        private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            // Nền xanh đậm
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(21, 101, 192)))
                e.Graphics.FillRectangle(brush, e.Bounds);

            // Chữ trắng, Times New Roman, 8, đậm
            Font font = new Font("Times New Roman", 8, FontStyle.Bold);
            using (SolidBrush txtBrush = new SolidBrush(Color.White))
                e.Graphics.DrawString(e.Header.Text, font, txtBrush,
                    e.Bounds.X + 5, e.Bounds.Y + 6);
            font.Dispose();
        }

        // ───────────────────────────────────────────────
        // Vẽ từng DÒNG (xen kẽ màu trắng / xanh nhạt)
        // ───────────────────────────────────────────────
        private void ListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Color bgColor = e.ItemIndex % 2 == 0
                ? Color.White
                : Color.FromArgb(232, 240, 251); // xanh nhạt

            if (e.Item.Selected)
                bgColor = Color.FromArgb(187, 222, 251); // xanh khi chọn

            using (SolidBrush brush = new SolidBrush(bgColor))
                e.Graphics.FillRectangle(brush, e.Bounds);
        }

        // ───────────────────────────────────────────────
        // Vẽ từng Ô trong dòng
        // ───────────────────────────────────────────────
        private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Color bgColor = e.ItemIndex % 2 == 0
                ? Color.White
                : Color.FromArgb(232, 240, 251);

            if (e.Item.Selected)
                bgColor = Color.FromArgb(187, 222, 251);

            // Vẽ nền ô
            using (SolidBrush brush = new SolidBrush(bgColor))
                e.Graphics.FillRectangle(brush, e.Bounds);

            // Vẽ chữ
            Font font = new Font("Times New Roman", 8, FontStyle.Regular);
            using (SolidBrush txtBrush = new SolidBrush(Color.Black))
                e.Graphics.DrawString(e.SubItem.Text, font, txtBrush,
                    e.Bounds.X + 5, e.Bounds.Y + 4);
            font.Dispose();

            // Vẽ border ô
            using (Pen pen = new Pen(Color.FromArgb(220, 220, 220)))
                e.Graphics.DrawRectangle(pen, e.Bounds);
        }

        // ═══════════════════════════════════════════════
        // VẼ TAB (giống Form6)
        // ═══════════════════════════════════════════════
        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            TabPage tp = tc.TabPages[e.Index];

            bool isSelected = (tc.SelectedIndex == e.Index);

            Color bgColor = isSelected ? Color.FromArgb(21, 101, 192) : Color.White;
            Color txtColor = isSelected ? Color.White : Color.FromArgb(21, 101, 192);

            // Vẽ nền tab
            using (SolidBrush brush = new SolidBrush(bgColor))
                e.Graphics.FillRectangle(brush, e.Bounds);

            // Font Times New Roman, 8, đậm
            Font font = new Font("Times New Roman", 8, FontStyle.Bold);

            // Vẽ chữ căn giữa
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            using (SolidBrush txtBrush = new SolidBrush(txtColor))
                e.Graphics.DrawString(tp.Text, font, txtBrush, e.Bounds, sf);

            // Vẽ border dưới tab đang chọn
            if (isSelected)
            {
                using (Pen pen = new Pen(Color.FromArgb(21, 101, 192), 2))
                    e.Graphics.DrawLine(pen,
                        e.Bounds.Left,
                        e.Bounds.Bottom - 1,
                        e.Bounds.Right,
                        e.Bounds.Bottom - 1);
            }

            font.Dispose();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1.Invalidate();
        }

        // ═══════════════════════════════════════════════
        // FORM LOAD
        // ═══════════════════════════════════════════════
        private void Form7_Load(object sender, EventArgs e)
        {
            SetupListViews();
        }

        // ═══════════════════════════════════════════════
        // TAB 1 - Thêm vị trí tuyển dụng
        // ═══════════════════════════════════════════════
        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: Thêm mới vị trí tuyển dụng
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // TODO: Làm mới form nhập liệu Tab 1
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        // ═══════════════════════════════════════════════
        // TAB 2 - Cập nhật vị trí tuyển dụng
        // ═══════════════════════════════════════════════
        private void button4_Click(object sender, EventArgs e)
        {
            // TODO: Tìm kiếm vị trí để cập nhật
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Load thông tin vị trí đã chọn vào form cập nhật
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                textBox7.Text = item.SubItems.Count > 0 ? item.SubItems[0].Text : "";
                textBox8.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
                textBox11.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";
                comboBox3.Text = item.SubItems.Count > 3 ? item.SubItems[3].Text : "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // TODO: Cập nhật vị trí tuyển dụng
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // TODO: Xoá vị trí tuyển dụng
        }

        // ═══════════════════════════════════════════════
        // TAB 3 - Tra cứu vị trí tuyển dụng
        // ═══════════════════════════════════════════════
        private void button7_Click(object sender, EventArgs e)
        {
            // TODO: Tìm kiếm / tra cứu vị trí
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // TODO: Làm mới bộ lọc Tab 3
            textBox12.Clear();
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Hiển thị chi tiết vị trí đã chọn (read-only)
            if (listView2.SelectedItems.Count > 0)
            {
                ListViewItem item = listView2.SelectedItems[0];
                textBox13.Text = item.SubItems.Count > 0 ? item.SubItems[0].Text : "";
                textBox14.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
                textBox17.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";
                textBox18.Text = item.SubItems.Count > 3 ? item.SubItems[3].Text : "";
            }
        }

        // ═══════════════════════════════════════════════
        // TAB 4 - In danh sách vị trí
        // ═══════════════════════════════════════════════
        private void button9_Click(object sender, EventArgs e)
        {
            // TODO: Xem trước danh sách theo bộ lọc
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // TODO: In danh sách vị trí tuyển dụng
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // TODO: Xuất danh sách ra Excel
        }
    }
}