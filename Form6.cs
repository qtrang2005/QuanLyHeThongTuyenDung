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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            SetupTabControl();
        }

        private void SetupTabControl()
        {
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new Size(160, 30);
            tabControl1.DrawItem += TabControl1_DrawItem;
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            foreach (TabPage page in tabControl1.TabPages)
            {
                page.BackColor = Color.White;
            }
        }
        private void SetupListView()
        {
            // listView1
            listView1.OwnerDraw = true;
            listView1.DrawColumnHeader += ListView1_DrawColumnHeader;
            listView1.DrawSubItem += ListView1_DrawSubItem;
            listView1.DrawItem += ListView1_DrawItem;

            // listView2
            listView2.OwnerDraw = true;
            listView2.DrawColumnHeader += ListView1_DrawColumnHeader;
            listView2.DrawSubItem += ListView1_DrawSubItem;
            listView2.DrawItem += ListView1_DrawItem;

            listView3.OwnerDraw = true;
            listView3.DrawColumnHeader += ListView1_DrawColumnHeader;
            listView3.DrawSubItem += ListView1_DrawSubItem;
            listView3.DrawItem += ListView1_DrawItem;
        }

        // Vẽ HEADER (hàng Tài khoản | Phân loại)
        private void ListView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

        // Vẽ từng DÒNG (xen kẽ màu)
        private void ListView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // Màu xen kẽ: trắng và xanh nhạt
            Color bgColor = e.ItemIndex % 2 == 0
                ? Color.White
                : Color.FromArgb(232, 240, 251); // xanh nhạt

            // Nếu đang được chọn thì màu khác
            if (e.Item.Selected)
                bgColor = Color.FromArgb(187, 222, 251);

            using (SolidBrush brush = new SolidBrush(bgColor))
                e.Graphics.FillRectangle(brush, e.Bounds);
        }

        // Vẽ từng Ô trong dòng
        private void ListView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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

            // Font Times New Roman, cỡ 8, in đậm
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

            // Dispose font sau khi dùng
            font.Dispose();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1.Invalidate();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            SetupListView();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}