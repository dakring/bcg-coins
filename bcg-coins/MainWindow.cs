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

namespace bcg_coins
{
    public partial class MainWindow : Form
    {

        #region member variables
        Image<Bgr, byte> _baseImage;
        Image<Gray, byte> _grayImage;
        Image<Gray, byte> _binaryImage;
        Image<Bgr, byte> _contourImage;
        Image<Bgr, byte> _roiImage;
        List<Image<Bgr, byte>> _roiImageList;
        Mat[] _coinReferenceList;
        int[] _coinValueList;
        List<ImageBox> _currentCoinImageBoxes;
        #endregion

        public MainWindow() {
            InitializeComponent();

            histogramBox.FunctionalMode = Emgu.CV.UI.HistogramBox.FunctionalModeOption.Minimum;
            LoadAllReferences();
        }


        private void btnDispHistogram_Click(object sender, EventArgs e) {

            if (_baseImage == null) {
                MessageBox.Show("Please select an Image first!");
                return;
            }
            histogramBox.ClearHistogram();
            _grayImage = _baseImage.Convert<Gray, byte>();


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
                if (menuSettingsInvertBinary.Checked) {
                    CvInvoke.BitwiseNot(_binaryImage, _binaryImage);
                }
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

        private void LoadAllReferences() {

            _coinReferenceList = new Mat[16];
            _coinValueList = new int[16];

            //Paths to Ground Truths
            string front_e2 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\2e_V.tif";            
            string back_e2 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\2e_R.tif";
            string front_e1 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\1e_V.tif";
            string back_e1 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\1e_R.tif";
            string front_c50 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\50c_V.tif";
            string back_c50 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\50c_R.tif";
            string front_c20 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\20c_V.tif";
            string back_c20 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\20c_R.tif";
            string front_c10 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\10c_V.tif";
            string back_c10 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\10c_R.tif";
            string front_c5 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\5c_V.tif";
            string back_c5 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\5c_R.tif";
            string front_c2 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\2c_V.tif";
            string back_c2 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\2c_R.tif";
            string front_c1 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\1c_V.tif";
            string back_c1 = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Log\\1c_R.tif";

            Mat coin_200_front = CvInvoke.Imread(front_e2);
            _coinReferenceList[0] = (coin_200_front);
            _coinValueList[0] = (200);

            Mat coin_200_back = CvInvoke.Imread(back_e2);
            _coinReferenceList[1] = (coin_200_back);
            _coinValueList[1] = (200);

            Mat coin_100_front = CvInvoke.Imread(front_e1);
            _coinReferenceList[2] = (coin_100_front);
            _coinValueList[2] = (100);

            Mat coin_100_back = CvInvoke.Imread(back_e1);
            _coinReferenceList[3] = (coin_100_back);
            _coinValueList[3] = (100);

            Mat coin_50_front = CvInvoke.Imread(front_c50);
            _coinReferenceList[4] = (coin_50_front);
            _coinValueList[4] = (50);

            Mat coin_50_back = CvInvoke.Imread(back_c50);
            _coinReferenceList[5] = (coin_50_back);
            _coinValueList[5] = (50);

            Mat coin_20_front = CvInvoke.Imread(front_c20);
            _coinReferenceList[6] = (coin_20_front);
            _coinValueList[6] = (20);

            Mat coin_20_back = CvInvoke.Imread(back_c20);
            _coinReferenceList[7] = (coin_20_back);
            _coinValueList[7] = (20);

            Mat coin_10_front = CvInvoke.Imread(front_c10);
            _coinReferenceList[8] = (coin_10_front);
            _coinValueList[8] = (10);

            Mat coin_10_back = CvInvoke.Imread(back_c10);
            _coinReferenceList[9] = (coin_10_back);
            _coinValueList[9] = (10);

            Mat coin_5_front = CvInvoke.Imread(front_c5);
            _coinReferenceList[10] = (coin_5_front);
            _coinValueList[10] = (5);

            Mat coin_5_back = CvInvoke.Imread(back_c5);
            _coinReferenceList[11] = (coin_5_back);
            _coinValueList[11] = (5);

            Mat coin_2_front = CvInvoke.Imread(front_c2);
            _coinReferenceList[12] = (coin_2_front);
            _coinValueList[12] = (2);

            Mat coin_2_back = CvInvoke.Imread(back_c2);
            _coinReferenceList[13] = (coin_2_back);
            _coinValueList[13] = (2);

            Mat coin_1_front = CvInvoke.Imread(front_c1);
            _coinReferenceList[14] = (coin_1_front);
            _coinValueList[14] = (1);

            Mat coin_1_back = CvInvoke.Imread(back_c1);
            _coinReferenceList[15] = (coin_1_back);
            _coinValueList[15] = (1);
        }

        private Mat ScrollByPixel(Mat toScroll, int iterations = 1) {

            int width = toScroll.Size.Width;
            int height = toScroll.Size.Height;

            if (iterations >= height) {
                MessageBox.Show("Cant Scroll that far!\nError in Code :(");
                return toScroll;
            }

            Mat output = new Mat(height, width, toScroll.Depth, toScroll.NumberOfChannels);
            
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {

                    if (i <= iterations) {
                        MatExtension.SetValues(output, iterations - i, j, MatExtension.GetValues(toScroll, height - (i + 1), j));
                    } else {
                        MatExtension.SetValues(output, i, j, MatExtension.GetValues(toScroll, i - iterations, j));
                    }
                }
            }

            return output;
        }

