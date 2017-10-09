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
                guestViewModel.TableNo = gs.TableNo;
                guestViewModel.Attendance = gs.Attendance;
                mylist.Add(guestViewModel);

                count++;
            }
            guestListViewModel.Guest = mylist;

            return View("Index", guestListViewModel);
        }
        public ActionResult ManageGuests()
        {
            GuestListViewModel guestListViewModel = new GuestListViewModel();
            GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();

            List<Guest> guests = guestBusinessLayer.GetGuests();
            List<GuestViewModel> mylist = new List<GuestViewModel>();

            foreach (Guest gs in guests)
            {
                GuestViewModel guestViewModel = new GuestViewModel();
                guestViewModel.Name = gs.Name;
                guestViewModel.GuestId = gs.GuestId.ToString();
                guestViewModel.ContactNo = gs.ContactNo;
                guestViewModel.TableNo = gs.TableNo;
                guestViewModel.Attendance = gs.Attendance;
                mylist.Add(guestViewModel);
            }
            guestListViewModel.Guest = mylist;

            return View("ManageGuestList", guestListViewModel);
        }
        public ActionResult AddNew()
        {
            GuestViewModel status = new GuestViewModel();
            status.Statusflag = false;
            return View("SaveGuestList", status);
        }
        [ValidateAntiForgeryToken]
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
                        return View("SaveGuestList", guestViewModel);
                    }
                    else
                    {
                        guestViewModel.Name = g.Name;
                        guestViewModel.ContactNo = g.ContactNo;
                        guestViewModel.TableNo = g.TableNo;
                        guestViewModel.Attendance = g.Attendance;
                        guestViewModel.Statusflag = false;
                        return View("SaveGuestList", guestViewModel);
                    }
                case "Cancel":
                    return RedirectToAction("ManageGuests");
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
                guestViewModel.TableNo = gs.TableNo;
                guestViewModel.Attendance = gs.Attendance;
                mylist.Add(guestViewModel);
            }
            guestListViewModel.Guest = mylist;

            return View("Index", guestListViewModel);
        }
        public JsonResult JFindGuest()
        {
            GuestListViewModel guestListViewModel = new GuestListViewModel();
            GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();

            List<Guest> guests = new List<Guest>();
            string keyword = "";

            if (RouteData.Values["id"] != null)
                keyword = RouteData.Values["id"].ToString();

            if (keyword == "all")
                guests = guestBusinessLayer.GetGuests();
            else if (keyword != "" && keyword != null)
                guests = guestBusinessLayer.FindGuests(keyword);

            List<GuestViewModel> mylist = new List<GuestViewModel>();

            foreach (Guest gs in guests)
            {
                GuestViewModel guestViewModel = new GuestViewModel();
                guestViewModel.Name = gs.Name;
                guestViewModel.GuestId = gs.GuestId.ToString();
                guestViewModel.ContactNo = gs.ContactNo;
                guestViewModel.TableNo = gs.TableNo;
                guestViewModel.Attendance = gs.Attendance;
                mylist.Add(guestViewModel);
            }
            guestListViewModel.Guest = mylist;

            return Json(guestListViewModel.Guest, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditGuest(int id)
        {
            GuestViewModel guestViewModel = new GuestViewModel();
            if (id != 0)
            {
                GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();
                Guest guests = guestBusinessLayer.FindGuestsById(id);
                if (guests == null)
                    return View("EditGuestListError");
                guestViewModel.Name = guests.Name;
                guestViewModel.GuestId = guests.GuestId.ToString();
                guestViewModel.ContactNo = guests.ContactNo;
                guestViewModel.TableNo = guests.TableNo;
                guestViewModel.Attendance = guests.Attendance;
            }
            return View("EditGuestList", guestViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGuest(Guest guest)
        {
            string BtnSubmit = Request.Form["BtnSubmit"];
            switch (BtnSubmit)
            {
                case "Save Guest":
                    GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();
                    if (ModelState.IsValid)
                        guestBusinessLayer.EditGuest(guest);
                    break;
                case "Cancel":
                    return RedirectToAction("ManageGuests");
            }
            return RedirectToAction("ManageGuests");
        }
        public ActionResult DeleteGuest(int id)
        {
            GuestViewModel guestViewModel = new GuestViewModel();
            if (id != 0)
            {
                GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();
                Guest guests = guestBusinessLayer.FindGuestsById(id);
                guestViewModel.Name = guests.Name;
                guestViewModel.GuestId = guests.GuestId.ToString();
                guestViewModel.ContactNo = guests.ContactNo;
                guestViewModel.TableNo = guests.TableNo;
                guestViewModel.Attendance = guests.Attendance;
            }
            return View("DeleteGuestConfirm", guestViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmedDeleteGuest(int id)
        {
            GuestViewModel guestViewModel = new GuestViewModel();
            string BtnSubmit = Request.Form["BtnSubmit"];

            if (id != 0)
            {
                switch (BtnSubmit)
                {
                    case "Yes":
                        GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();
                        guestBusinessLayer.DeleteGuest(id);
                        break;
                    case "Cancel":
                        return RedirectToAction("ManageGuests");
                }
            }
            return RedirectToAction("ManageGuests");
        }
        public ActionResult UpdateAttd()
        {
            string attendanceId = Request.Form["Attendance"];
            string GuestIdForAttd = Request.Form["GuestIdForAttd"];
            string FromPage = Request.Form["FromPage"];
            if (attendanceId == null)
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

            Guest guests = new Guest();
            List<GuestViewModel> mylist = new List<GuestViewModel>();

            foreach (string s in arrayGuestIdForAttd)
            {
                GuestViewModel guestViewModel = new GuestViewModel();
                guests = guestBusinessLayer.FindGuestsById(int.Parse(s));
                guestViewModel.Name = guests.Name;
                guestViewModel.GuestId = guests.GuestId.ToString();
                guestViewModel.ContactNo = guests.ContactNo;
                guestViewModel.TableNo = guests.TableNo;
                guestViewModel.Attendance = guests.Attendance;
                mylist.Add(guestViewModel);
            }
            guestListViewModel.Guest = mylist;

            if (FromPage == "ManageGuestList")
                return View("ManageGuestList", guestListViewModel);
            else
                return View("Index", guestListViewModel);
        }
        public JsonResult JUpdateAttd(AttdViewModel g)
        {
            string GuestId = g.GuestId;
            bool Attendance = g.Attendance;
            int GuestIdInt = int.Parse(g.GuestId);

            GuestListViewModel guestListViewModel = new GuestListViewModel();
            GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();
            
            if (Attendance)
                guestBusinessLayer.SetAttd(GuestId);
            else
                guestBusinessLayer.DelAttd(GuestId);

            return Json(g, JsonRequestBehavior.AllowGet);
        }
    }
}