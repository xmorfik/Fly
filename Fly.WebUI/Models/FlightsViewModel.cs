﻿using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models
{
    public class FlightsViewModel
    {
        public MetaData MetaData { get; set; } = new();
        public FlightParameter FlightParameter { get; set; } = new();
    }
}