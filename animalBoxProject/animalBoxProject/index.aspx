<%@ Page Title="" Language="C#" MasterPageFile="~/animalMaster.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!--Map and info Section-->
    <section class="p-5 dash-nav">
        <div class="row mt-5">

              <div class="container col-md-9">
                <div id="map-container-google-1" class="z-depth-1-half map-container" style="height: 600px">
                  <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d55551423.3191085!2d-35.59064575711649!3d26.592525873005705!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x52b30b71698e729d%3A0x131328839761a382!2sNorth%20America!5e0!3m2!1sen!2sus!4v1583158871645!5m2!1sen!2sus" width="800" height="600" frameborder="0" style="border:0;" allowfullscreen=""></iframe>
                </div>
              </div>
            
                <div class="container col-md-3">
                    <p class="text-center">Find your <strong>ARDE</strong>, and public projects on the map</p>
                    <p><strong>About Us:</strong> Servitae is a company focused on the research of small animals using ARDE: Animal Research Development equipment</p>
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
    </section>
</asp:Content>

