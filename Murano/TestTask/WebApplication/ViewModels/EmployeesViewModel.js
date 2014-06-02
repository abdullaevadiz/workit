/// <reference path="../Scripts/knockout-3.1.0.debug.js" />
/// <reference path="../Scripts/jquery-1.10.2.min.js" />

//Consts
var FILTER_BY_STATUS = {
    ALL : '0',
    ACTIVE : '1',
    INACTIVE : '2'
};
var ERROR_MESSAGE = "Not saved. The Data You Entered Is Incorrect";

function PagerModel(param) {
    var self = this;

    this.pageNum = ko.observable(param.pageNum);
    this.countPerPage = ko.observable(param.countPerPage);
    this.allCount = ko.observable(param.allCount);
    this.items = ko.computed(function() {
        var pagination = [];
        var pagesCount = self.allCount() / self.countPerPage();
        var additionalPagesCount = self.allCount() % self.countPerPage();
        if (additionalPagesCount > 0)
            pagesCount++;

        for (var i = 1; i <= pagesCount; i++) {
            pagination.push({
                cssClass: i == self.pageNum() ? "active" : "",
                num: i
            });
        }
        return pagination;
    });
}

function EmployeeModel(item) {
    var self = this;

    //Data
    this.Id = item.Id;
    this.Name = ko.observable(item.Name);
    this.PositionId = ko.observable(item.PositionId);
    this.PositionName = ko.observable(item.Position.Key);
    this.IsActive = ko.observable(item.IsActive);
    this.Salary = ko.observable(item.Salary);
    this.Tax = ko.computed(function() {
        if (self.Salary() < 10000)
            return 10;

        if (self.Salary() < 20001)
            return 15;

        return 25;
    });
    this.After = ko.computed(function() {
        return self.Salary() - (self.Tax() * self.Salary() * 0.01);
    });
    this.viewEdit = ko.observable(false);
}

function AddEmployeeForm(defaultPositionId) {
    var self = this;

    //Data
    this.Name = ko.observable("");
    this.PositionId = ko.observable(defaultPositionId);
    this.IsActive = ko.observable(true);
    this.Salary = ko.observable("");
    this.error = ko.observable("");
    this.defaultPositionId = defaultPositionId;

    //Behaviours
    this.clearForm = function() {
        self.error("");
        self.Name("");
        self.Salary("");
        self.PositionId(self.defaultPositionId);

    };
    this.viewForm = function () {
        self.clearForm();
        $('#addEmployeeModal').modal();
    };
    this.closeForm = function() {
        $('#addEmployeeModal').modal('hide');
    };

}

