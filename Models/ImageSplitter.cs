using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Drawing;

namespace SlideMasters_BlazorApp.Models
{
    public class ImageSplitter
    {
        public Image[] SplitImage(string path, int splitCount)
        {
            Image[] splitImages = new Image[splitCount];

            using (var image = Image.Load(path))
            {
                if (splitCount > 0)
                {
                    int partSize = (int)Math.Sqrt(splitCount);
                    if (partSize * partSize != splitCount)
                    {
                        throw new ArgumentException("splitCount must be a perfect square.");
                    }
                    int partWidth = image.Width / partSize;
                    int partHeight = image.Height / partSize;

                    for (int i = 0; i < splitCount; i++)
                    {
                        var clone = image.Clone(ctx =>
                            ctx.Crop(new SixLabors.ImageSharp.Rectangle(( i % partSize) * partWidth, (i / partSize) * partHeight, partWidth, partHeight)));

                        splitImages[i] = clone;
                    }
                }
            }

            return splitImages;
        }
    }
}
