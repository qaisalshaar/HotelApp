using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelRomeRepository
    {

        public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> GetHotelRoomByID(int roomId);


        public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms();


        // if is in update cancel is IsRoomUnique
        //public Task<bool> IsRoomUnique(string name);

        //int roomId = 0 if is create
        public Task<bool> IsRoomUnique(string name, int roomId = 0);


        public Task<int> DeleteHotelRoom(int roomId);
    }
}
