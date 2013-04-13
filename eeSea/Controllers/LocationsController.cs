using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Collections;
using eeSea.Models;
using eeSea.com.eesea.ws;
using System;
using System.Drawing;


namespace eeSea.Controllers
{
    public class LocationsController : Controller
    {
        //
        // GET: /Locations/
        //private EeSeaContext db = new EeSeaContext();
        Transportal eeSeaWS = new Transportal();


        public ActionResult Index()
        {
            List<eeSea.Models.Location> locations = null;
            var regions = eeSeaWS.GetAllRegions();
            ViewBag.Regions = regions;
            ViewBag.Locations = locations;

            List<eeSea.Models.Location> Alllocations = GetAllLocations();
            ViewBag.AllLocations = Alllocations;
            Session["AllLocations"] = Alllocations;
            // List<eeSea.Models.Link> AlllocationLinks = null;
            ViewBag.AllLocationLinks = null;
            return View();
        }

        public ViewResult LocationInfos(int id)
        {
            var locationTemp = eeSeaWS.GetAllLocations()
                                  .Select(location => new Models.Location
                                    {
                                        Id = location.Id,
                                        Code = location.Code,
                                        Name = location.Name,
                                        Latitude = location.Latitude,
                                        Longitude = location.Longitude,
                                        Region = new eeSea.Models.Region { Id = location.Region.Id, Name = location.Region.Name },
                                        City = new eeSea.Models.City
                                        {
                                            Id = location.MLocation.Id,
                                            Code = location.MLocation.Code,
                                            Name = location.MLocation.Name,
                                            State = new Models.State { Id = location.MLocation.State.Id, Code = location.MLocation.State.Code, Name = location.MLocation.State.Name },
                                            Country = new Models.Country { Id = location.MLocation.Country.Id, Code = location.MLocation.Country.Code, Name = location.MLocation.Country.Name },
                                            Continent = new Models.Continent { Id = location.MLocation.Continent.Id, Code = location.MLocation.Continent.Code, Name = location.MLocation.Continent.Name }
                                        }
                                    })
                                  .Where(l => l.Id == id).SingleOrDefault();
            return View(locationTemp);
        }

        public ActionResult Lookup()
        {
            return View();
        }

        //return Json Locations for Drop Down List

