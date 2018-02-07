var modelBase = require('./modelBase.js');
var AddingAuthorViewModel = require('./addAuthorViewModel.js');
var ko = require('knockout');
var request = require('./request.js');
var $ = require('jquery');
var status = require('./globalStatus.js');

BookViewModel.prototype = Object.create(modelBase.prototype)
function BookViewModel(booksModel, authorsModel, data) {
    this.booksModel = booksModel;
	this.authorsModel = authorsModel;
    this.id = ko.observable();
	this.title = ko.observable();
    this.year = ko.observable();
    this.pageNumber = ko.observable();
	this.ISBN = ko.observable();
	this.publisher = ko.observable();
	this.authors = ko.observableArray();
	this.addingAuthorModel = ko.observable(false);
    this.authorsPreview = ko.observable();
    this.templateName = 'bookTemplate';
    this.coverUrl = ko.observable();
    this.haveCover = ko.observable();
	
	if (data) {
		this.id(data.Id);
		this.title(data.Title);
		this.year(data.PublishYear);
		this.ISBN(data.ISBN);
		this.publisher(data.Publisher);
        this.pageNumber(data.PageNumber);
        this.haveCover(data.HaveCover);
        this.authors(this.deserializeAuthors(data.Authors));
        this.updateCoverUrl();
        this.authorsUpdated();
	}

    this.description = ko.computed(function() {
        return this.title()  + ', ' + this.year();
    }, this);

    var that = this;
    this.saveChangesCallback = function() {
        that.save();
    };

    this.removeBookCallback = function() {
        that.remove();
    };

    this.uploadCoverCallback = function() {
        that.uploadCover();
    };

    this.removeAuthorCallback = function(author) {
        that.removeAuthor(author);
    }

    //this.openAddAuthorCallback = function(author) {
    //    that.openAddAuthorPanel();
    //}

    this.switchAddAuthorPanel = function () {
        if (that.addingAuthorModel()) {
            that.closeAddAuthorPanel();
        }
        else {
            that.openAddAuthorPanel();
        }
    }

    this.validationErrors = ko.observable();
}

BookViewModel.prototype.updateCoverUrl = function () {
    if (!this.haveCover()) {
        return;
    }

    var url = request.getFileUrl(this.id());
    if (this.coverUrl() != url) {
        this.coverUrl(url);
    }
    else {
        this.coverUrl.valueHasMutated();
    }
}

BookViewModel.prototype.openAddAuthorPanel = function() {
	this.addingAuthorModel(new AddingAuthorViewModel(this.authorsModel, this));
}

BookViewModel.prototype.closeAddAuthorPanel = function() {
	this.addingAuthorModel(null);
}

BookViewModel.prototype.addAuthor = function (author) {
    if (this.authors().some(a => a.id() == author.id())) {
        return;
    }

	this.authors.push(author);
    this.authorsUpdated();
}

BookViewModel.prototype.removeAuthor = function(author) {
	this.authors.remove(author);
    this.authorsUpdated();
}

BookViewModel.prototype.serializeAuthors = function() {
	return this.authors().map(a => a.id());
}

BookViewModel.prototype.deserializeAuthors = function(value) {
	var result = [];
    for (var i = 0; i < value.length; i++) {
        var authorId = value[i].Id;
        var author = this.authorsModel.getAuthor(authorId);
        result.push(author);
    }

    return result;
}

BookViewModel.prototype.authorsUpdated = function () {
    var preview = '';
    for (var i = 0; i < this.authors().length; i++) {
        var author = this.authors()[i];
        preview += (author.lastname() + ' ' + author.firstname());
        
        if (i < this.authors().length - 1) {
            preview += ', ';
        }
    }

    this.authorsPreview(preview);
}

BookViewModel.prototype.checkIsModelValid = function() {
    var message = '';
    message += this.stringValid('title', this.title(), true, 20);
    message += this.stringValid('publisher', this.publisher(), false, 20);
    //message += this.stringValid('ISBN', this.publisher, false, 20);
    
    message += this.inRange('Year', this.year(), true, 1800, 1000 * 1000);
    message += this.inRange('Page number', this.pageNumber(), true, 0, 10 * 1000);

    if (this.authors().length < 1) {
        message += 'You must add at least one author';
    }

    this.validationErrors(message);
    return !message;
};

BookViewModel.prototype.stringValid = function(name, value, required, maxLength) {
    if (required && !value) {
        return 'Value for ' + name + ' is required \r\n';
    }

    if (!required && !value){
        return '';
    }

    if (maxLength && value.length > maxLength) {
        return 'Value for ' + name + ' cannot exceed ' + maxLength + ' symbols \r\n';
    }

    return '';
}

BookViewModel.prototype.inRange = function(name, value, required, min, max) {
    if (required && !value) {
        return 'Value for ' + name + ' is required \n';
    }

    if (!required && !value){
        return '';
    }

    if (value < min || value > max) {
        return 'Value for ' + name + ' cannot be more than ' + max + ' and less than ' + min + '. Current = ' + value + '\n';
    }

    return '';
}

BookViewModel.prototype.save = function(callback) {
    if (!this.checkIsModelValid()) {
        return;
    }

    // This should be done via events
    status.clear();

    var method = this.id() ? 'put' : 'post';
	this.request('book', method, this.id(), {
			Title: this.title(),
			PublishYear: this.year(),
			ISBN: this.ISBN(),
			Publisher: this.publisher(),
            PageNumber: this.pageNumber(),
			AuthorIds: this.serializeAuthors(),
    }, (error, data) => {

        // Just updating all list for simlicity
        this.booksModel.updateBooksList();

         callback && callback(error, data);
	});
};

BookViewModel.prototype.remove = function (callback) {

	this.request('book', 'delete', this.id(), (error, data) => {
        callback && callback(error, data);

        // better use events for this
        this.booksModel.updateBooksList();
	});
};

BookViewModel.prototype.uploadCover = function(callback) {
	var $input = $("#uploadimage");
    var fd = new FormData();

    fd.append('img', $input.prop('files')[0]);
    request.sendFile(this.id(), fd, (error, data) => {
        if (!error) {
            this.haveCover(true);
            this.updateCoverUrl();
        }
    });

};

module.exports = BookViewModel;