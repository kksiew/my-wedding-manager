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
            TableListViewModel tableList = new TableListViewModel();
            GuestBusinessLayer guestBusinessLayer = new GuestBusinessLayer();
            List<TableViewModel> myTablelist = new List<TableViewModel>();

            int Max_Table = 0;
            if (int.TryParse(guestBusinessLayer.GetMaxTable(), out Max_Table))
            {
                for (int i = 1; i <= Max_Table; i++)
                {
                    TableViewModel table = new TableViewModel();
                    List<TableGuestViewModel> tableGuest = new List<TableGuestViewModel>();
                    decimal attendees = 0;
                    decimal local_percentage = 0;

                    table.TableNo = i.ToString();

                    List<Guest> guests = guestBusinessLayer.GetTableGuest(i.ToString());
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
                }
            }

            tableList.Total = guestBusinessLayer.GetTotalGuest();
            tableList.Attended = guestBusinessLayer.GetAttendedGuest();
            tableList.OverallPercentage = (int)Math.Floor(((decimal)tableList.Attended / tableList.Total) * 100);
            tableList.Table = myTablelist;

            return View("TableView", tableList);
        }
    }
}