using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Patagames.Ocr;
using static Screen_N_Copy.AutoCorrect;

namespace Screen_N_Copy
{
    public partial class Screenshot : Form
	{
		//Start
		public Screenshot()
		{
			InitializeComponent();

			Hide();
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics.FromImage(bitmap).CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
				pictureBox1.Size = new Size(Width, Height);
				pictureBox1.Image = Image.FromStream(memoryStream);
			}
			Show();
			Cursor = Cursors.Cross;
		}

		int selectX;
		int selectY;
		int selectWidth;
		int selectHeight;
		public Pen selectPen;

		bool start = false;

		//Mouse Move
		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if (pictureBox1.Image == null)
			{
				return;
			}

			if (start)
			{
				pictureBox1.Refresh();
				selectWidth = e.X - selectX;
				selectHeight = e.Y - selectY;
				pictureBox1.CreateGraphics().DrawRectangle(selectPen, selectX, selectY, selectWidth, selectHeight);
			}
		}

		//Mouse Down
		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			if (!start)
			{
				if (e.Button == MouseButtons.Left)
				{
					selectX = e.X;
					selectY = e.Y;
					selectPen = new Pen(Color.Red, 1f);
					selectPen.DashStyle = DashStyle.DashDotDot;
				}
				pictureBox1.Refresh();
				start = true;
				return;
			}

			if (pictureBox1.Image == null)
			{
				return;
			}

			if (e.Button == MouseButtons.Left)
			{
				pictureBox1.Refresh();
				selectWidth = e.X - selectX;
				selectHeight = e.Y - selectY;
				pictureBox1.CreateGraphics().DrawRectangle(selectPen, selectX, selectY, selectWidth, selectHeight);
			}

			start = false;
			SaveToClipboardAsync();
		}

		

		//Save to Clipboard
		private async System.Threading.Tasks.Task SaveToClipboardAsync()
		{
			if (selectWidth > 0)
			{
                System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(selectX, selectY, selectWidth, selectHeight);
				Bitmap image = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
				Bitmap image2 = new Bitmap(selectWidth, selectHeight);
				Graphics graphics = Graphics.FromImage(image2);
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.DrawImage(image, 0, 0, srcRect, GraphicsUnit.Pixel);

				Color c;
				for (int i = 0; i < image2.Width; i++)
				{
					for (int j = 0; j < image2.Height; j++)
					{
						c = image2.GetPixel(i, j);
						byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

						image2.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
					}
				}
				image2.Save("ss.png");
				new DirectoryInfo("ss.png").Attributes = FileAttributes.Directory | FileAttributes.Hidden;
			}

			bool first = false;

			try
			{
				using (var api = OcrApi.Create())
				{
					api.Init(Patagames.Ocr.Enums.Languages.English);
					string plainText = api.GetTextFromImage("ss.png");

                    Spelling spelling = new Spelling();
                    string Correction = "";

					foreach (string item in plainText.Split(' '))
					{
						if (item == plainText.Split(' ').Last())
						{
							if (!first)
							{
								if (Main.AutoCorrect)
								{
									if (item == "&")
									{
										Correction += item;
									}
                                    else
                                    {
										//First and Only word with AUTOCORRECT ON.
										Correction += System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(spelling.Correct(item.Replace("\n", string.Empty)).ToLower());
                                    }
								}
								else
								{
									//First and Only word with AUTOCORRECT OFF
									Correction += item.Replace("\n", string.Empty);
								}
								first = true;
							}
							else
							{
								if (Main.AutoCorrect)
								{
									if (item == "&")
                                    {
										Correction += " " + item;
									}
                                    else
                                    {
										if (item.Any(char.IsUpper))
										{
											//Last word with AUTOCORRECT ON and UPPERCASE
											Correction += " " + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(spelling.Correct(item.Replace("\n", string.Empty)).ToLower());
										}
										else
										{
											//Last word with AUTOCORRECT ON and LOWERCASE
											Correction += " " + spelling.Correct(item.Replace("\n", string.Empty));
										}
									}
								}
								else
								{
									//Last word with AUTOCORRECT OFF
									Correction += " " + item.Replace("\n", string.Empty);
								}
							}
						}
						else
						{
							if (!first)
							{
								if (Main.AutoCorrect)
								{
									if (item == "&")
									{
										Correction += item;
									}
                                    else
                                    {
										//First word with AUTOCORRECT ON
										Correction = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(spelling.Correct(item).ToLower());
									}
								}
								else
								{
									//First word WITH AUTOCORRECT OFF
									Correction = item;
								}
								first = true;
							}
							else
							{
								if (Main.AutoCorrect)
								{
									if (item == "&")
                                    {
										Correction += " " + item;
                                    }
                                    else
                                    {
										if (item.Any(char.IsUpper))
										{
											//Word with AUTOCORRECT ON and UPPERCASE
											Correction += " " + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(spelling.Correct(item).ToLower());
										}
										else
										{
											//Word with AUTOCORRECT ON and LOWERCASE
											Correction += " " + spelling.Correct(item);
										}
									}
								}
								else
								{
									//Word with AUTOCORRECT OFF
									Correction += " " + item;
								}
							}
						}
					}
					var Text = Correction.Replace("\n", " ");
					Clipboard.SetText(Text);

					File.Delete("ss.png");
				}
			}
			catch
			{
				Clipboard.SetText(string.Empty);
				MessageBox.Show("No text found.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			new Main().Show();
			Main.AutoCorrect = true;
			Close();
		}
	}
}
