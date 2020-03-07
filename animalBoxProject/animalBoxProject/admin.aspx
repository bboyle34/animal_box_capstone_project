<%@ Page Title="" Language="C#" MasterPageFile="~/animalMaster.master" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container pt-5 dash-nav">
    
    <div class="row text-center mb-5">
        <div class="col-md-12 text-center">
            <img src="images/secondary_logo.png" class="img-fluid" width="15%" alt="Secondary Logo">
            <h1>Hello <%:Session["userEmail"]%></h1>
            <h2>Dashboard</h2>
        </div>
    </div>
    <!-- test comment -->
    <div class="row">
            <div class="nav flex-column nav-pills col-md-1" id="v-pills-tab" role="tablist" aria-orientation="vertical">
              <a class="nav-link active" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="true">Profile</a>
              <a class="nav-link" id="v-pills-project-tab" data-toggle="pill" href="#v-pills-project" role="tab" aria-controls="v-pills-project" aria-selected="false">Projects</a>
              <a class="nav-link" id="v-pills-data-tab" data-toggle="pill" href="#v-pills-data" role="tab" aria-controls="v-pills-data" aria-selected="false">Project Data</a>
              <a class="nav-link" id="v-pills-settings-tab" data-toggle="pill" href="#v-pills-setting" role="tab" aria-controls="v-pills-settings" aria-selected="false">ARDE Settings</a>
            </div>
            <div class="tab-content col-md-11" id="v-pills-tabContent">
                <div class="tab-pane fade" id="v-pills-profile" role="tabpanel" aria-labelledby="v-pills-profile-tab">...</div>
                <div class="tab-pane fade show active" id="v-pills-project" role="tabpanel" aria-labelledby="v-pills-project-tab">...</div>
                <div class="tab-pane fade" id="v-pills-data" role="tabpanel" aria-labelledby="v-pills-data-tab">
                    <div class="card-deck">
                        <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-3">
                                    <img class="card-img-top" src="images/secondary_logo.png" alt="Specie sprite">
                                </div>
                                <div class="col-9">
                                    <p class="card-text"><strong>Species Name:</strong> Ratus Patus</p>
                                    <p class="card-text"><strong>Common Name:</strong> Rat Boi</p>  
                                    <p class="card-text"><strong>ARDE Temperature:</strong> 78.2F</p>
                                    <p class="card-text"><strong>ARDE Location:</strong> London, UK</p>
                                    <p class="card-text"><strong>Description:</strong> Rat Boi is the man. He protects his fellow rats, loves el Cheese and above all believes in keeping himself clean to avoid the Corona Virus, but still enjoys an ice-cold Corona from time to time. </p>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                          <small class="text-muted">Last updated 3 mins ago</small>
                        </div>
                      </div>
                        <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-3">
                                    <img class="card-img-top" src="images/secondary_logo.png" alt="Specie sprite">
                                </div>
                                <div class="col-9">
                                    <p class="card-text"><strong>Species Name:</strong> Ratus Patus</p>
                                    <p class="card-text"><strong>Common Name:</strong> Rat Boi</p>  
                                    <p class="card-text"><strong>ARDE Temperature:</strong> 78.2F</p>
                                    <p class="card-text"><strong>ARDE Location:</strong> London, UK</p>
                                    <p class="card-text"><strong>Description:</strong> Rat Boi is the man. He protects his fellow rats, loves el Cheese and above all belives in keeping himslef clean to avoid the Corona Virus, but still enjoys an ice-cold Corona from time to time. </p>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                          <small class="text-muted">Last updated 3 mins ago</small>
                        </div>
                      </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-settings" role="tabpanel" aria-labelledby="v-pills-settings-tab">...</div>
            </div>
    </div>
</div>
</asp:Content>

