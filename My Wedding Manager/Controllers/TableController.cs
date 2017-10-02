using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Wedding_Manager.Models;
using My_Wedding_Manager.ViewModels;

namespace My_Wedding_Manager.Controllers
{
    public class TableController : Controller
    {
        // GET: Table
        public ActionResult Index()
        {
            int Max_Table = 6;
            TableListViewModel tableList = new TableListViewModel();
            GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();
            List<TableViewModel> myTablelist = new List<TableViewModel>();

            for (int i = 1; i <= Max_Table; i++)
            {
                TableViewModel table = new TableViewModel();
                List<TableGuestViewModel> tableGuest = new List<TableGuestViewModel>();
                decimal attendees = 0;
                decimal local_percentage = 0;

                table.TableNo = i.ToString();

                List<Guest>guests = guestBusinessLayer.GetTableGuest(i.ToString());
                if (guests.Count > 0)
                {
                    foreach (Guest gs in guests)
                    {
                        TableGuestViewModel myGuest = new TableGuestViewModel();
                        myGuest.GuestId = gs.GuestId.ToString();
                        myGuest.Name = gs.Name;
                        myGuest.TableNo = gs.TableNo;
                        myGuest.Attendance = gs.Attendance;
                        if (myGuest.Attendance)
                            attendees++;
                        tableGuest.Add(myGuest);
                    }
                    local_percentage = (attendees / guests.Count) * 100;
                    table.Percentage = (int)Math.Floor(local_percentage);
                    table.TableGuest = tableGuest;
                    myTablelist.Add(table);
                }
                else
                {
                    table.Percentage = 0;
                    break;
                }
            }

            tableList.Table = myTablelist;

            return View("TableView", tableList);
        }
    }
}