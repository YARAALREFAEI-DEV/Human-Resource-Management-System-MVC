using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS_FinalProject.Models
{
    public class Company
    {
      

        [Key]
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
        public string Phone { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public DateTime EstablishedDate { get; set; } 

      
    }
}