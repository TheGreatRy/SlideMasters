using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace SlideMasters_BlazorApp.Models
{
    public class ImageBlock
    {
        public Image Image { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int BoardX { get; set; }
        public int BoardY { get; set; }

        public ImageBlock(Image image)
        {
            Image = image;
            Width = image.Width;
            Height = image.Height;
        }

        public void MovePiece(int deltaX, int deltaY)
        {
            BoardX += deltaX;
            BoardY += deltaY;
        }
    }
}
