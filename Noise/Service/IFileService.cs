using System.Windows.Controls;

namespace Noise.Service
{
    public interface IFileService
    {
        Image Open(string filename);
        void Save(string filename, Image image);
    }
}
