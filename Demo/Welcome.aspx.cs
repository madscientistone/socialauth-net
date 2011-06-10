﻿/*
===========================================================================
Copyright (c) 2010 BrickRed Technologies Limited

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sub-license, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
===========================================================================

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Brickred.SocialAuth.NET.Core.BusinessObjects;


public partial class Welcome : System.Web.UI.Page
{
    public string Provider;
    public string Email;
    public string FirstName;
    public string LastName;
    public string DateOfBirth;
    public string Gender;
    public string ProfileURL;
    public string ProfilePicture;
    public string Country;
    public string Language;
    public string ContactsCount;
    public bool IsSTSaware;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!SocialAuthUser.IsLoggedIn())
            Response.Redirect("socialauth/logout.sauth");
        IsSTSaware = HttpContext.Current.ApplicationInstance.IsSTSaware();
        Provider = User.Identity.GetProvider();
        Email = User.Identity.GetProfile().Email;
        FirstName = User.Identity.GetProfile().FirstName;
        LastName = User.Identity.GetProfile().LastName;
        DateOfBirth = User.Identity.GetProfile().DateOfBirth;
        Gender = User.Identity.GetProfile().Gender;
        ProfileURL = User.Identity.GetProfile().ProfileURL;
        ProfilePicture = User.Identity.GetProfile().ProfilePictureURL;
        Country = User.Identity.GetProfile().Country;
        Language= User.Identity.GetProfile().Language;
        bool IsAlternate = false;
        User.Identity.GetContacts().ForEach(
            x =>
            {
                HtmlTableRow tr = new HtmlTableRow();
                tr.Attributes.Add("class", (IsAlternate) ? "dark" : "light");
                tr.Cells.Add(new HtmlTableCell() { InnerText= x.Name });
                tr.Cells.Add(new HtmlTableCell() { InnerText = x.Email });
                tr.Cells.Add(new HtmlTableCell() { InnerText = x.ProfileURL });
                tblContacts.Rows.Add(tr);
                IsAlternate = !IsAlternate;
            }
            
            );
        ContactsCount = (tblContacts.Rows.Count-1).ToString();
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        SocialAuthUser.GetCurrentUser().Logout();
    }
}

