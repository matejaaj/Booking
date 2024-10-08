﻿using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class ImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService()
        {
            _imageRepository = Injector.CreateInstance<IImageRepository>();
        }

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public List<Image> GetAll()
        {
            return _imageRepository.GetAll();
        }

        public Image Save(Image image)
        {
            return _imageRepository.Save(image);
        }

        public void Delete(Image image)
        {
            _imageRepository.Delete(image);
        }

        public Image Update(Image image)
        {
            return _imageRepository.Update(image);
        }

        public List<Image> GetImagesByEntityAndType(int entityId, ImageResourceType type)
        {
            return _imageRepository.GetAll().Where(img => img.EntityId == entityId && img.Type == type).ToList();
        }
    }

}
