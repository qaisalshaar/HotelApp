using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelAmenityRepository
    {

        public Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenityDTO);
        public Task<HotelAmenityDTO> UpdateHotelAmenity(int amentityId, HotelAmenityDTO hotelAmenityDTO);
        public Task<HotelAmenityDTO> GetHotelAmenityByID(int amentityId);


        public Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenitys();


        // if is in update cancel is IsRoomUnique
        //public Task<bool> IsRoomUnique(string name);

        //int roomId = 0 if is create
        public Task<bool> IsHotelAmenityUnique(string name, int amentityId = 0);


        public Task<int> DeleteHotelAmenity(int amentityId);
    }
}
