﻿using AutoMapper;
using DataAccsess.Data.Models;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>().ReverseMap();
            CreateMap<RoomImageDTO, RoomImage>().ReverseMap();
            CreateMap<HotelAmenityDTO, HotelAmenity>().ReverseMap();
        }


    }
}
