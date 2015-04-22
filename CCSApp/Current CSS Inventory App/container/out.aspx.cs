﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class container_out : System.Web.UI.Page
{
    private Container c;
    private int id;

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
            if (Request.QueryString["id"] != null)
            {
                id = int.Parse(Request.QueryString["id"]);

                using (CCSEntities db = new CCSEntities())
                {
                    c = (from co in db.Containers
                         where co.BinNumber == id
                         select co).First();

                    if (!Page.IsPostBack)
                    {

                        lblBinNumber1.Text = id.ToString();
                        lblBinNumber2.Text = id.ToString();
                        lblWeight.Text = c.Weight.ToString();
                        
                        if(c.FoodSourceType != null)
                            lblFoodSourceType.Text = c.FoodSourceType.FoodSourceType1.ToString();

                        chkUSDA.Checked = (bool)c.isUSDA;

                        //lblCategory.Text = (bool)c.isUSDA ? c.USDACategory.Description  : c.FoodCategory.CategoryType;

                        lblCategory.Text = c.FoodCategory != null ? c.FoodCategory.CategoryType : "";
                        lblCategory.Text = (bool)c.isUSDA ? c.USDACategory.Description : lblCategory.Text;

 

                        List<DistributionType> distributionTypes = (from dt in db.DistributionTypes select dt).ToList();
                        ddlDistributionType.DataSource = distributionTypes;
                        ddlDistributionType.DataBind();

                        /*List<FoodSourceType> foodSourceList = (from fs in db.FoodSourceTypes select fs).ToList();
                        ddlFoodSourceType.DataSource = foodSourceList;
                        ddlFoodSourceType.DataBind();*/

                        List<Agency> a = (from ag in db.Agencies select ag).ToList();
                        ddlAgency.DataSource = a;
                        ddlAgency.DataBind();
                    }
                }
            }
        //}
        //catch (System.Threading.ThreadAbortException) { }
        //catch (Exception ex)
        //{
        //    LogError.logError(ex);
        //    Response.Redirect("../errorpages/error.aspx");
        //}
    }
    protected void ddlDistributionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistributionType.SelectedValue == "2")
        {
            //Agency Transfer 
            Agency.Visible = true;
        }
        else
        {
            Agency.Visible = false;
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            //Is there a valid distribution type?
            if (ddlDistributionType.SelectedValue != "0")
            {
                bool agencyTransfer = (ddlDistributionType.SelectedValue == "2" && ddlAgency.SelectedValue != "0");
                //Check agency selection if applicable
                if (ddlDistributionType.SelectedValue != "2" || agencyTransfer)
                {
                    using (CCSEntities db = new CCSEntities())
                    {
                        //Create a new foodout log entry

                        FoodOut outLog = new FoodOut();

                        short typeVal = short.Parse(ddlDistributionType.SelectedValue);

                        DistributionType d = (from dt in db.DistributionTypes
                                              where dt.DistributionTypeID == typeVal
                                              select dt).First();
                        outLog.DistributionType = d;

                        //Add extra info to the outlog if agency transfer
                        if (agencyTransfer)
                        {
                            short agencyVal = short.Parse(ddlAgency.SelectedValue);

                            Agency a = (from ag in db.Agencies
                                        where ag.AgencyID == agencyVal
                                        select ag).First();
                            outLog.Agency = a;
                        }

                        c = (from co in db.Containers
                             where co.BinNumber == id
                             select co).First();

                        outLog.FoodSourceType = c.FoodSourceType;
                        outLog.Count = c.Cases;
                        outLog.FoodCategory = (bool)c.isUSDA ? null : c.FoodCategory;
                        outLog.TimeStamp = DateTime.Now;
                        outLog.USDACategory = (bool)c.isUSDA ? c.USDACategory : null;
                        outLog.Weight = (double)c.Weight;
                        outLog.FoodSourceType = (from f in db.FoodSourceTypes select f).FirstOrDefault();


                        db.FoodOuts.Add(outLog);

                        if ((bool) c.isUSDA || chkDelete.Checked)
                        {
                            //If the container is usda, delete it outright
                            //If they check "Delete this container", delete it outright
                            db.Containers.Remove(c);
                        }
                        else
                        {
                            //if the container is a permenant bin, keep it around
                            //clearout the existing container
                            c.Location = null;
                            c.FoodCategory = null;
                            c.USDACategory = null;
                            c.FoodSourceType = null;
                            c.isUSDA = false;
                            c.Weight = 0;
                            c.Cases = 0;
                        }

                        db.SaveChanges();

                        LogChange.logChange("Container " + c.BinNumber + " was moved out.", DateTime.Now, short.Parse(Session["userID"].ToString()));
                    }
                    Response.Redirect("~/");
                }
                else
                {
                    lblError.Text = "Please select an agency";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Please select a reason";
                lblError.Visible = true;
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
}