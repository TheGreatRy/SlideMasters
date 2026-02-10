using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Drawing;

namespace SlideMasters_BlazorApp.Models
{
    public class ImageSplitter
    {
        public SixLabors.ImageSharp.Image[] SplitImage(string path, int splitCount)
        {
            SixLabors.ImageSharp.Image[] splitImages = new SixLabors.ImageSharp.Image[splitCount];

            using (var image = SixLabors.ImageSharp.Image.Load(path))
            {
                if (splitCount > 0)
                {
                    int partWidth = image.Width / splitCount;
                    int partHeight = image.Height / splitCount;

                    for (int i = 0; i < splitCount; i++)
                    {
                        var clone = image.Clone(ctx =>
                            ctx.Crop(new SixLabors.ImageSharp.Rectangle(i * partWidth, 0, partWidth, image.Height)));

                        splitImages[i] = clone;
                    }
                }
            }

            return splitImages;
        }
    }
}
