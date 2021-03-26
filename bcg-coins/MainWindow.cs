using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using MetadataExtractor;

namespace bcg_coins {
    public partial class MainWindow : Form {

        #region member variables
        Image<Bgr, byte> _baseImage;
        Image<Gray, byte> _grayImage;
        Image<Gray, byte> _binaryImage;
        Image<Bgr, byte> _contourImage;
        Image<Bgr, byte> _roiImage;
        #endregion

        public MainWindow() {
            InitializeComponent();

            histogramBox.FunctionalMode = Emgu.CV.UI.HistogramBox.FunctionalModeOption.Minimum;
        }


        private void btnDispHistogram_Click(object sender, EventArgs e) {

            if (_baseImage == null) {
                MessageBox.Show("Please select an Image first!");
                return;
            }
            histogramBox.ClearHistogram();
            _grayImage = _baseImage.Convert<Gray, byte>();

            /*DenseHistogram intensityHistogram = new DenseHistogram(256, new RangeF(0, 255));
            intensityHistogram.Calculate(new Image<Gray, byte>[] { _grayImage }, false, null);

            Mat histData = new Mat();
            intensityHistogram.CopyTo(histData);

            histogramBox.GenerateHistogram("Intensity Histogram", Color.Blue, histData, 256, new float[] { 0, 256 });*/

            histogramBox.GenerateHistograms(_grayImage, 256);

            histogramBox.Refresh();

        }

        private void btnApplyAll_Click(object sender, EventArgs e) {

            btnDispHistogram_Click(sender, e);
            if (_baseImage == null) return;
            btnBinaryImage_Click(sender, e);
            btnFillHoles_Click(sender, e);
            btnFindContours_Click(sender, e);
        }

        private void btnBinaryImage_Click(object sender, EventArgs e) {
            if (_baseImage == null) return;

            Thresholdialog thresholdForm = new Thresholdialog();
            var threshold = 100;

            if (thresholdForm.ShowDialog(this) == DialogResult.OK) {
                threshold = thresholdForm.trackBar1.Value;
            }

            thresholdForm.Dispose();

            if (_grayImage == null) {
                btnDispHistogram_Click(sender, e);
            }


            if (_grayImage != null) {
                _binaryImage = _grayImage.ThresholdBinary(new Gray(threshold), new Gray(255));
                CvInvoke.BitwiseNot(_binaryImage, _binaryImage);
                binaryImageBox.Image = _binaryImage;
            }

        }

