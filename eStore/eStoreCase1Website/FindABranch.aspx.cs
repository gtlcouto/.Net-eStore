using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eStoreWebObjects;

namespace eStoreCase1Website
{
    public partial class FindABranch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["lat"] != null)
                {
                    // JQuery should give us latitude and longitude
                    var lat = Request.QueryString["lat"];
                    var lng = Request.QueryString["lng"];
                    BranchWeb branch = new BranchWeb();

                    branch.Longitude = Convert.ToDouble(lng);
                    branch.Latitude = Convert.ToDouble(lat);

                    List<BranchWeb> closeBranches = branch.GetThreeClosetBranches();

                    ListViewSearchResults.DataSource = closeBranches;
                    ListViewSearchResults.DataBind();

                    // Loop through each nearby location and build up the JavaScript to place the markers
                    var markers = new List<string>();

                    int count = 1;
                    foreach (BranchWeb location in closeBranches)
                    {
                        markers.Add(string.Format(
                                        @"{{ title: ""Store #{0}"", position: new google.maps.LatLng({1}, {2}), icon: ""img/marker{3}.png"" }}",
                                           location.BranchNumber,
                                           location.Latitude,
                                           location.Longitude,
                                           count++
                                        )
                                      );
                    }

                    var locationsJson = "[" + string.Join(",", markers.ToArray()) + "]";

                    // Inject the Google Maps script client side
                    ClientScript.RegisterStartupScript(this.GetType(), "Google Maps Initialization",
                                                       string.Format("init_map('map_canvas', {0}, {1}, 13, {2});",
                                                       lat, lng, locationsJson), true);
                }
            }
            catch (Exception ex)
            {
                LabelStatus.Text = "Problem getting branch information " + ex.Message;
            }
        }
    }
}