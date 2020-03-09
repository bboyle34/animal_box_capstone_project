<%@ Page Title="" Language="C#" MasterPageFile="~/animalMaster.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!--Map and info Section-->
    <section class="p-5 dash-nav">
        <div class="row mt-5">

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

                var markerMessages = ['ARDE BOX #234', 'ARDE BOX #98', 'ARDE BOX #543']
                var latSpan = .1;
                var lngSpan = .1;
                for (var i = 0; i < markerMessages.length; i++) {
                    var marker = new google.maps.Marker({
                        position: {
                            lat: 38.4495688 + i,
                            lng: -78.8689156 + i
                        },
                        map: map
                    });
                    attachMessage(marker, markerMessages[i]);
                }
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
            }
            function attachMessage(marker, message) {
                var infoWindow = new google.maps.infoWindow({
                    content: message
                });
                marker.addListenter('click', function () {
                    infoWindow.open(marker.get('map'), marker);
                });
            }
        </script>
    </section>
</asp:Content>

