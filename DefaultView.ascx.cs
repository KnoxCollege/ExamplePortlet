using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Jenzabar.Common;
using Jenzabar.JX.Common.WebServices.DataTransferObjects;
using Jenzabar.JX.Common.WebServices.Interfaces;
using Jenzabar.JX.Core;
using Jenzabar.JX.Core.Exceptions;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Web.UI;
using StructureMap;

namespace Jenzabar.JX.Examples.JICS.Portlet
{
	public partial class DefaultView : PortletViewBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsFirstLoad)
			{
                PopulateDropDowns();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			ggEmailAddressList.EditCommand += GgEmailAddressList_EditCommand;
			base.OnInit(e);
		}

        private void GetJxAddresses()
        {
            int userId = txtUserId.Text.HasValue()
                ? Convert.ToInt32(txtUserId.Text)
                : Convert.ToInt32(PortalUser.Current.HostID);
            IList<EmailAddressDTO> emailList;
            using (IContainer factory = ObjectFactory.Container.GetNestedContainer("ADV"))
            {
                //Create an instance of the EmailAddressService
                IEmailAddressService emailService = factory.GetInstance<IEmailAddressService>();

                // Get all email addresses for the provided user ID
                emailList = emailService.GetByAllConstituentId(userId);
            }
            // Sort the Email
            if (emailList != null)
                emailList = emailList.OrderByDescending(x => x.BeginsOn).ToList();
            else
                emailList = new List<EmailAddressDTO>();

            // Bind the returned data to the control
            ggEmailAddressList.DataSource = emailList;
            ggEmailAddressList.DataBind();
        }

            protected void btnGetData_Click(object sender, EventArgs e)
		{
            GetJxAddresses();
        }

        private void PopulateDropDowns()
        {
            using (IContainer factory = ObjectFactory.Container.GetNestedContainer("ADV"))
            {
                IEmailAddressTypeLookup emailAddressLookup = factory.GetInstance<IEmailAddressTypeLookup>();

                ddlAddressType.DataTextField = "Value";
                ddlAddressType.DataValueField = "Id";
                ddlAddressType.DataSource = emailAddressLookup.GetAll().ToList();
                ddlAddressType.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
		{
			ClearData();
		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
            bool isUpdate = false;

            //Create an instance of the EmailAddressService
            using (IContainer factory = ObjectFactory.Container.GetNestedContainer("ADV"))
            {
                IEmailAddressService emailService = factory.GetInstance<IEmailAddressService>();

                EmailAddressDTO emailAddress = new EmailAddressDTO();

                if (Session["EmailEditId"] != null)
                {
                    emailAddress = emailService.Get(Convert.ToInt32(Session["EmailEditId"].ToString()));
                    isUpdate = true;
                }

                emailAddress.Priority = chknPriority.Checked;
                emailAddress.EmailAddress = txtEmailAddr.Text;
                emailAddress.AddressTypeId = Convert.ToInt32(ddlAddressType.SelectedValue);
                emailAddress.BeginsOn = Convert.ToDateTime(txtBeginsOn.Text);
                if (txtEndsOn.Text.HasValue())
                    emailAddress.EndsOn = Convert.ToDateTime(txtEndsOn.Text);

                try
                {
                    EmailAddressDTO ret = isUpdate
                        ? emailService.Update(Convert.ToInt32(txtUserId.Text), emailAddress)
                        : emailService.Create(Convert.ToInt32(txtUserId.Text), emailAddress);

                    if (ret == null)
                        ParentPortlet.ShowFeedback(FeedbackType.Error, "Error occured while creating email address.");
                }
                catch (MissingDataException ex)
                {
                    ParentPortlet.ShowFeedback(FeedbackType.Error, ex.Message);
                }
            }

            ClearData();
            GetJxAddresses();
        }

		private void GgEmailAddressList_EditCommand(object source, DataGridCommandEventArgs e)
		{
            Session["EmailEditId"] = ggEmailAddressList.DataKeys[e.Item.ItemIndex].ToString();
            pnlAddUpdate.Visible = true;

            EmailAddressDTO emailAddress;
            using (IContainer factory = ObjectFactory.Container.GetNestedContainer("ADV"))
            {
                IEmailAddressService emailService = factory.GetInstance<IEmailAddressService>();
                emailAddress = emailService.Get(Convert.ToInt32(Session["EmailEditId"].ToString()));
            }

            txtEmailAddr.Text = emailAddress.EmailAddress;
            txtBeginsOn.Text = emailAddress.BeginsOn.ToString();
            txtEndsOn.Text = emailAddress.EndsOn != null ? emailAddress.EndsOn.ToString() : string.Empty;
            ddlAddressType.SelectedValue = emailAddress.AddressTypeId.ToString();
            chknPriority.Checked = emailAddress.Priority;
        }

		private void ClearData()
		{
			txtEmailAddr.Text = "";
			txtBeginsOn.Text = "";
			txtEndsOn.Text = "";
			pnlAddUpdate.Visible = false;
			Session.Remove("EmailEditId");
		}

		protected void btnAddNew_Click(object sender, EventArgs e)
		{
			ClearData();
			pnlAddUpdate.Visible = true;
		}
	}
}