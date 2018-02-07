var request = require('./request.js');
var ko = require('knockout');


function viewModelBase(data) {
    //this.loading = ko.observable(false);
}

viewModelBase.prototype.loaded = function () {
    // override
}

viewModelBase.prototype.request = function (controller, action, id, data, callback) {
    var req = request.create(controller, action, id);
    var parameter = data instanceof Function ? null : data;
    callback = data instanceof Function ? data : callback;
    
    parameter && req.withParameter(parameter);
    this.setLoading(true);
    var that = this;
    req.send(function () {
        var ex = null;
        try {
            callback.apply(that, arguments);
        } finally {
            that.setLoading(false);
        }
    });
}

viewModelBase.prototype.setLoading = function (val) {
    //this.loading(val);
}

viewModelBase.prototype.dispose = function () {

}

viewModelBase.prototype.observable = ko.observable;
viewModelBase.prototype.observableArray = ko.observableArray;


module.exports = viewModelBase;