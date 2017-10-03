using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My_Wedding_Manager.ViewModels
{
    public class GuestViewModel
    {
        public string GuestId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string TableNo { get; set; }
        public bool Attendance { get; set; }
        public bool Statusflag { get; set; }
    }
    public class GuestListViewModel
    {
        public List<GuestViewModel> Guest { get; set; }
        public string UserName { get; set; }
    }
    public class TableGuestViewModel
    {
        public string GuestId { get; set; }
        public string Name { get; set; }
        public string TableNo { get; set; }
        public bool Attendance { get; set; }
    }
    public class TableViewModel
    {
        public int Percentage { get; set; }
        public string TableNo { get; set; }
        public List<TableGuestViewModel> TableGuest { get; set; }
    }
    public class TableListViewModel
    {
        public int Total { get; set; }
        public int Attended { get; set; }
        public int OverallPercentage { get; set; }
        public List<TableViewModel> Table { get; set; }
    }
}