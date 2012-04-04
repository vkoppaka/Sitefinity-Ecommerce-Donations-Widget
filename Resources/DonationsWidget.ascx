<%@ Control Language="C#" %>
<%@ Register TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Fields" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>

<sitefinity:FormManager id="formManager" runat="server"/>

<div id="widgetStatus" runat="server" visible="false">
    <asp:Label ID="widgetStatusMessage" runat="server" />
</div>

<asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>
        <sf:Message runat="server" ID="addedToCartMessage" CssClass="sfMessage sfTopMsg" RemoveAfter="10000" />
    </ContentTemplate>
</asp:UpdatePanel>

<div id="donationsWidgetWrapper" runat="server">
    <ul id="donationsWrapper">
        <li>
             <sitefinity:ChoiceField ID="donationAmountDropDown" DisplayMode="Write" RenderChoicesAs="DropDown" runat="server" CssClass="sfDropDownList400" Title='Please select the amount to donate'>
                <Choices>
                    <sitefinity:ChoiceItem Text="$5.00" Value="5"  />
                    <sitefinity:ChoiceItem Text="$10.00" Value="10"  />
                    <sitefinity:ChoiceItem Text="$50.00 (Ideal Donation)" Value="50"  />
                    <sitefinity:ChoiceItem Text="$100.00" Value="100"  />
                    <sitefinity:ChoiceItem Text="Other" Value="OTHER"  />
                </Choices>
            </sitefinity:ChoiceField>
            <div id="otherAmountDiv">
                <sitefinity:TextField ID="otherAmount" runat="server" CssClass="sfTxt sfQuantity" DisplayMode="Write" Title='Enter other amount' />
            </div>
        </li>
        <li>
            <asp:Button ID="addToCartButton" runat="server" Text='<%$Resources:OrdersResources, AddToCart %>' CssClass="sfAddToCartBtn" />
        </li>
    </ul>
</div>
