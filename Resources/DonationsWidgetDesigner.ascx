<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>
<%@ Register TagPrefix="designers" Namespace="Telerik.Sitefinity.Web.UI.ControlDesign" Assembly="Telerik.Sitefinity" %>

<sitefinity:ResourceLinks id="resourcesLinks" runat="server">
   <sitefinity:ResourceFile Name="Styles/Window.css" />
</sitefinity:ResourceLinks>

<telerik:RadWindowManager ID="windowManager" runat="server"
    Height="100%"
    Width="100%"
    Behaviors="None"
    Skin="Sitefinity"
    ShowContentDuringLoad="false"
    VisibleStatusBar="false">
    <Windows>
        <telerik:RadWindow ID="widgetEditorDialog" runat="server" ReloadOnShow="true" Modal="true" />
    </Windows>
</telerik:RadWindowManager>

<div id="selectorTag" style="display: none;" class="sfDesignerSelector sfFlatDialogSelector">
   <designers:ContentSelector 
       ID="productSelector" 
       runat="server" 
       ItemsFilter="" 
       TitleText="<%$Resources:Labels, ChooseProduct %>"
       BindOnLoad="false"
       AllowMultipleSelection="true"
       WorkMode="List" 
       SearchBoxInnerText="" 
       SearchBoxTitleText="<%$Resources:Labels, NarrowByTyping %>" 
       ListModeClientTemplate="<strong class='sfItemTitle'>{{Item.Title.Value}}</strong>">
   </designers:ContentSelector>
</div>


<div class="sfContentViews sfSingleContentView">
    <h2>
        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Labels, ChooseProduct %>" />
        <span class="sfRequiredItalics sfNote">
            <asp:Literal ID="Literal2" runat="server" Text='<%$Resources:OrdersResources, RequiredField %>' />
        </span>
    </h2>
    <span class="sfLinkBtn sfChange" runat="server" id="btnSelectSingleItemWrapper">
        <span id="selectedProductText" class="sfSelectedItem" style='display: none;'></span>

        <asp:LinkButton NavigateUrl="javascript:void(0)" runat="server" ID="showProductSelectorButton" OnClientClick="return false;" CssClass="sfLinkBtnIn">
        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Labels, SelectProduct %>" />
        </asp:LinkButton>
    </span>
</div>
