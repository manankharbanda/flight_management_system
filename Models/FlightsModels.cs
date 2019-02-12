using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;

namespace FlightManagementSystem.Models
{
    public class FlightsModels
    {

    }

    public class AddFlights
    {
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [Display(Name = "Flight ID")]
        public int FlightId { get; set; }

        [Required]
        [Display(Name = "Flight Date")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Flight Time")]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Seats Available")]
        public int SeatsAvailable { get; set; }

        [Display(Name = "Runway ID")]
        public int RunwayId { get; set; }

        [Display(Name = "Gate")]
        public string Gate { get; set; }

        [Required]
        [Display(Name = "Plane ID")]
        public int PlaneID { get; set; }

        [Required]
        [Display(Name = "Destination ID")]
        public int DestinationId { get; set; }

        [Required]
        [Display(Name = "Origin ID")]
        public int OriginId { get; set; }

        [Display(Name = "Price")]
        public int Price { get; set; }

        [Display(Name = "Origin Name")]
        public string OriginName { get; set; }

        [Display(Name = "Origin City")]
        public string OriginCity { get; set; }

        [Display(Name = "Origin Country")]
        public string OriginCountry { get; set; }

        [Display(Name = "Destination Name")]
        public string DestName { get; set; }

        [Display(Name = "Destination City")]
        public string DestCity { get; set; }

        [Display(Name = "Destination Country")]
        public string DestCountry { get; set; }

        [Display(Name = "Is Checked In")]
        public int IsCheckedIn { get; set; }
    }

    public class AddPlane
    {
        [Required]
        [Display(Name = "Plane ID")]
        public int PlaneID  { get; set; }

        [Required]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Display(Name = "Seat Capacity")]
        public int SeatCapacity { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Last Maintenance On")]
        public string LastMaintenanceOn { get; set; }

        [Display(Name = "Next Maintenance On")]
        public string NextMaintenanceOn { get; set; }

        [Display(Name = "Maintenance Comments")]
        public string Maintenance_Comments { get; set; }

        [Display(Name = "Employee ID Manatainace")]
        public int EmployeeIdManatainace { get; set; }
    }
   
    public class Passenger
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string EMail { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNum { get; set; }

        [Display(Name = "Seat Number")]
        public string SeatNumber { get; set; }

        [Display(Name = "Seat Class")]
        public string SeatClass { get; set; }

        [Required]
        [Display(Name = "Amount Paid")]
        public int AmountPaid { get; set; }

        [Display(Name = "Meal Preference")]
        public string MealPreference { get; set; }


        [Display(Name = "Bag ID")]
        public string BagID { get; set; }


        [Display(Name = "Bag Weight")]
        public string BagWeight { get; set; }

        [Display(Name = "Is Checked In")]
        public string IsCheckedIn { get; set; }

        [Display(Name = "Flight ID")]
        public int FlightID { get; set; }

        [Display(Name = "Customer ID")]
        public string CustomerID { get; set; }
    }

    public class AddAirport
    {
        [Required]
        [Display(Name = "Airport ID")]
        public int AirportId { get; set; }

        [Required]
        [Display(Name = "Airport Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Airport City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Airport Country")]
        public string Country { get; set; }
    }

    public enum FlightClass
    {
        EconomyClass,
        PremiumEconomyClass,
        BusinessClass,
        FirstClass
    }
}