function EmployeesViewModel(params) {
    var self = this;
    
    //Data
    this.urls = params.urls;
    this.employees = ko.observableArray([]);
    this.statusFilter = ko.observable(FILTER_BY_STATUS.ALL);
    this.positions = ko.observable(params.positions);
    this.progressBarIsView = ko.observable(false);
    this.pager = ko.observable(new PagerModel(params));
    this.error = ko.observable("");

    //Behaviours
    this.getEmployeePositions = function() {
        self.progressBarIsView(true);
        var requestUrl = self.urls.baseUrl + self.urls.getEmployeePositionsUrl;
        $.getJSON(requestUrl, function (result) {
            self.positions(result);
            self.progressBarIsView(false);
        });
    };
    this.updateEmployeeList = function() {
        self.progressBarIsView(true);
        var jData = {
            PageNum: self.pager().pageNum(),
            CountPerPage: self.pager().countPerPage(),
            IsActive: (self.statusFilter() != FILTER_BY_STATUS.INACTIVE),
            ViewAll: (self.statusFilter() == FILTER_BY_STATUS.ALL)
        };
        var requestUrl = self.urls.baseUrl + self.urls.getEmployeesByStatusAndPageNumUrl;
        $.post(requestUrl, "jData=" + JSON.stringify(jData), function (returnedData) {
            var result = $.parseJSON(returnedData.replace(/&quot;/ig, '"'));
            var employeesArray = $.map(result.Employees, function (item) {
                return new EmployeeModel(item);
            });
            self.employees(employeesArray);
            self.pager(new PagerModel({ pageNum: result.PageNum, countPerPage: result.CountPerPage, allCount: result.AllCount }));
            self.progressBarIsView(false);
        });
    };
    this.searchPositionNameById = function(positionId) {
        var results = $.grep(self.positions(), function(e) {
            return e.Id == positionId;
        });
        if (results.length == 0)
            return "";

        return results[0].Key;
    }
    this.edit = function(employee) {
        var canEdit = true;
        $.each(self.employees(), function (index) {
            if (self.employees()[index].viewEdit()) {
                canEdit = false;
                return;
            }
        });
        if (!canEdit)
            return;

        employee.viewEdit(true);

        //getUpdateEmployeeUrl
    };
    this.save = function(employee) {
        self.error("");
        //#region check...
        if (employee.Name() == "") {
            self.error(ERROR_MESSAGE);
            return;
        }

        if (employee.Salary() == "") {
            self.error(ERROR_MESSAGE);
            return;
        }

        if (!(!isNaN(parseFloat(employee.Salary())) && isFinite(employee.Salary()))) {
            self.error(ERROR_MESSAGE);
            return;
        }
        //#endregion

        var requestUrl = self.urls.baseUrl + self.urls.updateEmployeeUrl;
        var jEmployee = ko.toJS(employee);
        $.post(requestUrl, "jEmployee=" + JSON.stringify(jEmployee), function (returnedData) {
            var result = $.parseJSON(returnedData);
            if (!result) {
                self.error(ERROR_MESSAGE);
                return;
            }
            if (self.statusFilter() == FILTER_BY_STATUS.ALL)
                return;

            if (self.statusFilter() == FILTER_BY_STATUS.ACTIVE && !(employee.IsActive())) {
                self.employees.remove(employee);
                self.pager().allCount(self.pager().allCount() - 1);
                return;
            }

            if (self.statusFilter() == FILTER_BY_STATUS.INACTIVE && (employee.IsActive())) {
                self.employees.remove(employee);
                self.pager().allCount(self.pager().allCount() - 1);
                return;
            }
        });

        employee.PositionName(self.searchPositionNameById(employee.PositionId()));
        employee.viewEdit(false);
    };
    this.changePage = function(page) {
        self.pager().pageNum(page.num);
        self.updateEmployeeList();
    };
    this.statusFilter.subscribe(function () {
        self.pager().pageNum(1);
        self.updateEmployeeList();
    });
    this.addEmployee = function () {
        self.addEmployeeForm.error("");
        //#region check...
        if (self.addEmployeeForm.Name() == "") {
            self.addEmployeeForm.error(ERROR_MESSAGE);
            return;
        }

        if (self.addEmployeeForm.Salary() == "") {
            self.addEmployeeForm.error(ERROR_MESSAGE);
            return;
        }

        if (!(!isNaN(parseFloat(self.addEmployeeForm.Salary())) && isFinite(self.addEmployeeForm.Salary()))) {
            self.addEmployeeForm.error(ERROR_MESSAGE);
            return;
        }
        //#endregion

        var requestUrl = self.urls.baseUrl + self.urls.createEmployeeUrl;
        var jEmployee = ko.toJS({
            Name: self.addEmployeeForm.Name(),
            IsActive: self.addEmployeeForm.IsActive(),
            PositionId: self.addEmployeeForm.PositionId(),
            Salary : self.addEmployeeForm.Salary()
        });
        $.post(requestUrl, "jEmployee=" + JSON.stringify(jEmployee), function (returnedData) {
            var result = $.parseJSON(returnedData);
            if (!result) {
                self.error(ERROR_MESSAGE);
                return;
            }
            self.pager().pageNum(1);
            self.updateEmployeeList();
            self.addEmployeeForm.closeForm();
        });
    };

    //First load
    self.getEmployeePositions();
    self.updateEmployeeList();

    self.addEmployeeForm = new AddEmployeeForm(self.positions().length > 0 ? self.positions()[0].Id : "");
}


ko.bindingHandlers.numeric = {
    init: function (element, valueAccessor) {
        $(element).on("keydown", function (event) {
            // Allow: backspace, delete, tab, escape, and enter
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                // Allow: Ctrl+A
                (event.keyCode == 65 && event.ctrlKey === true) ||
                // Allow: . ,
                (event.keyCode == 188 || event.keyCode == 190 || event.keyCode == 110) ||
                // Allow: home, end, left, right
                (event.keyCode >= 35 && event.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
        });
    }
};