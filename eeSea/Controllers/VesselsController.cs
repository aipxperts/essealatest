using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eeSea.com.eesea.ws;
using eeSea.Models;
using eeSea.Models.ViewModels;

namespace eeSea.Controllers
{
    public class VesselsController : Controller
    {

        Transportal eeSeaWS = new Transportal();
        private EeSeaContext db = new EeSeaContext();
        //
        // GET: /Vessels/
        //[OutputCache(Duration = 300)]
        public ActionResult Index()
        {

            var vesselsTemp = eeSeaWS.GetAllVessels(true);

            var vesselsNamesTemp = vesselsTemp.Where(v => v.Name.Length > 0)
                                             .Select(v => new
                                             {
                                                 Id = v.Id,
                                                 Name = v.Name[0].Name
                                             })
                                             .Distinct();

            var vesselsOwnersTemp = vesselsTemp.Where(v => v.Owner.Length > 0)
                                            .GroupBy(v => v.Owner[0].Company.Name)
                                            .Select(v => v.First().Owner[0].Company);

            var vesselsOperatorsTemp = vesselsTemp.Where(v => v.Operator.Length > 0)
                                            .GroupBy(v => v.Operator[0].Company.Name)
                                            .Select(v => v.First().Operator[0].Company);




            ViewBag.Vessels = vesselsNamesTemp;
            ViewBag.Owners = vesselsOwnersTemp;
            ViewBag.Operators = vesselsOperatorsTemp;
            return View();
        }


        public ActionResult GetVesselsResults(string VesselId = "0", string OwnerId = "0", string OperatorId = "0")
        {
            long vesselId;
            long ownerId;
            long operatorId;
            if (VesselId.Trim() != "")
            {
                vesselId = long.Parse(VesselId);
            }
            else
            {
                vesselId = 0;
            }

            if (OwnerId.Trim() != "")
            {
                ownerId = long.Parse(OwnerId);
            }
            else
            {
                ownerId = 0;
            }

            if (OperatorId.Trim() != "")
            {
                operatorId = long.Parse(OperatorId);
            }
            else
            {
                operatorId = 0;
            }
            var vesselsTemp = eeSeaWS.GetAllVessels(true)
                                     .Select(vessel => new eeSea.Models.Vessel
                                       {
                                           Id = vessel.Id,
                                           HullCode = vessel.HullCode,
                                           BuiltDate = vessel.BuiltDate,
                                           NominalCapacity = vessel.NominalCapacity,
                                           Length = vessel.Length,
                                           Breadth = vessel.Breadth,
                                           Beam = vessel.Beam,
                                           Draft = vessel.Draft,
                                           MaxSpeed = vessel.MaxSpeed,
                                           VesselName = vessel.Name.Length == 0 ? null : new eeSea.Models.VesselName
                                           {
                                               Id = vessel.Name[0].Id,
                                               Name = vessel.Name[0].Name,
                                               Date = vessel.Name[0].Date
                                           },
                                           VesselOwner = vessel.Owner.Length == 0 ? null : new eeSea.Models.VesselOwner
                                           {
                                               Id = vessel.Owner[0].Id,
                                               Date = vessel.Owner[0].Date,
                                               Company = vessel.Owner.Length == 0 ? null : new Models.Company
                                               {
                                                   Id = vessel.Owner[0].Company.Id,
                                                   Code = vessel.Owner[0].Company.Code,
                                                   Name = vessel.Owner[0].Company.Name
                                               }
                                           },
                                           VesselOperator = vessel.Operator.Length == 0 ? null : new Models.VesselOperator
                                           {
                                               Id = vessel.Operator[0].Id,
                                               Date = vessel.Operator[0].Date,
                                               Company = vessel.Operator.Length == 0 ? null : new Models.Company
                                                            {
                                                                Id = vessel.Operator[0].Company.Id,
                                                                Code = vessel.Operator[0].Company.Code,
                                                                Name = vessel.Operator[0].Company.Name
                                                            }
                                           }
                                       });
            if (vesselId != 0)
            {

                vesselsTemp = vesselsTemp.Where(l => l.Id == vesselId).ToList();

            }

            if (ownerId != 0)
            {
                vesselsTemp = vesselsTemp.Where(l => l.VesselOwner != null);
                vesselsTemp = vesselsTemp.Where(l => l.VesselOwner.Company.Id == ownerId).ToList();

            }

            if (operatorId != 0)
            {
                vesselsTemp = vesselsTemp.Where(l => l.VesselOperator != null);
                vesselsTemp = vesselsTemp.Where(l => l.VesselOperator.Company.Id == operatorId).ToList();

            }
            return View(vesselsTemp);
        }


