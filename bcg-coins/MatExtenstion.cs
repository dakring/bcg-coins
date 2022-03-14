//Class from https://stackoverflow.com/questions/32255440/how-can-i-get-and-set-pixel-values-of-an-emgucv-mat-image

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;


namespace bcg_coins {


    /*public static class MatExtension {
        public static dynamic GetValue(this Mat mat, int row, int col) {
            var value = CreateElement(mat.Depth);
            Marshal.Copy(mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, value, 0, 1);
            return value[0];
        }

        public static void SetValue(this Mat mat, int row, int col, dynamic value) {
            var target = CreateElement(mat.Depth, value);
            Marshal.Copy(target, 0, mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, 1);
        }
        private static dynamic CreateElement(DepthType depthType, dynamic value) {
            var element = CreateElement(depthType);
            element[0] = value;
            return element;
        }

        private static dynamic CreateElement(DepthType depthType) {
            if (depthType == DepthType.Cv8S) {
                return new sbyte[1];
            }
            if (depthType == DepthType.Cv8U) {
                return new byte[1];
            }
            if (depthType == DepthType.Cv16S) {
                return new short[1];
            }
            if (depthType == DepthType.Cv16U) {
                return new ushort[1];
            }
            if (depthType == DepthType.Cv32S) {
                return new int[1];
            }
            if (depthType == DepthType.Cv32F) {
                return new float[1];
            }
            if (depthType == DepthType.Cv64F) {
                return new double[1];
            }
            return new float[1];
        }
    }*/
    public static class MatExtension
    {
        public static dynamic GetValues(this Mat mat, int row, int col) {
            var value = CreateElement3Channels(mat.Depth);
            Marshal.Copy(mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, value, 0, 3);
            return value;
        }

        public static dynamic GetValue(this Mat mat, int channel, int row, int col) {
            var value = CreateElement3Channels(mat.Depth);
            Marshal.Copy(mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, value, 0, 3);
            return value[channel];
        }

        public static dynamic GetValue(this Mat mat, int row, int col) {
            var value = CreateElement(mat.Depth);
            Marshal.Copy(mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, value, 0, 1);
            return value[0];
        }

        public static void SetValues(this Mat mat, int row, int col, dynamic value) {
            Marshal.Copy(value, 0, mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, 3);
        }

        public static void SetValue(this Mat mat, int channel, int row, int col, dynamic value) {
            var element = GetValues(mat, row, col);
            var target = CreateElement(element, value, channel);
            Marshal.Copy(target, 0, mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, 3);
        }

        public static void SetValue(this Mat mat, int row, int col, dynamic value) {
            var target = CreateElement(mat.Depth, value);
            Marshal.Copy(target, 0, mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, 1);
        }

        private static dynamic CreateElement(dynamic element, dynamic value, int channel) {
            element[channel] = value;
            return element;
        }

        private static dynamic CreateElement(DepthType depthType, dynamic value) {
            var element = CreateElement(depthType);
            element[0] = value;
            return element;
        }

        private static dynamic CreateElement3Channels(DepthType depthType) {
            if (depthType == DepthType.Cv8S) {
                return new sbyte[3];
            }

            if (depthType == DepthType.Cv8U) {
                return new byte[3];
            }

            if (depthType == DepthType.Cv16S) {
                return new short[3];
            }

            if (depthType == DepthType.Cv16U) {
                return new ushort[3];
            }

            if (depthType == DepthType.Cv32S) {
                return new int[3];
            }

            if (depthType == DepthType.Cv32F) {
                return new float[3];
            }

            if (depthType == DepthType.Cv64F) {
                return new double[3];
            }

            return new float[3];
        }

        private static dynamic CreateElement(DepthType depthType) {
            if (depthType == DepthType.Cv8S) {
                return new sbyte[1];
            }

            if (depthType == DepthType.Cv8U) {
                return new byte[1];
            }

            if (depthType == DepthType.Cv16S) {
                return new short[1];
            }

            if (depthType == DepthType.Cv16U) {
                return new ushort[1];
            }

            if (depthType == DepthType.Cv32S) {
                return new int[1];
            }

            if (depthType == DepthType.Cv32F) {
                return new float[1];
            }

            if (depthType == DepthType.Cv64F) {
                return new double[1];
            }

            return new float[1];
        }
    }
}
