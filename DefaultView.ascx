<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefaultView.ascx.cs" Inherits="Jenzabar.JX.Examples.JICS.Portlet.DefaultView" %>
<%@ Register Assembly="Jenzabar.Common" Namespace="Jenzabar.Common.Web.UI.Controls" TagPrefix="cc1" %>

<table>
	<tr>
		<td><strong>Lookup ID</strong></td>
		<td><asp:TextBox ID="txtUserId" runat="server"></asp:TextBox></td>
		<td>
			<asp:Button ID="btnGetData" runat="server" Text="Get Data" OnClick="btnGetData_Click" />
		</td>
	</tr>
</table>
<br />
<h5>Email Addresses</h5>
<cc1:GroupedGrid ID="ggEmailAddressList" runat="server" RenderGroupHeaders="True" DataKeyField="Id">
	<EmptyTableTemplate>
			No Addresses to display
	</EmptyTableTemplate>
	<Columns>
			<asp:TemplateColumn HeaderText="Email">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "EmailAddress") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Type">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "AlternateDescription") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Priority">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "Priority") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Begins On">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "BeginsOn") %>
				</ItemTemplate>
			</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Ends On">
				<ItemTemplate>
					<%# DataBinder.Eval(Container.DataItem, "EndsOn") %>
				</ItemTemplate>
			</asp:TemplateColumn>
			<cc1:EditButtonColumn />
		</Columns>
</cc1:GroupedGrid>

<asp:Panel ID="pnlAddUpdate" runat="server" Visible="False">
	<table>
		<tr>
			<td>Email Address</td>
			<td>
				<asp:TextBox ID="txtEmailAddr" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>Email Address Type</td>
			<td>
				<asp:DropDownList ID="ddlAddressType" runat="server"></asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>Begins On</td>
			<td>
				<asp:TextBox ID="txtBeginsOn" runat="server" ></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>Ends On</td>
			<td>
				<asp:TextBox ID="txtEndsOn" runat="server" ></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				&nbsp;</td>
			<td>
				<asp:CheckBox ID="chknPriority" runat="server" Text="Is Priority Email" />
			</td>
		</tr>
		<tr><td colspan="2" style="text-align: center">
			<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
			<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
		</td></tr>
	</table>
</asp:Panel>
<asp:Button ID="btnAddNew" runat="server" Text="Add New Address" OnClick="btnAddNew_Click" />

