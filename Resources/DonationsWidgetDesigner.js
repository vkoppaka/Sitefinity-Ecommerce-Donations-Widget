Type.registerNamespace("Telerik.Sitefinity.Samples.Ecommerce.Donations");



Telerik.Sitefinity.Samples.Ecommerce.Donations.DonationsWidgetDesigner = function (element) {

    Telerik.Sitefinity.Samples.Ecommerce.Donations.DonationsWidgetDesigner.initializeBase(this, [element]);
    this._propertyEditor = null;
    this._controlData = null;
    this._showProductSelectorButton = null;
    this._productSelector = null;
    this._radWindowManager = null;

    this._showProductSelectorDelegate = null;
    this._productSelectedDelegate = null;
    
    this._emptyGuid = Telerik.Sitefinity.getEmptyGuid();
}

Telerik.Sitefinity.Samples.Ecommerce.Donations.DonationsWidgetDesigner.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        this.refreshUI();
        Telerik.Sitefinity.Samples.Ecommerce.Donations.DonationsWidgetDesigner.callBaseMethod(this, 'initialize');

        this._showProductSelectorDelegate = this.showProductSelectorDelegate || Function.createDelegate(this, this._showProductSelector);
        $addHandler(this.get_showProductSelectorButton(), "click", this._showProductSelectorDelegate);

        this._productSelectedDelegate = this._productSelectedDelegate || Function.createDelegate(this, this._productSelected);
        this._productSelector.add_doneClientSelection(this._productSelectedDelegate);


    },

    dispose: function () {

        Telerik.Sitefinity.Samples.Ecommerce.Donations.DonationsWidgetDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // refereshed the user interface. Call this method in case underlying control object
    // has been changed somewhere else then through this desinger.
    refreshUI: function () {
         var controlData = this.get_controlData();

        if (controlData.ProductId !== this._emptyGuid) {
            var productTitle = this._shortenProductName(controlData.ProductName);
            jQuery("#selectedProductText").text(productTitle).show();
            this.product = controlData.ProductId;
        }
    },

    // once the data has been modified, call this method to apply all the changes made
    // by this designer on the underlying control object.
    applyChanges: function () {
        var controlData = this.get_controlData();

        controlData.ProductId = this.product;
        
        
    },

    /* --------------------------------- event handlers --------------------------------- */
    _showProductSelector: function () {
        this.get_productSelector().dataBind();

        if (this.product) {
            var keys = [this.product];
            this._productSelector.set_selectedKeys(keys);
        }

        jQuery(this.get_element()).find('#selectorTag').show();
        dialogBase.resizeToContent();
    },

    _productSelected: function (items) {
        jQuery(this.get_element()).find('#selectorTag').hide();
        dialogBase.resizeToContent();

        if (items && items.length) {
            var productTitle = this._shortenProductName(this._productSelector.getSelectedItems()[0].Item.Title.Value);
            jQuery("#selectedProductText").text(productTitle).show();
            this.product = items[0];
        }
    },
    /* --------------------------------- private methods --------------------------------- */
        _shortenProductName: function (productName) {
        if (productName.length > 25) {
            var lastSpace = productName.lastIndexOf(' ', 25);
            if (lastSpace == -1) {
                return productName.substr(0, 25) + '...';
            }
            return productName.substr(0, lastSpace) + '...';
        }
        return productName;
    },

    /* --------------------------------- properties --------------------------------- */
    get_controlData: function () {
        return this.get_propertyEditor().get_control();
    },

    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },

    get_showProductSelectorButton: function () {
        return this._showProductSelectorButton;
    },

    set_showProductSelectorButton: function (value) {
        this._showProductSelectorButton = value;
    },

    get_productSelector: function () {
        return this._productSelector;
    },

    set_productSelector: function (value) {
        this._productSelector = value;
    },

    get_radWindowManager: function () {
        return this._radWindowManager
    },

    set_radWindowManager: function (value) {
        this._radWindowManager = value;
    },

}
Telerik.Sitefinity.Samples.Ecommerce.Donations.DonationsWidgetDesigner.registerClass('Telerik.Sitefinity.Samples.Ecommerce.Donations.DonationsWidgetDesigner', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IControlDesigner);