using My_Wedding_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using My_Wedding_Manager.DataAccessLayer;
using My_Wedding_Manager.Validation;
using System.Data.Entity;

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
    public class MaxTable
    {
        public string TableNo { get; set; }
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
        public Guest FindGuestsById(int guestId)
        {
            GuestsList dbGuest = new GuestsList();
            return dbGuest.dbGuestsList.Find(guestId);
        }
        public Guest SaveGuest(Guest g)
        {
            GuestsList dbGuest = new GuestsList();
            dbGuest.dbGuestsList.Add(g);
            dbGuest.SaveChanges();
            return g;
        }
        public Guest EditGuest(Guest g)
        {
            GuestsList dbGuest = new GuestsList();
            dbGuest.Entry(g).State = EntityState.Modified;
            dbGuest.SaveChanges();
            return g;
        }
        public void DeleteGuest(int guestId)
        {
            GuestsList dbGuest = new GuestsList();
            string sqlQuery = "DELETE FROM TblGuestsList WHERE GuestId = " + guestId;
            dbGuest.Database.ExecuteSqlCommand(sqlQuery);
            dbGuest.SaveChanges();
        }
        public bool GetAttd(int GuestId)
        {
            GuestsList dbGuest = new GuestsList();
            Guest guest = dbGuest.dbGuestsList.Where(a => a.GuestId == GuestId).Single(a => a.Attendance);
            return guest.Attendance;
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
        public string GetMaxTable()
        {
            GuestsList dbGuest = new GuestsList();
            return dbGuest.dbGuestsList.Max(g => g.TableNo);
        }
        public int GetTotalGuest()
        {
            GuestsList dbGuest = new GuestsList();
            int total = dbGuest.dbGuestsList.Count();
            return total;
        }
        public int GetAttendedGuest()
        {
            GuestsList dbGuest = new GuestsList();
            int attended = dbGuest.dbGuestsList.Where(g => g.Attendance == true).ToList().Count();
            return attended;
        }
    }
}