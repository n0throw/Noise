using Noise.Service;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Noise.VM
{
    internal class DataVM : ApplicationViewModel
    {
        private string width;
        private string height;
        private string seed;
        private ImageSource sourceImage;
        private RelayCommand generateImage;
        private RelayCommand saveImage;

        private IFileService fileService;
        private IDialogService dialogService;

        public DataVM(IFileService fileService, IDialogService dialogService)
        {
            this.fileService = fileService;
            this.dialogService = dialogService;
        }

        public string Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }

        public string Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        public string Seed
        {
            get => seed;
            set
            {
                seed = value;
                OnPropertyChanged("Seed");
            }
        }

        public ImageSource SourceImage
        {
            get => sourceImage;
            set
            {
                sourceImage = value;
                OnPropertyChanged("SourceImage");
            }
        }


        public RelayCommand GenerateImage => generateImage ??= new RelayCommand(obj =>
        {
            int numWidth = int.Parse(width);
            int numHeight = int.Parse(height);
            Random rand = new(GetSeed());

            WriteableBitmap output = new(numWidth, numHeight, 96, 96, PixelFormats.Pbgra32, null);

            for (int x = 0; x < numWidth; x++)
            {
                for (int y = 0; y < numHeight; y++)
                {
                    byte num = (byte)rand.Next(0, 256);
                    output.WritePixels(new Int32Rect(x, y, 1, 1), new byte[] { num, num, num, 255 }, 4, 0);
                }
            }
            SourceImage = output;
        });

        public RelayCommand SaveImage => saveImage ??= new RelayCommand(obj =>
        {
            try
            {
                if (dialogService.SaveFileDialog())
                {
                    fileService.Save(dialogService.FilePath, new Image() { Source = sourceImage });
                    dialogService.ShowMessage("Файл сохранен");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        });

        private int GetSeed()
        {
            if (!int.TryParse(seed, out int numSeed))
            {
                byte[] value = Encoding.Default.GetBytes(seed);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(value);
                numSeed = BitConverter.ToInt32(value, 0);
            }

            return numSeed;
        }
    }
}
