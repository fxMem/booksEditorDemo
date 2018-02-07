var modelBase = require('./modelBase.js');
var ko = require('knockout');
var AuthorsViewModel = require('./authorsViewModel.js');
var BookViewModel = require('./bookViewModel.js');
var BookInfoViewModel = require('./bookInfoViewModel.js');
var AddingBookViewModel = require('./addBookViewModel.js');
var SortingViewModel = require('./sortingViewModel.js');
var status = require('./globalStatus.js');

BooksViewModel.prototype = Object.create(modelBase.prototype);
BooksViewModel.prototype.constructor = BooksViewModel;
function BooksViewModel() {
    modelBase.call(this);

	this.selectedModel = ko.observable();
	this.authors = new AuthorsViewModel();
	this.books = ko.observableArray();
	this.addingBookModel = ko.observable();
	this.errorMessage = ko.observable();
	this.status = status;
	
    this.templateName = ko.observable();
    this.sorting = new SortingViewModel(this);

	this.initialize();

	var that = this;
	this.openBookCallback = function(book) {
		that.selectBook(book);
	};

	this.openAddBookPanel = function () {
		that.selectModel(new AddingBookViewModel(that.authors, that));
	};

	this.closeAddBookPanel = function () {
		that.selectModel(null);
	};

	this.selectModel(null);
}

BooksViewModel.prototype.selectModel = function(model) {
	try{
		// this may throw because of mismatch of bindings in
		// diffefent view models. The cleaner solution is to use
		// components, but for demo purposes this will do.
		this.selectedModel(model);
	}
	catch (e) {
		
	}
	
	var templateName = (model && model.templateName) || 'emptyTemplate';
	this.templateName(templateName);
}

BooksViewModel.prototype.initialize = function () {
    this.authors.initialize();
    this.sorting.initialize();

    this.updateBooksList();
};

BooksViewModel.prototype.sortBy = function (property) {
    var sortCallback = null;
    switch (property) {
        case SortingViewModel.types.title:
            sortCallback = (a, b) => {
                if (a.title() == b.title()) {
                    return 0;
                }

                return a.title() < b.title() ? -1 : 1;
            };
            break;
        case SortingViewModel.types.year:
            sortCallback = (a, b) => { return a.year() - b.year(); }
            break;
    }

    if (sortCallback) {
        this.books(this.books().sort(sortCallback));
    }
};

BooksViewModel.prototype.updateBooksList = function () {
    this.request('books', 'get', null, null, (error, data) => {
        if (!error) {
            this.books(data.map(function (b) { return new BookInfoViewModel(b); }));

            this.checkSelectedBookExists();
        }
    });
}

BooksViewModel.prototype.checkSelectedBookExists = function () {
    var selectedBook = this.selectedModel();
    var selectedId = selectedBook instanceof BookViewModel && selectedBook.id();
    if (!this.books().some(b => b.id() == selectedId)) {
        this.selectModel(null);
    }
}

BooksViewModel.prototype.selectBook = function(bookInfo) {
	if (!(bookInfo instanceof BookInfoViewModel)) {
		return;
	}
    
// For demo purpuses I omit caching etc
	 this.request('book', 'get', bookInfo.id(), null, (error, data) => {
	 	if (!error) { 
	 		var book = new BookViewModel(this, this.authors, data);
			this.selectModel(book);
	 	}
	 });
}


BooksViewModel.prototype.selectTemplate = function(name) {
	this.templateName(name);
};

BooksViewModel.prototype.closeAddBookPanel = function () {
	this.selectModel(null);
};

BooksViewModel.prototype.removeSelectedBook = function() {
	if (!this.selectedModel || !(this.selectedModel instanceof BookViewModel)) {
		return;
	}

	this.selectedModel.remove((err, data) => {
		if (!err) {
			this.books.remove(this.selectedModel());
		}
	});
};

BooksViewModel.prototype.clearError = function (message) {
	this.status.clear();
};

BooksViewModel.prototype.setError = function (message) {
	this.status.setMessage(message);
};

module.exports = BooksViewModel;