        #endregion

        private void btnFillHoles_Click(object sender, EventArgs e) {
            if (_binaryImage != null) {

                Mat kernel = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(10, 10), new Point(-1, -1));
                _binaryImage = _binaryImage.MorphologyEx(MorphOp.Close, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(1.0));

                //_binaryImage = _binaryImage.Dilate(10).Erode(10);
                binaryImageBox.Image = _binaryImage;
            }
        }

        private void menuSettingsInvertBinary_Click(object sender, EventArgs e) {
            menuSettingsInvertBinary.Checked = !menuSettingsInvertBinary.Checked;
        }

        private void btnFindContours_Click(object sender, EventArgs e) {

            if (_baseImage == null) {
                return;
            }

            _roiImageList = new List<Image<Bgr, byte>>();

            //Reference Shape
            string referenceFile = "C:\\Users\\Menta\\Desktop\\BCG\\Dataset_own\\circle.tif";
            Mat reference = CvInvoke.Imread(referenceFile);
            Mat grayReference = new Mat();
            CvInvoke.CvtColor(reference, grayReference, ColorConversion.Bgr2Gray);
            CvInvoke.Threshold(grayReference, grayReference, 100, 255, ThresholdType.Binary);
            //CvInvoke.Imshow("reference", grayReference);

            VectorOfVectorOfPoint contours_reference = new VectorOfVectorOfPoint();
            VectorOfRect hierarchy_reference = new VectorOfRect();
            CvInvoke.FindContours(grayReference, contours_reference, hierarchy_reference, RetrType.External, ChainApproxMethod.ChainApproxNone);


            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();

            VectorOfRect hierarchy = new VectorOfRect();

            _contourImage = _baseImage.Copy();

            CvInvoke.FindContours(_binaryImage, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxTc89Kcos);

            VectorOfVectorOfPoint contours_poly = new VectorOfVectorOfPoint(contours.Size);
            //VectorOfRect boundRect = new VectorOfRect(contours.Size);

            for (int i = 0; i < contours.Size; i++) {

                double matchValue = CvInvoke.MatchShapes(contours_reference[0], contours[i], ContoursMatchType.I1);

                if (matchValue < 0.1) {

                    var mask = _baseImage.Copy();
                    _roiImage = _baseImage.Copy();
                    mask.SetValue(new Bgr(0, 0, 0));
                    CvInvoke.DrawContours(mask, contours, i, new MCvScalar(255, 255, 255), 1);
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
                    _roiImageList.Add(_roiImage);
                }
            }

            contoursImageBox.Image = _contourImage;
        }

