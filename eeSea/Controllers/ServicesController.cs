using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eeSea.com.eesea.ws;
using eeSea.Models;
using eeSea.App_Code;
namespace eeSea.Controllers
{
    public class ServicesController : Controller
    {
        Transportal eeSeaWS = new Transportal();
       
        //
        // GET: /Services/
        
        public ActionResult Index()
        {
            var servicesTemp = eeSeaWS.GetActiveServiceDetails()
                                      .Select(service => new Service
                                      {
                                          Id = service.Id,
                                          Name = service.Name,
                                          Code = service.Code
                                      })
                                      .OrderBy(service => service.Name);
            var companiesTemp = eeSeaWS.GetAllCompanies()
                                      .Select(company => new eeSea.Models.Company
                                      {
                                          Id = company.Id,
                                          Code = company.Code,
                                          Name = company.Name
                                      })
                                      .OrderBy(service => service.Name);

            var regions = eeSeaWS.GetAllRegions();

            List<eeSea.Models.Service> ServicesList = null;

            ViewBag.Regions = regions;
            ViewBag.Services = servicesTemp;
            ViewBag.Companies = companiesTemp;
            return View();
        }


        public ActionResult GetServicesResults(string ServiceCode, string RegionId, string LocationCode)
        {
            List<eeSea.Models.ViewModels.ServiceViewModel> servicesTemp = new List<eeSea.Models.ViewModels.ServiceViewModel>();
            if (RegionId != "")
            {
                servicesTemp = eeSeaWS.GetServiceListByRegionId(long.Parse(RegionId))
                                 .Select(service => new eeSea.Models.ViewModels.ServiceViewModel
                                  {
                                      Id = service.Id,
                                      ServiceCode = service.Code,
                                      ServiceName = service.Name
                                  }).ToList();
            }

            if (LocationCode != "0" && LocationCode != null && LocationCode.Trim() != string.Empty)
            {
                servicesTemp.Clear();
                servicesTemp = eeSeaWS.GetServiceListByLocId(long.Parse(LocationCode))
                                   .Select(service => new eeSea.Models.ViewModels.ServiceViewModel
                                   {
                                       Id = service.Id,
                                       ServiceCode = service.Code,
                                       ServiceName = service.Name

                                   }).ToList();
            }

            if (ServiceCode != "")
            {
                servicesTemp.Clear();
                var services = eeSeaWS.GetServiceDetails(long.Parse(ServiceCode));
                var servicesTemp1 = new eeSea.Models.ViewModels.ServiceViewModel
                    {
                        Id = services.ScheduleId,
                        ServiceCode = services.StringCode,
                        ServiceName = services.StringName
                    //    Frequency = services.Frequency.ToString(),
                     //   AvgCapacity = services.Capacity.ToString()
                    };
                servicesTemp.Add(servicesTemp1);

            }

            return View(servicesTemp);
        }

        public ActionResult GetServicesResultsByTerm(string term)
        {
            term = term.Replace("%", " ");
            var servicesTemp = eeSeaWS.GetAllStrings()
                                           .Select(service => new eeSea.Models.ViewModels.ServiceViewModel
                                           {
                                               Id = service.Id,
                                               ServiceCode = service.Code,
                                               ServiceName = service.Name
                                           })
                                           .Where(l => l.ServiceCode.Trim() == term.Trim() || l.ServiceName.Trim() == term.Trim());

            return View("GetServicesResults", servicesTemp);

        }

         [OutputCache(Duration = 600, VaryByParam = "id")]

