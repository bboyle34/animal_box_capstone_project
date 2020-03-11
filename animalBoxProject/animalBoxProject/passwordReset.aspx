<%@ Page Title="" Language="C#" MasterPageFile="~/animalMaster.master" AutoEventWireup="true" CodeFile="passwordReset.aspx.cs" Inherits="passwordReset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="jumbotron d-flex align-items-center">
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4 mb-2">
                    Password Reset for:
                    <asp:Label runat="server" id="user" value=""></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    New Password:
                </div>
                <div class="col-md-4 mb-1">
                    <asp:TextBox type="password" id="txtNewPassword" runat="server" CssClass="form-control validate" required OnTextChanged="txtNewPassword_Changed" AutoPostBack="true"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4 mb-2">
                    <asp:Label runat="server" ID="passwordReqs"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    Confirm New Password:
                </div>
                <div class="col-md-4 mb-1">
                    <asp:TextBox type="password" id="txtConfirmNewPassword" runat="server" CssClass="form-control validate" required AutoPostBack="true"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4 mb-3">
                    <asp:Label runat="server" ID="passwordMatch"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                </div> 
                <div class="col-md-4">
                    <asp:Button ID="btnNewPassword" runat="server" CssClass="btn btn-secondary" Text="Set New Password" OnClick="btnNewPass_Clicked"/>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4 mb-3">
                    <asp:Label runat="server" ID="errorMessage"></asp:Label>
                </div>
            </div>
        </div>
    </div>    
    <!--<script>
        var url_string = window.location.href; //window.location.href
        var url = new URL(url_string);
        var c = url.searchParams.get("variable");
        document.getElementById("user").value = c;
    </script>-->
</asp:Content>

