using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace Screen_N_Copy
{
    public partial class Main : Form
    {
        public Main()
        {
            if (!Directory.Exists("tessdata"))
            {
                MessageBox.Show("Awaiting File Installation...\nDo NOT open again.");

                try
                {
                    using (var client = new WebClient())
                    {
                        try
                        {
                            client.DownloadFile("https://github.com/feticks/ScreenNCopy/raw/main/bin/Debug/tessdata.zip", "tessdata.zip");
                            new DirectoryInfo("tessdata.zip").Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                        }
                        catch
                        {
                            MessageBox.Show("Error. Tessdata failed to Install.");
                            System.Windows.Forms.Application.Exit();
                        }

                        try
                        {
                            Directory.CreateDirectory("tessdata");
                            new DirectoryInfo("tessdata").Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                            ZipFile.ExtractToDirectory("tessdata.zip", "tessdata");
                        }
                        catch
                        {
                            MessageBox.Show("Error. Tessdata failed to Extract.");
                            System.Windows.Forms.Application.Exit();
                        }

                        try
                        {
                            File.Delete("tessdata.zip");
                        }
                        catch
                        {
                            MessageBox.Show("Error. Tessdata failed to remove.");
                            System.Windows.Forms.Application.Exit();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Error. An Internet Connection is required to proceed.");
                    System.Windows.Forms.Application.Exit();
                }
            }

            if (!Directory.Exists("x86"))
            {
                try
                {
                    using (var client2 = new WebClient())
                    {
                        try
                        {
                            Directory.CreateDirectory("x86");
                            client2.DownloadFile("https://github.com/feticks/ScreenNCopy/raw/main/bin/Debug/x86/tesseract.dll", "x86//tesseract.dll");
                            new DirectoryInfo("x86").Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                        }
                        catch
                        {
                            MessageBox.Show("Error. DLL x86 failed to Install.");
                            System.Windows.Forms.Application.Exit();
                        }
                    }
                }
                catch { }
            }

            if (!Directory.Exists("x86"))
            {
                try
                {
                    using (var client3 = new WebClient())
                    {
                        try
                        {
                            Directory.CreateDirectory("x64");
                            client3.DownloadFile("https://github.com/feticks/ScreenNCopy/raw/main/bin/Debug/x64/tesseract.dll", "x64//tesseract.dll");
                            new DirectoryInfo("x64").Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                        }
                        catch
                        {
                            MessageBox.Show("Error. DLL x64 failed to Install.");
                            System.Windows.Forms.Application.Exit();
                        }
                    }
                }
                catch { }
            }

            try
            {
                using (var client4 = new WebClient())
                {
                    client4.DownloadFile("http://norvig.com/big.txt", "big.txt");
                    new DirectoryInfo("big.txt").Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
            }
            catch { }

            InitializeComponent();
            this.TopMost = true;
        }

        //Button
        private async void Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            await System.Threading.Tasks.Task.Delay(250);
            new Screenshot().Show();
        }


        //Close ALL Forms on Exit.
        private void Close(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        public static bool AutoCorrect = true;
        private void CheckedChanged(object sender, EventArgs e)
        {
            if (autocorrect.Checked)
            {
                AutoCorrect = true;
            }
            else
            {
                AutoCorrect = false;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
