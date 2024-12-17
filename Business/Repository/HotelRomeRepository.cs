﻿using AutoMapper;
using Business.Repository.IRepository;


using DataAccsess.Data.Models;
using DataAccsess.Data;
using DTOS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRomeRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "kais";
            hotelRoom.UpdatedDate = DateTime.Now;
            hotelRoom.UpdatedBy = "kais";
            var addedHotelRoom = await db.HotelRooms.AddAsync(hotelRoom);
            await db.SaveChangesAsync();
            return mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
        }

        //Update Room
        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    //valid
                    HotelRoom roomToUpdate = await db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomToUpdate);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = db.HotelRooms.Update(room);
                    await db.SaveChangesAsync();
                    return mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    //invalid
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Get Room Details
        public async Task<HotelRoomDTO> GetHotelRoomByID(int roomId)
        {
            try
            {
                // بدون جلب صور
                //HotelRoomDTO roomDetailsDTO = mapper.Map<HotelRoom, HotelRoomDTO>(
                //    await db.HotelRooms.FirstOrDefaultAsync(r => r.Id == roomId));

                // بجلب صور
                HotelRoomDTO roomDetailsDTO = mapper.Map<HotelRoom, HotelRoomDTO>(
                   await db.HotelRooms.Include(r => r.RoomImages).FirstOrDefaultAsync(r => r.Id == roomId));
                return roomDetailsDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Get All Rooms Details
        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms()
        {
            try
            {
                IEnumerable<HotelRoomDTO> roomsDetailsDTOs =
                      // بدون جلب صور       
                      //mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>
                      //        (db.HotelRooms);
                      // مع الصور
                      mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>
                            (db.HotelRooms.Include(r => r.RoomImages));




                return roomsDetailsDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Check If Room Name Is Unique
        public async Task<bool> IsRoomUnique(string name, int roomId = 0)
        {

            if (roomId == 0)
            {
                // create

                HotelRoom hotelRoom = await db.HotelRooms.FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());
                if (hotelRoom == null)
                {
                    // اذا الاسم غير موجود يظهر رسالة النجاح
                    return true;
                }
                else
                {

                    // اذا الاسم موجود يظهر رسالة خطا
                    return false;
                }


            }
            else
            {
                // Update



                HotelRoom hotelRoom = await db.HotelRooms.FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower()

                && r.Id != roomId);
                if (hotelRoom == null)
                {
                    // اذا الاسم غير موجود يظهر رسالة النجاح
                    return true;
                }
                else
                {

                    // اذا الاسم موجود يظهر رسالة خطا
                    return false;
                }






            }




        }

        //Delete Room
        public async Task<int> DeleteHotelRoom(int roomId)
        {
            var roomToDelete = await db.HotelRooms.FindAsync(roomId);
            if (roomToDelete != null)
            {

                // حذف الصور الخاصة بالغرفة
                // الحصول عل جميع الصور لغرفة محددة
                var AllImagesforRoom = await db.RoomImages.Where(r => r.RoomId == roomId).ToListAsync();
                // حذف جميع الصور المحددة
                db.RoomImages.RemoveRange(AllImagesforRoom);
                db.HotelRooms.Remove(roomToDelete);
                return await db.SaveChangesAsync();//return number of deleted records 
            }
            return 0;
        }
    }
}
