﻿using System.ComponentModel.DataAnnotations;

namespace TireApi.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<int>? CarIds { get; set; } // 1 : N

        public ClientModel(int id, string name, string email, string phone, string address, List<int> carIds)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            CarIds = carIds;
        }
    }
}
