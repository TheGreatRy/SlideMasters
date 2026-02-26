using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace SlideMasters_BlazorApp.Models
{
    public class ImageBlock
    {
        public Image Image { get; set; }
        public string DataUri { get; private set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public int BoardID { get; set; }
        public bool IsEmptyBlock { get; set; } = false;

        public ImageBlock(Image image)
        {
            Image = image;
            Width = image.Width;
            Height = image.Height;
            DataUri = ConvertImageToDataUri(image);
        }

        public void MovePiece(int deltaX, int deltaY)
        {
            BoardX += deltaX;
            BoardY += deltaY;
        }

        private string ConvertImageToDataUri(Image image)
        {
            using var ms = new MemoryStream();
            image.SaveAsPng(ms); // or SaveAsJpeg
            var imageBytes = ms.ToArray();
            var base64String = Convert.ToBase64String(imageBytes);
            return $"data:image/png;base64,{base64String}";
        }
    }
}
