var ko = require('knockout');
var BooksViewModel = require('./booksViewModel.js');

var rootModel = new BooksViewModel();
ko.applyBindings(rootModel);