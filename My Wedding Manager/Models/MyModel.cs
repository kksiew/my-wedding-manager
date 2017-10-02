using My_Wedding_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using My_Wedding_Manager.DataAccessLayer;
using My_Wedding_Manager.Validation;

namespace My_Wedding_Manager.Models
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }
        [NameValidation]
        public string Name { get; set; }
        [ContactValidation]
        public string ContactNo { get; set; }
        public string TableNo { get; set; }
        public bool Attendance { get; set; }
        //public string Meal { get; set; }
    }
    public class GuestBusinessLayer
    {
        public List<Guest> GetGuests()
        {
            GuestsList dbGuest = new GuestsList();
            return dbGuest.dbGuestsList.ToList();
        }
        public List<Guest> FindGuests(string name)
        {
            GuestsList dbGuest = new GuestsList();
            string sqlQuery = "SELECT * FROM TblGuestsList WHERE Name LIKE '%" + name + "%'";
            return dbGuest.dbGuestsList.SqlQuery(sqlQuery).ToList();
        }
        public List<Guest> FindGuestsById(string guestId)
        {
            GuestsList dbGuest = new GuestsList();
            string sqlQuery = "SELECT * FROM TblGuestsList WHERE GuestId =" + guestId;
            return dbGuest.dbGuestsList.SqlQuery(sqlQuery).ToList();
        }
        public Guest SaveGuest(Guest g)
        {
            GuestsList dbGuest = new GuestsList();
            dbGuest.dbGuestsList.Add(g);
            dbGuest.SaveChanges();
            return g;
        }
        public void SetAttd(string GuestId)
        {
            GuestsList dbGuest = new GuestsList();
            string sqlQuery = "UPDATE TblGuestsList SET Attendance = 1 WHERE GuestId = " + GuestId;
            dbGuest.Database.ExecuteSqlCommand(sqlQuery);
            dbGuest.SaveChanges();
        }
        public void DelAttd(string GuestId)
        {
            GuestsList dbGuest = new GuestsList();
            string sqlQuery = "UPDATE TblGuestsList SET Attendance = 0 WHERE GuestId = " + GuestId;
            dbGuest.Database.ExecuteSqlCommand(sqlQuery);
            dbGuest.SaveChanges();
        }
        public List<Guest> GetTableGuest(string TableNo)
        {
            GuestsList dbGuest = new GuestsList();
            string sqlQuery = "SELECT * FROM TblGuestsList WHERE TableNo = " + TableNo;
            return dbGuest.dbGuestsList.SqlQuery(sqlQuery).ToList();
        }
    }
}