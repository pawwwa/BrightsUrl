﻿using System.Net;

namespace BrightsUrl.Web.ViewModels
{
    public class UrlDataViewModel
    {
        public string Url { get; set; }
        public string? Title { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}