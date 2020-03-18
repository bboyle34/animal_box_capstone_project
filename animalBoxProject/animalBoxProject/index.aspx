<%@ Page Title="" Language="C#" MasterPageFile="~/animalMaster.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!--Map and info Section-->
    <section class="p-5 dash-nav">
        <div class="row mt-5">
            <!-- github training -->
                <!-- second training -->

              <div class="container col-md-9">
                <div id="map-container-google-1" class="z-depth-1-half map-container" style="height: 600px">
                </div>
              </div>
            
                <div class="container col-md-3">
                    <p class="text-center">Find your <strong>ARDE</strong>, and public projects on the map</p>
                    <p><strong>About Us:</strong> Servitae is a company focused on the research of small animals using ARDE: Animal Research Discovery Equipment</p>
                    <br>
                    <p><strong>Our Vision:</strong> To Preserve and research small animals in order to ensure their sustainability and well-being.</p>
                    <p class="text-center"><strong>Learn More</strong> by Visiting Our About Page and <strong>Get Involved</strong> with your very own <strong>ARDE</strong> unit!</p>
                    <div class="text-center">
                    <a role="button" class="btn btn-success" href="#">Learn More</a>
                    <a role="button" class="btn btn-success" href="#">Buy an ARDE unit</a>
                    </div>
                </div>
                <hr class="my-5 bg-dark">
            </div>
        <script>
            function initMap() {
                var hburg = { lat: 38.4495688, lng: -78.8689156 };
                var jmu = { lat: 38.4092923, lng: -78.753912 };
                var bridge = { lat: 38.3782182, lng: -78.9693624 };
                var map = new google.maps.Map(document.getElementById('map-container-google-1'), { zoom: 10, center: hburg });
                //var marker = new google.maps.Marker({ position: hburg, map: map, title: 'Project One' });
                //var marker = new google.maps.Marker({ position: jmu, map: map, title: 'Project Two' });
                //var marker = new google.maps.Marker({ position: bridge, map: map, title: 'Project Three' });

                //var markerMessages = ['ARDE BOX #234', 'ARDE BOX #98', 'ARDE BOX #543']
                //var latSpan = .1;
                //var lngSpan = .1;
                //for (var i = 0; i < markerMessages.length; ++i) {
                //    var marker = new google.maps.Marker({
                //        position: {
                //            lat: 38.4495688 + i,
                //            lng: -78.8689156 + i
                //        },
                //        map: map
                //    });
                //    attachMessage(marker, markerMessages[i]);
                //}

                //map.addListener('center_changed', function () {
                //    // 3 seconds after the center of the map has changed, pan back to the
                //    // marker.
                //    window.setTimeout(function () {
                //        map.panTo(marker.getPosition());
                //    }, 3000);
                //});

                //marker.addListener('click', function () {
                //    map.setZoom(15);
                //    map.setCenter(marker.getPosition());
                //});
                //var bounds = {
                //    north: -25.363882,
                //    south: -31.203405,
                //    east: 131.044922,
                //    west: 125.244141
                //};

                //var latCoords = [
                //    38.4495688,
                //    38.4092923,
                //    38.3782182
                //];
                //var longCoords = [
                //    -78.8689156,
                //    -78.753912,
                //    -78.9693624
                //];
                //var sites = [];
                //function () {
                //    $.ajax({
                //        type: "Post",
                //        url: "index.aspx/getUrl",
                //        contentType: "application/json; charset=utf-8",
                //        dataType: "json",
                //        success: function (response) {
                //            var sites = response.d;
                //            alert(sites.length);
                //        },

                //        failure: function (msg) {
                //            alert(msg);
                //        }
                //    });
                //}


                // Display the area between the location southWest and northEast.
                //map.fitBounds(bounds);

                // Add 5 markers to map at random locations.
                // For each of these markers, give them a title with their index, and when
                // they are clicked they should open an infowindow with text from a secret
                // message.
                //var messages = [
                //    'ARDE BOX #234',
                //    'ARDE BOX #98',
                //    'ARDE BOX #543'
                //];
                //var lngSpan = bounds.east - bounds.west;
                //var latSpan = bounds.north - bounds.south;
                for (var i = 0; i < sites.length; ++i) {
                    var marker = new google.maps.Marker({
                        position: {
                            lat: latitudes[i],
                            lng: longitudes[i]
                        },
                        map: map,
                        url: sites[i]
                    });
                    attachMessage(marker, messages[i], sites[i]);
                }
            }

            // Attaches an info window to a marker with the provided message. When the
            // marker is clicked, the info window will open with the secret message.
            function attachMessage(marker, message, site) {
                var infowindow = new google.maps.InfoWindow({
                    content: message
                });

                marker.addListener('click', function () {
                    infowindow.open(marker.get('map'), marker);
                    //window.open = marker.url;
                });
                google.maps.event.addListener(marker, "dblclick", function (e) {
                    window.open(marker.url);
                });
            }
            //function attachMessage(marker, message) {
            //    var infowindow = new google.maps.InfoWindow({
            //        content: message
            //    });
            //    marker.addListenter('click', function () {
            //        infowindow.open(marker.get('map'), marker);
            //    });
            //}
        </script>
    </section>
</asp:Content>

