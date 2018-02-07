var modelBase = require('./modelBase.js');
var BookViewModel = require('./bookViewModel.js');

AddingBookViewModel.prototype = Object.create(modelBase.prototype);
function AddingBookViewModel(authorsModel, booksModel) {
	this.authorsModel = authorsModel;
	this.booksModel = booksModel;
	
    this.book = new BookViewModel(booksModel, authorsModel);
    this.templateName = 'addBookTemplate';

    var that = this;
    this.applyCallback = function() {
        that.OK();
    };

    this.cancelCallback = function() {
        that.Cancel();
    };
}

AddingBookViewModel.prototype.OK = function() {
	this.book.save((err, data) => {
        if (!err) {
            this.booksModel.closeAddBookPanel();

             
        }
    });
	
}

AddingBookViewModel.prototype.Cancel = function() {
	this.booksModel.closeAddBookPanel();
}

module.exports = AddingBookViewModel;