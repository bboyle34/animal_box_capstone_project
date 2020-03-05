<%@ Page Title="" Language="C#" MasterPageFile="~/animalMaster.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container pt-5 dash-nav">
    <div class="row">
        <div class="col-md-12 text-center">
            <img src="images/secondary_logo.png" class="img-fluid" width="15%" alt="Secondary Logo">
            <h2>Register Your Animal Reseach Discovery Equipment (ARDE) Unit</h2>
        </div>
    </div>
   <form>
  <div class="form-row">
    <div class="form-group col-md-6">
      <label for="inputEmail4">Email</label>
      <!--<input type="email" class="form-control" id="inputEmail4" placeholder="Email">-->
        <asp:TextBox type="email" ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
    </div>
    <div class="form-group col-md-6">
      <label for="inputPassword4">Password</label>
      <!--<input type="password" class="form-control" id="inputPassword4" placeholder="Temporary Password (Provided with your ARDE unit)">-->
        <asp:TextBox type="password" ID="txtPassword" runat="server" CssClass="form-control" placeholder="Temporary Password (Provided with your ARDE unit)"></asp:TextBox>
    </div>
  </div>
  <div class="form-group">
    <label for="inputAddress">Address</label>
    <!--<input type="text" class="form-control" id="inputAddress" placeholder="1234 Main St">-->
      <asp:TextBox type="text" ID="txtAddress" runat="server" CssClass="form-control" placeholder="1234 Main St"></asp:TextBox>
  </div>
  <div class="form-group">
    <label for="inputAddress2">Address 2</label>
    <!--<input type="text" class="form-control" id="inputAddress2" placeholder="Apartment, studio, or floor">-->
      <asp:TextBox type="text" ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Apartment, Studio, or Floor #"></asp:TextBox>
  </div>
  <div class="form-row">
    <div class="form-group col-md-6">
      <label for="inputCity">City</label>
      <!--<input type="text" class="form-control" id="inputCity" placeholder="City">-->
        <asp:TextBox type="text" ID="txtCity" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
    </div>
    <div class="form-group col-md-4">
      <label for="inputState">State</label>
      <!--<select id="inputState" class="form-control">
        <option selected>State</option>
        <option>...</option>
      </select>-->
        <asp:TextBox type="text" ID="txtState" runat="server" CssClass="form-control" placeholder="State"></asp:TextBox>
    </div>
    <div class="form-group col-md-2">
      <label for="inputZip">Zip</label>
      <!--<input type="text" class="form-control" id="inputZip" placeholder="Zip">-->
        <asp:TextBox type="text" ID="txtZip" runat="server" CssClass="form-control" placeholder="Zip"></asp:TextBox>
    </div>
  </div>
  <div class="form-group">
    <div class="form-check">
      <!--<input class="form-check-input" type="checkbox" id="gridCheck">
      <label class="form-check-label" for="gridCheck">
        Check me out-->
      </label>
    </div>
  </div>
  <!--<button type="submit" class="btn btn-primary">Sign in</button>-->
       <asp:Button type="submit" runat="server" ID="btnCreate" CssClass="btn btn-primary" text="Sign In" OnClick="btnCreate_Click"/>
       <br />
       <asp:Label runat="server" ID="errorMessage"></asp:Label>
</form>
</div>
</asp:Content>

