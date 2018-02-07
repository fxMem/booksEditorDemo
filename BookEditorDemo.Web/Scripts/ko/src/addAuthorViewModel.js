var modelBase = require('./modelBase.js');
var ko = require('knockout');

AddingAuthorViewModel.prototype = Object.create(modelBase.prototype)
function AddingAuthorViewModel(authorsModel, bookModel) {
	this.authorsModel = authorsModel;
	this.bookModel = bookModel;
    this.allAuthors = ko.observableArray(this.authorsModel.all());
	this.selectedAuthor = ko.observable();

    var that = this;
    this.add = function() {
        that.OK();
    };
}

AddingAuthorViewModel.prototype.OK = function () {
	var author = this.selectedAuthor();
	if (author) {
        this.bookModel.addAuthor(author);	
	}
	
	this.bookModel.closeAddAuthorPanel();
};

AddingAuthorViewModel.prototype.Cancel = function () {
	this.bookModel.closeAddAuthorPanel();
};

module.exports = AddingAuthorViewModel;