        private void findCoins_Click(object sender, EventArgs e) {
            //Make sure Contours exist before attempting to find coins
            if (_roiImageList == null) {
                MessageBox.Show("No contours!");
                return; 
            }

            //Delete possible old instances
            if (_currentCoinImageBoxes != null) {
                foreach (ImageBox box in _currentCoinImageBoxes) {
                    box.Dispose();
                }
            }
            _currentCoinImageBoxes = new List<ImageBox>();
            
            foreach (Image<Bgr, byte> roiImg in _roiImageList) {
                if (roiImg.IsROISet) {
                    _roiImage = roiImg.Copy();

                    //CvInvoke.PolarToCart(_roiImage, _roiImage, ptCenter, 120);
                    Mat roiDst = new Mat();
                    //CvInvoke.LinearPolar(_baseImage, roiDst, ptCenter, (_roiImage.Cols / 2 + _roiImage.Rows / 2) / 2, Inter.Cubic);

                    /*//---------------------------------
                    //DEBUG: Reference 2E Front Log
                    string E200_Front_Log = "C:\\Users\\Menta\\Desktop\\BCG\\GroundTruths\\Polar\\2e_V.tiff";
                    Mat E200_Front_Log_Image = CvInvoke.Imread(E200_Front_Log);
                    //TODO Find way to Wrap Image Around, Moving it on Y
                    */

                    CvInvoke.LinearPolar(_roiImage, roiDst, new Point(_roiImage.Cols / 2, _roiImage.Rows / 2), (_roiImage.Cols / 2) , Inter.Cubic);

                    //CvInvoke.LogPolar(_roiImage, roiDst, new Point(_roiImage.Cols / 2, _roiImage.Rows / 2), _roiImage.Cols / Math.Log((_roiImage.Cols / 2) * 0.7f), Inter.Cubic);
                    //(currentCoin.Cols / 2 + currentCoin.Rows / 2) / 4.85f
                    //currentCoin.Cols / Math.Log((currentCoin.Cols / 2 + currentCoin.Rows / 2))

                  
                    float score = 0;
                    float curScore = 0;
                    int coinIndex = 0;
                    int stepsize = 5;
                    for (int i = 0; i < 16; i++) {
                        for (int j = 0; j < roiDst.Rows / stepsize; j++) {

                            Mat result = new Mat();
                            Mat curCoinRef = new Mat();   
                            if (_coinReferenceList[i].Rows < roiDst.Rows || _coinReferenceList[i].Cols < roiDst.Cols) {
                                CvInvoke.Resize(roiDst, roiDst, _coinReferenceList[i].Size);
                                curCoinRef = _coinReferenceList[i];
                            } else {
                                CvInvoke.Resize(_coinReferenceList[i], curCoinRef, roiDst.Size);
                            }
                            
                            CvInvoke.MatchTemplate(roiDst, curCoinRef, result, TemplateMatchingType.CcorrNormed);
                            
                            curScore = MatExtension.GetValue(result, 0, 0);
                            if (curScore > score) {
                                score = curScore;
                                coinIndex = i;
                            }

                            roiDst = ScrollByPixel(roiDst, stepsize);
                            //MessageBox.Show("Score No." + i + ": " + score + " - Coin Index: " + coinIndex + " - Value: " + _coinValueList[coinIndex]);
                            textBoxDebug.Text += "Score No." + i + ": " + curScore + " - Value: " + _coinValueList[coinIndex] + Environment.NewLine;
                        }
                    }

                    /*double threshold = 1.0E+9;
                    textBoxMetadata.Text = textBoxMetadata.Text + Environment.NewLine + "Score: " + score.ToString() + " - Nummer: ";// + i;

                    if (score > threshold) {
                        CvInvoke.PutText(roiDst, _coinValueList[coinIndex] + "Cent", new Point(roiDst.Cols / 2, roiDst.Rows / 2), FontFace.HersheySimplex, 0.5f, new MCvScalar(0, 255, 0), 2);
                    } else {
                        CvInvoke.PutText(roiDst, "no score", new Point(roiDst.Cols / 2, roiDst.Rows / 2), FontFace.HersheySimplex, 0.5f, new MCvScalar(0, 0, 255), 2);
                    }*/

                    //MessageBox.Show("index: " + coinIndex + " - curScore: " + curScore + " - score: " + score);
                    CvInvoke.PutText(roiDst, _coinValueList[coinIndex] + "Cent", new Point(roiDst.Cols / 2, roiDst.Rows / 2), FontFace.HersheySimplex, 0.5f, new MCvScalar(0, 255, 0), 2);



                    ImageBox imgb = new ImageBox();
                    imgb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    imgb.MinimumSize = new System.Drawing.Size(200, 200);
                    imgb.Size = new System.Drawing.Size(35, 20);
                    imgb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                    imgb.TabStop = false;
                    imgb.Image = roiDst;
                    flowLayoutPanel1.Controls.Add(imgb);
                    _currentCoinImageBoxes.Add(imgb);
                    
                }
            }
        }

        private void groupBox6_Enter(object sender, EventArgs e) {

        }

        private void binaryImageBox_Click(object sender, EventArgs e) {

        }
    }
}
