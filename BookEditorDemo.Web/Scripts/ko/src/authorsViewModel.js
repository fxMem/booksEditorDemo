var modelBase = require('./modelBase.js');
var ko = require('knockout');
var AuthorViewModel = require('./authorViewModel.js');

AuthorsViewModel.prototype = Object.create(modelBase.prototype)
function AuthorsViewModel() {
	this.all = ko.observableArray();
	this.newAuthor = ko.observable();
}

AuthorsViewModel.prototype.initialize = function() {
    //this.all(this.getTestAuthors());

	this.request('authors', 'get', null, null, (error, data) => {
		if (!error) { 
			this.all(data.map((a) => { return new AuthorViewModel(this, a); }));
		}
	});
}

AuthorsViewModel.prototype.getTestAuthors = function() {
    var a = new AuthorViewModel(this, {
        id: 1,
        lastname: 'Tolstoy',
        firstname: 'Leo'
    });

    return [a];
}

AuthorsViewModel.prototype.getAuthor = function(id) {
    var all = this.all();
    return this.all().find(a => a.id() == id);
    //return ko.utils.arrayFirst(this.all, a => a.id() == id);
}

AuthorsViewModel.prototype.createAuthor = function () {
	this.newAuthor(new AuthorViewModel());
}

AuthorsViewModel.prototype.authorCreated = function () {
    var author = this.newAuthor();
	this.all.add(author);
	this.newAuthor(null);
}

AuthorsViewModel.prototype.authorRemoved = function (author) {
	this.all.remove(author);
}

AuthorsViewModel.prototype.closeCreateAuthor = function () {
	this.newAuthor(null);
}

module.exports = AuthorsViewModel;