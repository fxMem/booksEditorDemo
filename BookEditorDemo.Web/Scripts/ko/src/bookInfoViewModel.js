var modelBase = require('./modelBase.js');
var ko = require('knockout');

BookInfoViewModel.prototype = Object.create(modelBase.prototype)
function BookInfoViewModel(data) {
    this.id = ko.observable();
	this.title = ko.observable("");
	this.year = ko.observable("");
    this.author = ko.observable("");
	
	if (data) {
		this.id(data.Id);
		this.title(data.Title);
		this.year(data.PublicationYear);
		this.author(data.AuthorLastName + ' ' + data.AuthorFirstName);
	}

    this.description = ko.computed(function() {
        return this.title() + ' by ' + this.author()  + ', ' + this.year();
    }, this);
}


module.exports = BookInfoViewModel;