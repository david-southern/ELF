<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="AuthFailure.aspx.cs" Inherits="Website.AuthFailure" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <div class="pageheader">
        You have tried to access a page that requires you to be logged in. Please visit the
        <a href="Login.aspx">Login page</a>
        to login first.
      </div>
    </div>
  </center>
</asp:Content>
