using System.IO;

namespace SuitSupply.Core.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] LoadImageToByte(this byte[] imageDestination, string imagePath)
        {
            FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

            imageDestination = new byte[fs.Length];

            fs.Read(imageDestination, 0, System.Convert.ToInt32(fs.Length));

            fs.Close();
            return imageDestination;
        }
    }
}
