using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace SlideMasters_BlazorApp.Models
{
    public class ImageSplitter
    {
        public Image[] SplitImage(string path, int splitCount)
        {
            Image[] splitImages = new Image[splitCount / 2];

            using (var image = Image.Load(path))
            {
                if (splitCount > 0)
                { 
                    int partWidth = image.Width / (splitCount / 2);
                    int partHeight = image.Height / (splitCount / 2);

                    for (int i = 0; i < (splitCount / 2); i++)
                    {
                        var clone = image.Clone(ctx =>
                            ctx.Crop(new Rectangle(i * partWidth, i * partHeight, partWidth, partHeight)));

                        splitImages[i] = clone;
                    }
                }
            }

            return splitImages;
        }
    }
}
