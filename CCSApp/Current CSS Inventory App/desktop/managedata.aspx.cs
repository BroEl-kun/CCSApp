using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class desktop_managedata : System.Web.UI.Page
{
    private String sortingColumn;
    private Boolean sortAscending;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userID"] == null)
            Response.Redirect("login.aspx");

        if (!IsPostBack)
        {
            if (Request.QueryString["do"] != null)
            {
                if (Request.QueryString["do"].Equals("FI"))
                {
                    try
                    {
                        if (chkUSDAin.Checked == false)
                        {
                            txtSearchFoodIn.Visible = true;
                            txtSearchUSDAFoodIn.Visible = false;
                        }
                        else
                        {
                            txtSearchFoodIn.Visible = false;
                            txtSearchUSDAFoodIn.Visible = true;
                        }
                        searchFoodInDiv.DefaultButton = "btnSearchFoodIn";
                        lblFoodInError.Text = "";
                        foodInDiv.Visible = true;
                        foodOutDiv.Visible = false;
                        btnFoodOut.Visible = true;
                        lblBtnFoodOut.Visible = false;
                        lblBtnFoodIn.Visible = true;
                        btnFoodIn.Visible = false;
                        searchFoodInDiv.Visible = true;
                        searchFoodOutDiv.Visible = false;
                        lblResponse.Visible = false;
                        lblResponse.Text = "";
                        Session["Searching"] = "";

                        sortingColumn = "Category"; // default sort
                        sortAscending = true;

                        ViewState["sortingColumn"] = sortingColumn;
                        ViewState["sortAscending"] = sortAscending;

                        bindFoodInGridView();
                    }
                    catch (System.Threading.ThreadAbortException) { }
                    catch (Exception ex)
                    {
                        LogError.logError(ex);
                        Response.Redirect("../errorpages/error.aspx");
                    }
                }
                if (Request.QueryString["do"].Equals("FO"))
                {
                    try
                    {
                        if (chkUSDA.Checked == false)
                        {
                            txtSearchFoodOut.Visible = true;
                            txtSearchUSDAFoodOut.Visible = false;
                        }
                        else
                        {
                            txtSearchFoodOut.Visible = false;
                            txtSearchUSDAFoodOut.Visible = true;
                        }
                        searchFoodOutDiv.DefaultButton = "btnSearchFoodOut";
                        lblFoodOutError.Text = "";
                        foodInDiv.Visible = false;
                        foodOutDiv.Visible = true;
                        btnFoodOut.Visible = false;
                        lblBtnFoodOut.Visible = true;
                        lblBtnFoodIn.Visible = false;
                        btnFoodIn.Visible = true;
                        searchFoodInDiv.Visible = false;
                        searchFoodOutDiv.Visible = true;
                        lblResponse.Visible = false;
                        lblResponse.Text = "";
                        Session["Searching"] = "";

                        sortingColumn = "Category"; // default sort
                        sortAscending = true;

                        ViewState["sortingColumn"] = sortingColumn;
                        ViewState["sortAscending"] = sortAscending;

                        bindFoodOutGridView();
                    }
                    catch (System.Threading.ThreadAbortException) { }
                    catch (Exception ex)
                    {
                        LogError.logError(ex);
                        Response.Redirect("../errorpages/error.aspx");
                    }
                }
            }
            else
            {
                // By default, Food In is loaded.
                try
                {
                    if (chkUSDAin.Checked == false)
                    {
                        txtSearchFoodIn.Visible = true;
                        txtSearchUSDAFoodIn.Visible = false;
                    }
                    else
                    {
                        txtSearchFoodIn.Visible = false;
                        txtSearchUSDAFoodIn.Visible = true;
                    }
                    searchFoodInDiv.DefaultButton = "btnSearchFoodIn";
                    lblFoodInError.Text = "";
                    foodInDiv.Visible = true;
                    foodOutDiv.Visible = false;
                    btnFoodOut.Visible = true;
                    lblBtnFoodOut.Visible = false;
                    lblBtnFoodIn.Visible = true;
                    btnFoodIn.Visible = false;
                    searchFoodInDiv.Visible = true;
                    searchFoodOutDiv.Visible = false;
                    lblResponse.Visible = false;
                    lblResponse.Text = "";
                    Session["Searching"] = "";

                    sortingColumn = "Category"; // default sort
                    sortAscending = true;

                    ViewState["sortingColumn"] = sortingColumn;
                    ViewState["sortAscending"] = sortAscending;

                    bindFoodInGridView();
                }
                catch (System.Threading.ThreadAbortException) { }
                catch (Exception ex)
                {
                    LogError.logError(ex);
                    Response.Redirect("../errorpages/error.aspx");
                }
            }
        }
        else 
        {
            sortingColumn = (String)ViewState["sortingColumn"];
            sortAscending = (Boolean)ViewState["sortAscending"];
        }
    }

    // @author: Anthony Dietrich - row functions
    // Selects the row the user pressed the button on and performs
    // the corresponding (CommandName) action/code to that button.
    protected void grdFoodInData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "editData")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdFoodInData.Rows[index];

                Label temp = row.FindControl("lblFoodInID") as Label;
                if (temp != null) { Session["editWhat"] = temp.Text; }

                Response.Redirect("editfood.aspx?do=i");
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }

    // @author: Anthony Dietrich - row functions
    // Selects the row the user pressed the button on and performs
    // the corresponding (CommandName) action/code to that button.
    protected void grdFoodOutData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "editData")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdFoodOutData.Rows[index];

                Label temp = row.FindControl("lblDistributionID") as Label;
                if (temp != null) { Session["editWhat"] = temp.Text; }

                Response.Redirect("editfood.aspx?do=o");
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }

    // @author: Anthony Dietrich - delete row/user function
    // Selects the row the user pressed the button on, deletes the row,
    // and sends the corresponding userID/index data to the deleteUser method.
    protected void grdFoodInData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            GridViewRow row = grdFoodInData.Rows[index];
            String sDelFoodIn = "";
            Label temp = row.FindControl("lblFoodInID") as Label;
            if (temp != null) { sDelFoodIn = temp.Text; }

            short deleteWhat = short.Parse(sDelFoodIn.ToString());
            deleteFoodIn(deleteWhat);
            bindFoodInGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich - delete row/user function
    // Selects the row the user pressed the button on, deletes the row,
    // and sends the corresponding userID/index data to the deleteUser method.
    protected void grdFoodOutData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            GridViewRow row = grdFoodOutData.Rows[index];
            String sDelFoodOut = "";
            Label temp = row.FindControl("lblDistributionID") as Label;
            if (temp != null) { sDelFoodOut = temp.Text; }

            short deleteWhat = short.Parse(sDelFoodOut.ToString());
            deleteFoodOut(deleteWhat);
            bindFoodOutGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich - deleteFoodIn()
    // Deletes the FoodIn table data given the FoodInID.
    protected void deleteFoodIn(short delFoodIn)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                db.FoodIns.Remove(db.FoodIns.Single(x => x.FoodInID == delFoodIn));
                db.SaveChanges();
                Response.Redirect("managedata.aspx");
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich - deleteFoodOut()
    // Deletes the FoodOut table data given the DistributionID.
    protected void deleteFoodOut(short delFoodOut)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                db.FoodOuts.Remove(db.FoodOuts.Single(x => x.DistributionID == delFoodOut));
                db.SaveChanges();
                Response.Redirect("managedata.aspx");
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich
    // Paging the FoodIn gridView
    protected void grdFoodInData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdFoodInData.PageIndex = e.NewPageIndex;
            if (Session["Searching"].Equals(""))
                bindFoodInGridView();
            else
                updateFoodInGrid();

            lblResponse.Visible = false;
            lblResponse.Text = "";
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich
    //Paging the FoodOut gridView
    protected void grdFoodOutData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdFoodOutData.PageIndex = e.NewPageIndex;
            if (Session["Searching"].Equals(""))
                bindFoodOutGridView();
            else
                updateFoodOutGrid();

            lblResponse.Visible = false;
            lblResponse.Text = "";
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich - bindGridView()
    // Queries the database and returns specified fields from FoodIns.
    // Binds that data to the html gridview.
    private void bindFoodInGridView()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                if (chkUSDAin.Checked == true)
                {
                    var allFoodInQuery = (from c in db.FoodIns
                                          where c.USDAID != null
                                          select new
                                          {
                                              c.FoodInID,
                                              c.USDACategory.Description,
                                              c.Weight,
                                              c.FoodSource.Source,
                                              c.TimeStamp
                                          }).ToList();

                    // sort list according to user choice
                    if (sortingColumn.Equals("Category")) // if user wants to sort by Category
                    {
                        if (sortAscending)
                            allFoodInQuery.Sort((x, y) => String.Compare(x.Description, y.Description)); // ascending Category
                        else
                            allFoodInQuery.Sort((x, y) => String.Compare(y.Description, x.Description)); // descending Category
                    }

                    if (sortingColumn.Equals("Weight")) // if user wants to sort by Weight
                    {
                        if (sortAscending)
                            allFoodInQuery.Sort((x, y) => String.Compare(x.Weight.ToString(), y.Weight.ToString())); // ascending weight
                        else
                            allFoodInQuery.Sort((x, y) => String.Compare(y.Weight.ToString(), x.Weight.ToString())); // descending weight
                    }

                    if (sortingColumn.Equals("Source")) // if user wants to sort by Source (donor)
                    {
                        if (sortAscending)
                            allFoodInQuery.Sort((x, y) => String.Compare(x.Source, y.Source)); // ascending Source (donor)
                        else
                            allFoodInQuery.Sort((x, y) => String.Compare(y.Source, x.Source)); // descending Source (donor)
                    }

                    if (sortingColumn.Equals("Timestamp")) // if user wants to sort by date (timestamp)
                    {
                        if (sortAscending)
                            allFoodInQuery.Sort((x, y) => String.Compare(x.TimeStamp.ToString(), y.TimeStamp.ToString())); // ascending date (timestamp)
                        else
                            allFoodInQuery.Sort((x, y) => String.Compare(y.TimeStamp.ToString(), x.TimeStamp.ToString())); // descending date (timestamp)
                    }
                    // end sort list according to user choice

                    DataTable dtFoodResults = new DataTable();
                    dtFoodResults.Columns.Add("FoodInID");
                    dtFoodResults.Columns.Add("Types");
                    dtFoodResults.Columns.Add("Weight");
                    dtFoodResults.Columns.Add("Source");
                    dtFoodResults.Columns.Add("TimeStamp");

                    for (int i = 0; i < allFoodInQuery.Count; i++)
                    {

                        dtFoodResults.Rows.Add(allFoodInQuery.ElementAt(i).FoodInID,
                            allFoodInQuery.ElementAt(i).Description, allFoodInQuery.ElementAt(i).Weight,
                            allFoodInQuery.ElementAt(i).Source, allFoodInQuery.ElementAt(i).TimeStamp);

                    }

                    grdFoodInData.DataSource = dtFoodResults;
                    grdFoodInData.DataBind();
                }

                if (chkUSDAin.Checked == false)
                {
                    var allFoodInQuery = (from c in db.FoodIns
                                          where c.FoodCategoryID != null
                                          select new
                                          {
                                              c.FoodInID,
                                              c.FoodCategory.CategoryType,
                                              c.Weight,
                                              c.FoodSource.Source,
                                              c.TimeStamp
                                          }).ToList();

                    // sort list according to user choice
                    if (sortingColumn.Equals("Category")) // if user wants to sort by Category
                    {
                        if (sortAscending)
                            allFoodInQuery.Sort((x, y) => String.Compare(x.CategoryType, y.CategoryType)); // ascending Category
                        else
                            allFoodInQuery.Sort((x, y) => String.Compare(y.CategoryType, x.CategoryType)); // descending Category
                    }

                    if (sortingColumn.Equals("Weight")) // if user wants to sort by Weight
                    {
                        if (sortAscending)
                            allFoodInQuery.Sort((x, y) => String.Compare(x.Weight.ToString(), y.Weight.ToString())); // ascending weight
                        else
                            allFoodInQuery.Sort((x, y) => String.Compare(y.Weight.ToString(), x.Weight.ToString())); // descending weight
                    }

                    if (sortingColumn.Equals("Source")) // if user wants to sort by Source (donor)
                    {
                        if (sortAscending)
                            allFoodInQuery.Sort((x, y) => String.Compare(x.Source, y.Source)); // ascending Source (donor)
                        else
                            allFoodInQuery.Sort((x, y) => String.Compare(y.Source, x.Source)); // descending Source (donor)
                    }

                    if (sortingColumn.Equals("Timestamp")) // if user wants to sort by date (timestamp)
                    {
                        if (sortAscending)
                            allFoodInQuery.Sort((x, y) => String.Compare(x.TimeStamp.ToString(), y.TimeStamp.ToString())); // ascending date (timestamp)
                        else
                            allFoodInQuery.Sort((x, y) => String.Compare(y.TimeStamp.ToString(), x.TimeStamp.ToString())); // descending date (timestamp)
                    }
                    // end sort list according to user choice

                    DataTable dtFoodResults = new DataTable();
                    dtFoodResults.Columns.Add("FoodInID");
                    dtFoodResults.Columns.Add("Types");
                    dtFoodResults.Columns.Add("Weight");
                    dtFoodResults.Columns.Add("Source");
                    dtFoodResults.Columns.Add("TimeStamp");

                    for (int i = 0; i < allFoodInQuery.Count; i++)
                    {

                        dtFoodResults.Rows.Add(allFoodInQuery.ElementAt(i).FoodInID,
                            allFoodInQuery.ElementAt(i).CategoryType, allFoodInQuery.ElementAt(i).Weight,
                            allFoodInQuery.ElementAt(i).Source, allFoodInQuery.ElementAt(i).TimeStamp);

                    }

                    grdFoodInData.DataSource = dtFoodResults;
                    grdFoodInData.DataBind();

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

    // @author: Anthony Dietrich - bindGridView()
    // Queries the database and returns specified fields from FoodOuts.
    // Binds that data to the html gridview.
    private void bindFoodOutGridView()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                if (chkUSDA.Checked == true)
                {
                    var allFoodOutQuery = (from c in db.FoodOuts
                                           where c.USDAID != null
                                           select new 
                                           { 
                                               c.DistributionID, 
                                               c.USDACategory.Description,
                                               c.Weight, 
                                               c.Count, 
                                               c.TimeStamp 
                                           }).ToList();

                    // sort list according to user choice
                    if (sortingColumn.Equals("Category")) // if user wants to sort by Category
                    {
                        if (sortAscending)
                            allFoodOutQuery.Sort((x, y) => String.Compare(x.Description, y.Description)); // ascending Category
                        else
                            allFoodOutQuery.Sort((x, y) => String.Compare(y.Description, x.Description)); // descending Category
                    }

                    if (sortingColumn.Equals("Weight")) // if user wants to sort by Weight
                    {
                        if (sortAscending)
                            allFoodOutQuery.Sort((x, y) => String.Compare(x.Weight.ToString(), y.Weight.ToString())); // ascending weight
                        else
                            allFoodOutQuery.Sort((x, y) => String.Compare(y.Weight.ToString(), x.Weight.ToString())); // descending weight
                    }

                    if (sortingColumn.Equals("Count")) // if user wants to sort by Count
                    {
                        if (sortAscending)
                            allFoodOutQuery.Sort((x, y) => String.Compare(x.Count.ToString(), y.Count.ToString())); // ascending Count
                        else
                            allFoodOutQuery.Sort((x, y) => String.Compare(y.Count.ToString(), x.Count.ToString())); // descending Count
                    }

                    if (sortingColumn.Equals("Timestamp")) // if user wants to sort by date (timestamp)
                    {
                        if (sortAscending)
                            allFoodOutQuery.Sort((x, y) => String.Compare(x.TimeStamp.ToString(), y.TimeStamp.ToString())); // ascending date (timestamp)
                        else
                            allFoodOutQuery.Sort((x, y) => String.Compare(y.TimeStamp.ToString(), x.TimeStamp.ToString())); // descending date (timestamp)
                    }
                    // end sort list according to user choice

                    DataTable dtFoodResults = new DataTable();
                    dtFoodResults.Columns.Add("DistributionID");
                    dtFoodResults.Columns.Add("Types");
                    dtFoodResults.Columns.Add("Weight");
                    dtFoodResults.Columns.Add("Count");
                    dtFoodResults.Columns.Add("TimeStamp");

                    for (int i = 0; i < allFoodOutQuery.Count; i++)
                    {

                        dtFoodResults.Rows.Add(allFoodOutQuery.ElementAt(i).DistributionID,
                            allFoodOutQuery.ElementAt(i).Description, allFoodOutQuery.ElementAt(i).Weight,
                            allFoodOutQuery.ElementAt(i).Count, allFoodOutQuery.ElementAt(i).TimeStamp);

                    }

                    grdFoodOutData.DataSource = dtFoodResults;
                    grdFoodOutData.DataBind();
                }

                if (chkUSDA.Checked == false)
                {

                    var allFoodOutQuery = (from c in db.FoodOuts
                                           where c.FoodCategoryID != null
                                           select new 
                                           { 
                                               c.DistributionID, 
                                               c.FoodCategory.CategoryType,
                                               c.Weight, 
                                               c.Count, 
                                               c.TimeStamp 
                                           }).ToList();

                    DataTable dtFoodResults = new DataTable();
                    dtFoodResults.Columns.Add("DistributionID");
                    dtFoodResults.Columns.Add("Types");
                    dtFoodResults.Columns.Add("Weight");
                    dtFoodResults.Columns.Add("Count");
                    dtFoodResults.Columns.Add("TimeStamp");

                    for (int i = 0; i < allFoodOutQuery.Count; i++)
                    {

                        dtFoodResults.Rows.Add(allFoodOutQuery.ElementAt(i).DistributionID,
                            allFoodOutQuery.ElementAt(i).CategoryType, allFoodOutQuery.ElementAt(i).Weight, 
                            allFoodOutQuery.ElementAt(i).Count, allFoodOutQuery.ElementAt(i).TimeStamp);

                    }

                    grdFoodOutData.DataSource = dtFoodResults;
                    grdFoodOutData.DataBind();

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

    // @author: Anthony Dietrich - toggleUSDAout()
    // Toggles the gridView between USDA and non-USDA items.
    protected void toggleUSDAout(object sender, EventArgs e)
    {
        if (chkUSDA.Checked == true)
        {
            txtSearchFoodOut.Visible = false;
            txtSearchUSDAFoodOut.Visible = true;
            lblResponse.Visible = true;
            lblResponse.Text = "Showing USDA items...";
            bindFoodOutGridView();
        }
        if (chkUSDA.Checked == false)
        {
            txtSearchFoodOut.Visible = true;
            txtSearchUSDAFoodOut.Visible = false;
            lblResponse.Visible = true;
            lblResponse.Text = "Showing NON-USDA items...";
            bindFoodOutGridView();
        }
    }

    // @author: Anthony Dietrich - toggleUSDAin()
    // Toggles the gridView between USDA and non-USDA items.
    protected void toggleUSDAin(object sender, EventArgs e)
    {
        if (chkUSDAin.Checked == true)
        {
            txtSearchFoodIn.Visible = false;
            txtSearchUSDAFoodIn.Visible = true;
            lblResponse.Visible = true;
            lblResponse.Text = "Showing USDA items...";
            bindFoodInGridView();
        }
        if (chkUSDAin.Checked == false)
        {
            txtSearchFoodIn.Visible = true;
            txtSearchUSDAFoodIn.Visible = false;
            lblResponse.Visible = true;
            lblResponse.Text = "Showing NON-USDA items...";
            bindFoodInGridView();
        }
    }

    // @author: Anthony Dietrich
    // Simple button click toggle to swap Food In/Food Out views
    protected void btnFoodIn_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkUSDAin.Checked == false)
            {
                txtSearchFoodIn.Visible = true;
                txtSearchUSDAFoodIn.Visible = false;
            }
            else
            {
                txtSearchFoodIn.Visible = false;
                txtSearchUSDAFoodIn.Visible = true;
            }
            searchFoodInDiv.DefaultButton = "btnSearchFoodIn";
            lblFoodInError.Text = "";
            bindFoodInGridView();
            foodInDiv.Visible = true;
            foodOutDiv.Visible = false;
            btnFoodOut.Visible = true;
            lblBtnFoodOut.Visible = false;
            lblBtnFoodIn.Visible = true;
            btnFoodIn.Visible = false;
            searchFoodInDiv.Visible = true;
            searchFoodOutDiv.Visible = false;
            lblResponse.Visible = false;
            lblResponse.Text = "";
            Session["Searching"] = "";
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich
    // Simple button click toggle to swap Food In/Food Out views
    protected void btnFoodOut_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkUSDA.Checked == false)
            {
                txtSearchFoodOut.Visible = true;
                txtSearchUSDAFoodOut.Visible = false;
            }
            else
            {
                txtSearchFoodOut.Visible = false;
                txtSearchUSDAFoodOut.Visible = true;
            }
            searchFoodOutDiv.DefaultButton = "btnSearchFoodOut";
            lblFoodOutError.Text = "";
            bindFoodOutGridView();
            foodInDiv.Visible = false;
            foodOutDiv.Visible = true;
            btnFoodOut.Visible = false;
            lblBtnFoodOut.Visible = true;
            lblBtnFoodIn.Visible = false;
            btnFoodIn.Visible = true;
            searchFoodInDiv.Visible = false;
            searchFoodOutDiv.Visible = true;
            lblResponse.Visible = false;
            lblResponse.Text = "";
            Session["Searching"] = "";
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich
    // Search Food In button click method.
    protected void btnSearchFoodIn_Click(object sender, EventArgs e)
    {
        if (chkUSDAin.Checked == true)
        {
            if (txtSearchUSDAFoodIn.Text.Equals(""))
            {
                lblResponse.Visible = true;
                lblResponse.Text = "Please enter a search term.";
            }
            else
            {
                lblResponse.Visible = true;
                lblResponse.Text = "Searching for " + txtSearchUSDAFoodIn.Text + " by " + ddlSearchFInBy.SelectedValue.ToString() + "...";
                searchFoodInDiv.DefaultButton = "btnSearchFoodIn";
                Session["Searching"] = txtSearchUSDAFoodIn.Text;
                Session["isSearching"] = true;
                updateFoodInGrid();
            }
        }
        if (chkUSDAin.Checked == false)
        {
            if (txtSearchFoodIn.Text.Equals(""))
            {
                lblResponse.Visible = true;
                lblResponse.Text = "Please enter a search term.";
            }
            else
            {
                lblResponse.Visible = true;
                lblResponse.Text = "Searching for " + txtSearchFoodIn.Text + " by " + ddlSearchFInBy.SelectedValue.ToString() + "...";
                searchFoodInDiv.DefaultButton = "btnSearchFoodIn";
                Session["Searching"] = txtSearchFoodIn.Text;
                Session["isSearching"] = true;
                updateFoodInGrid();
            }
        }
    }

    // @author: Anthony Dietrich
    // Search Food Out button click method.
    protected void btnSearchFoodOut_Click(object sender, EventArgs e)
    {
        if (chkUSDA.Checked == true)
        {
            if (txtSearchUSDAFoodOut.Text.Equals(""))
            {
                lblResponse.Visible = true;
                lblResponse.Text = "Please enter a search term.";
            }
            else
            {
                lblResponse.Visible = true;
                lblResponse.Text = "Searching for " + txtSearchUSDAFoodOut.Text + " by " + ddlSearchFOutBy.SelectedValue.ToString() + "...";
                searchFoodOutDiv.DefaultButton = "btnSearchFoodOut";
                Session["Searching"] = txtSearchUSDAFoodOut.Text;
                Session["isSearching"] = true;
                updateFoodOutGrid();
            }
        }
        if (chkUSDA.Checked == false)
        {
            if (txtSearchFoodOut.Text.Equals(""))
            {
                lblResponse.Visible = true;
                lblResponse.Text = "Please enter a search term.";
            }
            else
            {
                lblResponse.Visible = true;
                lblResponse.Text = "Searching for " + txtSearchFoodOut.Text + " by " + ddlSearchFOutBy.SelectedValue.ToString() + "...";
                searchFoodOutDiv.DefaultButton = "btnSearchFoodOut";
                Session["Searching"] = txtSearchFoodOut.Text;
                Session["isSearching"] = true;
                updateFoodOutGrid();
            }
        }
    }

    // @author: Anthony Dietrich
    // Reset Food In button click method.
    protected void btnResetFoodIn_Click(object sender, EventArgs e)
    {
        txtSearchFoodIn.Text = "";
        Session["Searching"] = "";
        Session["isSearching"] = false;
        lblResponse.Visible = true;
        lblResponse.Text = "Resetting Food In Table...";
        grdFoodInData.PageIndex = 0;
        bindFoodInGridView();
    }

    // @author: Anthony Dietrich
    // Reset Food Out button click method.
    protected void btnResetFoodOut_Click(object sender, EventArgs e)
    {
        txtSearchFoodOut.Text = "";
        txtSearchUSDAFoodOut.Text = "";
        Session["Searching"] = "";
        Session["isSearching"] = false;
        lblResponse.Visible = true;
        lblResponse.Text = "Resetting Food Out Table...";
        grdFoodOutData.PageIndex = 0;
        bindFoodOutGridView();
    }

    // @author: Anthony Dietrich
    // Update gridView with search term
    private void updateFoodInGrid()
    {
        try
        {
            if (Session["isSearching"].Equals(true))
                grdFoodInData.PageIndex = 0;
            string sTerm = Session["Searching"].ToString();
            using (CCSEntities db = new CCSEntities())
            {
                //********************** USDA SEARCHING ***************************//
                if (chkUSDAin.Checked == true)
                {
                    //********************** USDA SEARCHING BY CATEGORY ***************************//
                    if (ddlSearchFInBy.SelectedValue.Equals("Category"))
                    {
                        var listFood = (from c in db.FoodIns.OrderBy(x => x.USDAID)
                                        where c.USDACategory.Description.ToLower().Contains(sTerm.ToLower()) && c.USDAID != null
                                        select new
                                        {
                                            c.FoodInID,
                                            c.USDACategory.Description,
                                            c.Weight,
                                            c.FoodSource.Source,
                                            c.TimeStamp
                                        }).ToList();

                        DataTable dtFoodResults = new DataTable();
                        dtFoodResults.Columns.Add("FoodInID");
                        dtFoodResults.Columns.Add("Types");
                        dtFoodResults.Columns.Add("Weight");
                        dtFoodResults.Columns.Add("Source");
                        dtFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listFood.Count; i++)
                        {

                            dtFoodResults.Rows.Add(listFood.ElementAt(i).FoodInID,
                                listFood.ElementAt(i).Description, listFood.ElementAt(i).Weight,
                                listFood.ElementAt(i).Source, listFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodInData.DataSource = dtFoodResults;
                        grdFoodInData.DataBind();

                        if (dtFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchUSDAFoodIn.Text + " cannot be located.";
                        }
                        txtSearchUSDAFoodIn.Text = "";
                        Session["isSearching"] = false;
                    }
                    //********************** USDA SEARCHING BY DATE ***************************//
                    if (ddlSearchFInBy.SelectedValue.Equals("Date"))
                    {
                        if (!sTerm.Contains("/"))
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = "Invalid date! Proper formats: MM/DD, MM/DD/YYYY";
                        }
                        else
                        {
                            DateTime dt = DateTime.Parse(sTerm);

                            var listFood = (from c in db.FoodIns.OrderBy(x => x.TimeStamp)
                                            where c.TimeStamp.Equals(dt.Date) && c.USDAID != null
                                            select new
                                            {
                                                c.FoodInID,
                                                c.USDACategory.Description,
                                                c.Weight,
                                                c.FoodSource.Source,
                                                c.TimeStamp
                                            }).ToList();

                            DataTable dtFoodResults = new DataTable();
                            dtFoodResults.Columns.Add("FoodInID");
                            dtFoodResults.Columns.Add("Types");
                            dtFoodResults.Columns.Add("Weight");
                            dtFoodResults.Columns.Add("Source");
                            dtFoodResults.Columns.Add("TimeStamp");

                            for (int i = 0; i < listFood.Count; i++)
                            {

                                dtFoodResults.Rows.Add(listFood.ElementAt(i).FoodInID,
                                    listFood.ElementAt(i).Description, listFood.ElementAt(i).Weight,
                                    listFood.ElementAt(i).Source, listFood.ElementAt(i).TimeStamp);

                            }

                            grdFoodInData.DataSource = dtFoodResults;
                            grdFoodInData.DataBind();

                            if (dtFoodResults.Rows.Count <= 0)
                            {
                                lblResponse.Visible = true;
                                lblResponse.Text = txtSearchUSDAFoodIn.Text + " cannot be located.";
                            }
                            txtSearchUSDAFoodIn.Text = "";
                            Session["isSearching"] = false;
                        }
                    }
                    //********************** USDA SEARCHING BY DONOR ***************************//
                    if (ddlSearchFInBy.SelectedValue.Equals("Donor"))
                    {
                        var listFood = (from c in db.FoodIns.OrderBy(x => x.FoodSourceID)
                                        where c.FoodSource.Source.ToLower().Contains(sTerm.ToLower()) && c.USDAID != null
                                        select new
                                        {
                                            c.FoodInID,
                                            c.USDACategory.Description,
                                            c.Weight,
                                            c.FoodSource.Source,
                                            c.TimeStamp
                                        }).ToList();
                      
                        DataTable dtFoodResults = new DataTable();
                        dtFoodResults.Columns.Add("FoodInID");
                        dtFoodResults.Columns.Add("Types");
                        dtFoodResults.Columns.Add("Weight");
                        dtFoodResults.Columns.Add("Source");
                        dtFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listFood.Count; i++)
                        {

                            dtFoodResults.Rows.Add(listFood.ElementAt(i).FoodInID,
                                listFood.ElementAt(i).Description, listFood.ElementAt(i).Weight,
                                listFood.ElementAt(i).Source, listFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodInData.DataSource = dtFoodResults;
                        grdFoodInData.DataBind();

                        if (dtFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchUSDAFoodIn.Text + " cannot be located.";
                        }
                        txtSearchUSDAFoodIn.Text = "";
                        Session["isSearching"] = false;
                    }
                    //********************** USDA SEARCHING BY Weight ***************************//
                    if (ddlSearchFInBy.SelectedValue.Equals("Weight"))
                    {
                        Decimal d = Decimal.Parse(sTerm);
                        var listFood = (from c in db.FoodIns.OrderBy(x => x.Weight)
                                        where c.Weight.Equals(d) && c.USDAID != null
                                        select new
                                        {
                                            c.FoodInID,
                                            c.USDACategory.Description,
                                            c.Weight,
                                            c.FoodSource.Source,
                                            c.TimeStamp
                                        }).ToList();

                        DataTable dtFoodResults = new DataTable();
                        dtFoodResults.Columns.Add("FoodInID");
                        dtFoodResults.Columns.Add("Types");
                        dtFoodResults.Columns.Add("Weight");
                        dtFoodResults.Columns.Add("Source");
                        dtFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listFood.Count; i++)
                        {

                            dtFoodResults.Rows.Add(listFood.ElementAt(i).FoodInID,
                                listFood.ElementAt(i).Description, listFood.ElementAt(i).Weight,
                                listFood.ElementAt(i).Source, listFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodInData.DataSource = dtFoodResults;
                        grdFoodInData.DataBind();

                        if (dtFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchUSDAFoodIn.Text + " cannot be located.";
                        }
                        txtSearchUSDAFoodIn.Text = "";
                        Session["isSearching"] = false;
                    }

                    
                }
                //********************** FOOD CATEGORY (NON-USDA) SEARCHING ***************************//
                if (chkUSDAin.Checked == false)
                {
                    //********************** FOOD CATEGORY (NON-USDA) SEARCHING BY CATEGORY ***************************//
                    if (ddlSearchFInBy.SelectedValue.Equals("Category"))
                    {
                        var listFood = (from c in db.FoodIns.OrderBy(x => x.FoodCategoryID)
                                        where c.FoodCategory.CategoryType.ToLower().Contains(sTerm.ToLower()) && c.FoodCategoryID != null
                                        select new
                                        {
                                            c.FoodInID,
                                            c.FoodCategory.CategoryType,
                                            c.Weight,
                                            c.FoodSource.Source,
                                            c.TimeStamp
                                        }).ToList();

                        DataTable dtFoodResults = new DataTable();
                        dtFoodResults.Columns.Add("FoodInID");
                        dtFoodResults.Columns.Add("Types");
                        dtFoodResults.Columns.Add("Weight");
                        dtFoodResults.Columns.Add("Source");
                        dtFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listFood.Count; i++)
                        {

                            dtFoodResults.Rows.Add(listFood.ElementAt(i).FoodInID,
                                listFood.ElementAt(i).CategoryType, listFood.ElementAt(i).Weight,
                                listFood.ElementAt(i).Source, listFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodInData.DataSource = dtFoodResults;
                        grdFoodInData.DataBind();

                        if (dtFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchFoodIn.Text + " cannot be located.";
                        }
                        txtSearchFoodIn.Text = "";
                        Session["isSearching"] = false;
                    }
                    //********************** FOOD CATEGORY (NON-USDA) SEARCHING BY DATE ***************************//
                    if (ddlSearchFInBy.SelectedValue.Equals("Date"))
                    {
                        if (!sTerm.Contains("/"))
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = "Invalid date! Proper formats: MM/DD, MM/DD/YYYY";
                        }
                        else
                        {
                            DateTime dt = DateTime.Parse(sTerm);

                            var listFood = (from c in db.FoodIns.OrderBy(x => x.TimeStamp)
                                            where c.TimeStamp.Equals(dt.Date) && c.FoodCategoryID != null
                                            select new
                                            {
                                                c.FoodInID,
                                                c.FoodCategory.CategoryType,
                                                c.Weight,
                                                c.FoodSource.Source,
                                                c.TimeStamp
                                            }).ToList();

                            DataTable dtFoodResults = new DataTable();
                            dtFoodResults.Columns.Add("FoodInID");
                            dtFoodResults.Columns.Add("Types");
                            dtFoodResults.Columns.Add("Weight");
                            dtFoodResults.Columns.Add("Source");
                            dtFoodResults.Columns.Add("TimeStamp");

                            for (int i = 0; i < listFood.Count; i++)
                            {

                                dtFoodResults.Rows.Add(listFood.ElementAt(i).FoodInID,
                                    listFood.ElementAt(i).CategoryType, listFood.ElementAt(i).Weight,
                                    listFood.ElementAt(i).Source, listFood.ElementAt(i).TimeStamp);

                            }

                            grdFoodInData.DataSource = dtFoodResults;
                            grdFoodInData.DataBind();

                            if (dtFoodResults.Rows.Count <= 0)
                            {
                                lblResponse.Visible = true;
                                lblResponse.Text = txtSearchFoodIn.Text + " cannot be located.";
                            }
                            txtSearchFoodIn.Text = "";
                            Session["isSearching"] = false;
                        }
                    }
                    //********************** FOOD CATEGORY (NON-USDA) SEARCHING BY DONOR ***************************//
                    if (ddlSearchFInBy.SelectedValue.Equals("Donor"))
                    {

                        var listFood = (from c in db.FoodIns.OrderBy(x => x.FoodSourceID)
                                        where c.FoodSource.Source.ToLower().Contains(sTerm.ToLower()) && c.FoodCategoryID != null
                                        select new
                                        {
                                            c.FoodInID,
                                            c.FoodCategory.CategoryType,
                                            c.Weight,
                                            c.FoodSource.Source,
                                            c.TimeStamp
                                        }).ToList();
                       
                        DataTable dtFoodResults = new DataTable();
                        dtFoodResults.Columns.Add("FoodInID");
                        dtFoodResults.Columns.Add("Types");
                        dtFoodResults.Columns.Add("Weight");
                        dtFoodResults.Columns.Add("Source");
                        dtFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listFood.Count; i++)
                        {

                            dtFoodResults.Rows.Add(listFood.ElementAt(i).FoodInID,
                                listFood.ElementAt(i).CategoryType, listFood.ElementAt(i).Weight,
                                listFood.ElementAt(i).Source, listFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodInData.DataSource = dtFoodResults;
                        grdFoodInData.DataBind();

                        if (dtFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchFoodIn.Text + " cannot be located.";
                        }
                        txtSearchFoodIn.Text = "";
                        Session["isSearching"] = false;
                    }
                    //********************** FOOD CATEGORY (NON-USDA) SEARCHING BY WEIGHT ***************************//
                    if (ddlSearchFInBy.SelectedValue.Equals("Weight"))
                    {
                        Decimal d = Decimal.Parse(sTerm);

                        var listFood = (from c in db.FoodIns.OrderBy(x => x.Weight)
                                        where c.Weight.Equals(d) && c.FoodCategoryID != null
                                        select new
                                        {
                                            c.FoodInID,
                                            c.FoodCategory.CategoryType,
                                            c.Weight,
                                            c.FoodSource.Source,
                                            c.TimeStamp
                                        }).ToList();

                        DataTable dtFoodResults = new DataTable();
                        dtFoodResults.Columns.Add("FoodInID");
                        dtFoodResults.Columns.Add("Types");
                        dtFoodResults.Columns.Add("Weight");
                        dtFoodResults.Columns.Add("Source");
                        dtFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listFood.Count; i++)
                        {

                            dtFoodResults.Rows.Add(listFood.ElementAt(i).FoodInID,
                                listFood.ElementAt(i).CategoryType, listFood.ElementAt(i).Weight,
                                listFood.ElementAt(i).Source, listFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodInData.DataSource = dtFoodResults;
                        grdFoodInData.DataBind();

                        if (dtFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchFoodIn.Text + " cannot be located.";
                        }
                        txtSearchFoodIn.Text = "";
                        Session["isSearching"] = false;

                    }

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

    // @author: Anthony Dietrich
    // Update gridView with search term
    private void updateFoodOutGrid()
    {
        try
        {
            if (Session["isSearching"].Equals(true))
                grdFoodOutData.PageIndex = 0;
            string sTerm = Session["Searching"].ToString();
            using (CCSEntities db = new CCSEntities())
            {
                //********************** USDA SEARCHING ***************************//
                if (chkUSDA.Checked == true)
                {
                    //********************** USDA SEARCHING BY CATEGORY ***************************//
                    if (ddlSearchFOutBy.SelectedValue.Equals("Category"))
                    { 
                        var listUSDAFood = (from c in db.FoodOuts.OrderBy(x => x.USDACategory.Description)
                                            where c.USDACategory.Description.ToLower().Contains(sTerm.ToLower()) && c.USDAID != null
                                            select new 
                                            { 
                                                c.DistributionID, 
                                                c.USDACategory.Description, 
                                                c.Weight, 
                                                c.Count, 
                                                c.TimeStamp 
                                            }).ToList();

                        DataTable dtUSDAFoodResults = new DataTable();
                        dtUSDAFoodResults.Columns.Add("DistributionID");
                        dtUSDAFoodResults.Columns.Add("Types");
                        dtUSDAFoodResults.Columns.Add("Weight");
                        dtUSDAFoodResults.Columns.Add("Count");
                        dtUSDAFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listUSDAFood.Count; i++)
                        {

                            dtUSDAFoodResults.Rows.Add(listUSDAFood.ElementAt(i).DistributionID,
                                listUSDAFood.ElementAt(i).Description, listUSDAFood.ElementAt(i).Weight,
                                listUSDAFood.ElementAt(i).Count, listUSDAFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodOutData.DataSource = dtUSDAFoodResults;
                        grdFoodOutData.DataBind();

                        if (dtUSDAFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchUSDAFoodOut.Text + " cannot be located.";
                        }
                        txtSearchUSDAFoodOut.Text = "";
                        Session["isSearching"] = false;
                    }
                    //********************** USDA SEARCHING BY WEIGHT ***************************//
                    if (ddlSearchFOutBy.SelectedValue.Equals("Weight"))
                    {
                        double d = double.Parse(sTerm);

                        var listUSDAFood = (from c in db.FoodOuts.OrderBy(x => x.Weight)
                                            where c.Weight.Equals(d) && c.USDAID != null
                                            select new
                                            {
                                                c.DistributionID,
                                                c.USDACategory.Description,
                                                c.Weight,
                                                c.Count,
                                                c.TimeStamp
                                            }).ToList();

                        DataTable dtUSDAFoodResults = new DataTable();
                        dtUSDAFoodResults.Columns.Add("DistributionID");
                        dtUSDAFoodResults.Columns.Add("Types");
                        dtUSDAFoodResults.Columns.Add("Weight");
                        dtUSDAFoodResults.Columns.Add("Count");
                        dtUSDAFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listUSDAFood.Count; i++)
                        {

                            dtUSDAFoodResults.Rows.Add(listUSDAFood.ElementAt(i).DistributionID,
                                listUSDAFood.ElementAt(i).Description, listUSDAFood.ElementAt(i).Weight,
                                listUSDAFood.ElementAt(i).Count, listUSDAFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodOutData.DataSource = dtUSDAFoodResults;
                        grdFoodOutData.DataBind();

                        if (dtUSDAFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchUSDAFoodOut.Text + " cannot be located.";
                        }
                        txtSearchUSDAFoodOut.Text = "";
                        Session["isSearching"] = false;
                    }
                    //********************** USDA SEARCHING BY COUNT ***************************//
                    if (ddlSearchFOutBy.SelectedValue.Equals("Count"))
                    {
                        short sCount = short.Parse(sTerm);

                        var listUSDAFood = (from c in db.FoodOuts.OrderBy(x => x.Count)
                                            where c.Count == sCount && c.USDAID != null
                                            select new
                                            {
                                                c.DistributionID,
                                                c.USDACategory.Description,
                                                c.Weight,
                                                c.Count,
                                                c.TimeStamp
                                            }).ToList();

                        DataTable dtUSDAFoodResults = new DataTable();
                        dtUSDAFoodResults.Columns.Add("DistributionID");
                        dtUSDAFoodResults.Columns.Add("Types");
                        dtUSDAFoodResults.Columns.Add("Weight");
                        dtUSDAFoodResults.Columns.Add("Count");
                        dtUSDAFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listUSDAFood.Count; i++)
                        {

                            dtUSDAFoodResults.Rows.Add(listUSDAFood.ElementAt(i).DistributionID,
                                listUSDAFood.ElementAt(i).Description, listUSDAFood.ElementAt(i).Weight,
                                listUSDAFood.ElementAt(i).Count, listUSDAFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodOutData.DataSource = dtUSDAFoodResults;
                        grdFoodOutData.DataBind();

                        if (dtUSDAFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchUSDAFoodOut.Text + " cannot be located.";
                        }
                        txtSearchUSDAFoodOut.Text = "";
                        Session["isSearching"] = false;
                    }
                    //********************** USDA SEARCHING BY DATE (TimeStamp) ***************************//
                    if (ddlSearchFOutBy.SelectedValue.Equals("Date"))
                    {
                        if (!sTerm.Contains("/"))
                        { 
                            lblResponse.Visible = true;
                            lblResponse.Text = "Invalid date! Proper formats: MM/DD, MM/DD/YYYY";
                        }
                        else
                        {
                            DateTime dt = DateTime.Parse(sTerm);

                            var listUSDAFood = (from c in db.FoodOuts.OrderBy(x => x.TimeStamp)
                                                where c.TimeStamp.Equals(dt.Date) && c.USDAID != null
                                                select new
                                                {
                                                    c.DistributionID,
                                                    c.USDACategory.Description,
                                                    c.Weight,
                                                    c.Count,
                                                    c.TimeStamp
                                                }).ToList();

                            DataTable dtUSDAFoodResults = new DataTable();
                            dtUSDAFoodResults.Columns.Add("DistributionID");
                            dtUSDAFoodResults.Columns.Add("Types");
                            dtUSDAFoodResults.Columns.Add("Weight");
                            dtUSDAFoodResults.Columns.Add("Count");
                            dtUSDAFoodResults.Columns.Add("TimeStamp");

                            for (int i = 0; i < listUSDAFood.Count; i++)
                            {

                                dtUSDAFoodResults.Rows.Add(listUSDAFood.ElementAt(i).DistributionID,
                                    listUSDAFood.ElementAt(i).Description, listUSDAFood.ElementAt(i).Weight,
                                    listUSDAFood.ElementAt(i).Count, listUSDAFood.ElementAt(i).TimeStamp);

                            }

                            grdFoodOutData.DataSource = dtUSDAFoodResults;
                            grdFoodOutData.DataBind();

                            if (dtUSDAFoodResults.Rows.Count <= 0)
                            {
                                lblResponse.Visible = true;
                                lblResponse.Text = txtSearchUSDAFoodOut.Text + " cannot be located.";
                            }
                            txtSearchUSDAFoodOut.Text = "";
                            Session["isSearching"] = false;
                        }
                    }

                }
                //********************** FOOD CATEGORY (NON-USDA) SEARCHING ***************************//
                if (chkUSDA.Checked == false)
                {
                    //********************** FOOD CATEGORY (NON-USDA) SEARCHING BY CATEGORY ***************************//
                    if (ddlSearchFOutBy.SelectedValue.Equals("Category"))
                    {
                        var listFood = (from c in db.FoodOuts.OrderBy(x => x.FoodCategoryID)
                                        where c.FoodCategory.CategoryType.ToLower().Contains(sTerm.ToLower()) && c.FoodCategoryID != null
                                        select new
                                        {
                                            c.DistributionID,
                                            c.FoodCategory.CategoryType,
                                            c.Weight,
                                            c.Count,
                                            c.TimeStamp
                                        }).ToList();

                        DataTable dtFoodResults = new DataTable();
                        dtFoodResults.Columns.Add("DistributionID");
                        dtFoodResults.Columns.Add("Types");
                        dtFoodResults.Columns.Add("Weight");
                        dtFoodResults.Columns.Add("Count");
                        dtFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listFood.Count; i++)
                        {

                            dtFoodResults.Rows.Add(listFood.ElementAt(i).DistributionID, listFood.ElementAt(i).CategoryType,
                                listFood.ElementAt(i).Weight, listFood.ElementAt(i).Count, listFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodOutData.DataSource = dtFoodResults;
                        grdFoodOutData.DataBind();

                        if (dtFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchFoodOut.Text + " cannot be located.";
                        }
                        txtSearchFoodOut.Text = "";
                        Session["isSearching"] = false;
                    }
                    //********************** FOOD CATEGORY (NON-USDA) SEARCHING BY WEIGHT ***************************//
                    if (ddlSearchFOutBy.SelectedValue.Equals("Weight"))
                    {
                        double d = double.Parse(sTerm);

                        var listFood = (from c in db.FoodOuts.OrderBy(x => x.Weight)
                                        where c.Weight.Equals(d) && c.FoodCategoryID != null
                                        select new
                                        {
                                            c.DistributionID,
                                            c.FoodCategory.CategoryType,
                                            c.Weight,
                                            c.Count,
                                            c.TimeStamp
                                        }).ToList();

                        DataTable dtFoodResults = new DataTable();
                        dtFoodResults.Columns.Add("DistributionID");
                        dtFoodResults.Columns.Add("Types");
                        dtFoodResults.Columns.Add("Weight");
                        dtFoodResults.Columns.Add("Count");
                        dtFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listFood.Count; i++)
                        {

                            dtFoodResults.Rows.Add(listFood.ElementAt(i).DistributionID, listFood.ElementAt(i).CategoryType,
                                listFood.ElementAt(i).Weight, listFood.ElementAt(i).Count, listFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodOutData.DataSource = dtFoodResults;
                        grdFoodOutData.DataBind();

                        if (dtFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchFoodOut.Text + " cannot be located.";
                        }
                        txtSearchFoodOut.Text = "";
                        Session["isSearching"] = false;
                    }
                    //********************** FOOD CATEGORY (NON-USDA) SEARCHING BY COUNT ***************************//
                    if (ddlSearchFOutBy.SelectedValue.Equals("Count"))
                    {
                        short sCount = short.Parse(sTerm);

                        var listFood = (from c in db.FoodOuts.OrderBy(x => x.Count)
                                        where c.Count == sCount && c.FoodCategoryID != null
                                        select new
                                        {
                                            c.DistributionID,
                                            c.FoodCategory.CategoryType,
                                            c.Weight,
                                            c.Count,
                                            c.TimeStamp
                                        }).ToList();

                        DataTable dtFoodResults = new DataTable();
                        dtFoodResults.Columns.Add("DistributionID");
                        dtFoodResults.Columns.Add("Types");
                        dtFoodResults.Columns.Add("Weight");
                        dtFoodResults.Columns.Add("Count");
                        dtFoodResults.Columns.Add("TimeStamp");

                        for (int i = 0; i < listFood.Count; i++)
                        {

                            dtFoodResults.Rows.Add(listFood.ElementAt(i).DistributionID, listFood.ElementAt(i).CategoryType,
                                listFood.ElementAt(i).Weight, listFood.ElementAt(i).Count, listFood.ElementAt(i).TimeStamp);

                        }

                        grdFoodOutData.DataSource = dtFoodResults;
                        grdFoodOutData.DataBind();

                        if (dtFoodResults.Rows.Count <= 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = txtSearchFoodOut.Text + " cannot be located.";
                        }
                        txtSearchFoodOut.Text = "";
                        Session["isSearching"] = false;
                    }
                    //********************** FOOD CATEGORY (NON-USDA) SEARCHING BY DATE ***************************//
                    if (ddlSearchFOutBy.SelectedValue.Equals("Date"))
                    {

                        if (!sTerm.Contains("/"))
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = "Invalid date! Proper formats: MM/DD, MM/DD/YYYY";
                        }
                        else
                        {
                            DateTime dt = DateTime.Parse(sTerm);

                            var listFood = (from c in db.FoodOuts.OrderBy(x => x.TimeStamp)
                                            where c.TimeStamp.Equals(dt.Date) && c.FoodCategoryID != null
                                            select new
                                            {
                                                c.DistributionID,
                                                c.FoodCategory.CategoryType,
                                                c.Weight,
                                                c.Count,
                                                c.TimeStamp
                                            }).ToList();

                            DataTable dtFoodResults = new DataTable();
                            dtFoodResults.Columns.Add("DistributionID");
                            dtFoodResults.Columns.Add("Types");
                            dtFoodResults.Columns.Add("Weight");
                            dtFoodResults.Columns.Add("Count");
                            dtFoodResults.Columns.Add("TimeStamp");

                            for (int i = 0; i < listFood.Count; i++)
                            {

                                dtFoodResults.Rows.Add(listFood.ElementAt(i).DistributionID, listFood.ElementAt(i).CategoryType,
                                    listFood.ElementAt(i).Weight, listFood.ElementAt(i).Count, listFood.ElementAt(i).TimeStamp);

                            }

                            grdFoodOutData.DataSource = dtFoodResults;
                            grdFoodOutData.DataBind();

                            if (dtFoodResults.Rows.Count <= 0)
                            {
                                lblResponse.Visible = true;
                                lblResponse.Text = txtSearchFoodOut.Text + " cannot be located.";
                            }
                            txtSearchFoodOut.Text = "";
                            Session["isSearching"] = false;
                        }
                    }
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

    //****************************** Sorting Food In Grid ***********************************//
    protected void FoodIn_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (sortingColumn != null && sortingColumn.Equals(e.SortExpression))    // if the list is already sorted by this column,                                                                  
            {                                                                       // toggle ascending/descending
                sortAscending = !sortAscending;
            }
            else                                                                    // else, set new column and set sorting to ascending
            {
                sortingColumn = e.SortExpression;
                sortAscending = true;
            }

            ViewState["sortingColumn"] = sortingColumn;
            ViewState["sortAscending"] = sortAscending;

            bindFoodInGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    //****************************** Sorting Food Out Grid ***********************************//
    protected void FoodOut_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (sortingColumn != null && sortingColumn.Equals(e.SortExpression))    // if the list is already sorted by this column,                                                                  
            {                                                                       // toggle ascending/descending
                sortAscending = !sortAscending;
            }
            else                                                                    // else, set new column and set sorting to ascending
            {
                sortingColumn = e.SortExpression;
                sortAscending = true;
            }

            ViewState["sortingColumn"] = sortingColumn;
            ViewState["sortAscending"] = sortAscending;

            bindFoodOutGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
}