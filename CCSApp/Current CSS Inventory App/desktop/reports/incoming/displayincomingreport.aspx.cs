﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

/// <summary>
/// Allows the user to set the options of the incoming eport.
/// </summary>
public partial class desktop_reports_incoming_DisplayIncomingReport : System.Web.UI.Page
{
    /// <summary>
    /// Loads the Data into the report and sets the paramters
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                LogChange.logChange("Ran Incoming Report", DateTime.Now, short.Parse(Session["userID"].ToString()));

                if (Session["startDate"] == null || Session["endDate"] == null || Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                DateTime startDate = (DateTime)Session["startDate"];
                DateTime endDate = (DateTime)Session["endDate"];
                IncomingReportTemplate template = (IncomingReportTemplate)Session["reportTemplate"];

                ReportDataSource source = new ReportDataSource("DataSet", (DataTable)(LoadData(startDate, endDate, template).Incoming));
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.LocalReport.SetParameters(new ReportParameter("startDate", startDate.ToString("d")));
                reportViewer.LocalReport.SetParameters(new ReportParameter("endDate", endDate.ToString("d")));
                reportViewer.LocalReport.SetParameters(new ReportParameter("reportTitle", template.Title));
                reportViewer.LocalReport.SetParameters(new ReportParameter("showGrandTotal", (!template.ShowGrandTotal).ToString()));
                reportViewer.LocalReport.SetParameters(new ReportParameter("display", ((int)template.DisplayType).ToString()));
                reportViewer.LocalReport.SetParameters(new ReportParameter("showDonorAddress", (!template.ShowDonorAddress).ToString()));


                reportViewer.DataBind();
                reportViewer.LocalReport.Refresh();

            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("~/errorpages/error.aspx");
        }
    }


    /// <summary>
    /// Loads the data into a dataset depending on the options set by the user.
    /// </summary>
    /// <param name="startdate">The date to start from</param>
    /// <param name="enddate">The dataset with the data loaded into it</param>
    /// <param name="template">The template to load options for the query</param>
    /// <returns>The dataset with the data loaded into it</returns>
    private ReportsDataSet LoadData(DateTime startdate, DateTime enddate, IncomingReportTemplate template)
    {
        ReportsDataSet ds = new ReportsDataSet();

        using (CCSEntities db = new CCSEntities())
        {
            List<FoodIn> data = db.FoodIns.Where(f => (f.TimeStamp.Year >= startdate.Year && f.TimeStamp.Day >= startdate.Day && f.TimeStamp.Month >= startdate.Month) &&
                                (f.TimeStamp.Year <= enddate.Year && f.TimeStamp.Day <= enddate.Day && f.TimeStamp.Month <= enddate.Month)).ToList();

            if (template.FoodSourceTypesSelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedTypes = new List<short>();
                foreach (var i in template.FoodSourceTypes)
                    selectedTypes.Add(short.Parse(i));

                data = (from c in data
                        where selectedTypes.Contains((short)c.FoodSource.FoodSourceTypeID)
                        select c).ToList();
            }

            if (template.FoodSourcesSelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedSources = new List<short>();
                foreach (var i in template.FoodSources)
                    selectedSources.Add(short.Parse(i));

                data = (from c in data
                        where selectedSources.Contains((short)c.FoodSourceID)
                        select c).ToList();
            }


            List<FoodIn> foodInRegularData = new List<FoodIn>();

            if(template.CategoriesSelection != ReportTemplate.SelectionType.NONE)
                foodInRegularData = data.Where(f => f.FoodCategory != null && f.USDACategory == null).ToList();

            List<FoodIn> foodInUSDAData = data.Where(f => f.USDACategory != null && f.FoodCategory == null).ToList();


            if (template.CategoriesSelection == ReportTemplate.SelectionType.REGULAR)
            {
                foodInRegularData = (from c in foodInRegularData
                              where c.FoodCategory.Perishable == false && c.FoodCategory.NonFood == false
                              select c).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.PERISHABLE)
            {
                foodInRegularData = (from c in foodInRegularData
                              where c.FoodCategory.Perishable == true && c.FoodCategory.NonFood == false
                              select c).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.NONFOOD)
            {
                foodInRegularData = (from c in foodInRegularData
                              where c.FoodCategory.Perishable == false && c.FoodCategory.NonFood == true
                              select c).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedCategories = new List<short>();
                foreach (var i in template.FoodCategories)
                    selectedCategories.Add(short.Parse(i));

                foodInRegularData = (from c in foodInRegularData
                              where selectedCategories.Contains((short)c.FoodCategoryID)
                              select c).ToList();
            }


            if (template.USDASelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedUSDA = new List<short>();
                foreach (var i in template.USDACategories)
                    selectedUSDA.Add(short.Parse(i));

                foodInUSDAData = (from u in foodInUSDAData
                                      where selectedUSDA.Contains((short)u.USDAID)
                                      select u).ToList();
            }

            if (template.USDASelection != ReportTemplate.SelectionType.NONE)
            {
                foodInRegularData.InsertRange(0, foodInUSDAData);
            }

            data = foodInRegularData;

            foreach (var i in data)
            {
                if (i.FoodCategory != null || i.USDACategory != null)
                {
                    string address = string.Format("{0} {1} {2}, {3} {4}", i.FoodSource.Address.StreetAddress1, i.FoodSource.Address.StreetAddress2, i.FoodSource.Address.City.CityName, i.FoodSource.Address.State.StateShortName, i.FoodSource.Address.Zipcode.ZipCode1);
                    ds.Incoming.AddIncomingRow(i.FoodCategory == null ? i.USDACategory.Description : i.FoodCategory.CategoryType, i.TimeStamp, i.Count == null ? 0 : (double)i.Count, i.Weight == null ? 0 : (double)i.Weight, i.FoodSource.Source, address, i.FoodSource.FoodSourceType.FoodSourceType1);
                }
            }
        }
        return ds;
    }
}