        public ActionResult ServiceInfos(string id)
        {

            var services = eeSeaWS.GetServiceDetails(long.Parse(id));
            var serviceTemp = new eeSea.Models.ViewModels.ServiceViewModel
            {
                Id = services.ScheduleId,
                ServiceCode = services.StringCode,
                ServiceName = services.StringName,
                CarrierName = "*****",
                MarketingName = "*****",
                StartDate = services.StartDate.ToString("dd/MM/yyyy"),
                EndDate = services.EndDate.ToString("dd/MM/yyyy"),
                Frequency = services.Frequency.ToString(),
                AvgCapacity = services.Capacity.ToString(),
                NoVess = services.NoVess,
                NoVSA = services.NoVSA

            };



            long linkDefintionId;
            var waterLocations = new List<Coordinate>();
            var scheduleLocations = eeSeaWS.GetLocationsOfSchedule(serviceTemp.Id)
                                          .Select(l => new eeSea.Models.Schedule
                                          {
                                              Id = l.Id,
                                              Code = l.LocationFrom.Code,
                                              Name = l.LocationFrom.Name,
                                              LocationFromLatitude = l.LocationFrom.Latitude,
                                              LocationFromLongitude = l.LocationFrom.Longitude,
                                              LocationToLatitude = l.LocationTo.Latitude,
                                              LocationToLongitude = l.LocationTo.Longitude,
                                              WaterRoute = l.WaterRoute.Id
                                          })
                                          .ToList();
            List<string> scheduleSteps = new List<string>();
            string step;
            string fullMapCoordinates;
            fullMapCoordinates = "[";
            foreach (var item in scheduleLocations)
            {
                //waterLocations.Add(new Coordinate { Latitude = item.LocationFromLatitude, Longitude = item.LocationFromLongitude });
                Coordinate[] coordinates = eeSeaWS.GetCoordinatesOfLinkDefinition(item.WaterRoute);   
                if(coordinates.Length > 0)
                   {
                       fullMapCoordinates = fullMapCoordinates + "new google.maps.LatLng(" + coordinates.FirstOrDefault().Latitude.ToString() + "," + coordinates.FirstOrDefault().Longitude.ToString() + "),";
                    step = "[";    
                foreach (Coordinate coordinate in coordinates)
                    {

                        waterLocations.Add(new Coordinate { Latitude = coordinate.Latitude, Longitude = coordinate.Longitude });
                        step = step + "new google.maps.LatLng(" + coordinate.Latitude.ToString() + "," + coordinate.Longitude.ToString() + "),";
                        
                    }
                    step = step.Substring(0, step.Length - 1);
                    step = step + "]";
                    
                    scheduleSteps.Add(step);
                    
                   }

                // waterLocations.Add(new Coordinate { Latitude = item.LocationToLatitude, Longitude = item.LocationToLongitude });
                
                
            }
            fullMapCoordinates = fullMapCoordinates.Substring(0, fullMapCoordinates.Length - 1);
            fullMapCoordinates = fullMapCoordinates + "]";

            ViewBag.TempCoordinates = waterLocations;
            ViewBag.ScheduleLocations = scheduleSteps;
            ViewBag.TempCoordinatesLength = waterLocations.Count;
            ViewBag.FullMapCoordinates = fullMapCoordinates;
            return View(serviceTemp);
        }

          [OutputCache(Duration = 600, VaryByParam = "id")]
        public ActionResult GetFullSchedule(string id)
        {
            var services = eeSeaWS.GetServiceDetails(long.Parse(id));
             Common commonFcts = new Common();
            var serviceTemp = new eeSea.Models.ViewModels.ServiceViewModel
            
            {
                Id = services.ScheduleId,
                ServiceCode = services.StringCode,
                ServiceName = services.StringName,
                CarrierName = "*****",
                MarketingName = "*****",
                StartDate = services.StartDate.ToString("dd/MM/yyyy"),
                EndDate = services.EndDate.ToString("dd/MM/yyyy"),
                Frequency = services.Frequency.ToString(),
                AvgCapacity = services.Capacity.ToString(),
                NoVess = services.NoVess,
                NoVSA = services.NoVSA

            };
            //var actualScheduleLocations = eeSeaWS.
            var scheduleLocations = eeSeaWS.GetLocationsOfSchedule(serviceTemp.Id)
                                         .Select(l => new eeSea.Models.ViewModels.ScheduleLocation
                                         {
                                             Id = l.Id,
                                             StringCode = serviceTemp.ServiceCode,
                                             Code = l.LocationFrom.Code,
                                             Name = l.LocationFrom.Name,
                                             ArrivalDoW = Common.ResolveByteDay(l.ArrivalDoW) + " @ " + Common.ConvertMinutesToTime(l.ArrivalTime),
                                             DepartureDoW = Common.ResolveByteDay(l.DepartureDoW) + " @ " + Common.ConvertMinutesToTime(l.DepartureTime),
                                             ArivalOffset = l.ArrivalOffset ,
                                             DepartureOffset = l.DepartureOffset,
                                             Arival = l.Arrival,
                                             TimeInSea = "***",
                                             Departure = l.Departure,                                             
                                             DistanceToNextPort = Math.Round((decimal)commonFcts.distance((double)l.LocationFrom.Latitude , (double)l.LocationFrom.Longitude
                                                                                     ,(double)l.LocationTo.Latitude, (double)l.LocationTo.Longitude, 'N'),2),
                                              Priority = l.Priority,
                                             Distance = l.WaterRoute.Distance,
                                             
                                         }).ToArray()
                                         ;
            

            var lastDepToFirstArr = -1;

            if (lastDepToFirstArr <= -1)
            {
                lastDepToFirstArr = scheduleLocations[scheduleLocations.Count() - 1].ArivalOffset * 7 * 24 * 60 + scheduleLocations[0].Arival;
            }

            Decimal timeAtSea = 0;

            for (var i = 0; i < scheduleLocations.Count(); i++)
            {
                // check if there is a next location
                if (scheduleLocations.Count() > i + 1)
                    timeAtSea = scheduleLocations[i + 1].Arival - (scheduleLocations[i].Arival + scheduleLocations[i].Departure);
                else if (scheduleLocations.Count() == i + 1)
                    timeAtSea = lastDepToFirstArr - (scheduleLocations[i].Arival + scheduleLocations[i].Departure);
                scheduleLocations[i].TimeInSea = Common.ConvertMinutesToTime((Int32)timeAtSea);
                if (timeAtSea != 0)
                {
                    scheduleLocations[i].Speed = Math.Round(scheduleLocations[i].DistanceToNextPort / (timeAtSea / 60),2);
                }
                else
                {
                    scheduleLocations[i].Speed = 0;
                }
                 
            }

            ViewBag.ScheduleLocations = scheduleLocations;
            return View(serviceTemp);
        }