        public ActionResult GetLocationByRegionId(string regionID)
        {
            var locations = default(IList);

            if (!string.IsNullOrEmpty(regionID))
            {
                long? tempRegionId = long.Parse(regionID);
                locations = eeSeaWS.GetAllLocations()
                                   .Where(l => l.Region.Id == tempRegionId)
                                   .Select(l => new { l.Name, l.Code })
                                   .OrderBy(l => l.Name).ToList();
            }
            return Json(locations, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetLocationIdsByRegionId(string regionID)
        {
            var locations = default(IList);

            if (!string.IsNullOrEmpty(regionID))
            {
                long? tempRegionId = long.Parse(regionID);
                locations = eeSeaWS.GetAllLocations()
                                   .Where(l => l.Region.Id == tempRegionId)
                                   .Select(l => new { l.Name, l.Id })
                                   .OrderBy(l => l.Name).ToList();
            }
            return Json(locations, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetLocationsResuls(string RegionId = "0", string LocationCode = "0")
        {
            //var locations = default(IList);
            var locations = default(IList);
            long regionID = long.Parse(RegionId);
            string locationCode = LocationCode;
            var locationsTemp = eeSeaWS.GetAllLocations()
                                       .Select(location => new eeSea.Models.Location
                                       {
                                           Id = location.Id,
                                           Code = location.Code,
                                           Name = location.Name,
                                           Latitude = location.Latitude,
                                           Longitude = location.Longitude,
                                           Region = new eeSea.Models.Region { Id = location.Region.Id, Name = location.Region.Name },
                                           City = new eeSea.Models.City
                                           {
                                               Id = location.MLocation.Id,
                                               Code = location.MLocation.Code,
                                               Name = location.MLocation.Name,
                                               State = new Models.State { Id = location.MLocation.State.Id, Code = location.MLocation.State.Code, Name = location.MLocation.State.Name },
                                               Country = new Models.Country { Id = location.MLocation.Country.Id, Code = location.MLocation.Country.Code, Name = location.MLocation.Country.Name },
                                               Continent = new Models.Continent { Id = location.MLocation.Continent.Id, Code = location.MLocation.Continent.Code, Name = location.MLocation.Continent.Name }
                                           }
                                       })
                                       .Where(l => l.Region.Id == regionID);

            if (locationCode != null && locationCode != "0")
            {
                locations = locationsTemp.Where(l => l.Code == locationCode).ToList();

            }
            else
            {
                locations = locationsTemp.ToList();
            }

            ViewBag.Locations = locations;
            return View(locations);
        }

        public List<Models.Location> GetAllLocations()
        {
            return eeSeaWS.GetAllLocations().Select(location => new Models.Location
                                    {
                                        Id = location.Id,
                                        Code = location.Code,
                                        Name = location.Name,
                                        Latitude = location.Latitude,
                                        Longitude = location.Longitude,
                                        Region = new eeSea.Models.Region { Id = location.Region.Id, Name = location.Region.Name },
                                        City = new eeSea.Models.City
                                        {
                                            Id = location.MLocation.Id,
                                            Code = location.MLocation.Code,
                                            Name = location.MLocation.Name,
                                            State = new Models.State { Id = location.MLocation.State.Id, Code = location.MLocation.State.Code, Name = location.MLocation.State.Name },
                                            Country = new Models.Country { Id = location.MLocation.Country.Id, Code = location.MLocation.Country.Code, Name = location.MLocation.Country.Name },
                                            Continent = new Models.Continent { Id = location.MLocation.Continent.Id, Code = location.MLocation.Continent.Code, Name = location.MLocation.Continent.Name }
                                        }
                                    }).ToList();
        }


        public ActionResult GetAllLinkDefinitions(string FromLocationCode = "0", string ToLocationCode = "0")
        {
            //List<Link> LstLinks = new List<Link>();
            Link ObjLinkModel = null;
            // List<Coordinate> AllLstLinkCoordinate = new List<Coordinate>();
            if (FromLocationCode != "0" && ToLocationCode != "0")
            {
                if (FromLocationCode != ToLocationCode)
                {
                    try
                    {
                        //var data1 = eeSeaWS.GetAllLinkDefinitionsOfLinkedLocations(long.Parse(FromLocationCode), long.Parse(ToLocationCode));
                        List<LinkDefinition> LstLnkDef = eeSeaWS.GetAllLinkDefinitionsOfLinkedLocations(long.Parse(FromLocationCode), long.Parse(ToLocationCode)).ToList();
                        if (LstLnkDef != null)
                        {
                            GPSLib oGPSLib = new GPSLib();
                            ObjLinkModel = new Link();

                            ObjLinkModel.LocationA = ((List<Models.Location>)Session["AllLocations"]).Where(da => da.Id == long.Parse(FromLocationCode)).SingleOrDefault();
                            ObjLinkModel.LocationB = ((List<Models.Location>)Session["AllLocations"]).Where(da => da.Id == long.Parse(ToLocationCode)).SingleOrDefault();

                            ObjLinkModel.Definition = LstLnkDef;
                            ObjLinkModel.MapCoordinates = new Dictionary<long, List<Coordinate>>();
                            ObjLinkModel.RandomColor = new Dictionary<long, System.Drawing.KnownColor>();
                            System.Random randonGen = new System.Random();
                            //string[] colorNames = Enum.GetNames(typeof(KnownColor));
                            string[] colorNames = new string[13];

                            colorNames[0] = Enum.GetName(typeof(KnownColor), KnownColor.DarkBlue);
                            colorNames[1] = Enum.GetName(typeof(KnownColor), KnownColor.Red);
                            colorNames[2] = Enum.GetName(typeof(KnownColor), KnownColor.Green);
                            colorNames[3] = Enum.GetName(typeof(KnownColor), KnownColor.Yellow);
                            colorNames[4] = Enum.GetName(typeof(KnownColor), KnownColor.Black);
                            colorNames[5] = Enum.GetName(typeof(KnownColor), KnownColor.Brown);
                            colorNames[6] = Enum.GetName(typeof(KnownColor), KnownColor.DeepPink);
                            colorNames[7] = Enum.GetName(typeof(KnownColor), KnownColor.Purple);
                            colorNames[8] = Enum.GetName(typeof(KnownColor), KnownColor.Orange);
                            colorNames[9] = Enum.GetName(typeof(KnownColor), KnownColor.Silver);
                            colorNames[10] = Enum.GetName(typeof(KnownColor), KnownColor.Wheat);
                            colorNames[11] = Enum.GetName(typeof(KnownColor), KnownColor.DarkMagenta);
                            colorNames[12] = Enum.GetName(typeof(KnownColor), KnownColor.Gold);

                            List<KnownColor> lstcheckcolors = new List<KnownColor>();
                            foreach (LinkDefinition item in LstLnkDef)
                            {
                                var data = eeSeaWS.GetCoordinatesOfLinkDefinition(item.Id);
                                if (data != null)
                                {
                                    if (data.Count() > 0)
                                    {
                                        List<Coordinate> LstLinkCoordinate = data.ToList();
                                        ObjLinkModel.MapCoordinates.Add(item.Id, LstLinkCoordinate);
                                        KnownColor knownColor = (KnownColor)Enum.Parse(typeof(KnownColor), colorNames[randonGen.Next(1, 12)]);
                                        while (lstcheckcolors.Count != 12 && lstcheckcolors.Contains(knownColor))
                                        {
                                            knownColor = (KnownColor)Enum.Parse(typeof(KnownColor), colorNames[randonGen.Next(1, 12)]);
                                        }
                                        lstcheckcolors.Add(knownColor);
                                        ObjLinkModel.RandomColor.Add(item.Id, knownColor);


                                    }
                                }
                            }

                            ObjLinkModel.GMapJs = GPSLib.PlotGPSPtLocations(ObjLinkModel.MapCoordinates, ObjLinkModel.RandomColor);
                            //ObjLinkModel.GMapJs = GPSLib.PlotMainMap(ObjLinkModel.GMapJs);


                        }

                    }
                    catch { }
                }
            }
            Session["LinkModel"] = ObjLinkModel;
            
            return PartialView(ObjLinkModel);
        }

        public ActionResult DisplayGMap()
        {
            if (Session["LinkModel"] != null)
            {
                Link ObjLinkModel = null;

                ObjLinkModel = (Link)Session["LinkModel"];
                return PartialView(ObjLinkModel);
            }
            return PartialView();
        }


        public ActionResult GetRegionsLocationsByName(string term)
        {
            var termsTemp = eeSeaWS.GetAllLocations()
                                   .Select(location => new
                                   {
                                       label = location.Name
                                   })
                                   .Where(location => location.label.ToLower().Contains(term.ToLower()));
            var tempRegions = eeSeaWS.GetAllRegions()
                                     .Select(region => new
                                     {
                                         label = region.Name
                                     })
                                     .Where(region => region.label.ToLower().Contains(term.ToLower()));


            termsTemp = termsTemp.Union(tempRegions);
            return Json(termsTemp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLocationsResulsByTerm(string term)
        {
            term = term.Replace("%", " ");
            var locationsTemp = eeSeaWS.GetAllLocations()
                                           .Select(location => new eeSea.Models.Location
                                           {
                                               Id = location.Id,
                                               Code = location.Code,
                                               Name = location.Name,
                                               Latitude = location.Latitude,
                                               Longitude = location.Longitude,
                                               Region = new eeSea.Models.Region { Id = location.Region.Id, Name = location.Region.Name },
                                               City = new eeSea.Models.City
                                               {
                                                   Id = location.MLocation.Id,
                                                   Code = location.MLocation.Code,
                                                   Name = location.MLocation.Name,
                                                   State = new Models.State { Id = location.MLocation.State.Id, Code = location.MLocation.State.Code, Name = location.MLocation.State.Name },
                                                   Country = new Models.Country { Id = location.MLocation.Country.Id, Code = location.MLocation.Country.Code, Name = location.MLocation.Country.Name },
                                                   Continent = new Models.Continent { Id = location.MLocation.Continent.Id, Code = location.MLocation.Continent.Code, Name = location.MLocation.Continent.Name }
                                               }
                                           })
                                           .Where(l => l.Name.ToLower().Trim().Contains(term.ToLower().Trim()) || l.Region.Name.ToLower().Trim().Contains(term.ToLower().Trim()));

            return View("GetLocationsResuls", locationsTemp);

        }
    }
}
