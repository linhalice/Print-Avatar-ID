using System.Diagnostics;

namespace Print_Avatar_ID
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sourceImagePath = ChooseImageFile();

            if (string.IsNullOrEmpty(sourceImagePath))
            {
                Console.WriteLine("Không có ảnh nào được chọn.");
                return;
            }
            string outputImagePath = "Ok.jpg"; // Đường dẫn lưu ảnh kết quả

            // Resize ảnh nhỏ về kích thước 4x6 cm (472x708 pixels)
            Size smallImageSize = new Size(472, 708);

            // Kích thước tờ A4 (2480x3508 pixels)
            Size a4Size = new Size(2480, 3508);

            // Khoảng cách giữa các ảnh
            int spacing = 1; // Thay đổi giá trị này để thay đổi khoảng cách

            using (Image sourceImage = Image.FromFile(sourceImagePath))
            using (Bitmap a4Image = new Bitmap(a4Size.Width, a4Size.Height))
            using (Graphics g = Graphics.FromImage(a4Image))
            {
                g.Clear(Color.White); // Nền trắng

                for (int y = 0; y + smallImageSize.Height <= a4Size.Height; y += smallImageSize.Height + spacing)
                {
                    for (int x = 0; x + smallImageSize.Width <= a4Size.Width; x += smallImageSize.Width + spacing)
                    {
                        g.DrawImage(sourceImage, new Rectangle(x+40, y+40, smallImageSize.Width, smallImageSize.Height));
                    }
                }

                a4Image.Save(outputImagePath); // Lưu ảnh
            }

            Console.WriteLine("Ảnh đã được ghép và lưu thành công!");
        }

        static string ChooseImageFile()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Chọn ảnh";
                dialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
                else
                {
                    return null;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }
        static void OpenDirectory(string path)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Không thể mở thư mục: {ex.Message}");
            }
        }
    }
}