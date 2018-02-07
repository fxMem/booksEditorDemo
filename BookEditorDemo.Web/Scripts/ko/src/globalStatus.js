var ko = require('knockout');

// For simplicity, I'm having singleton status bag
function GlobalStatus() {
    this.message = ko.observable();
}

GlobalStatus.prototype.setMessage = function(message) {
    this.message(message);
}

GlobalStatus.prototype.clear = function() {
    this.message('');
}

var instance = new GlobalStatus();
module.exports = instance;