         // [OutputCache(Duration = 600, VaryByParam = "id")]
        public ActionResult GetScheduleVessels(string id)
        {
            var services = eeSeaWS.GetServiceDetails(long.Parse(id));
            var serviceTemp = new eeSea.Models.ViewModels.ServiceViewModel
            
            {
                Id = services.ScheduleId,
                ServiceCode = services.StringCode,
                ServiceName = services.StringName,
                CarrierName = "*****",
                MarketingName = "*****",
                StartDate = services.StartDate.ToString("dd/MM/yyyy"),
                EndDate = services.EndDate.ToString("dd/MM/yyyy"),
                Frequency = services.Frequency.ToString(),
                AvgCapacity = services.Capacity.ToString(),
                NoVess = services.NoVess,
                NoVSA = services.NoVSA

            };
            var scheduleVessels = eeSeaWS.GetVesselsOfSchedule(serviceTemp.Id)
                                         .Select(l => new eeSea.Models.ViewModels.VesselAttributes
                                         {
                                              
                                             Id = serviceTemp.Id,
                                             //Name = l.Vessel.Name[0].Name,
                                             Priority = l.Priority.ToString(),
                                             VesselInfos = eeSeaWS.GetVesselById(l.Vessel.Id),
                                             NominalCapacity = l.Vessel.NominalCapacity.ToString(),
                                              
                                         }).ToList();
            ViewBag.scheduleVessels =  scheduleVessels;
            return View(serviceTemp);
        }


        public ActionResult GetScheduleVSA(string id)
        {
            var services = eeSeaWS.GetServiceDetails(long.Parse(id));
            var serviceTemp = new eeSea.Models.ViewModels.ServiceViewModel

            {
                Id = services.ScheduleId,
                ServiceCode = services.StringCode,
                ServiceName = services.StringName,
                CarrierName = "*****",
                MarketingName = "*****",
                StartDate = services.StartDate.ToString("dd/MM/yyyy"),
                EndDate = services.EndDate.ToString("dd/MM/yyyy"),
                Frequency = services.Frequency.ToString(),
                AvgCapacity = services.Capacity.ToString(),
                NoVess = services.NoVess,
                NoVSA = services.NoVSA

            };
            var scheduleVSAs = eeSeaWS.GetVSADealsOfSchedule(serviceTemp.Id)
                                         .Select(l => new eeSea.Models.ViewModels.VSADeal
                                         {
                                              
                                             Id = serviceTemp.Id,
                                             Carrier = l.Company.Name,
                                             StringCode = l.StringCode,
                                             StringName = l.StringName,
                                             Percentage = l.Percentage,
                                             AVGShare = (decimal.Parse(serviceTemp.AvgCapacity) * l.Percentage/100)
                                              
                                         }).ToList();
            //Get VSA
            var scheduleSlotCharters = eeSeaWS.GetLocationsOfSchedule(serviceTemp.Id)
                                        .Select(l => new eeSea.Models.ViewModels.SlotCharterView
                                        {
                                            Id = l.Id,
                                            LocationFrom = l.LocationFrom.Name,
                                            LocationTo = l.LocationTo.Name,
                                            SlotCharterInfo = eeSeaWS.GetSlotDealsOfScheduleLocation(l.Id)
                                                              .Select( s => new eeSea.Models.ViewModels.SlotCharterInfo
                                                              {
                                                                Buyer = s.Buyer.Name,
                                                                Seller = s.Seller.Name,
                                                                TEU = s.Capacity,
                                                                
                                                                
                                                              }).FirstOrDefault()
                                        }).ToList();


            ViewBag.scheduleVSAs = scheduleVSAs;
            ViewBag.AvgCap = serviceTemp.AvgCapacity;
            scheduleSlotCharters = scheduleSlotCharters.FindAll(l => l.SlotCharterInfo != null);
            ViewBag.scheduleSlotCharters = (List<eeSea.Models.ViewModels.SlotCharterView>)scheduleSlotCharters;
            return View(serviceTemp);
        }


        public ActionResult GetServicesNames(string term)
        {
            var servicesTemp = eeSeaWS.GetAllStrings()
                                   .Select(service => new
                                   {
                                       label = service.Name
                                   })
                                   .Where(service => service.label.ToLower().Contains(term.ToLower()));



            //termsTemp = termsTemp.Union(servicesTemp);
            return Json(servicesTemp, JsonRequestBehavior.AllowGet);
        }
    }
}
