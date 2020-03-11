<%@ Page Title="" Language="C#" MasterPageFile="~/animalMaster.master" AutoEventWireup="true" CodeFile="passwordRecovery.aspx.cs" Inherits="passwordRecovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-12 mb-5">
            </div>
        </div>
    </div>
    <div class="jumbotron d-flex align-items-center">
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    Please enter you Email:
                </div>
                <div class="col-md-4 mb-2">
                    <asp:TextBox type="email" id="txtUserEmail" runat="server" CssClass="form-control validate" required></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4 mb-3">
                    <asp:Label runat="server" ID="errorMessage"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                </div> 
                <div class="col-md-4">
                    <asp:Button ID="btnRecoverPass" runat="server" CssClass="btn btn-secondary" OnClick="btnRecoverPass_Clicked" Text="Send Recovery Email"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

