using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_editfood : System.Web.UI.Page
{

    private List<FoodCategory> lstFoodCategory;
    private List<FoodSource> lstFoodSource;
    private List<USDACategory> lstUSDA;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userID"] == null)
            Response.Redirect("login.aspx");
        else
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["do"].Equals("i"))
                {
                    editFoodOutDiv.Visible = false;
                    lblTitle.Text = "Edit Food In Data";

                    try
                    {
                        using (CCSEntities db = new CCSEntities())
                        {

                            short shFoodInID = short.Parse(Session["editWhat"].ToString());

                            var editFoodInQuery = (from c in db.FoodIns
                                                   where c.FoodInID.Equals(shFoodInID)
                                                   select new 
                                                   { 
                                                       c.FoodInID, 
                                                       c.FoodCategoryID,
                                                       c.USDAID,
                                                       c.FoodSourceID,
                                                       c.Weight, 
                                                       c.TimeStamp 
                                                   });

                            lstFoodCategory = db.FoodCategories.OrderBy(x => x.FoodCategoryID).ToList();
                            ddlFoodInCategoryType.DataSource = lstFoodCategory;
                            ddlFoodInCategoryType.DataBind();

                            lstUSDA = db.USDACategories.OrderBy(x => x.USDAID).ToList();
                            ddlFoodInUSDA.DataSource = lstUSDA;
                            ddlFoodInUSDA.DataBind();

                            lstFoodSource = db.FoodSources.OrderBy(x => x.FoodSourceID).ToList();
                            ddlFoodInSource.DataSource = lstFoodSource;
                            ddlFoodInSource.DataBind();


                            if (editFoodInQuery.ToList().ElementAt(0).FoodInID.ToString() != null)
                                txtEditFoodInID.Text = editFoodInQuery.ToList().ElementAt(0).FoodInID.ToString();

                            if (editFoodInQuery.ToList().ElementAt(0).FoodCategoryID != null)
                            {
                                ddlFoodInCategoryType.SelectedValue = editFoodInQuery.ToList().ElementAt(0).FoodCategoryID.ToString();
                                lblFoodInCatType.Visible = true;
                                ddlFoodInCategoryType.Visible = true;
                                lblFoodInUSDA.Visible = false;
                                ddlFoodInUSDA.Visible = false;
                                chkUSDAin.Checked = false;
                            }
                            else
                            {
                                lblFoodInCatType.Visible = false;
                                ddlFoodInCategoryType.Visible = false;
                                lblFoodInUSDA.Visible = true;
                                ddlFoodInUSDA.Visible = true;
                                chkUSDAin.Checked = true;
                            }

                            if (editFoodInQuery.ToList().ElementAt(0).USDAID != null)
                            {
                                ddlFoodInUSDA.SelectedValue = editFoodInQuery.ToList().ElementAt(0).USDAID.ToString();
                                lblFoodInCatType.Visible = false;
                                ddlFoodInCategoryType.Visible = false;
                                lblFoodInUSDA.Visible = true;
                                ddlFoodInUSDA.Visible = true;
                                chkUSDAin.Checked = true;
                            }
                            else
                            {
                                lblFoodInCatType.Visible = true;
                                ddlFoodInCategoryType.Visible = true;
                                lblFoodInUSDA.Visible = false;
                                ddlFoodInUSDA.Visible = false;
                                chkUSDAin.Checked = false;
                            }

                            txtFoodInWeight.Text = editFoodInQuery.ToList().ElementAt(0).Weight.ToString();
                            ddlFoodInSource.SelectedValue = editFoodInQuery.ToList().ElementAt(0).FoodSourceID.ToString();
                            txtFoodInTime.Text = editFoodInQuery.ToList().ElementAt(0).TimeStamp.ToString();

                        }
                    }
                    catch (System.Threading.ThreadAbortException) { }
                    catch (Exception ex)
                    {
                        LogError.logError(ex);
                        Response.Redirect("../errorpages/error.aspx");
                    }
                }
                else if (Request.QueryString["do"].Equals("o"))
                {
                    editFoodInDiv.Visible = false;
                    lblTitle.Text = "Edit Food Out Data";

                    try
                    {
                        using (CCSEntities db = new CCSEntities())
                        {

                            short shDistID = short.Parse(Session["editWhat"].ToString());

                            var editFoodOutQuery = (from c in db.FoodOuts
                                                 where c.DistributionID.Equals(shDistID)
                                                 select new 
                                                 { 
                                                     c.DistributionID, 
                                                     c.FoodCategoryID, 
                                                     c.USDAID,
                                                     c.Weight, 
                                                     c.Count, 
                                                     c.TimeStamp,
                                                 });

                            lstFoodCategory = db.FoodCategories.OrderBy(x => x.FoodCategoryID).ToList();
                            ddlFoodOutCategoryType.DataSource = lstFoodCategory;
                            ddlFoodOutCategoryType.DataBind();

                            lstUSDA = db.USDACategories.OrderBy(x => x.USDAID).ToList();
                            ddlFoodOutUSDA.DataSource = lstUSDA;
                            ddlFoodOutUSDA.DataBind();

                            txtFoodOutDistID.Text = editFoodOutQuery.ToList().ElementAt(0).DistributionID.ToString();

                            if (editFoodOutQuery.ToList().ElementAt(0).FoodCategoryID != null)
                            {
                                ddlFoodOutCategoryType.SelectedValue = editFoodOutQuery.ToList().ElementAt(0).FoodCategoryID.ToString();
                                lblFoodOutCatType.Visible = true;
                                ddlFoodOutCategoryType.Visible = true;
                                lblFoodOutUSDA.Visible = false;
                                ddlFoodOutUSDA.Visible = false;
                                chkUSDA.Checked = false;
                            }
                            else
                            {
                                lblFoodOutCatType.Visible = false;
                                ddlFoodOutCategoryType.Visible = false;
                                lblFoodOutUSDA.Visible = true;
                                ddlFoodOutUSDA.Visible = true;
                                chkUSDA.Checked = true;
                            }

                            if (editFoodOutQuery.ToList().ElementAt(0).USDAID != null)
                            {
                                ddlFoodOutUSDA.SelectedValue = editFoodOutQuery.ToList().ElementAt(0).USDAID.ToString();
                                lblFoodOutCatType.Visible = false;
                                ddlFoodOutCategoryType.Visible = false;
                                lblFoodOutUSDA.Visible = true;
                                ddlFoodOutUSDA.Visible = true;
                                chkUSDA.Checked = true;
                            }
                            else
                            {
                                lblFoodOutCatType.Visible = true;
                                ddlFoodOutCategoryType.Visible = true;
                                lblFoodOutUSDA.Visible = false;
                                ddlFoodOutUSDA.Visible = false;
                                chkUSDA.Checked = false;
                            }

                            txtFoodOutWeight.Text = editFoodOutQuery.ToList().ElementAt(0).Weight.ToString();
                            txtFoodOutCount.Text = editFoodOutQuery.ToList().ElementAt(0).Count.ToString();
                            txtFoodOutTime.Text = editFoodOutQuery.ToList().ElementAt(0).TimeStamp.ToString();
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
        }
    }


    // @author: Anthony Dietrich - Save Food In Edit Data
    // Saves Edit Data from the Food In version of the editfood page.
    protected void btnEditFoodInSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                short shFoodInID = short.Parse(Session["editWhat"].ToString());

                FoodIn editFoodIn = (from c in db.FoodIns
                                     where c.FoodInID.Equals(shFoodInID)
                                     select c).FirstOrDefault();

                if (chkUSDAin.Checked == true)
                {
                    //save ddlUSDA info, set FoodCatID to null
                    int ID = int.Parse(ddlFoodInUSDA.SelectedValue);
                    USDACategory uc = db.USDACategories.Single(x => x.USDAID == ID);

                    editFoodIn.FoodCategoryID = null;
                    editFoodIn.USDACategory = uc;
                    editFoodIn.FoodSourceID = short.Parse(ddlFoodInSource.SelectedValue);
                    editFoodIn.Weight = Convert.ToDecimal(txtFoodInWeight.Text);
                    editFoodIn.TimeStamp = Convert.ToDateTime(txtFoodInTime.Text);

                    db.SaveChanges();
                    lblResponse.Visible = true;
                    lblResponse.Text = "Changes to Food In ID: " + txtEditFoodInID.Text + ", successfully saved!";

                }
                if (chkUSDAin.Checked == false)
                {
                    //save ddlFoodCat info, set USDAID to null
                    editFoodIn.FoodCategoryID = short.Parse(ddlFoodInCategoryType.SelectedValue);
                    editFoodIn.USDAID = null;
                    editFoodIn.Weight = Convert.ToDecimal(txtFoodInWeight.Text);
                    editFoodIn.FoodSourceID = short.Parse(ddlFoodInSource.SelectedValue);
                    editFoodIn.TimeStamp = Convert.ToDateTime(txtFoodInTime.Text);

                    db.SaveChanges();
                    lblResponse.Visible = true;
                    lblResponse.Text = "Changes to Food In ID: " + txtEditFoodInID.Text + ", successfully saved!";

                }
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }

    // @author: Anthony Dietrich - Save Food Out Edit Data
    // Saves Edit Data from the Food Out version of the editfood page.
    protected void btnEditFoodOutSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {

                short shFoodOutID = short.Parse(Session["editWhat"].ToString());

                FoodOut editFoodOut = (from c in db.FoodOuts
                                       where c.DistributionID.Equals(shFoodOutID)
                                       select c).FirstOrDefault();

                if (chkUSDA.Checked == true)
                {
                    //save ddlUSDA info, set FoodCatID to null
                    int ID = int.Parse(ddlFoodOutUSDA.SelectedValue);
                    USDACategory uc = db.USDACategories.Single(x => x.USDAID == ID);

                    editFoodOut.FoodCategoryID = null;
                    editFoodOut.USDACategory = uc;
                    editFoodOut.Weight = Convert.ToDouble(txtFoodOutWeight.Text);
                    editFoodOut.Count = short.Parse(txtFoodOutCount.Text);
                    editFoodOut.TimeStamp = Convert.ToDateTime(txtFoodOutTime.Text);

                    db.SaveChanges();
                    lblResponse.Visible = true;
                    lblResponse.Text = "Changes to Distribution ID: " + txtFoodOutDistID.Text + ", successfully saved!";

                }
                if (chkUSDA.Checked == false) 
                { 
                    //save ddlFoodCat info, set USDAID to null
                    editFoodOut.DistributionID = short.Parse(txtFoodOutDistID.Text);
                    editFoodOut.FoodCategoryID = short.Parse(ddlFoodOutCategoryType.SelectedValue);
                    editFoodOut.USDAID = null;
                    editFoodOut.Weight = Convert.ToDouble(txtFoodOutWeight.Text);
                    editFoodOut.Count = short.Parse(txtFoodOutCount.Text);
                    editFoodOut.TimeStamp = Convert.ToDateTime(txtFoodOutTime.Text);

                    db.SaveChanges();
                    lblResponse.Visible = true;
                    lblResponse.Text = "Changes to Distribution ID: " + txtFoodOutDistID.Text + ", successfully saved!";

                }
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich - toggleEditUSDA()
    // Toggles an item from USDA to non-USDA
    protected void toggleEditUSDAout(object sender, EventArgs e)
    {
        if (chkUSDA.Checked == true)
        {
            lblFoodOutCatType.Visible = false;
            ddlFoodOutCategoryType.Visible = false;
            lblFoodOutUSDA.Visible = true;
            ddlFoodOutUSDA.Visible = true;
        }
        if (chkUSDA.Checked == false)
        {
            lblFoodOutCatType.Visible = true;
            ddlFoodOutCategoryType.Visible = true;
            lblFoodOutUSDA.Visible = false;
            ddlFoodOutUSDA.Visible = false;
        }
    }

    // @author: Anthony Dietrich - toggleEditUSDA()
    // Toggles an item from USDA to non-USDA
    protected void toggleEditUSDAin(object sender, EventArgs e)
    {
        if (chkUSDAin.Checked == true)
        {
            lblFoodInCatType.Visible = false;
            ddlFoodInCategoryType.Visible = false;
            lblFoodInUSDA.Visible = true;
            ddlFoodInUSDA.Visible = true;
        }
        if (chkUSDAin.Checked == false)
        {
            lblFoodInCatType.Visible = true;
            ddlFoodInCategoryType.Visible = true;
            lblFoodInUSDA.Visible = false;
            ddlFoodInUSDA.Visible = false;
        }
    }
}