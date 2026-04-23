using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hệ_thống_quản_lý_tuyển_dụng
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
            SetupTabControl();
        }

        // ═══════════════════════════════════════════════
        // SETUP TAB CONTROL – giống Form10
        // ═══════════════════════════════════════════════
        private void SetupTabControl()
        {
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new Size(220, 30);
            tabControl1.DrawItem += TabControl1_DrawItem;
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            foreach (TabPage page in tabControl1.TabPages)
                page.BackColor = Color.White;
        }

        // ═══════════════════════════════════════════════
        // SETUP LIST VIEWS – giống Form10
        // ═══════════════════════════════════════════════
        private void SetupListViews()
        {
            // listView1 – Tab 1: Báo cáo tuyển dụng
            listView1.OwnerDraw = true;
            listView1.DrawColumnHeader += ListView_DrawColumnHeader;
            listView1.DrawSubItem += ListView_DrawSubItem;
            listView1.DrawItem += ListView_DrawItem;

            // listView2 – Tab 2: Thống kê ứng viên
            listView2.OwnerDraw = true;
            listView2.DrawColumnHeader += ListView_DrawColumnHeader;
            listView2.DrawSubItem += ListView_DrawSubItem;
            listView2.DrawItem += ListView_DrawItem;

            // listView3 – Tab 3: Thống kê vị trí tuyển dụng
            listView3.OwnerDraw = true;
            listView3.DrawColumnHeader += ListView_DrawColumnHeader;
            listView3.DrawSubItem += ListView_DrawSubItem;
            listView3.DrawItem += ListView_DrawItem;

            // listView4 – Tab 4: Xuất báo cáo
            listView4.OwnerDraw = true;
            listView4.DrawColumnHeader += ListView_DrawColumnHeader;
            listView4.DrawSubItem += ListView_DrawSubItem;
            listView4.DrawItem += ListView_DrawItem;
        }

        // ───────────────────────────────────────────────
        // Vẽ HEADER cột (nền xanh đậm #1565C0, chữ trắng)
        // ───────────────────────────────────────────────
        private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(21, 101, 192)))
                e.Graphics.FillRectangle(brush, e.Bounds);

            using (Font font = new Font("Times New Roman", 8, FontStyle.Bold))
            using (SolidBrush txtBrush = new SolidBrush(Color.White))
                e.Graphics.DrawString(e.Header.Text, font, txtBrush,
                    e.Bounds.X + 5, e.Bounds.Y + 6);
        }

        // ───────────────────────────────────────────────
        // Vẽ DÒNG (xen kẽ trắng / xanh nhạt / xanh khi chọn)
        // ───────────────────────────────────────────────
        private void ListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Color bg = GetRowColor(e.ItemIndex, e.Item.Selected);
            using (SolidBrush brush = new SolidBrush(bg))
                e.Graphics.FillRectangle(brush, e.Bounds);
        }

        // ───────────────────────────────────────────────
        // Vẽ Ô (nền, chữ, border)
        // ───────────────────────────────────────────────
        private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Color bg = GetRowColor(e.ItemIndex, e.Item.Selected);

            using (SolidBrush brush = new SolidBrush(bg))
                e.Graphics.FillRectangle(brush, e.Bounds);

            using (Font font = new Font("Times New Roman", 8, FontStyle.Regular))
            using (SolidBrush txtBrush = new SolidBrush(Color.Black))
                e.Graphics.DrawString(e.SubItem.Text, font, txtBrush,
                    e.Bounds.X + 5, e.Bounds.Y + 4);

            using (Pen pen = new Pen(Color.FromArgb(220, 220, 220)))
                e.Graphics.DrawRectangle(pen, e.Bounds);
        }

        // Helper màu dòng
        private Color GetRowColor(int index, bool selected)
        {
            if (selected) return Color.FromArgb(187, 222, 251);   // xanh khi chọn
            return index % 2 == 0
                ? Color.White
                : Color.FromArgb(232, 240, 251);                   // xanh nhạt xen kẽ
        }

        // ═══════════════════════════════════════════════
        // VẼ TAB – giống Form10
        // ═══════════════════════════════════════════════
        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            TabPage tp = tc.TabPages[e.Index];
            bool isSelected = (tc.SelectedIndex == e.Index);

            Color bgColor = isSelected ? Color.FromArgb(21, 101, 192) : Color.White;
            Color txtColor = isSelected ? Color.White : Color.FromArgb(21, 101, 192);

            using (SolidBrush brush = new SolidBrush(bgColor))
                e.Graphics.FillRectangle(brush, e.Bounds);

            using (Font font = new Font("Times New Roman", 8, FontStyle.Bold))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                using (SolidBrush txtBrush = new SolidBrush(txtColor))
                    e.Graphics.DrawString(tp.Text, font, txtBrush, e.Bounds, sf);
            }

            if (isSelected)
            {
                using (Pen pen = new Pen(Color.FromArgb(21, 101, 192), 2))
                    e.Graphics.DrawLine(pen,
                        e.Bounds.Left, e.Bounds.Bottom - 1,
                        e.Bounds.Right, e.Bounds.Bottom - 1);
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1.Invalidate();
        }

        // ═══════════════════════════════════════════════
        // FORM LOAD
        // ═══════════════════════════════════════════════
        private void Form11_Load(object sender, EventArgs e)
        {
            SetupListViews();
        }

        // ═══════════════════════════════════════════════
        // TAB 1 – Báo cáo tuyển dụng
        // ═══════════════════════════════════════════════
        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: Lọc và hiển thị báo cáo tuyển dụng theo thời gian và vị trí
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Làm mới bộ lọc Tab 1
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            listView1.Items.Clear();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            ListViewItem item = listView1.SelectedItems[0];

            textBox1.Text = item.SubItems.Count > 0 ? item.SubItems[0].Text : "";
            textBox2.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
            textBox3.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";
            textBox4.Text = item.SubItems.Count > 3 ? item.SubItems[3].Text : "";
            textBox5.Text = item.SubItems.Count > 4 ? item.SubItems[4].Text : "";
        }

        // ═══════════════════════════════════════════════
        // TAB 2 – Thống kê ứng viên
        // ═══════════════════════════════════════════════
        private void button4_Click(object sender, EventArgs e)
        {
            // TODO: Thống kê số lượng ứng viên theo vị trí, trạng thái, tỷ lệ trúng tuyển
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Làm mới bộ lọc Tab 2
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            listView2.Items.Clear();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0) return;
            ListViewItem item = listView2.SelectedItems[0];

            textBox6.Text = item.SubItems.Count > 0 ? item.SubItems[0].Text : "";
            textBox7.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
            textBox8.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";
            textBox9.Text = item.SubItems.Count > 3 ? item.SubItems[3].Text : "";
        }

        // ═══════════════════════════════════════════════
        // TAB 3 – Thống kê vị trí tuyển dụng
        // ═══════════════════════════════════════════════
        private void button6_Click(object sender, EventArgs e)
        {
            // TODO: Thống kê vị trí tuyển dụng đang mở, đã đóng, số lượng cần tuyển và đã tuyển
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Làm mới bộ lọc Tab 3
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            listView3.Items.Clear();
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count == 0) return;
            ListViewItem item = listView3.SelectedItems[0];

            textBox10.Text = item.SubItems.Count > 0 ? item.SubItems[0].Text : "";
            textBox11.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
            textBox12.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";
            textBox13.Text = item.SubItems.Count > 3 ? item.SubItems[3].Text : "";
            textBox14.Text = item.SubItems.Count > 4 ? item.SubItems[4].Text : "";
        }

        // ═══════════════════════════════════════════════
        // TAB 4 – Xuất báo cáo
        // ═══════════════════════════════════════════════
        private void button8_Click(object sender, EventArgs e)
        {
            // TODO: Lọc và hiển thị danh sách báo cáo theo bộ lọc
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // TODO: In báo cáo
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // TODO: Xuất báo cáo ra file Excel
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // TODO: Xuất báo cáo ra file PDF
        }
    }
}