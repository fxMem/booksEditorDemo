var $ = require('jquery');
var status = require('./globalStatus.js');

function request(controller, action, id) {
        var getUrl = function (controller, action, id) {
            if (!controller || !action) {
                throw new Error("Controller or acton is not specified!");
            }

            return "api/" + controller + "/"  + (id ? id : "");
        }

        this.url = getUrl(controller, action, id);
		this.method = action;
        this.parameter = null;
    }

    request.create = function (controller, action, id) {
        return new request(controller, action, id);
    }

    request.prototype.withParameter = function (value) {
        this.parameter = value;
        return this;
    }

    request.prototype.send = function (callback) {

        

        var onError = function(errorMessage,  callback) {
            status.setMessage('Error: ' + errorMessage)
            callback && callback(errorMessage, null);
        };

        $.ajax({
            type: this.method,
            data: this.parameter ? JSON.stringify(this.parameter) : null,
            url: this.url,
            contentType: "application/json",
            success: function (data, status, x) {
                if (!callback) {
                    return;
                }

                if (!data) {
                    callback(null, null);
                    return;
                }

                if (data.Code !== 0) {
                    onError(data.Message || {}, callback);
                }
                else {
                    callback(null, data.Data);
                }
            },
            error: function (x, status, err) {
                var errorMessage = x.responseText || status || err || 'Unknown error';
                onError(errorMessage, callback);
            }
        });
    }

    // It should be placed to a separate class
    request.getFileUrl = function (bookId) {
        return 'api/file/' + bookId;
    };

    request.sendFile = function (bookId, form, callback) {

        status.clear();

        var onError = function(errorMessage,  callback) {
            status.setMessage('Error: ' + errorMessage)
            callback && callback(errorMessage, null);
        };

        $.ajax({
            url: request.getFileUrl(bookId),
            data: form,
            processData: false,
            contentType: false,
            type: 'post',
            success: function (data, status, x) {
                if (!callback) {
                    return;
                }

                if (!data) {
                    callback(null, null);
                    return;
                }

                if (data.Code !== 0) {
                    onError(data.Message || {}, callback);
                }
                else {
                    callback(null, data.Data);
                }
            },
            error: function (x, status, err) {
                var errorMessage = x.responseText || status || err || 'Unknown error';
                onError(errorMessage, callback);
            }
        });
    }

module.exports = request;