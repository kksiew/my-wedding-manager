using My_Wedding_Manager.Models;
using My_Wedding_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My_Wedding_Manager.Controllers
{
    public class GuestListController : Controller
    {
        public ActionResult Index()
        {
            GuestListViewModel guestListViewModel = new GuestListViewModel();
            GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();

            List<Guest> guests = guestBusinessLayer.GetGuests();
            List<GuestViewModel> mylist = new List<GuestViewModel>();

            int count = 0;

            foreach(Guest gs in guests)
            {
                if (count == 10)
                    break;
                GuestViewModel guestViewModel = new GuestViewModel();
                guestViewModel.Name = gs.Name;
                guestViewModel.GuestId = gs.GuestId.ToString();
                guestViewModel.ContactNo = gs.ContactNo;
                guestViewModel.Attendance = gs.Attendance;
                mylist.Add(guestViewModel);

                count++;
            }
            guestListViewModel.Guest = mylist;
            guestListViewModel.UserName = "Admin";

            return View("Index", guestListViewModel);
        }
        public ActionResult AddNew()
        {
            GuestViewModel status = new GuestViewModel();
            status.Statusflag = false;
            return View("CreateGuestList", status);
        }
        public ActionResult SaveGuest(Guest g, string BtnSubmit)
        {
            GuestViewModel guestViewModel = new GuestViewModel();
            guestViewModel.Statusflag = false;

            switch (BtnSubmit)
            {
                case "Save Guest":
                    if (ModelState.IsValid)
                    {
                        GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();
                        g.ContactNo = new string(g.ContactNo.Where(char.IsDigit).ToArray());
                        guestBusinessLayer.SaveGuest(g);
                        guestViewModel.Statusflag = true;
                        return View("CreateGuestList", guestViewModel);
                    }
                    else
                    {
                        guestViewModel.Name = g.Name;
                        guestViewModel.ContactNo = g.ContactNo;
                        guestViewModel.Attendance = g.Attendance;
                        guestViewModel.Statusflag = false;
                        return View("CreateGuestList", guestViewModel);
                    }
                case "Cancel":
                    return RedirectToAction("Index");
            }
            return new EmptyResult();
        }
        public ActionResult FindGuest()
        {
            string searchname = Request.Form["SearchByName"];

            GuestListViewModel guestListViewModel = new GuestListViewModel();
            GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();

            List<Guest> guests = new List<Guest>();

            if (searchname == "all")
                guests = guestBusinessLayer.GetGuests();
            else if (searchname != null && searchname != "")
                guests = guestBusinessLayer.FindGuests(searchname);

            List<GuestViewModel> mylist = new List<GuestViewModel>();

            foreach (Guest gs in guests)
            {
                GuestViewModel guestViewModel = new GuestViewModel();
                guestViewModel.Name = gs.Name;
                guestViewModel.GuestId = gs.GuestId.ToString();
                guestViewModel.ContactNo = gs.ContactNo;
                guestViewModel.Attendance = gs.Attendance;
                mylist.Add(guestViewModel);
            }
            guestListViewModel.Guest = mylist;

            return View("Index", guestListViewModel);
        }
        public ActionResult UpdateAttd()
        {
            string attendanceId = Request.Form["Attendance"];
            string GuestIdForAttd = Request.Form["GuestIdForAttd"];
            if(attendanceId == null)
                attendanceId = "0";

            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] arrayGuestIdForAttd = GuestIdForAttd.Split(delimiterChars);
            string[] arrayAttendance = attendanceId.Split(delimiterChars);
            bool foundFlag = false;

            GuestListViewModel guestListViewModel = new GuestListViewModel();
            GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();

            if (attendanceId != "0")
            {
                for (int i = 0; i < arrayGuestIdForAttd.Length; i++)
                {
                    foundFlag = false;
                    for (int j = 0; j < arrayAttendance.Length; j++)
                    {
                        if (arrayGuestIdForAttd[i] == arrayAttendance[j])
                        {
                            guestBusinessLayer.SetAttd(arrayGuestIdForAttd[i]);
                            foundFlag = true;
                        }
                    }
                    if (!foundFlag)
                        guestBusinessLayer.DelAttd(arrayGuestIdForAttd[i]);
                }
            }
            else if (attendanceId == "0")
            {
                for (int i = 0; i < arrayGuestIdForAttd.Length; i++)
                {
                    guestBusinessLayer.DelAttd(arrayGuestIdForAttd[i]);
                }
            }

            List<Guest> guests = new List<Guest>();
            List<GuestViewModel> mylist = new List<GuestViewModel>();

            foreach (string s in arrayGuestIdForAttd)
            {
                guests = guestBusinessLayer.FindGuestsById(s);

                foreach (Guest gs in guests)
                {
                    GuestViewModel guestViewModel = new GuestViewModel();
                    guestViewModel.Name = gs.Name;
                    guestViewModel.GuestId = gs.GuestId.ToString();
                    guestViewModel.ContactNo = gs.ContactNo;
                    guestViewModel.Attendance = gs.Attendance;
                    mylist.Add(guestViewModel);
                }
            }
            guestListViewModel.Guest = mylist;

            return View("Index", guestListViewModel) ;
        }
    }
}