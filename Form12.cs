using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hệ_thống_quản_lý_tuyển_dụng
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
            SetupTabControl();
        }

        // ═══════════════════════════════════════════════
        // SETUP TAB CONTROL – giống Form10/Form11
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
        // SETUP LIST VIEWS
        // ═══════════════════════════════════════════════
        private void SetupListViews()
        {
            // listView1 – Tab 1: Tìm kiếm vị trí
            listView1.OwnerDraw = true;
            listView1.DrawColumnHeader += ListView_DrawColumnHeader;
            listView1.DrawSubItem += ListView_DrawSubItem;
            listView1.DrawItem += ListView_DrawItem;

            // listView2 – Tab 4: Theo dõi lịch sử ứng tuyển
            listView2.OwnerDraw = true;
            listView2.DrawColumnHeader += ListView_DrawColumnHeader;
            listView2.DrawSubItem += ListView_DrawSubItem;
            listView2.DrawItem += ListView_DrawItem;
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
            if (selected) return Color.FromArgb(187, 222, 251);
            return index % 2 == 0
                ? Color.White
                : Color.FromArgb(232, 240, 251);
        }

        // ═══════════════════════════════════════════════
        // VẼ TAB – giống Form10/Form11
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
        private void Form12_Load(object sender, EventArgs e)
        {
            SetupListViews();
        }

        // ═══════════════════════════════════════════════
        // TAB 1 – Tìm kiếm vị trí
        // ═══════════════════════════════════════════════
        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: Tìm kiếm và lọc vị trí tuyển dụng phù hợp
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Làm mới bộ lọc Tab 1
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            listView1.Items.Clear();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            ListViewItem item = listView1.SelectedItems[0];

            textBox2.Text = item.SubItems.Count > 0 ? item.SubItems[0].Text : "";
            textBox3.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
            textBox4.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";
            textBox5.Text = item.SubItems.Count > 3 ? item.SubItems[3].Text : "";
            textBox6.Text = item.SubItems.Count > 4 ? item.SubItems[4].Text : "";
            richTextBox1.Text = item.SubItems.Count > 5 ? item.SubItems[5].Text : "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // TODO: Chuyển sang Tab 2 để ứng tuyển vị trí đang chọn
            if (listView1.SelectedItems.Count > 0)
            {
                comboBox4.Text = textBox2.Text;
                tabControl1.SelectedIndex = 1;
            }
        }

        // ═══════════════════════════════════════════════
        // TAB 2 – Ứng tuyển vị trí
        // ═══════════════════════════════════════════════
        private void button5_Click(object sender, EventArgs e)
        {
            // TODO: Nộp hồ sơ ứng tuyển vào vị trí đã chọn
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Làm mới form ứng tuyển Tab 2
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            comboBox4.SelectedIndex = -1;
            richTextBox2.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // TODO: Đính kèm file CV
        }

        // ═══════════════════════════════════════════════
        // TAB 3 – Cập nhật hồ sơ cá nhân
        // ═══════════════════════════════════════════════
        private void button8_Click(object sender, EventArgs e)
        {
            // TODO: Lưu thông tin cá nhân, CV và các thông tin liên quan
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Làm mới form hồ sơ Tab 3
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear();
            comboBox5.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            richTextBox3.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // TODO: Tải lên file CV (PDF/Word)
        }

        // ═══════════════════════════════════════════════
        // TAB 4 – Theo dõi lịch sử ứng tuyển
        // ═══════════════════════════════════════════════
        private void button11_Click(object sender, EventArgs e)
        {
            // TODO: Tra cứu lịch sử ứng tuyển theo bộ lọc
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // Làm mới bộ lọc Tab 4
            comboBox6.SelectedIndex = -1;
            comboBox7.SelectedIndex = -1;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            listView2.Items.Clear();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0) return;
            ListViewItem item = listView2.SelectedItems[0];

            textBox19.Text = item.SubItems.Count > 0 ? item.SubItems[0].Text : "";
            textBox20.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
            textBox21.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";
            textBox22.Text = item.SubItems.Count > 3 ? item.SubItems[3].Text : "";
            textBox23.Text = item.SubItems.Count > 4 ? item.SubItems[4].Text : "";
        }
    }
}