var modelBase = require('./modelBase.js');
var ko = require('knockout');

AuthorViewModel.prototype = Object.create(modelBase.prototype)
function AuthorViewModel(authorsModel, data) {
	this.authorsModel = authorsModel;
	this.id = ko.observable();
	this.lastname = ko.observable();
	this.firstname = ko.observable();
	
	if (data) {
		this.id(data.Id);
		this.lastname(data.LastName);
		this.firstname(data.FirstName);
    }

    this.fullname = this.lastname() + ' ' + this.firstname();
}

AuthorViewModel.prototype.save = function() {
	this.request('author', 'post', this.id(), {
		lastName: this.lastname(),
		firstName: this.firstname()
	}, (error, data) => {
        if (!error) {
            this.authorsModel.confirmAddAuthor();
        }
	});
}

AuthorViewModel.prototype.remove = function() {
this.request('author', 'delete', this.id(), (error, data) => {
        if (!error) {
            this.authorsModel.confirmAddAuthor();
        }
	});
};

module.exports = AuthorViewModel;