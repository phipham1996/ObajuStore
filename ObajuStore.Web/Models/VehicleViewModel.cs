using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObajuStore.Model.Abstracts;

namespace ObajuStore.Web.Models
{
    public class VehicleViewModel : Auditable
    {
        public int ID { get; set; }

        public string VehicleNumber { get; set; }

        public string DriverName { get; set; }

        public string Name { get; set; }

        public string ModelID { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<TrackOrderViewModel> TrackOrders { get; set; }
    }
}