        private void menuOpen_Click(object sender, EventArgs e) {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Filter = "TIFF-Image (*.tiff;*.tif)|*.tiff;*.tif|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    ResetComponents();
                    filePath = openFileDialog.FileName;
                    _baseImage = new Image<Bgr, byte>(filePath);

                    baseImageBox.Image = _baseImage;


                    contoursImageBox.Image = _contourImage;

                    IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(filePath);
                    foreach (var directory in directories) {
                        //if (directory.Name != "Exif IFD0") { return; }
                        foreach (var tag in directory.Tags)
                            textBoxMetadata.Text = textBoxMetadata.Text + $"{directory.Name} - {tag.Name} = {tag.Description}" + Environment.NewLine;
                    }



                }

            }
        }



        private void menuExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void menuCredits_Click(object sender, EventArgs e) {
            MessageBox.Show("bcg-coins by Alexander Kring\nBetreuender Professor: Andreas Karge\nTechnische Hochschule Köln 2021", "Credits", MessageBoxButtons.OK);
        }

        private void menuResetZoom_Click(object sender, EventArgs e) {
            baseImageBox.SetZoomScale(1, new Point(baseImageBox.Width, baseImageBox.Height));
            baseImageBox.HorizontalScrollBar.Hide();
            baseImageBox.VerticalScrollBar.Hide();
            binaryImageBox.SetZoomScale(1, new Point(baseImageBox.Width, baseImageBox.Height));
            binaryImageBox.HorizontalScrollBar.Hide();
            binaryImageBox.VerticalScrollBar.Hide();
            contoursImageBox.SetZoomScale(1, new Point(baseImageBox.Width, baseImageBox.Height));
            contoursImageBox.HorizontalScrollBar.Hide();
            contoursImageBox.VerticalScrollBar.Hide();
        }

        #region helper methods
        private void ResetComponents() {
            _baseImage = null;
            baseImageBox.Image = null;
            textBoxMetadata.Text = string.Empty;
            histogramBox.ClearHistogram();
            binaryImageBox.Image = null;
            contoursImageBox.Image = null;
            return;
        }


        private Mat ScrollByPixel(Mat toScroll, int iterations = 1) {
            Mat temp = new Mat();
            toScroll.CopyTo(temp);
        https://stackoverflow.com/questions/58107625/opencv-translate-image-wrap-pixels-around-edges-c
            return toScroll;
        }

        #endregion

        private void btnFillHoles_Click(object sender, EventArgs e) {
            if (_binaryImage != null) {

                Mat kernel = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(10,10), new Point(-1,-1));
                _binaryImage = _binaryImage.MorphologyEx(MorphOp.Close, kernel, new Point(-1,-1), 1, BorderType.Default, new MCvScalar(1.0));

                //_binaryImage = _binaryImage.Dilate(10).Erode(10);
                binaryImageBox.Image = _binaryImage;
            }
        }

        private void menuSettingsInvertBinary_Click(object sender, EventArgs e) {

        }

        private void btnFindContours_Click(object sender, EventArgs e) {

            if (_baseImage == null) {
                return;
            }

            //Reference Shape
            string referenceFile = "D:\\DesktopFolders\\Uni\\WS2020\\Bildbasierte Computergrafik\\Datensatz\\circle.tif";
            Mat temp = CvInvoke.Imread(referenceFile);
            Mat grayReference = new Mat();

            //DEBUG: Reference 2E Front Log
            string E200_Front_Log = "D:\\DesktopFolders\\Uni\\WS2020\\Bildbasierte Computergrafik\\Datensatz\\Referenzen\\200_Front_log.tif";
            Mat E200_Front_Log_Image = CvInvoke.Imread(E200_Front_Log);
            //TODO Find way to Wrap Image Around, Moving it on Y


            CvInvoke.CvtColor(temp, grayReference, ColorConversion.Bgr2Gray);
            CvInvoke.Threshold(grayReference, grayReference, 100, 255, ThresholdType.Binary);
            //CvInvoke.Imshow("reference", grayReference);

            VectorOfVectorOfPoint contours_reference = new VectorOfVectorOfPoint();
            VectorOfRect hierarchy_reference = new VectorOfRect();
            CvInvoke.FindContours(grayReference, contours_reference, hierarchy_reference, RetrType.External, ChainApproxMethod.ChainApproxNone);


            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();

            VectorOfRect hierarchy = new VectorOfRect();
            //Mat hierarchy = new Mat();

            _contourImage = _baseImage.Copy();
            

            CvInvoke.FindContours(_binaryImage, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxTc89Kcos);

            VectorOfVectorOfPoint contours_poly = new VectorOfVectorOfPoint(contours.Size);
            //VectorOfRect boundRect = new VectorOfRect(contours.Size);

            for (int i = 0; i < contours.Size; i++) {

                double matchValue = CvInvoke.MatchShapes(contours_reference[0], contours[i], ContoursMatchType.I1);

                if(matchValue < 0.1) {

                    var mask = _baseImage.Copy();
                    _roiImage = _baseImage.Copy();
                    mask.SetValue(new Bgr(0,0,0));
                    CvInvoke.DrawContours(mask, contours, i, new MCvScalar(255,255,255), 1);
                    CvInvoke.FillConvexPoly(mask, contours[i], new MCvScalar(255, 255, 255));

                    CvInvoke.BitwiseAnd(_roiImage, mask, _roiImage);

                    CvInvoke.ApproxPolyDP(contours[i], contours_poly[i], 3, true);
                    Rectangle rect = CvInvoke.BoundingRectangle(contours_poly[i]);

                    if (0 <= rect.X && 0 <= rect.Width && rect.X + rect.Width <= _baseImage.Cols &&
                        0 <= rect.Y && 0 <= rect.Height && rect.Y + rect.Height <= _baseImage.Rows) {
                        _roiImage.ROI = rect;
                    }

                    CvInvoke.DrawContours(_contourImage, contours, i, new MCvScalar(0, 255, 0), 2);

                    double area = CvInvoke.ContourArea(contours[i], false);  //Calculate the area perimeter
                    double length = CvInvoke.ArcLength(contours[i], true);
                    var moments = CvInvoke.Moments(contours[i], false);       //Calculate the contour moment
                    Point ptCenter = new Point();
                    ptCenter.X = (int)(moments.M10 / moments.M00);          //Calculate the centroid
                    ptCenter.Y = (int)(moments.M01 / moments.M00);
                    CvInvoke.Circle(_contourImage, ptCenter, 3, new MCvScalar(0, 0, 255), -1);//Draw the centroid


                    if (_roiImage.IsROISet) {
                        _roiImage = _roiImage.Copy();

                        //CvInvoke.PolarToCart(_roiImage, _roiImage, ptCenter, 120);
                        Mat roiDst = new Mat();
                        //CvInvoke.LinearPolar(_baseImage, roiDst, ptCenter, (_roiImage.Cols / 2 + _roiImage.Rows / 2) / 2, Inter.Cubic);
                        CvInvoke.LogPolar(_roiImage, roiDst, new Point(_roiImage.Cols/2, _roiImage.Rows/2), (_roiImage.Cols / 2 + _roiImage.Rows / 2) / 5.4f, Inter.Cubic);

                        Mat result = new Mat();
                        CvInvoke.MatchTemplate(_roiImage, E200_Front_Log_Image, result, TemplateMatchingType.Sqdiff);

                        float score = MatExtension.GetValue(result, 0, 0);
                        double threshold = 1.0E+9;
                        textBoxMetadata.Text = textBoxMetadata.Text + Environment.NewLine + "Score: " + score.ToString() + " - Nummer: " + i;

                        if(score > threshold) {
                            CvInvoke.PutText(_roiImage, score.ToString(), new Point(roiDst.Cols/2, roiDst.Rows/2), FontFace.HersheySimplex, 0.5f, new MCvScalar(0,255,0), 2);
                        } else {
                            CvInvoke.PutText(_roiImage, score.ToString(), new Point(roiDst.Cols / 2, roiDst.Rows / 2), FontFace.HersheySimplex, 0.5f, new MCvScalar(0, 0, 255), 2);
                        }

                        ImageBox imgb = new ImageBox();
                        imgb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        imgb.MinimumSize = new System.Drawing.Size(200, 200);
                        imgb.Size = new System.Drawing.Size(35, 20);
                        imgb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                        imgb.TabStop = false;
                        imgb.Image = _roiImage;
                        flowLayoutPanel1.Controls.Add(imgb);
                    }

                }
            }

            contoursImageBox.Image = _contourImage;
        }
    }
}
