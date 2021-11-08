using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Noise.Service
{
    public class PngFileService : IFileService
    {
        public void Save(string filename, Image image)
        {
            //RenderTargetBitmap bmp = new((int)image.Source.Width, (int)image.Source.Height, 96, 96, PixelFormats.Pbgra32);

            //bmp.Render(image);
            //PngBitmapEncoder encoder = new();
            //encoder.Frames.Add(BitmapFrame.Create(bmp));

            //using Stream stream = new FileStream(filename, FileMode.Create);
            //encoder.Save(stream);
        }

        Image IFileService.Open(string filename) => new Image();
    }
}
