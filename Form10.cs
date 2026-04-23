using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hệ_thống_quản_lý_tuyển_dụng
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            SetupTabControl();
        }

        // ═══════════════════════════════════════════════
        // SETUP TAB CONTROL – giống Form8/Form9
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
        // SETUP LIST VIEWS – giống Form8/Form9
        // ═══════════════════════════════════════════════
        private void SetupListViews()
        {
            // listView1 – Tab 1
            listView1.OwnerDraw = true;
            listView1.DrawColumnHeader += ListView_DrawColumnHeader;
            listView1.DrawSubItem += ListView_DrawSubItem;
            listView1.DrawItem += ListView_DrawItem;

            // listView2 – Tab 2
            listView2.OwnerDraw = true;
            listView2.DrawColumnHeader += ListView_DrawColumnHeader;
            listView2.DrawSubItem += ListView_DrawSubItem;
            listView2.DrawItem += ListView_DrawItem;

            // listView3 – Tab 3
            listView3.OwnerDraw = true;
            listView3.DrawColumnHeader += ListView_DrawColumnHeader;
            listView3.DrawSubItem += ListView_DrawSubItem;
            listView3.DrawItem += ListView_DrawItem;

            // listView4 – Tab 4
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
        // VẼ TAB – giống Form8/Form9
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
        private void Form10_Load(object sender, EventArgs e)
        {
            SetupListViews();
        }

        // ═══════════════════════════════════════════════
        // TAB 1 – Lên lịch phỏng vấn
        // ═══════════════════════════════════════════════
        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: Lưu lịch phỏng vấn mới vào CSDL
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Làm mới form lên lịch
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            ListViewItem item = listView1.SelectedItems[0];

            textBox1.Text = item.SubItems.Count > 0 ? item.SubItems[0].Text : "";
            textBox2.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
            comboBox2.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";

            if (item.SubItems.Count > 3 && DateTime.TryParse(item.SubItems[3].Text, out DateTime ngay))
                dateTimePicker1.Value = ngay;

            comboBox1.Text = item.SubItems.Count > 5 ? item.SubItems[5].Text : "";
        }

        // ═══════════════════════════════════════════════
        // TAB 2 – Nhập kết quả phỏng vấn
        // ═══════════════════════════════════════════════
        private void button4_Click(object sender, EventArgs e)
        {
            // TODO: Tìm kiếm lịch phỏng vấn theo tên / vị trí / ngày
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0) return;
            ListViewItem item = listView2.SelectedItems[0];

            textBox6.Text = item.SubItems.Count > 0 ? item.SubItems[0].Text : "";
            textBox7.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
            textBox8.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // TODO: Lưu kết quả đánh giá và nhận xét sau buổi phỏng vấn
        }

        // ═══════════════════════════════════════════════
        // TAB 3 – Tra cứu lịch phỏng vấn
        // ═══════════════════════════════════════════════
        private void button6_Click(object sender, EventArgs e)
        {
            // TODO: Tra cứu lịch phỏng vấn theo ngày / ứng viên / vị trí
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Làm mới bộ lọc Tab 3
            textBox11.Clear();
            comboBox6.SelectedIndex = -1;
            dateTimePicker3.Value = DateTime.Now;
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count == 0) return;
            ListViewItem item = listView3.SelectedItems[0];

            textBox12.Text = item.SubItems.Count > 0 ? item.SubItems[0].Text : "";
            textBox13.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
            textBox14.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";

            if (item.SubItems.Count > 3 && DateTime.TryParse(item.SubItems[3].Text, out DateTime ngay))
                textBox15.Text = ngay.ToShortDateString();

            textBox16.Text = item.SubItems.Count > 4 ? item.SubItems[4].Text : "";
        }

        // ═══════════════════════════════════════════════
        // TAB 4 – In danh sách phỏng vấn
        // ═══════════════════════════════════════════════
        private void button8_Click(object sender, EventArgs e)
        {
            // TODO: Lọc và hiển thị danh sách phỏng vấn theo bộ lọc
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // TODO: In danh sách phỏng vấn
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // TODO: Xuất danh sách phỏng vấn ra Excel
        }
    }
}