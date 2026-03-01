using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Ticket_Booking_System
{
    class Cinema
    {
        string cinema_id = "default";
        string location = "default";
        string opening_hours = "0000-0000";
        int no_of_screens = 0;
        int no_of_tickets = 0;
        float total_sales = 0;
        

        // display Cinema Objects
        public string DisplayCinemaInfo()
        {
            string display;
            display = cinema_id.PadRight(20) + location.PadRight(20) + opening_hours.PadRight(20) + no_of_screens.ToString();
            return display;
        }

        public string DisplayCinemaSales()
        {
            string display_sales;
            display_sales = cinema_id.PadRight(20) + no_of_tickets.ToString().PadRight(20) + total_sales.ToString("C").PadRight(20) + "\n";
            return display_sales;
        }

        // update Cinema Objects
        public void Update(string my_cinema_id, string my_location, string my_opening_hours, int my_no_of_screens)
        {
            cinema_id = my_cinema_id;
            location = my_location;
            opening_hours = my_opening_hours;
            no_of_screens = my_no_of_screens;
        }

        // getter methods
        public float GetTotalSales()
        {
            return total_sales;
        }

        public int GetNoOfTicketsSold()
        {
            return no_of_tickets;
        }

        public string GetID()
        {
            return cinema_id;
        }

        public string GetLocation()
        {
            return location;
        }

        public string GetOpeningHours()
        {
            return opening_hours;
        }

        public int GetNoOfScreens()
        {
            return no_of_screens;
        }

        // setter methods
        public void SetTotalSales(float my_total_sales)
        {
            total_sales = my_total_sales;
        }

        public void SetNoOfTicketsSold(int tickets)
        {
            no_of_tickets = tickets;
        }

        public void SetID(string id)
        {
            cinema_id = id;
        }

        public void SetLocation(string l)
        {
            location = l;
        }

        public void SetOpeningHours(string o)
        {
            opening_hours = o;
        }

        public void SetNoOfScreens(int no)
        {
            no_of_screens = no;
        }
    }
}
