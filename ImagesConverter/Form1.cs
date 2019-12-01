using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagesConverter
{
    public partial class Form1 : Form
    {
        List<int> RList { get; set; }
        List<int> GList { get; set; }
        List<int> BList { get; set; }

        public int K { get; private set; } = 2;
        public int Kr { get; private set; } = 2;
        public int Kg { get; private set; } = 2;
        public int Kb { get; private set; } = 2;
        public int imgWidth { get; private set; }
        public int imgHeight { get; private set; }
        public AlgorithmsTypes SelectedAlg { get; private set; } = AlgorithmsTypes.DrawImage;

        public Bitmap ResizedBitmap { get; private set; }
        public Color[,] Bitmap { get; private set; }
        public PictureBox Workspace { get; private set; }
        public Bitmap WorkspaceBitmap { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.Workspace = this.workspace;
            this.imgHeight = this.workspace.Height;
            this.imgWidth = this.workspace.Width;

            this.RList = PreparePallette.DividePalette(this.Kr);
            this.GList = PreparePallette.DividePalette(this.Kg);
            this.BList = PreparePallette.DividePalette(this.Kb);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png, jpeg files (*.png,*.jpeg)|*png;*.jpg";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color[,] loadedBitmap = new Color[this.imgWidth, this.imgHeight];
                Bitmap bitmap = new Bitmap(dialog.FileName);
                Bitmap resized = new Bitmap(bitmap, new Size(this.imgWidth, this.imgHeight));
                this.Bitmap = new Color[this.imgWidth, this.imgHeight];
                for (int i = 0; i < this.imgWidth; i++)
                    for (int j = 0; j < this.imgHeight; j++)
                        this.Bitmap[i, j] = resized.GetPixel(i, j);

                this.orignal.Image = resized;
                this.ResizedBitmap = resized;
            }
        }

        private void Redrawbutton_Click(object sender, EventArgs e)
        {
            switch (this.SelectedAlg)
            {
                case AlgorithmsTypes.DrawImage:
                    this.WorkspaceBitmap = this.ResizedBitmap;
                    break;
                case AlgorithmsTypes.AveDithering:
                    this.WorkspaceBitmap = AverageDithering.averageDitheringAlgorithm(this.Bitmap, RList, GList, BList);
                    break;
                case AlgorithmsTypes.OrdDithering1:
                    this.WorkspaceBitmap = OrderDitheringAlgorithm.OrderDithering(this.Bitmap, this.Kr, this.Kg, this.Kb, RList, GList, BList);
                    break;
                case AlgorithmsTypes.OrdDithering2:
                    this.WorkspaceBitmap = OrderDitheringAlgorithm.OrderDithering(this.Bitmap, this.Kr, this.Kg, this.Kb, RList, GList, BList, true);
                    break;
                case AlgorithmsTypes.ErrorPropagation:
                    this.WorkspaceBitmap = ErrorPropagationAlgorithm.ErrorPropagation(RList, GList, BList, Bitmap);
                    break;
                case AlgorithmsTypes.PopularityAlg:
                    this.WorkspaceBitmap = PopularityAlgorithm.Popularity(this.Bitmap, this.K);
                    break;
                default:
                    break;
            }
            this.Workspace.Image = this.WorkspaceBitmap;
            this.Workspace.Invalidate();
        }

        private void workspace_Paint(object sender, PaintEventArgs e)
        {

            using (Graphics gp = this.Workspace.CreateGraphics())
            {
                if (this.ResizedBitmap != null)
                    gp.DrawImage(this.ResizedBitmap, new PointF(0, 0));
            }
        }

        private void AveDitradioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.SelectedAlg = AlgorithmsTypes.AveDithering;
        }

        private void OrdDit1radioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.SelectedAlg = AlgorithmsTypes.OrdDithering1;
        }

        private void OrdDit2radioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.SelectedAlg = AlgorithmsTypes.OrdDithering2;
        }

        private void MetProradioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.SelectedAlg = AlgorithmsTypes.ErrorPropagation;
        }

        private void AlgPopradioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.SelectedAlg = AlgorithmsTypes.PopularityAlg;
        }

        private void KtrackBar_Scroll(object sender, EventArgs e)
        {
            this.K = KtrackBar.Value;
            this.Klabel.Text = this.K.ToString();
        }

        private void KRtrackBar_Scroll(object sender, EventArgs e)
        {
            this.Kr = KRtrackBar.Value;
            this.RList = PreparePallette.DividePalette(this.Kr);
            this.KRlabel.Text = this.Kr.ToString();
        }

        private void KGtrackBar_Scroll(object sender, EventArgs e)
        {
            this.Kg = KGtrackBar.Value;
            this.GList = PreparePallette.DividePalette(this.Kg);
            this.KGlabel.Text = this.Kg.ToString();
        }

        private void KBtrackBar_Scroll(object sender, EventArgs e)
        {
            this.Kb = KBtrackBar.Value;
            this.BList = PreparePallette.DividePalette(this.Kb);
            this.KBlabel.Text = this.Kb.ToString();
        }

        private void DrawImageradioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.SelectedAlg = AlgorithmsTypes.DrawImage;
        }

        private void workspace_Paint_1(object sender, PaintEventArgs e)
        {
            using (Graphics gp = this.Workspace.CreateGraphics())
            {
                if (this.WorkspaceBitmap != null)
                {
                    gp.DrawImage(this.WorkspaceBitmap, new PointF(0, 0));
                }
            }
        }

        private void workspace_Click(object sender, EventArgs e)
        {

        }
    }
}
