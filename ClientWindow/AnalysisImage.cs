using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;

namespace ClientWindow
{
    class AnalysisImage
    {
        // 把圖片壓成能夠發送的大小
        public static Image ReszieImage(string ImagePath)
        {
            // 加载原始图像
            Image originalImage = Image.FromFile(ImagePath);

            // 指定目标宽度和高度
            int targetWidth = 200;
            int targetHeight = 200;

            // 创建目标图像对象
            Image targetImage = new Bitmap(targetWidth, targetHeight);

            // 创建绘图对象
            Graphics graphics = Graphics.FromImage(targetImage);

            // 绘制调整后的图像
            graphics.DrawImage(originalImage, 0, 0, targetWidth, targetHeight);

            return targetImage;
        }

        // 把圖片壓成發送的文字串
        public static byte[] Image2Byte(Image image)
        {
            byte[] imageBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBytes = memoryStream.ToArray();
            }
            return imageBytes;
        }

        // 把接收的文字串解壓成圖片
        public static Image Byte2Image(byte[] imagedata)
        {
            Image receivedImage;
            using (MemoryStream memoryStream = new MemoryStream(imagedata))
            {
                receivedImage = Image.FromStream(memoryStream);
            }
            return receivedImage;
        }

        // 把圖片Byte拆解成多包傳送
        public static List<byte[]> ImgDataChunkBy(byte[] InputData, int ChunkSize = 1024)
        {
            List<byte[]> OutputData = InputData.Select((v, i) => new { value = v, Index = i })
                .GroupBy(x => x.Index / ChunkSize)
                .Select(g => g.Select(d => d.value).ToArray()).ToList();

            return OutputData;
        }

        // 把圖片Byte多包合成一個
        public static byte[] ChunkByData2Img(List<byte[]> InputData)
        {
            return InputData.SelectMany(innerList => innerList).ToArray();
        }

    }
}
