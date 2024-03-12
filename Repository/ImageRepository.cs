using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class ImageRepository
    {
        private const string FilePath = "../../../Resources/Data/images.csv";
        private readonly Serializer<Image> _serializer;
        private List<Image> _images;

        public ImageRepository()
        {
            _serializer = new Serializer<Image>();
            _images = _serializer.FromCSV(FilePath);
        }

        public List<Image> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Image Save(Image image)
        {
            image.Id = NextId();
            _images = _serializer.FromCSV(FilePath);
            _images.Add(image);
            _serializer.ToCSV(FilePath, _images);
            return image;
        }

        public int NextId()
        {
            _images = _serializer.FromCSV(FilePath);
            if (_images.Count < 1)
            {
                return 1;
            }
            return _images.Max(img => img.Id) + 1;
        }

        public void Delete(Image image)
        {
            _images = _serializer.FromCSV(FilePath);
            Image toDelete = _images.Find(img => img.Id == image.Id);
            if (toDelete != null)
            {
                _images.Remove(toDelete);
                _serializer.ToCSV(FilePath, _images);
                DeleteImageFile(toDelete.Path);
            }
        }

        public Image Update(Image image)
        {
            _images = _serializer.FromCSV(FilePath);
            int index = _images.FindIndex(img => img.Id == image.Id);
            if (index != -1)
            {
                _images[index] = image;
                _serializer.ToCSV(FilePath, _images);
            }
            return image;
        }

        private void DeleteImageFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

