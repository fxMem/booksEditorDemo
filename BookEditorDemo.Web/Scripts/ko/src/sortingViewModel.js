var ko = require('knockout');
var modelBase = require('./modelBase.js');

var sortProperty = {
    title : 0,
    year : 1
};

SortingViewModel.prototype = Object.create(modelBase.prototype);
function SortingViewModel(booksModel) {
    this.booksModel = booksModel;
    this.sortingTypes = {
        '0': { title: 'Title', value: sortProperty.title },
        '1': { title: 'Year', value: sortProperty.year },
    }

    this.sortProperties = ko.observableArray([this.sortingTypes[sortProperty.title], this.sortingTypes[sortProperty.year]]);
    this.selectedSorting = ko.observable(this.sortingTypes[sortProperty.title]);

   
    
}

SortingViewModel.prototype.initialize = function () {
    this.request('settings', 'get', null, null,
        (error, data) => {
        if (!error) {
            this.selectedSorting(this.sortingTypes[data.SortBy]);
            this.sort();
         }

        var that = this;

        this.selectedSorting.subscribe(function (newVal) {
            that.sort();
            that.saveSortingSettings();
        });
    });

    
}

SortingViewModel.prototype.sort = function (sorting) {
    this.booksModel.sortBy(this.selectedSorting().value);
    
    
}

SortingViewModel.prototype.saveSortingSettings = function () {
    this.request('settings', 'post', null, {
        SortProperty: this.selectedSorting().value
    }, (error, data) => {

    });
}

SortingViewModel.types = sortProperty;

module.exports = SortingViewModel;