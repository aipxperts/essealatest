using eeSea.com.eesea.ws;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eeSea.Controllers
{
    class GPSLib
    {
        public static String PlotGPSPoints(Dictionary<long, List<Coordinate>> MapCoordinates)
        {
            try
            {
                String Locations = "";
                String sJScript = "";
                int i = 0;
                foreach (KeyValuePair<long, List<Coordinate>> item in MapCoordinates)
                {
                    for (int j = 0; j < item.Value.Count; j++)
                    {
                        // bypass empty rows 
                        if (item.Value[j].Latitude.ToString().Trim().Length == 0)
                            continue;

                        string Latitude = item.Value[j].Latitude.ToString();
                        string Longitude = item.Value[j].Latitude.ToString();

                        // create a line of JavaScript for marker on map for this record 
                        Locations += Environment.NewLine + @"
                path.push(new google.maps.LatLng(" + Latitude + ", " + Longitude + @"));

                var marker" + i.ToString() + @" = new google.maps.Marker({
                    position: new google.maps.LatLng(" + Latitude + ", " + Longitude + @"),
                    title: '#' + path.getLength(),
                    map: map
                });";
                        j++;

                    }

                }


                // construct the final script
                sJScript = @"<script type='text/javascript'>

            var poly;
            var map;

            function initialize() {
                var cmloc = new google.maps.LatLng(33.779005, -118.178985);
                var myOptions = {
                    zoom: 11,
                    center: cmloc,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };

                map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);

                var polyOptions = {
                    strokeColor: 'blue',
                    strokeOpacity: 0.5,
                    strokeWeight: 3
                }
                poly = new google.maps.Polyline(polyOptions);
                poly.setMap(map);

                var path = poly.getPath();

               " + Locations + @"

                    }
                </script>";
                return sJScript;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static String PlotGPSPtLocations(Dictionary<long, List<Coordinate>> MapCoordinates, Dictionary<long, System.Drawing.KnownColor> MapRouteColors)
        {
            try
            {
                String Locations = "";
                int i = 0;
                foreach (KeyValuePair<long, List<Coordinate>> item in MapCoordinates)
                {
                    Locations += @"    var flightPlanCoordinates" + i.ToString() + " = [";
                    //                    Locations += @"new google.maps.LatLng(37.772323, -122.214897),
                    //                new google.maps.LatLng(21.291982, -157.821856),
                    //                new google.maps.LatLng(-18.142599, 178.431),
                    //                new google.maps.LatLng(-27.46758, 153.027892)
                    //            ];";
                    for (int j = 0; j < item.Value.Count; j++)
                    {
                        // bypass empty rows 
                        if (item.Value[j].Latitude.ToString().Trim().Length == 0)
                            continue;

                        string Latitude = item.Value[j].Latitude.ToString();
                        string Longitude = item.Value[j].Longitude.ToString();

                        // create a line of JavaScript for marker on map for this record 

                        if (j == item.Value.Count - 1)
                        {
                            Locations += Environment.NewLine + @"   new google.maps.LatLng(" + Latitude + ", " + Longitude + ")";
                            Locations += "  ];";
                        }
                        else
                        {
                            Locations += Environment.NewLine + @"   new google.maps.LatLng(" + Latitude + ", " + Longitude + "),";
                        }

                    }
                    Locations += @"    var flightPath" + i.ToString() + " = new google.maps.Polyline({";
                    Locations += " path: flightPlanCoordinates" + i.ToString() + ",";
                    Locations += @"strokeColor: '" + Enum.GetName(typeof(System.Drawing.KnownColor), MapRouteColors[item.Key]) + "',";
                    //Locations += @"strokeColor: 'Red',";
                    Locations += @"strokeOpacity: 1.0,
                strokeWeight: 2
            });
            flightPath" + i.ToString() + ".setMap(map);";
                    i++;
                }


                return Locations;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static String PlotGPSPtLocations2(Dictionary<long, List<Coordinate>> MapCoordinates)
        {
            try
            {
                String Locations = "";
                int i = 0;
                foreach (KeyValuePair<long, List<Coordinate>> item in MapCoordinates)
                {
                    for (int j = 0; j < item.Value.Count; j++)
                    {
                        // bypass empty rows 
                        if (item.Value[j].Latitude.ToString().Trim().Length == 0)
                            continue;

                        string Latitude = item.Value[j].Latitude.ToString();
                        string Longitude = item.Value[j].Latitude.ToString();

                        // create a line of JavaScript for marker on map for this record 
                        Locations += Environment.NewLine + @"
                path.push(new google.maps.LatLng(" + Latitude + ", " + Longitude + @"));

                var marker" + i.ToString() + @" = new google.maps.Marker({
                    position: new google.maps.LatLng(" + Latitude + ", " + Longitude + @"),
                    title: '#' + path.getLength(),
                    map: map
                });";
                        i++;

                    }

                }


                return Locations;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static String PlotMainMap(string Locations)
        {
            try
            {

                String sJScript = "";


                // construct the final script
                sJScript = @"<script type='text/javascript'>

            var poly;
            var map;

            function initialize() {
alert('asdasd');
                var cmloc = new google.maps.LatLng(33.779005, -118.178985);
                var myOptions = {
                    zoom: 11,
                    center: cmloc,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };

                map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);

                var polyOptions = {
                    strokeColor: 'blue',
                    strokeOpacity: 0.5,
                    strokeWeight: 3
                }
                poly = new google.maps.Polyline(polyOptions);
                poly.setMap(map);

                var path = poly.getPath();

               " + Locations + @"

                    }
                </script>";
                return sJScript;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
