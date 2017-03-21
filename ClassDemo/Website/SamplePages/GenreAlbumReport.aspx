<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GenreAlbumReport.aspx.cs" Inherits="SamplePages_GenreAlbumReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <rsweb:ReportViewer ID="GenreAlbumReportViewer" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Reports\GenreAlbumReport.rdlc">
            <DataSources>
                <rsweb:ReportDataSource Name="GenreAlbumDS" DataSourceId="ReportODS"></rsweb:ReportDataSource>
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    
    <asp:ObjectDataSource ID="ReportODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GenreAlbumReport_Get" 
        TypeName="ChinookSystem.BLL.TrackController">
    </asp:ObjectDataSource>
</asp:Content>

