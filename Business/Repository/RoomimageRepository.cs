﻿using AutoMapper;
using Business.Repository.IRepository;
using DataAccsess.Data;
using DataAccsess.Data.Models;
using DTOS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class RoomimageRepository : IRoomimageRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public RoomimageRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;

        }


        public async Task<int> CreateRoomImage(RoomImageDTO imageDTO)
        {
            // تحويل من dto to model
            var image = mapper.Map<RoomImageDTO, RoomImage>(imageDTO);
            await db.RoomImages.AddAsync(image);
            return await db.SaveChangesAsync();
        }

        public async Task<int> DeleteImageByImageUrl(string imageUrl)
        {
            //  RoomImage حذف مسار الصورة من جدول الصور 
            var allImages = await db.RoomImages.FirstOrDefaultAsync
                                (i => i.ImageUrl.ToLower() == imageUrl.ToLower());
            if (allImages == null)
            {
                return 0;
            }
            db.RoomImages.Remove(allImages);
            return await db.SaveChangesAsync();

        }

        public async Task<int> DeleteRoomImageByImageId(int imageId)
        {
            // حذف صورة غرفة واحدة من مخزن الصور بالاعتماد على رقم الصورة
            var image = await db.RoomImages.FindAsync(imageId);
            db.RoomImages.Remove(image);
            return await db.SaveChangesAsync();
        }

        public async Task<int> DeleteRoomImageByRoomId(int roomId)
        {

            // استخدمنا where  لانه توجد اكثر من صورة
            // حذف صور 
            var imageList = await db.RoomImages.Where(i => i.RoomId == roomId).ToListAsync();
            db.RoomImages.RemoveRange(imageList);
            return await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<RoomImageDTO>> GetRoomImagesById(int roomId)
        {
            return mapper.Map<IEnumerable<RoomImage>, IEnumerable<RoomImageDTO>>(
            await db.RoomImages.Where(i => i.RoomId == roomId).ToListAsync());
        }


    }
}