        public ActionResult GetVesselsResultsByTerm(string term)
        {
            term = term.Replace("%", " ");
            var vesselsTemp = eeSeaWS.GetAllVessels(true)
                                     .Select(vessel => new eeSea.Models.Vessel
                                     {
                                         Id = vessel.Id,
                                         HullCode = vessel.HullCode,
                                         BuiltDate = vessel.BuiltDate,
                                         NominalCapacity = vessel.NominalCapacity,
                                         Length = vessel.Length,
                                         Breadth = vessel.Breadth,
                                         Beam = vessel.Beam,
                                         Draft = vessel.Draft,
                                         MaxSpeed = vessel.MaxSpeed,
                                         VesselName = vessel.Name.Length == 0 ? null : new eeSea.Models.VesselName
                                         {
                                             Id = vessel.Name[0].Id,
                                             Name = vessel.Name[0].Name,
                                             Date = vessel.Name[0].Date
                                         },
                                         VesselOwner = vessel.Owner.Length == 0 ? null : new eeSea.Models.VesselOwner
                                         {
                                             Id = vessel.Owner[0].Id,
                                             Date = vessel.Owner[0].Date,
                                             Company = vessel.Owner.Length == 0 ? null : new Models.Company
                                             {
                                                 Id = vessel.Owner[0].Company.Id,
                                                 Code = vessel.Owner[0].Company.Code,
                                                 Name = vessel.Owner[0].Company.Name
                                             }
                                         },
                                         VesselOperator = vessel.Operator.Length == 0 ? null : new Models.VesselOperator
                                         {
                                             Id = vessel.Operator[0].Id,
                                             Date = vessel.Operator[0].Date,
                                             Company = vessel.Operator.Length == 0 ? null : new Models.Company
                                             {
                                                 Id = vessel.Operator[0].Company.Id,
                                                 Code = vessel.Operator[0].Company.Code,
                                                 Name = vessel.Operator[0].Company.Name
                                             }
                                         }
                                     })
                                     .Where(vessel => vessel.VesselName != null && vessel.VesselName.Name.ToLower().Contains(term.ToLower()))
                                    ;

            return View("GetVesselsResults", vesselsTemp);

        }






