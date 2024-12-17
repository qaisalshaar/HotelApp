using AutoMapper;
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
using DataAccsess.Migrations;

namespace Business.Repository
{
    public class HotelAmenityRepository : IHotelAmenityRepository
    {

        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public HotelAmenityRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenityDTO)
        {
            HotelAmenity hotelAmenity = mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenityDTO);
            hotelAmenity.CreatedDate = DateTime.Now;
            hotelAmenity.CreatedBy = "kais";
            hotelAmenity.UpdatedDate = DateTime.Now;
            hotelAmenity.UpdatedBy = "kais";
            var addedHotelAmenity = await db.HotelAmenitys.AddAsync(hotelAmenity);
            await db.SaveChangesAsync();
            return mapper.Map<HotelAmenity, HotelAmenityDTO>(addedHotelAmenity.Entity);
        }

        public async Task<int> DeleteHotelAmenity(int amentityId)
        {
            var amenityToDelete = await db.HotelAmenitys.FindAsync(amentityId);
            if (amenityToDelete != null)
            {


                db.HotelAmenitys.Remove(amenityToDelete);
                return await db.SaveChangesAsync();//return number of deleted records 
            }
            return 0;
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenitys()
        {
            try
            {
                IEnumerable<HotelAmenityDTO> amenityDetailsDTOs =
                      // بدون جلب صور       
                      mapper.Map<IEnumerable<HotelAmenity>, IEnumerable<HotelAmenityDTO>>
                              (db.HotelAmenitys);
                //// مع الصور
                //mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>
                //      (db.HotelRooms.Include(r => r.RoomImages));




                return amenityDetailsDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> GetHotelAmenityByID(int amentityId)
        {
            try
            {

                HotelAmenityDTO amenityDetailsDTO = mapper.Map<HotelAmenity, HotelAmenityDTO>(
                    await db.HotelAmenitys.FirstOrDefaultAsync(r => r.Id == amentityId));
                return amenityDetailsDTO;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> IsHotelAmenityUnique(string name, int amentityId = 0)
        {
            if (amentityId == 0)
            {
                //Create
                HotelAmenity hotelAmenity = await db.HotelAmenitys
                .FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower());
                if (hotelAmenity == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                //Update
                HotelAmenity hotelAmenity = await db.HotelAmenitys
               .FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower()
                && a.Id != amentityId
                );
                if (hotelAmenity == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

        public async Task<HotelAmenityDTO> UpdateHotelAmenity(int amentityId, HotelAmenityDTO hotelAmenityDTO)
        {
            try
            {
                if (amentityId == hotelAmenityDTO.Id)
                {
                    //valid
                    HotelAmenity amenityToUpdate = await db.HotelAmenitys.FindAsync(amentityId);
                    HotelAmenity amenity = mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenityDTO, amenityToUpdate);

                    var updatedamenity = db.HotelAmenitys.Update(amenity);
                    await db.SaveChangesAsync();
                    return mapper.Map<HotelAmenity, HotelAmenityDTO>(updatedamenity.Entity);
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
    }
}
