using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using FlightManagementSystem.Filters;
using FlightManagementSystem.Models;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace FlightManagementSystem.Controllers
{
    public class FlightsController : Controller
    {
        //
        // GET: /Flights/

        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult AddFlight()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddFlight(AddFlights model)
        {
            MySqlConnection conn = null;
            string myConnectionString  = "server=localhost;uid=root;pwd=rootpassword;database=flightmanagement";
                try
                {
                    conn = new MySqlConnection(myConnectionString);
                    MySqlCommand cmd = new MySqlCommand("InsertFlightDetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Date", model.Date);
                    cmd.Parameters.AddWithValue("@Time", model.Time);
                    cmd.Parameters.AddWithValue("@SeatsAvailable", model.SeatsAvailable);
                    cmd.Parameters.AddWithValue("@Runway", model.RunwayId);
                    cmd.Parameters.AddWithValue("@Gate", model.Gate);
                    cmd.Parameters.AddWithValue("@PlainId", model.PlaneID);
                    cmd.Parameters.AddWithValue("@AirportID_Origin", model.DestinationId);
                    cmd.Parameters.AddWithValue("@AirportID_Destination", model.OriginId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                catch (MySqlException ex)
                {

                }

            return View("AddFlight", model);
        }

        public ActionResult AddPlane()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPlane(AddPlane mod)
        {
            MySqlConnection conn = null;
            string myConnectionString = "server=localhost;uid=root;pwd=rootpassword;database=flightmanagement";
            try
            {
                conn = new MySqlConnection(myConnectionString);
                MySqlCommand cmd = new MySqlCommand("InsertPlaneDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PlaneID", mod.PlaneID);
                cmd.Parameters.AddWithValue("@Model", mod.Model);
                cmd.Parameters.AddWithValue("@Manufacturer", mod.Manufacturer);
                cmd.Parameters.AddWithValue("@SeatCapacity", mod.SeatCapacity);
                cmd.Parameters.AddWithValue("@Age", mod.Age);
                cmd.Parameters.AddWithValue("@LastMaintenancedOn", mod.LastMaintenanceOn);
                cmd.Parameters.AddWithValue("@NextMaintenancedOn", mod.NextMaintenanceOn);
                cmd.Parameters.AddWithValue("@Maintenance_Comments", mod.Maintenance_Comments);
                cmd.Parameters.AddWithValue("@EmployeeID_Maintenance", mod.EmployeeIdManatainace);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (MySqlException ex)
            {

            }

            return View();
        }

        public ActionResult BookingFlights()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetFlights(string date)
        {
            MySqlConnection conn = null;
            string myConnectionString = "server=localhost;uid=root;pwd=rootpassword;database=flightmanagement";
            DataSet ds = new DataSet();
            List<AddFlights> flight = new List<AddFlights>();
            using ( conn = new MySqlConnection(myConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand("GetFlightDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", date);
                {
                    cmd.Connection = conn;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows) // loop for adding add from dataset to list<modeldata>
                {
                    flight.Add(new AddFlights
                    {
                        FlightId = Convert.ToInt32(dr["FlightID"]),
                        PlaneID = Convert.ToInt32(dr["PlaneID"]), // adding data from dataset row in to list<modeldata>
                        Date = dr["Date"].ToString(),
                        Time = dr["Time"].ToString(),
                        SeatsAvailable = Convert.ToInt32(dr["SeatsAvailable"]),
                        Price = Convert.ToInt32(dr["Price"]),
                        OriginName = dr["OriginName"].ToString(),
                        OriginCity = dr["OriginCity"].ToString(),
                        OriginCountry = dr["OriginCountry"].ToString(),
                        DestName = dr["DestName"].ToString(),
                        DestCity = dr["DestCity"].ToString(),
                        DestCountry = dr["DestCountry"].ToString()
                    });
                }
            }
            ViewBag.userdetails = flight;
            return View("BookingFlights");
        }

        public ActionResult UpdateFlight(int flightid, int PlaneId, string Date, string Time, int SeatsAvailable, int Price)
        {
            AddFlights updateflight = new AddFlights();
            ViewBag.flightid = flightid;
            ViewBag.planeid = PlaneId;
            ViewBag.Date = Date;
            ViewBag.Time = Time;
            ViewBag.seatsavailable = SeatsAvailable;
            ViewBag.Price = Price;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateFlight(AddFlights updateflight)
        {
            MySqlConnection conn = null;
            string myConnectionString = "server=localhost;uid=root;pwd=rootpassword;database=flightmanagement";
            using (conn = new MySqlConnection(myConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand("UpdateFlightDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_FlightId", updateflight.FlightId);
                cmd.Parameters.AddWithValue("@p_PlainId", updateflight.PlaneID);
                cmd.Parameters.AddWithValue("@p_Date", updateflight.Date);
                cmd.Parameters.AddWithValue("@p_Time", updateflight.Time);
                cmd.Parameters.AddWithValue("@p_SeatsAvailable", updateflight.SeatsAvailable);
                cmd.Parameters.AddWithValue("@p_Price", updateflight.Price);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return Redirect("GetFlights?date=" + updateflight.Date);
            }
        }

        public ActionResult FlightBookingDetails(int flightid, int price)
        {
            Passenger psg = new Passenger();
            psg.FlightID = flightid;
            psg.AmountPaid = price;
            return View(psg);
        }

        [HttpPost]
        public ActionResult FlightBookingDetails(Passenger png)
        {
            MySqlConnection conn = null;
            string myConnectionString = "server=localhost;uid=root;pwd=rootpassword;database=flightmanagement";
            using (conn = new MySqlConnection(myConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SaveBooking", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_FirstName", png.FirstName);
                cmd.Parameters.AddWithValue("@P_LastName", png.LastName);
                cmd.Parameters.AddWithValue("@P_Address", png.Address);
                cmd.Parameters.AddWithValue("@P_DOB", png.DOB);
                cmd.Parameters.AddWithValue("@P_Email", png.EMail);
                cmd.Parameters.AddWithValue("@P_PhoneNumber", png.PhoneNum);
                cmd.Parameters.AddWithValue("@P_FlightID", png.FlightID);
                cmd.Parameters.AddWithValue("@P_AmountPaid", png.AmountPaid);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return View("BookingFlights");
        }

        public ActionResult AddAirport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAirport(AddAirport Airport)
        {
            MySqlConnection conn = null;
            string myConnectionString = "server=localhost;uid=root;pwd=rootpassword;database=flightmanagement";
            using (conn = new MySqlConnection(myConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand("AddAirportDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_AirportId", Airport.AirportId);
                cmd.Parameters.AddWithValue("@P_Name", Airport.Name);
                cmd.Parameters.AddWithValue("@P_City", Airport.City);
                cmd.Parameters.AddWithValue("@P_Country", Airport.Country);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return View();
            }
        }

        public ActionResult CheckIn()
        {
            return View("CheckInDetails");
        }

        [HttpGet]
        public ActionResult CheckInDetails(int flightid, string Fname, string Lname)
        {
            MySqlConnection conn = null;
            string myConnectionString = "server=localhost;uid=root;pwd=rootpassword;database=flightmanagement";
            DataSet ds = new DataSet();
            List<AddFlights> flights = new List<AddFlights>();
            using (conn = new MySqlConnection(myConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand("GetCheckInInfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_FirstName", Fname);
                cmd.Parameters.AddWithValue("@P_LastName", Lname);
                cmd.Parameters.AddWithValue("@P_flightID", flightid);
                {
                    cmd.Connection = conn;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows) // loop for adding add from dataset to list<modeldata>
                {
                    flights.Add(new AddFlights
                    {
                        FlightId = Convert.ToInt32(dr["FlightID"]),
                        PlaneID = Convert.ToInt32(dr["PlaneID"]), // adding data from dataset row in to list<modeldata>
                        Date = dr["Date"].ToString(),
                        Time = dr["Time"].ToString(),
                        SeatsAvailable = Convert.ToInt32(dr["SeatsAvailable"]),
                        OriginName = dr["OriginName"].ToString(),
                        OriginCity = dr["OriginCity"].ToString(),
                        OriginCountry = dr["OriginCountry"].ToString(),
                        DestName = dr["DestName"].ToString(),
                        DestCity = dr["DestCity"].ToString(),
                        DestCountry = dr["DestCountry"].ToString(),
                        IsCheckedIn = Convert.ToInt32(dr["IsCheckedIn"]),
                        CustomerId = Convert.ToInt32(dr["CustomerId"])
                    });
                }
            }
            ViewBag.checkIndetails = flights;
            return View();
        }

        public ActionResult SaveCheckIn(int flightid, int custid)
        {
            MySqlConnection conn = null;
            string myConnectionString = "server=localhost;uid=root;pwd=rootpassword;database=flightmanagement";
            using (conn = new MySqlConnection(myConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SaveCheckIn", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_CheckIn", 1);
                cmd.Parameters.AddWithValue("@P_custid", custid);
                cmd.Parameters.AddWithValue("@P_flightid", flightid);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return View("CheckInDetails");
        }
    }
}