        public ActionResult VesselInfos(int id)
        {
            var vesselTemp = eeSeaWS.GetAllVessels(true)
                                     .Select(vessel => new eeSea.Models.Vessel
                                     {
                                         Id = vessel.Id,
                                         HullCode = vessel.HullCode,
                                         BuiltDate = vessel.BuiltDate,
                                         NominalCapacity = vessel.NominalCapacity,
                                         Length = vessel.Length,
                                         Breadth = vessel.Breadth,
                                         Beam = vessel.Beam,
                                         Draft = vessel.Draft,
                                         MaxSpeed = vessel.MaxSpeed,
                                         VesselName = vessel.Name.Length == 0 ? null : new eeSea.Models.VesselName
                                         {
                                             Id = vessel.Name[0].Id,
                                             Name = vessel.Name[0].Name,
                                             Date = vessel.Name[0].Date
                                         },
                                         VesselOwner = vessel.Owner.Length == 0 ? null : new eeSea.Models.VesselOwner
                                         {
                                             Id = vessel.Owner[0].Id,
                                             Date = vessel.Owner[0].Date,
                                             Company = vessel.Owner.Length == 0 ? null : new Models.Company
                                             {
                                                 Id = vessel.Owner[0].Company.Id,
                                                 Code = vessel.Owner[0].Company.Code,
                                                 Name = vessel.Owner[0].Company.Name
                                             }
                                         },
                                         VesselOperator = vessel.Operator.Length == 0 ? null : new Models.VesselOperator
                                         {
                                             Id = vessel.Operator[0].Id,
                                             Date = vessel.Operator[0].Date,
                                             Company = vessel.Operator.Length == 0 ? null : new Models.Company
                                             {
                                                 Id = vessel.Operator[0].Company.Id,
                                                 Code = vessel.Operator[0].Company.Code,
                                                 Name = vessel.Operator[0].Company.Name
                                             }
                                         }
                                     })
                                     .Where(vessel => vessel.Id == id).FirstOrDefault();

            //List<VesselDynamicAttributesViewModel> vesselDynamicAttributes = db.VesselDynamicAttributesViewModels
            //                                .Where(v => v.VesselNameId == id)
            //                                .OrderByDescending(v => v.VesselNameDate)
            //                                .ThenByDescending(v => v.VesselOwnerDate)
            //                                .ThenByDescending(v => v.VesselOperatorDate)
            //                                .ToList();

            var vesselDynamicAttributes = eeSeaWS.GetVesselHistory(id)
                                                 .Select(vessel => new eeSea.Models.ViewModels.VesselDynamicAttributesViewModel
                                                 {
                                                     VesselDynamicAttributId = vessel.AttrId,
                                                     VesselNameId = vessel.VessId,
                                                     VesselName = vessel.VessName,
                                                     VesselNameDate = vessel.NameStart,
                                                     OwnerName = vessel.OwnName,
                                                     VesselOwnerDate = vessel.OwnerStart,
                                                     OperatorName = vessel.OpName,
                                                     VesselOperatorDate = vessel.OpStart
                                                 })

                                                 .OrderByDescending(v => v.VesselNameDate)
                                                 .ThenByDescending(v => v.VesselOwnerDate)
                                                 .ThenByDescending(v => v.VesselOperatorDate)

                                                 .ToList();


            ViewBag.VesselDynamicAttributes = vesselDynamicAttributes;
            ViewBag.Total = vesselDynamicAttributes.Count();

            return View(vesselTemp);
        }

        [OutputCache(Duration = 300)]
        public ActionResult GetVesselsAttributes(string term)
        {
            var vesselsTemp = eeSeaWS.GetAllVessels(true)
                                     .Select(vessel => new
                                     {
                                         label = vessel.Name.Length == 0 ? "" : vessel.Name[0].Name
                                     })
                                     .Where(vessel => vessel.label.ToLower().Contains(term.ToLower()));

            var ownersTemp = eeSeaWS.GetAllVessels(true)
                                     .Select(vessel => new
                                     {
                                         label = vessel.Owner.Length == 0 ? "" : vessel.Owner[0].Company.Name
                                     })
                                     .Where(vessel => vessel.label.ToLower().Contains(term.ToLower()));

            var operatorsTemp = eeSeaWS.GetAllVessels(true)
                                      .Select(vessel => new
                                      {
                                          label = vessel.Operator.Length == 0 ? "" : vessel.Operator[0].Company.Name
                                      })
                                      .Where(vessel => vessel.label.ToLower().Contains(term.ToLower()));


            vesselsTemp = vesselsTemp.Union(ownersTemp);
            vesselsTemp = vesselsTemp.Union(operatorsTemp);

            vesselsTemp = vesselsTemp.OrderBy(v => v.label);
            return Json(vesselsTemp, JsonRequestBehavior.AllowGet);
        }



    }
}
