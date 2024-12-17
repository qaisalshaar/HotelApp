﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class HotelRoomDTO
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Enter room name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter room Occupancy")]
        public int Occupancy { get; set; }
        [Range(1, 2000, ErrorMessage = "Price must be Betwwen 1$ to 2000$ ")]
        public double Price { get; set; }
        public string Details { get; set; }
        public string Area { get; set; }


        // for images

        public virtual ICollection<RoomImageDTO> RoomImages { get; set; }
        // لكل غرفة صورة او اكثر
        // ويتم تخزين الصور فيه
        public List<string> ImagesUrl { get; set; }

    }
}
